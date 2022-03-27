using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicProject.ViewModel
{
    public class ForgotPassViewModel
    {
        [Required(ErrorMessage = "پسورد خود را وارد کنید")]
        [MinLength(8, ErrorMessage = "پسورد باید حداقل 8 کاراکتر باشد")]
        public string password { get; set; }


        [Compare("password",ErrorMessage ="پسورد ها یکسان نیست")]
        public string repassword { get; set; }

    }
}
