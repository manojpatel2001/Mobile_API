using Mobile_Infrastructure.Interface.AuthManage;
using Mobile_Infrastructure.Interface.EmployeeAttedance;
using Mobile_Infrastructure.Interface.EmployeeManage;
using Mobile_Infrastructure.Interface.EmpployeeManage;
using Mobile_Infrastructure.Interface.Intraction;
using Mobile_Infrastructure.Interface.Settings;
using Mobile_Infrastructure.Interface.TaxDocument;
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
        IEmployeeManageRepository EmpployeeManageRepository { get; }
        IEmployeeReportRepository EmployeeReportRepository { get; }
        IEmployeeLeaveManageRepository EmployeeLeaveManageRepository { get; }
        IBirthdayInteractionRepository BirthdayInteractionRepository { get; }
        IUserSettingRepository UserSettingRepository { get; }
        IMobileNotificationRepository MobileNotificationRepository { get; }
        IAnnouncementRepository AnnouncementRepository { get; }
        ITaxDocumentRepository TaxDocumentRepository { get; }

    }
}
