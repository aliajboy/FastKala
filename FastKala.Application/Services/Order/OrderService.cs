using Dapper;
using FastKala.Application.Data;
using FastKala.Application.Interfaces.Order;
using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.Orders;
using FastKala.Domain.Enums.Global;
using FastKala.Domain.Models.Product;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
                Cart cartItem = await connection.QuerySingleOrDefaultAsync<Cart>("Select * From Cart where CustomerId = @customerid and ProductId = @productid", new
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
}