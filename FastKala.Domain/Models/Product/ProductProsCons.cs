using FastKala.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FastKala.Domain.Models.Product;

public record ProductProsCons
{
    public int Id { get; set; }
    [Required]
    [StringLength(500, ErrorMessage = "متن بیشتر از 150 کاراکتر نباید باشد")]
    public required string Text { get; set; }
    public ProsConsType IsPros { get; set; }
    public int ProductId { get; set; }
}