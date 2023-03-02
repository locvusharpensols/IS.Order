using src.Domain.Common;

namespace IS.Order.Domain.Entities;

public class Order : BaseAuditableEntity
{
    public string? OrderNumber { get; set; }
    public string? CustomerId { get; set; }
    public decimal Amount { get; set; }
    public int FundId { get; set; }
    public DateTime OrderDate { get; set; }
}