using System;
using System.Collections.Generic;

namespace GiftCardManagementSystem.DbService.AppDbContextModels;

public partial class TblAdminuser
{
    public string UserId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string? Password { get; set; }

    public string? UserRole { get; set; }

    public bool? DelFlag { get; set; }
}
