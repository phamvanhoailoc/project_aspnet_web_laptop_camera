using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebQLKSORACLE.Areas.ADMIN.ModelViews
{
    public class EditPassword
    {
        [Key]
        public int MaNv { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu cũ")]
        public string MatkhauCu { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(5, ErrorMessage = "Bạn cần đặt mật khẩu tối thiểu 5 ký tự")]
        public string MatkhauNv { get; set; }
        [MinLength(5, ErrorMessage = "Bạn cần đặt mật khẩu tối thiểu 5 ký tự")]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("MatkhauNv", ErrorMessage = "Vui lòng nhập mật khẩu giống nhau")]
        public string ConfirmMatkhauNv { get; set; }
    }
}
