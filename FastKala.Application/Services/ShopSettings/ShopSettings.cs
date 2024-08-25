using Dapper;
using FastKala.Application.Data;
using FastKala.Application.Interfaces.ShopSettings;
using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.ShopSettings;
using FastKala.Domain.Models.Orders;
using Microsoft.Data.SqlClient;

namespace FastKala.Application.Services.ShopSettings;

public class ShopSettings : IShopSettings
{
    private readonly DapperContext _dapperContext;

    public ShopSettings(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<List<ShippingSettings>> GetActiveShippingTypes()
    {
        try
        {
            using SqlConnection connection = _dapperContext.CreateConnection();
            var shippings = await connection.QueryAsync<ShippingSettings>("select * from ShippingSettings Where Active = 1");
            return shippings.ToList();
        }
        catch
        {
            return new List<ShippingSettings>();
        }
    }

    public async Task<List<ShippingSettings>> GetAllShippingTypes()
    {
        try
        {
            using SqlConnection connection = _dapperContext.CreateConnection();
            var shippings = await connection.QueryAsync<ShippingSettings>("select * from ShippingSettings");
            return shippings.ToList();
        }
        catch
        {
            return new List<ShippingSettings>();
        }
    }

    public async Task<OperationResult> ToggleShipping(int id)
    {
        try
        {
            using SqlConnection connection = _dapperContext.CreateConnection();
            int status = await connection.ExecuteAsync("if ((SELECT Active FROM ShippingSettings where id = @shippingid) = 0) begin update ShippingSettings set Active = 1 where id = @shippingid end else begin update ShippingSettings set Active = 0 where id = @shippingid end", new
            {
                shippingid = id
            });

            if (status == 1)
            {
                return new OperationResult()
                {
                    OperationStatus = Domain.Enums.Global.OperationStatus.Success,
                    Message = "تغییر وضعیت با موفقیت انجام شد"
                };
            }
            else
            {
                return new OperationResult()
                {
                    OperationStatus = Domain.Enums.Global.OperationStatus.Fail,
                    Message = "تغییر وضعیت شیوه ارسال با خطا مواجه شد"
                };
            }
        }
        catch
        {
            return new OperationResult()
            {
                OperationStatus = Domain.Enums.Global.OperationStatus.Exception,
                Message = "تغییر وضعیت شیوه ارسال با خطا غیر منتظره مواجه شد"
            };
        }
    }

    public async Task<ShippingSettings?> GetShippingById(int id)
    {
        try
        {
            using SqlConnection connection = _dapperContext.CreateConnection();
            var shipping = await connection.QuerySingleOrDefaultAsync<ShippingSettings>("select * from ShippingSettings where Id = @Id", new
            {
                Id = id,
            });
            if (shipping == null)
            {
                return null;
            }
            return shipping;
        }
        catch
        {
            return null;
        }
    }

    public async Task<OperationResult> UpdateShipping(UpdateDeliveryViewModel updateDelivery)
    {
        try
        {
            using SqlConnection connection = _dapperContext.CreateConnection();

            int status = await connection.ExecuteAsync("update ShippingSettings set Price = @price, FreeShippingPrice = @freeshippingprice, ExtraPrice = @extraprice where Id = @Id", new
            {
                price = updateDelivery.Price,
                freeshippingprice = updateDelivery.FreeShippingPrice,
                extraprice = updateDelivery.ExtraPrice,
                Id = updateDelivery.Id
            });

            if (status == 1)
            {
                return new OperationResult()
                {
                    OperationStatus = Domain.Enums.Global.OperationStatus.Success,
                    Message = "عملیات با موفقیت انجام شد"
                };
            }

            return new OperationResult()
            {
                OperationStatus = Domain.Enums.Global.OperationStatus.Fail,
                Message = "عملیات بروزرسانی با خطا مواجه شد"
            };
        }
        catch
        {
            return new OperationResult()
            {
                OperationStatus = Domain.Enums.Global.OperationStatus.Exception,
                Message = "عملیات بروزرسانی با خطا غیر منتظره مواجه شد"
            };
        }
    }
}