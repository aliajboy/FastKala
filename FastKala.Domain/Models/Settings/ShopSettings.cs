namespace FastKala.Domain.Models.Settings;

public record ShopSettings
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Logo { get; set; }
    public int TaxPercent { get; set; }
    public int Town { get; set; }
    public int City { get; set; }
    public int DefaultWeight { get; set; }
}