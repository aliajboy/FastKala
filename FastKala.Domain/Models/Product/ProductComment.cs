namespace FastKala.Domain.Models.Product;
public record ProductComment
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Rate { get; set; } = 5;
    public int HelpedCount { get; set; }
    public int NotHelpedCount { get; set; }
    public DateTime SubmitDate { get; set; }
    public string UserName { get; set; }
    public bool IsBuyer { get; set; } = false;
    public bool Recommended { get; set; } = true;
    public int ProductId { get; set; }

    public List<ProductCommentProsCons> ProsCons { get; set; } = new();
}

public record ProductCommentProsCons
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsPros { get; set; }

    public ProductComment comment { get; set; } = new();
}