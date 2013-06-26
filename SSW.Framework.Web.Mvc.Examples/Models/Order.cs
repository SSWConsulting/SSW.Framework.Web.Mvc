using System;
using System.ComponentModel.DataAnnotations;

namespace SSW.Framework.Web.Mvc.Examples.Models
{
    public class Order
    {
        [Display(Name = "Customer Name")]
        [Required(ErrorMessage = "Customer Name is required")]
        public string CustomerName { get; set; }

        [Display(Name = "Order Number")]
        [Required(ErrorMessage = "Order Number is required")]
        public string OrderNumber { get; set; }

        [Display(Name = "Order Status")]
        public OrderStatus Status { get; set; }
    }
}