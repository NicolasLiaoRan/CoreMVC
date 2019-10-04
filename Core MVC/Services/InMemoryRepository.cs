using Core_MVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_MVC.Services
{
    public class InMemoryRepository:IRepository<Student>
    {
        private readonly List<Student> _students;
        public InMemoryRepository()
        {
            _students= new List<Student>
            {
                new Student
                {
                    Id=1,
                    FirstName="Weiyuqi",
                    LastName="Sam",
                    Birthday=new DateTime(1991,1,4)
                },
                new Student
                {
                    Id=2,
                    FirstName="Weiyuqiao",
                    LastName="aWei",
                    Birthday=new DateTime(1992,1,2)
                },
                new Student
                {
                    Id=3,
                    FirstName="Weiyujiao",
                    LastName="xiaoyu",
                    Birthday=new DateTime(1993,1,3)
                }
            };
        }

        //现在是写死的数据
        public IEnumerable<Student> GetAll()
        {
            return _students;
        }

        public Student GetById(int id)
        {
            return _students.FirstOrDefault(x => x.Id==id);
        }
        public Student Add(Student student)
        {
            var maxId = _students.Max(x => x.Id);
            student.Id = maxId + 1;
            _students.Add(student);
            return student;
        }
    }
}
