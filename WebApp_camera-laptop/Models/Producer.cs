using System;
using System.Collections.Generic;

#nullable disable

namespace WebApp_camera_laptop.Models
{
    public partial class Producer
    {
        public Producer()
        {
            Products = new HashSet<Product>();
        }

        public int ProducerId { get; set; }
        public string ProducerName { get; set; }
        public string Description { get; set; }
        public string Thumb { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
