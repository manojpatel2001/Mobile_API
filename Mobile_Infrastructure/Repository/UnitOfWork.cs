using Microsoft.Extensions.Logging;
using Mobile_Core.DB;
using Mobile_Infrastructure.Interface;
using Mobile_Infrastructure.Interface.AuthManage;
using Mobile_Infrastructure.Interface.EmployeeAttedance;
using Mobile_Infrastructure.Interface.EmployeeManage;
using Mobile_Infrastructure.Interface.EmpployeeManage;
using Mobile_Infrastructure.Interface.Intraction;
using Mobile_Infrastructure.Repository.AuthManage;
using Mobile_Infrastructure.Repository.EmployeeAttedance;
using Mobile_Infrastructure.Repository.EmployeeManage;
using Mobile_Infrastructure.Repository.EmpployeeManage;
using Mobile_Infrastructure.Repository.Intraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Infrastructure.Repository
{
    

    public class UnitOfWork : IUnitOfWork
    {
        private readonly MobileDbcontext _dbContext;
        private readonly ILogger<LoginRepository> _loginLogger;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(MobileDbcontext dbContext,
                         ILogger<LoginRepository> loginLogger,
                         ILogger<UnitOfWork> logger)
        {
            _dbContext = dbContext;
            _loginLogger = loginLogger;
            _logger = logger;

            // Initialize repository with logger
            LoginRepository = new LoginRepository(_dbContext, _loginLogger);
            EmployeeAttendanceRepository = new EmployeeAttendanceRepository(_dbContext);
            EmpployeeManageRepository = new EmployeeManageRepository(_dbContext);
            EmployeeReportRepository = new EmployeeReportRepository(_dbContext);
            BirthdayInteractionRepository = new BirthdayInteractionRepository(_dbContext);

            _logger.LogInformation("UnitOfWork initialized successfully");
        }

        public ILoginRepository LoginRepository { get; set; }
        public IEmployeeAttendanceRepository EmployeeAttendanceRepository { get; set; }
        public IEmployeeManageRepository EmpployeeManageRepository { get; set; }
        public IEmployeeReportRepository EmployeeReportRepository { get; set; }
        public IEmployeeLeaveManageRepository EmployeeLeaveManageRepository { get; set; }
        public IBirthdayInteractionRepository BirthdayInteractionRepository { get; set; }
        

        // Optional: Add disposal logging
        public void Dispose()
        {
            try
            {
                _logger.LogInformation("Disposing UnitOfWork and database context");
                _dbContext?.Dispose();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while disposing UnitOfWork");
                throw;
            }
        }
    }
}
