using System;
using System.Collections.Generic;

namespace GiftCardManagementSystem.DbService.AppDbContextModels;

public partial class TblGiftcard
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? GiftCardNo { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public decimal? Amount { get; set; }
}
