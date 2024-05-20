using FastKala.Domain.Enums.Global;

namespace FastKala.Application.ViewModels.Global;
public class OperationResult
{
    public OperationStatus OperationStatus { get; set; }
    public string? Message { get; set; }
}