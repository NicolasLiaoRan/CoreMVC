using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_MVC.Model;
using Core_MVC.Model.ViewModels;
using Core_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Core_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Student> repository;//只读字段

        public HomeController(IRepository<Student> _repository)
        {
            this.repository = _repository;
        }
        public IActionResult Index()
        {
            var list = repository.GetAll();
            var vm = list.Select(x => new HomeViewModel
            {
                Id = x.Id,
                Name = $"{x.FirstName}{x.LastName}",
                Age = DateTime.Now.Subtract(x.Birthday).Days / 365
            });
            var vms = new StudentViewModel
            {
                Students = vm
            };
            return View(vms);
        }
        //通过id寻找信息
        public IActionResult Detail(int id)
        {
            var student = repository.GetById(id);
            if (student == null)
                return RedirectToAction(nameof(Index));
            return View(student);
        }
        //GET添加
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //POST添加
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentInputViewModel student)
        {
            //验证
            if(ModelState.IsValid)
            {
                var newStudent = new Student
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Birthday = student.Birthday,
                    Gender = student.Gender
                };
                var newModel = repository.Add(newStudent);

                //return View("Detail", newModel);
                return RedirectToAction(nameof(Detail), new { id = newModel.Id });
            }
            return View();
        }
    }
}