using Mobile_Infrastructure.Interface.AuthManage;
using Mobile_Infrastructure.Interface.EmployeeAttedance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Infrastructure.Interface
{
    public interface IUnitOfWork
    {
        ILoginRepository LoginRepository { get; }
        IEmployeeAttendanceRepository EmployeeAttendanceRepository { get; }

    }
}
