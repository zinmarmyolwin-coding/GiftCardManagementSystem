using System;
using System.Collections.Generic;

namespace GiftCardManagementSystem.DbService.AppDbContextModels;

public partial class TblUser
{
    public string UserId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string PhoneNo { get; set; } = null!;

    public int? MaximunLimit { get; set; }
}
