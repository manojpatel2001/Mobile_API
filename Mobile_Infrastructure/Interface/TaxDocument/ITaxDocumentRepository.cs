using Dapper;
using Microsoft.Data.SqlClient;
using Mobile_Core.ViewModel.TaxDocument;
using Mobile_Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mobile_Infrastructure.Interface.TaxDocument
{

    public interface ITaxDocumentRepository
    {
        Task<APIResponse> GetTaxDocuments(int userId);
        Task<APIResponse> InsertTaxDocument(TaxDocuments taxDocument);
        Task<APIResponse> InsertDeclaration(TaxDeclaration taxDeclaration);
        Task<APIResponse> UpdateDeclaration(TaxDeclaration taxDeclaration);
        Task<APIResponse> GetTaxSummary(TaxSummaryVM model);
    }
}
