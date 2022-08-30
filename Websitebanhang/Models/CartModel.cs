using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Websitebanhang.Context;

namespace Websitebanhang.Models
{
    public class CartModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}