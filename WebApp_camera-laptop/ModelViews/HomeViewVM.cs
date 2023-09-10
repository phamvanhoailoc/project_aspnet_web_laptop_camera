using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_camera_laptop.Models;

namespace WebApp_camera_laptop.ModelViews
{
    public class HomeViewVM
    {
  
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }

        public void SetDefaultThumbValues()
        {
            if (Products != null)
            {
                Products = Products.Select(product =>
                {
                    if (product.Thumb == null)
                    {
                        // Gán giá trị mặc định cho thuộc tính thumb nếu nó là null
                        product.Thumb = "default,default,default,default,default,default"; // Thay thế "Giá trị mặc định" bằng giá trị mặc định thực tế của bạn
                    }
                    return product;
                }).ToList();
            }
        }
    }
}
