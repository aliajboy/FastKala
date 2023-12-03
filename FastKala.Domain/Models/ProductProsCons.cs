using System.ComponentModel;

namespace FastKala.Domain.Models;

public class ProductPros
{
    public int Id { get; set; }
    [DisplayName("نقاط قوت")]
    public string Text { get; set; }

    // Relations
    public Product Product { get; set; }
}

public class ProductCons
{
    public int Id { get; set; }
    [DisplayName("نقاط ضعف")]
    public string Text { get; set; }

    // Relations
    public Product Product { get; set; }
}