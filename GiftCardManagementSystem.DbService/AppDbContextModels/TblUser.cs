using System;
using System.Collections.Generic;

namespace GiftCardManagementSystem.Infrastructure.AppDbContextModels;

public partial class TblUser
{
    public string UserId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string PhoneNo { get; set; } = null!;

    public int? MaximunLimit { get; set; }

    public int? CashbackPoint { get; set; }

    public decimal? CashbackAmount { get; set; }
}
