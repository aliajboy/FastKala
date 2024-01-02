namespace FastKala.Domain.Models;
public class ProductTagRelation
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int TagId { get; set; }
}