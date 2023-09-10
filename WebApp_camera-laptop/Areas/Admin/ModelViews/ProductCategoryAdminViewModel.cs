using System.Collections.Generic;
using WebApp_camera_laptop.Models;

namespace WebApp_camera_laptop.Areas.Admin.ModelViews
{
    public class ProductCategoryAdminViewModel
    {
        public int ProductCatId { get; set; }
        public List<int> SelectedCategoryIds { get; set; }
        public List<Category> AvailableCategories { get; set; }
    }
}
