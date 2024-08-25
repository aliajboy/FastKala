using Dapper;
using FastKala.Application.Data;
using FastKala.Application.Interfaces.OnlinePayment;
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

    public async Task<PaymentSettings?> GetZarinpalData()
    {
        try
        {
            using var connection = _dapperContext.CreateConnection();
            return await connection.QuerySingleAsync<PaymentSettings>("SELECT Id, Name, Type, ApiKey, Currency FROM PaymentSettings where type = @type", new
            {
                type = (int)PaymentType.ZarinPal
            });
        }
        catch
        {
            return null;
        }
    }
}
