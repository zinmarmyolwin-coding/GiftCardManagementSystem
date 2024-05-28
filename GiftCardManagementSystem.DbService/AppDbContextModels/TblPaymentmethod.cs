using System;
using System.Collections.Generic;

namespace GiftCardManagementSystem.Infrastructure.AppDbContextModels;

public partial class TblPaymentmethod
{
    public int PaymentMethodId { get; set; }

    public string PaymentMethodCode { get; set; } = null!;

    public string PaymentMethodName { get; set; } = null!;

    public decimal? Discount { get; set; }

    public bool? Active { get; set; }
}
