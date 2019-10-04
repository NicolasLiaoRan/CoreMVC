using Core_MVC.Model;
using Core_MVC.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_MVC.Model.ViewModels
{
    public class StudentViewModel
    {
        public IEnumerable<HomeViewModel> Students { get; set; }
    }
}
