﻿using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.ShopSettings;
using FastKala.Domain.Models.Orders;
using FastKala.Domain.Models.Payment;

namespace FastKala.Application.Interfaces.ShopSettings;

public interface IShopSettings
{
    public Task<List<ShippingSettings>> GetAllShippingTypes();

    public Task<List<ShippingSettings>> GetActiveShippingTypes();

    public Task<OperationResult> ToggleShipping(int id);

    public Task<ShippingSettings?> GetShippingById(int id);

    public Task<OperationResult> UpdateShipping(UpdateDeliveryViewModel updateDelivery);

    public Task<List<PaymentSettings>?> GetAllPayments();

    public Task<OperationResult> UpdateGateway(UpdatePaymentViewModel updatePayment);

    public Task<OperationResult> AddPayment(PaymentSettings payment);

    public Task<OperationResult> RemovePayment(int paymentId);
}