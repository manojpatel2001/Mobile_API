using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Core.ViewModel.Intraction
{
    public class BirthdayInteraction
    {
        public int? BirthdayInteractionId { get; set; } 
        public int? PostId { get; set; }
        public int? UserId { get; set; }
        public string? InteractionType { get; set; } // "Like" or "Comment"
        public int? ParentInteractionId { get; set; }  // Nullable for main comments
        public string? CommentText { get; set; }
    }



}
