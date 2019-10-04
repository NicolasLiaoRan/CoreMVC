using Core_MVC.Data;
using Core_MVC.Model;
using System.Collections.Generic;
using System.Linq;

namespace Core_MVC.Services
{
    public class EFCoreRepository : IRepository<Student>
    {
        private readonly DataContext _context;

        //因为已经写入服务，这里就可以用构造函数直接注入
        public EFCoreRepository(DataContext context)
        {
            _context = context;
        }
        public Student Add(Student t)
        {
            _context.Students.Add(t);
            _context.SaveChanges();
            return t;
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public Student GetById(int id)
        {
            return _context.Students.Find(id);
        }
    }
}
