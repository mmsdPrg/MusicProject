using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicProject.ViewModel
{
    public class SignInViewModel
    {
        [Required(ErrorMessage ="نام کاربری خود را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل به درستی وارد نشده است")]

        public string UserName { get; set; }
        [Required(ErrorMessage = "پسورد  خود را وارد کنید")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
