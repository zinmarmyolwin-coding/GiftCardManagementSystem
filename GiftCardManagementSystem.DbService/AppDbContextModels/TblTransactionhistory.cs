using System;
using System.Collections.Generic;

namespace GiftCardManagementSystem.Infrastructure.AppDbContextModels;

public partial class TblTransactionhistory
{
    public int TranHistoryId { get; set; }

    public string? TranId { get; set; }

    public string? UserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? TransactionDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? NetAmount { get; set; }

    public decimal? DiscountAmount { get; set; }

    public decimal? CashbackAmount { get; set; }

    public decimal? BackCashbackAmount { get; set; }

    public string? Status { get; set; }

    public string? PaymentCode { get; set; }

    public bool? IsUsedCahback { get; set; }
}
