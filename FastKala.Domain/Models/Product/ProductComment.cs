using FastKala.Domain.Enums;

namespace FastKala.Domain.Models.Product;
public record ProductComment
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public byte Rate { get; set; } = 5;
    public int HelpedCount { get; set; }
    public int NotHelpedCount { get; set; }
    public DateTime SubmitDate { get; set; }
    public CommentStatus Status { get; set; }
    public string UserName { get; set; }
    public bool IsBuyer { get; set; } = false;
    public bool Recommended { get; set; } = true;
    public ProductCommentRatings Quality { get; set; } = ProductCommentRatings.Perfect;
    public ProductCommentRatings Value { get; set; } = ProductCommentRatings.Perfect;
    public ProductCommentRatings Support { get; set; } = ProductCommentRatings.Perfect;
    public ProductCommentRatings Feature { get; set; } = ProductCommentRatings.Perfect;
    public ProductCommentRatings Usability { get; set; } = ProductCommentRatings.Perfect;
    public ProductCommentRatings Design { get; set; } = ProductCommentRatings.Perfect;
    public int ProductId { get; set; }

    public List<ProductCommentsProsCons> ProsCons { get; set; } = new();
}

public record ProductCommentsProsCons
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsPros { get; set; }
    public int CommentId { get; set; }
}