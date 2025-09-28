namespace Mobile_Core.ViewModel.Intraction
{
    public class CommentReplyModel
    {
        public int? ReplyId { get; set; }
        public int? UserId { get; set; }
        public string? FullName { get; set; }
        public string? EmployeeCode { get; set; }
        public string? EmployeeProfileUrl { get; set; }
        public string? CommentText { get; set; }
        public DateTime? CreatedDate { get; set; }
    }



}
