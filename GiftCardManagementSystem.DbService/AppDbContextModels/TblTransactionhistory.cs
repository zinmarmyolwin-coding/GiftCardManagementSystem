using System;
using System.Collections.Generic;

namespace GiftCardManagementSystem.DbService.AppDbContextModels;

public partial class TblTransactionhistory
{
    public int TransactionHistoryId { get; set; }

    public string GiftCardCode { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime? TransactionDate { get; set; }

    public decimal? Amount { get; set; }
}
