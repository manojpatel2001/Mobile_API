using Mobile_Core.CommonClass;
using Mobile_Core.ViewModel.Intraction;
using Mobile_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Infrastructure.Interface.Intraction
{
    public interface IBirthdayInteractionRepository
    {
        Task<APIResponse> AddBirthdayInteraction(BirthdayInteraction model);
        Task<APIResponse> DeleteBirthdayComment(DeleteViewModel model);
        Task<APIResponse> GetBirthdayCommentsWithReplies(int PostId);
    }
}
