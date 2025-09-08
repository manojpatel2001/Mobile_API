using Microsoft.EntityFrameworkCore;
using Mobile_Core.AuthManage;
using Mobile_Core.CommonClass;
using Mobile_Core.EmployeeAttedance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Core.DB
{
    public class MobileDbcontext:DbContext
    {
        public MobileDbcontext(DbContextOptions options) : base(options)
        {

        }



        //Register Viwemodel
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // For SP return type
            modelBuilder.Entity<SP_Response>().HasNoKey();
            modelBuilder.Entity<GetLoginData>().HasNoKey();
            modelBuilder.Entity<EmployeeAttendanceStatusVM>().HasNoKey();
            modelBuilder.Entity<AutoListVM>().HasNoKey();




        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
