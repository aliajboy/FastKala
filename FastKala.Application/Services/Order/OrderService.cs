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
using System.Text;
using System.Text.Json;
using Z.Dapper.Plus;

namespace FastKala.Application.Services.Order;
public class OrderService : IOrderService
{
    private readonly DapperContext _context;
    public OrderService(DapperContext context)
    {
        _context = context;
    }

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

                    await connection.ExecuteAsync(sql, new { productid = productId, quantity = quantity, customerid = userId });
                }
                else
                {
                    sql = "INSERT INTO Cart (ProductId,Quantity,CustomerId) VALUES (@productid,@quantity,@customerid)";

                    await connection.ExecuteAsync(sql, new { productid = productId, quantity = quantity, customerid = userId });
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
            return new List<CartItemsViewModel>();
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
                town = checkout.Town,
                city = checkout.City,
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

            List<OrderItem> orderItems = new List<OrderItem>();
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

    public async Task<long> GetShippingPrice(ShippingMethods shipping, long orderPrice)
    {
        try
        {
            using SqlConnection connection = _context.CreateConnection();
            // Get Shipping Data From Database
            ShippingSettings shippingData = await connection.QuerySingleAsync<ShippingSettings>("SELECT * FROM ShippingSettings Where Type = @type", new
            {
                type = (byte)shipping
            });

            // Free Shipping
            if (shippingData.FreeShippingPrice != 0 && orderPrice >= shippingData.FreeShippingPrice)
            {
                return 0;
            }

            // Not Free Shipping
            switch (shipping)
            {
                case ShippingMethods.Post:
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://api.tapin.ir/");
                        client.Timeout = TimeSpan.FromSeconds(5);
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoiZmE1NWE0ZDctOGUzMC00MTk2LWI1NzktMTNlZDU4MTFmNzliIiwidXNlcm5hbWUiOiIwOTM5MzI0MDM0MCIsImVtYWlsIjoiamViYWxlYWxpQGdtYWlsLmNvbSIsImV4cCI6MjU4ODQyMzE5OSwib3JpZ19pYXQiOjE3MjQ0MjMxOTl9.Ewj8GGAY9XrzFNnATtgfYK9xdHj-x23cKGbCtgwEMkE");
                        var requestModel = new TapinRequestPriceModel()
                        {
                            shop_id = "04eaf906-6dc0-4551-adce-aa808cafeaf2",
                            address = "تهرانه دیگه",
                            box_id = 1,
                            city_code = 1,
                            province_code = 1,
                            description = null,
                            email = null,
                            employee_code = -1,
                            first_name = "First Name",
                            last_name = "Last Name",
                            mobile = "09111111111",
                            phone = null,
                            postal_code = "1313131313",
                            order_type = 1,
                            pay_type = 1,
                            package_weight = 0,
                            products = new List<TapinRequestPriceProductModel>()
                            {
                                new TapinRequestPriceProductModel()
                                {
                                    count = 1,
                                    discount = 0,
                                    price = 0,
                                    product_id = null,
                                    title = "My Product",
                                    weight = 250,
                                }
                            }
                        };
                        var response = await client.PostAsync("api/v2/public/order/post/check-price/", new StringContent(JsonSerializer.Serialize(requestModel), Encoding.UTF8, "application/json"));
                        if (response.IsSuccessStatusCode)
                        {
                            var resultString = await response.Content.ReadAsStringAsync();
                            var result = JsonSerializer.Deserialize<TapinResponsePriceModel>(resultString);
                            if (result != null)
                            {
                                var shippingPrice = result.entries?.total_price;
                                return Convert.ToInt64((Math.Round(Convert.ToDecimal(shippingPrice) / 500) * 500)) / 10;
                            }
                        }
                    }
                    return shippingData.Price;
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

}