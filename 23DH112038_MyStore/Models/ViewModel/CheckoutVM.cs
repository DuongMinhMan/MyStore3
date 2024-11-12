using _23DH112038_MyStore.Models.ViewModel;
using _23DH112038_MyStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _23DH112038_MyStore.Models.ViewModel
{
    public class CheckoutVM // lưu thông tin form Checkout
    {
        public List<CartItem> CartItems { get; set; }
        public int CustomerID { get; set; }

        [Display(Name = "Ngày đặt hàng")]
        public System.DateTime OrderDate { get; set; }

        [Display(Name = "Tổng giá trị")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Trạng thái thanh toán")]
        public string PaymentStatus { get; set; }

        [Display(Name = "Phương thức thanh toán")]
        public string PaymentMethod { get; set; }

        [Display(Name = "Phương thức giao hàng")]
        public string DeliveryMethod { get; set; }

        [Display(Name = "Địa chỉ giao hàng")]
        public string AddressDelivery { get; set; }
        public string Username { get; set; }
        //Các thuộc tính khác của đơn hàng
        public List<OrderDetail> OrderDetail { get; set; }
    }
}