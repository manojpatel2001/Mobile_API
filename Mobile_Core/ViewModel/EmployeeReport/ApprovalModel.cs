using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Core.ViewModel.EmployeeReport
{
    public class ApprovalModel
    {
       public int @UserId {  get; set; }
       public int ApplicationId {  get; set; }
       public string Status {  get; set; }
       public string ApplicationType {  get; set; }
    }
}
