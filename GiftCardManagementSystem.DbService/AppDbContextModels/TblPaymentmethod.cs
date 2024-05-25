using System;
using System.Collections.Generic;

namespace GiftCardManagementSystem.DbService.AppDbContextModels;

public partial class TblPaymentmethod
{
    public int PaymentMethodId { get; set; }

    public string? PaymentMethodName { get; set; }

    public decimal? Discount { get; set; }
}
