using System;
using System.Collections.Generic;

namespace GiftCardManagementSystem.Infrastructure.AppDbContextModels;

public partial class TblTransactionhistorydetail
{
    public int TransactionHistoryDetailId { get; set; }

    public string TranId { get; set; } = null!;

    public string GiftCardNo { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string? ToUserId { get; set; }

    public bool? IsSelfService { get; set; }

    public DateTime? TransactionDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public decimal? Amount { get; set; }

    public int? Quantity { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? Status { get; set; }
}
