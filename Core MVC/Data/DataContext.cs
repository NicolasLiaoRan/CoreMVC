using Core_MVC.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_MVC.Data
{
    public class DataContext:DbContext
    {
        //配置信息
        public DataContext(DbContextOptions<DataContext>options):base(options)
        {
            //继承自父类，并将参数传递给父类以实现完整的EFCore功能
        }
        public DbSet<Student> Students { get; set; }
    }
}
