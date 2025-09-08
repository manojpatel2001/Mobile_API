using Mobile_Core.AuthManage;
using Mobile_Core.CommonClass;
using Mobile_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Infrastructure.Interface.AuthManage
{
    public interface ILoginRepository
    {
        Task<SP_Response> AuthenticateUser(UserLogin userLogin);
        Task<GetLoginData?> GetLoginData(UserLogin userLogin);
    }
}
