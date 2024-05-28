using System;
using System.Collections.Generic;

namespace GiftCardManagementSystem.Infrastructure.AppDbContextModels;

public partial class TblGiftcard
{
    public int GiftCardId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? GiftCardNo { get; set; }

    public int? CashbackPoint { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public decimal? Amount { get; set; }

    public int Quantity { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedUserId { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedUserId { get; set; }

    public bool? IsActive { get; set; }

    public decimal? CashbackAmount { get; set; }
}
