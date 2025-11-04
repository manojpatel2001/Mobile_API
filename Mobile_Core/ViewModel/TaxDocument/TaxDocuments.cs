using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Core.ViewModel.TaxDocument
{
    public class TaxDocuments
    {
        public int? DocumentId { get; set; }
        public int? UserId { get; set; }
        public int? FinancialYear { get; set; }
        public string? DocumentType { get; set; }
        public string? DocumentTitle { get; set; }
        public DateTime? IssuedOn { get; set; }
        public string? DownloadUrl { get; set; }
        public decimal? TotalIncome { get; set; }
        public decimal? TaxDeducted { get; set; }
    }

    public class TaxDeclaration
    {
        public int? DeclarationId { get; set; }
        public int? UserId { get; set; }
        public int? FinancialYear { get; set; }
        public string? DeclarationType { get; set; }
        public string? Status { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string? Remarks { get; set; }
    }
    public class TaxSummaryVM
    {
        public int? UserId { get; set; }
        public int? FinancialYear { get; set; }
       
    }


}
