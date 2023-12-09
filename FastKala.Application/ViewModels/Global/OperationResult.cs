using FastKala.Domain.Enums;

namespace FastKala.Application.ViewModels.Global;
public class OperationResult
{
    public OperationStatus OperationStatus { get; set; }
    public string? Message { get; set; }
}