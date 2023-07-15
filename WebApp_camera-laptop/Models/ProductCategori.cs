using System;
using System.Collections.Generic;

#nullable disable

namespace WebApp_camera_laptop.Models
{
    public partial class ProductCategori
    {
        public int? ProductsId { get; set; }
        public int? CatId { get; set; }
        public int ProductCatId { get; set; }

        public virtual Category Cat { get; set; }
        public virtual Product Products { get; set; }
    }
}
