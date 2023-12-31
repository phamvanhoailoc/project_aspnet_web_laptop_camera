﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WebApp_camera_laptop.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            ProductCategoris = new HashSet<ProductCategori>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ShortDesc { get; set; }
        public string Description { get; set; }
        public int? CatId { get; set; }
        public int? Price { get; set; }
        public int? Discount { get; set; }
        public string Thumb { get; set; }
        public string Video { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? BestSellers { get; set; }
        public bool? HomeFlag { get; set; }
        public bool? Active { get; set; }
        public string Tags { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKey { get; set; }
        public int? UnitslnStock { get; set; }
        public int? ProducerId { get; set; }
        public int? StatusId { get; set; }
        public string Cpu { get; set; }
        public string Ram { get; set; }
        public string HardDrive { get; set; }
        public string GraphicCard { get; set; }
        public string Screen { get; set; }
        public string Location { get; set; }
        public string TypeCameraId { get; set; }
        public string Resoluton { get; set; }
        public string CameraAngle { get; set; }
        public string Storege { get; set; }
        public string FarInfraredVision { get; set; }
        public string DeviceSupport { get; set; }
        public string ControlPhone { get; set; }
        public string Utilities { get; set; }
        public bool? Conversation { get; set; }
        public string Adapter { get; set; }
        public string InputPower { get; set; }
        public string Size { get; set; }
        public string Configuration { get; set; }

        public virtual Producer Producer { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductCategori> ProductCategoris { get; set; }
    }
}
