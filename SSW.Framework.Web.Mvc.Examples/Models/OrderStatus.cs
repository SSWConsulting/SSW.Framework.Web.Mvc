using System;
using System.ComponentModel.DataAnnotations;

namespace SSW.Framework.Web.Mvc.Examples.Models
{
    public enum OrderStatus
    {
        [Display(Name="New Order")]
        New = 1,
        [Display(Name = "On Hold")]
        OnHold = 2,
        [Display(Name = "Pending Payment")]
        PendingPayment = 3,
        [Display(Name = "Shipped")]
        Shipped = 4
    }

}