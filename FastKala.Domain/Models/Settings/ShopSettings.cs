namespace FastKala.Domain.Models.Settings;

public record ShopSettings
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? LogoFileName { get; set; }
    public int TaxPercent { get; set; }
    public int TownId { get; set; }
    public int CityId { get; set; }
    public int DefaultWeight { get; set; }
}