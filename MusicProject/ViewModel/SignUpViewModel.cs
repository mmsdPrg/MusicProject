using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicProject.ViewModel
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage ="نام خود را وارد کنید")]
        public string Name { get; set; }
        [Required(ErrorMessage = "ایمیل خود را وارد کنید")]
        [EmailAddress(ErrorMessage ="ایمیل به درستی وارد نشده است")]
        public string Email { get; set; }
        [Required(ErrorMessage = "پسورد خود را وارد کنید")]
        [MinLength(8,ErrorMessage ="پسورد باید حداقل 8 کاراکتر باشد")]
        public string Password { get; set; }
    }
}
