﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WebApp_camera_laptop.Models
{
    public partial class Category
    {
        public Category()
        {
            ProductCategoris = new HashSet<ProductCategori>();
        }

        public int CatId { get; set; }
        public string CatName { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public int? Levels { get; set; }
        public bool? Ordering { get; set; }
        public string Published { get; set; }
        public string Thumb { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKey { get; set; }
        public string Cover { get; set; }
        public string SchemaMarkup { get; set; }

        public virtual ICollection<ProductCategori> ProductCategoris { get; set; }
    }
}
