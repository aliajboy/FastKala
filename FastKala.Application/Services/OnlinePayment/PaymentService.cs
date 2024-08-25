using Dapper;
using FastKala.Application.Data;
using FastKala.Application.Interfaces.OnlinePayment;
using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.ShopSettings;
using FastKala.Domain.Enums.OnlinePayment;
using FastKala.Domain.Models.Payment;

namespace FastKala.Application.Services.OnlinePayment;

public class PaymentService : IPaymentService
{
    private readonly DapperContext _dapperContext;

    public PaymentService(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<List<PaymentSettings>?> GetAllPayments()
    {
        try
        {
            using var connection = _dapperContext.CreateConnection();
            var payments = await connection.QueryAsync<PaymentSettings>("SELECT * FROM PaymentSettings");
            return payments.ToList();
        }
        catch
        {
            return null;
        }
    }

    public async Task<OperationResult> UpdateGateway(UpdatePaymentViewModel updatePayment)
    {
        try
        {
            using var connection = _dapperContext.CreateConnection();
            int status = await connection.ExecuteAsync("UPDATE PaymentSettings set Name = @name, Type = @type, ApiKey = @apikey where Id = @id", new
            {
                name = updatePayment.Name,
                type = updatePayment.Type,
                apikey = updatePayment.ApiKey,
                id = updatePayment.Id,
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
                Message = "عملیات با خطا مواجه شد"
            };
        }
        catch
        {
            return new OperationResult()
            {
                OperationStatus = Domain.Enums.Global.OperationStatus.Exception,
                Message = "عملیات با خطا غیر منتظره مواجه شد"
            };
        }
    }

    public async Task<OperationResult> AddPayment(PaymentSettings payment)
    {
        try
        {
            using var connection = _dapperContext.CreateConnection();
            int status = await connection.ExecuteAsync("INSERT INTO PaymentSettings (Name,Type,ApiKey,Currency,CartNumber,AccountNumber,ShebaNumber) VALUES (@name,@type,@apikey,@currency,@cartnumber,@accountnumber,@shebanumber)", new
            {
                name = payment.Name,
                type = payment.Type,
                apikey = payment.ApiKey,
                currency = payment.Currency,
                cartnumber = payment.CartNumber,
                accountnumber = payment.AccountNumber,
                shebanumber = payment.ShebaNumber
            });

            if (status == 1)
            {
                return new OperationResult()
                {
                    OperationStatus = Domain.Enums.Global.OperationStatus.Success,
                    Message = "با موفقیت افزوده شد"
                };
            }

            return new OperationResult()
            {
                OperationStatus = Domain.Enums.Global.OperationStatus.Fail,
                Message = "عملیات با خطا مواجه شد"
            };
        }
        catch
        {
            return new OperationResult()
            {
                OperationStatus = Domain.Enums.Global.OperationStatus.Exception,
                Message = "عملیات با خطا غیر منتظره مواجه شد"
            };
        }
    }

    public async Task<OperationResult> RemovePayment(int paymentId)
    {
        try
        {
            using var connection = _dapperContext.CreateConnection();
            int status = await connection.ExecuteAsync("Delete From PaymentSettings Where Id = @id", new
            {
                id = paymentId
            });

            if (status == 1)
            {
                return new OperationResult()
                {
                    OperationStatus = Domain.Enums.Global.OperationStatus.Success,
                    Message = "با موفقیت حذف شد"
                };
            }

            return new OperationResult()
            {
                OperationStatus = Domain.Enums.Global.OperationStatus.Fail,
                Message = "عملیات با خطا مواجه شد"
            };
        }
        catch
        {
            return new OperationResult()
            {
                OperationStatus = Domain.Enums.Global.OperationStatus.Exception,
                Message = "عملیات با خطا غیر منتظره مواجه شد"
            };
        }
    }

    public async Task<PaymentSettings?> GetZarinpalData()
    {
        try
        {
            using var connection = _dapperContext.CreateConnection();
            return await connection.QuerySingleAsync<PaymentSettings>("SELECT Id, Name, Type, ApiKey FROM PaymentSettings where type = @type", new
            {
                type = PaymentType.ZarinPal
            });
        }
        catch
        {
            return null;
        }
    }
}