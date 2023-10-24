using System;
using System.Collections.Generic;

#nullable disable

namespace WebApp_camera_laptop.Models
{
    public partial class CategoriesNews
    {
        public CategoriesNews()
        {
            News = new HashSet<News>();
        }

        public int CatNewId { get; set; }
        public string CatName { get; set; }
        public string Description { get; set; }
        public string Thumb { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKey { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}
