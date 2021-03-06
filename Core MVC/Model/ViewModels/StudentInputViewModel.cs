﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core_MVC.Model.ViewModels
{
    public class StudentInputViewModel
    {
        [Display(Name ="姓"),Required]
        public string FirstName { get; set; }
        [Display(Name ="名"),Required]
        public string LastName { get; set; }
        [Display(Name ="出生日期")]
        public DateTime Birthday { get; set; }
        [Display(Name ="性别")]
        public Gender Gender { get; set; }
    }
}
