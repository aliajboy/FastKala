namespace FastKala.Domain.Models.Product;
public class ProductTagRelation
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int TagId { get; set; }
}