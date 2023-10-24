using System;
using System.ComponentModel.DataAnnotations;
using WebApp_camera_laptop.Models;

namespace WebApp_camera_laptop.Areas.Admin.ModelViews
{
    public class NewsAdminViewModel
    {
        public int NewId { get; set; }
        [MaxLength(250)]
        [Required(ErrorMessage = "Tiêu đề bài viết không thể bỏ trống")]
        public string Title { get; set; }
        public string Scontents { get; set; }
        public string Contents { get; set; }
        public string Thumb { get; set; }
        public bool Published { get; set; }
        public string Alias { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Author { get; set; }
        public int? AccountId { get; set; }
        public string Tags { get; set; }
        [Required(ErrorMessage = "Danh mục không thể bỏ trống")]
        public int? CatId { get; set; }
        public bool IsHot { get; set; }
        public bool IsNewfeed { get; set; }
        public string MetaKey { get; set; }
        public string MetaDesc { get; set; }
        public int? Views { get; set; }

    }
}
