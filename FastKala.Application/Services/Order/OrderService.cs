using Dapper;
using FastKala.Application.Data;
using FastKala.Application.Interfaces.Order;
using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.Orders;
using FastKala.Domain.Enums.Global;
using FastKala.Domain.Enums.Orders;
using FastKala.Domain.Models.Orders;
using FastKala.Domain.Models.Product;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Z.Dapper.Plus;

namespace FastKala.Application.Services.Order;

public class OrderService(DapperContext context) : IOrderService
{
    private readonly DapperContext _context = context;

    public async Task<OperationResult> AddToCard(int productId, int quantity, string userId)
    {
        try
        {
            string sql = "";
            using (SqlConnection connection = _context.CreateConnection())
            {
                Cart? cartItem = await connection.QuerySingleOrDefaultAsync<Cart>("Select * From Cart where CustomerId = @customerid and ProductId = @productid", new
                {
                    customerid = userId,
                    productid = productId
                });

                if (cartItem != null)
                {
                    sql = "UPDATE Cart SET Quantity = (Quantity + @quantity) WHERE CustomerId = @customerid and ProductId = @productid";

                    await connection.ExecuteAsync(sql, new { productid = productId, quantity, customerid = userId });
                }
                else
                {
                    sql = "INSERT INTO Cart (ProductId,Quantity,CustomerId) VALUES (@productid,@quantity,@customerid)";

                    await connection.ExecuteAsync(sql, new { productid = productId, quantity, customerid = userId });
                }
            }
            return new OperationResult() { OperationStatus = OperationStatus.Success, Message = "با موفقیت به سبد خرید اضافه شد" };
        }
        catch (Exception ex)
        {
            return new OperationResult() { OperationStatus = OperationStatus.Fail, Message = ex.StackTrace };
        }
    }

    public async Task<List<CartItemsViewModel>> GetCartItems(string userId)
    {
        try
        {
            List<CartItemsViewModel> cartItemsViewModel = new List<CartItemsViewModel>();

            using (SqlConnection connection = _context.CreateConnection())
            {
                cartItemsViewModel = (await connection.QueryAsync<CartItemsViewModel>("select p.Id as ProductId, Name as ProductName, Price, c.Quantity, p.MainImage as ProductImage from Cart c join Products p on c.ProductId = p.Id where c.CustomerId = @customerid", new { customerId = userId })).ToList();
            }

            return cartItemsViewModel;
        }
        catch
        {
            return new();
        }
    }

    public async Task<OperationResult> ChangeCartValue(int productId, int quantity, string userId)
    {
        try
        {
            using (SqlConnection connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync("UPDATE Cart SET Quantity = @quantity WHERE CustomerId = @customerid and ProductId = @productid", new { productid = productId, quantity = quantity, customerid = userId });
            }
            return new OperationResult() { OperationStatus = OperationStatus.Success, Message = "با موفقیت ویرایش شد اضافه شد" };
        }
        catch (Exception ex)
        {
            return new OperationResult() { OperationStatus = OperationStatus.Fail, Message = ex.StackTrace };
        }
    }

    public async Task<OperationResult> RemoveCartItem(int productId, string userId)
    {
        try
        {
            using (SqlConnection connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync("Delete From Cart where ProductId = @productid and CustomerId = @customerid", new { productid = productId, customerid = userId });
            }
            return new OperationResult() { OperationStatus = OperationStatus.Success, Message = "آیتم مورد نظر با موفقیت حذف شد" };
        }
        catch (Exception ex)
        {
            return new OperationResult() { OperationStatus = OperationStatus.Fail, Message = ex.StackTrace };
        }
    }

    public async Task<OperationResult> RemoveAllCartItems(string userId)
    {
        try
        {
            using (SqlConnection connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync("delete from cart where customerId = @customerid", new { customerid = userId });
            }
            return new OperationResult() { OperationStatus = OperationStatus.Success, Message = "سبد خرید خالی شد" };
        }
        catch (Exception ex)
        {
            return new OperationResult() { OperationStatus = OperationStatus.Fail, Message = ex.StackTrace };
        }
    }

    public async Task<long> GetTotalOrderPrice(string userId)
    {
        try
        {
            using SqlConnection connection = _context.CreateConnection();

            return await connection.ExecuteScalarAsync<long>("select (Price * c.Quantity) from Cart c join Products p on c.ProductId = p.Id where c.CustomerId = @customerid", new { customerid = userId });
        }
        catch
        {
            return 0;
        }
    }

    public async Task<OperationResult> SubmitOrder(CheckoutViewModel checkout, string userId, long shippingPrice)
    {
        try
        {
            using SqlConnection connection = _context.CreateConnection();
            await connection.OpenAsync();

            var order = new
            {
                status = OrderStatus.Waiting,
                datetimepaid = (DateTime?)null,
                datetimecompleted = (DateTime?)null,
                customerid = userId,
                name = checkout.Name,
                family = checkout.Family,
                town = checkout.TownId,
                city = checkout.CityId,
                address = checkout.Address,
                email = checkout.Email,
                phone = checkout.Phone,
                description = checkout.Description,
                paymentmethod = checkout.PaymentMethod,
                transactionid = (string?)null,
                cartnumber = (string?)null,
                totalprice = checkout.TotalPrice,
                totaltax = 0,
                totalshipping = shippingPrice,
                shippingtypeid = (byte)checkout.ShippingMethod,
                authority = checkout.Authority
            };

            // Make New Order
            int orderId = await connection.ExecuteScalarAsync<int>("INSERT INTO Orders (Status,DateTimeCreated,DateTimePaid,DateTimeCompleted,CustomerId,CustomerFirstName,CustomerLastName,CustomerTown,CustomerCity ,CustomerAddress,CustomerEmail,CustomerPhone,CustomerNote,PaymentMethod,TransactionId,CartNumber,TotalPrice,TotalTax,TotalShipping,ShippingTypeId,Authority) OUTPUT Inserted.Id VALUES (@status,GETDATE(),@datetimepaid,@datetimecompleted,@customerid,@name ,@family,@town,@city,@address ,@email,@phone,@description,@paymentmethod ,@transactionid,@cartnumber,@totalprice,@totaltax,@totalshipping,@shippingtypeid,@authority)", order);

            List<OrderItem> orderItems = new();
            var cartItems = await connection.QueryAsync("select p.Id as ProductId, Name as ProductName, Price, c.Quantity, p.MainImage as ProductImage from Cart c join Products p on c.ProductId = p.Id where c.CustomerId = @customerid", new { customerid = userId });
            foreach (var item in cartItems)
            {
                orderItems.Add(new OrderItem()
                {
                    ProductId = item.ProductId,
                    Fee = item.Price,
                    Quantity = item.Quantity,
                    OrderId = orderId
                });
            }

            await connection.BulkInsertAsync<OrderItem>(orderItems);

            await connection.ExecuteAsync("DELETE from Cart where CustomerId = @customerid", new { customerid = userId });
            await connection.CloseAsync();

            return new OperationResult() { OperationStatus = OperationStatus.Success, Message = "عملیات با موفقیت انجام شد" };
        }
        catch
        {
            return new OperationResult() { OperationStatus = OperationStatus.Exception, Message = "خطا سرویس ثبت سفارش" };
        }
    }

    public async Task<long> GetShippingPrice(ShippingMethods shipping, long orderPrice, int state, int city)
    {
        try
        {
            using SqlConnection connection = _context.CreateConnection();
            await connection.OpenAsync().ConfigureAwait(false);
            // Get Shipping Data From Database
            ShippingSettings shippingData = await connection.QuerySingleAsync<ShippingSettings>("SELECT * FROM ShippingSettings Where Type = @type", new
            {
                type = (byte)shipping
            });

            int defaultWeight = await connection.ExecuteScalarAsync<int>("SELECT TOP 1 DefaultWeight FROM ShopSettings");
            await connection.CloseAsync().ConfigureAwait(false);

            // Free Shipping
            if (shippingData.FreeShippingPrice != 0 && orderPrice >= shippingData.FreeShippingPrice)
            {
                return 0;
            }

            // Not Free Shipping
            switch (shipping)
            {
                case ShippingMethods.Post:

                    int senderFee = 10500;
                    int shippingPrice = 0;
                    int services = 8550;
                    int insurance = 4000;
                    int boxPrice = 4000;

                    switch (state)
                    {
                        case 1:
                            // weight below 500g
                            shippingPrice += 14200;
                            shippingPrice += senderFee;
                            shippingPrice += services;
                            shippingPrice += insurance;
                            shippingPrice += 1000; //box price tehran
                            return shippingPrice;
                        case 31:
                        case 10:
                        case 11:
                        case 13:
                        case 9:
                            // weight below 500g
                            shippingPrice += 19500;
                            shippingPrice += senderFee;
                            shippingPrice += services;
                            shippingPrice += insurance;
                            shippingPrice += boxPrice; //box price tehran
                            return shippingPrice;
                        default:
                            // weight below 500g
                            shippingPrice += 23400;
                            shippingPrice += senderFee;
                            shippingPrice += services;
                            shippingPrice += insurance;
                            shippingPrice += boxPrice; //box price tehran
                            return shippingPrice;
                    }
                case ShippingMethods.Tipax:
                    return shippingData.Price;
                case ShippingMethods.Peyk:
                    return shippingData.Price;
                case ShippingMethods.Chapar:
                    return shippingData.Price;
                case ShippingMethods.Barbari:
                    return shippingData.Price;
                case ShippingMethods.TahvilHozori:
                    return shippingData.Price;
                default:
                    return 0;
            }
        }
        catch
        {
            return 0;
        }
    }

    public async Task<List<IranCities>?> GetIranStates()
    {
        try
        {
            using var connection = _context.CreateConnection();

            var states = await connection.QueryAsync<IranCities>("SELECT distinct State, StateId FROM IranCities order by State asc");

            return states.ToList();
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<IranCities>?> GetStateCities(int stateId)
    {
        try
        {
            using var connection = _context.CreateConnection();

            var states = await connection.QueryAsync<IranCities>("SELECT distinct City, CityId FROM IranCities where StateId = @stateid order by City asc", new
            {
                stateid = stateId
            });

            return states.ToList();
        }
        catch
        {
            return null;
        }
    }
}