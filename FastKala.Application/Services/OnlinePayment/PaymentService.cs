using Dapper;
using FastKala.Application.Data;
using FastKala.Application.Interfaces.OnlinePayment;
using FastKala.Domain.Models.Payment;

namespace FastKala.Application.Services.OnlinePayment;

public class PaymentService : IPaymentService
{
    private readonly DapperContext _dapperContext;

    public PaymentService(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<PaymentSettings> GetZarinpalData()
    {
        using var connection = _dapperContext.CreateConnection();
        return await connection.QuerySingleAsync<PaymentSettings>("SELECT * FROM PaymentSettings where Name = N'Zarinpal'");
    }
}