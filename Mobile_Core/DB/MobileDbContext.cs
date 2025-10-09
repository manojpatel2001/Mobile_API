using Microsoft.EntityFrameworkCore;
using Mobile_Core.AuthManage;
using Mobile_Core.CommonClass;
using Mobile_Core.EmployeeAttedance;
using Mobile_Core.ViewModel;
using Mobile_Core.ViewModel.Employee;
using Mobile_Core.ViewModel.EmployeeReport;
using Mobile_Core.ViewModel.Intraction;
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
            modelBuilder.Entity<vmGetEmployeeById>().HasNoKey();
            modelBuilder.Entity<vmGetSalarySalaryDetails>().HasNoKey();
            modelBuilder.Entity<vmGetTodayBirthdaysByCompany>().HasNoKey();
            modelBuilder.Entity<vmGetUpcomingHolidays>().HasNoKey();
            modelBuilder.Entity<vmAttedanceCalanderDaysSummary>().HasNoKey();
            modelBuilder.Entity<vmAttedanceCalanderDays>().HasNoKey();
            modelBuilder.Entity<vmGetMonthlyAttendanceDetails>().HasNoKey();
            modelBuilder.Entity<Login_Response>().HasNoKey();
            modelBuilder.Entity<EmployeeDashboardCountModel>().HasNoKey();
            modelBuilder.Entity<vmGetDay>().HasNoKey();
            modelBuilder.Entity<vmGetAttendanceByDate>().HasNoKey();
            modelBuilder.Entity<vmGetLeaveBalance>().HasNoKey();
            modelBuilder.Entity<vmGetLeaveType>().HasNoKey();
            modelBuilder.Entity<vmGetHalfDayType>().HasNoKey();
            modelBuilder.Entity<vmGetResponsibleperson>().HasNoKey();
            modelBuilder.Entity<CommentModel>().HasNoKey();
            modelBuilder.Entity<CommentReplyModel>().HasNoKey();
            modelBuilder.Entity<LastCheckInDetailsVM>().HasNoKey();


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
