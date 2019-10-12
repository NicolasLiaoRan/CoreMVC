﻿using System.ComponentModel.DataAnnotations;

namespace Core_MVC.Model.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name ="用户名")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="密码")]
        public string Password { get; set; }
    }
}
