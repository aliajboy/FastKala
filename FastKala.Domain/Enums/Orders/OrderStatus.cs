namespace FastKala.Domain.Enums.Orders;

public enum OrderStatus : byte
{
    Waiting = 0,
    Pending = 1,
    Proccessing = 2,
    Packaged = 3,
    Sent = 4,
    Completed = 5
}