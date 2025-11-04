using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Mobile_Core.DB;
using Mobile_Core.ViewModel.TaxDocument;
using Mobile_Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Infrastructure.Interface.TaxDocument
{
    public class TaxDocumentRepository : ITaxDocumentRepository
    {
        private readonly string _connectionString;
        private readonly MobileDbcontext _db;

        public TaxDocumentRepository(MobileDbcontext db)
        {
            _db = db;
            _connectionString = db.Database.GetDbConnection().ConnectionString;
        }

        public async Task<APIResponse> GetTaxDocuments(int userId)
        {
            var response = new APIResponse();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Action", "GET");
                    parameters.Add("@UserId", userId);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    var result = await connection.QueryAsync<dynamic>(
                        "USP_Mobile_TaxDocuments_Operations",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    response.Status = parameters.Get<bool>("@Success");
                    response.ResponseMessage = parameters.Get<string>("@ResponseMessage");
                    response.Data = result;
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ResponseMessage = $"An error occurred: {ex.Message}";
                response.Data = null;
            }
            return response;
        }

        public async Task<APIResponse> InsertTaxDocument(TaxDocuments taxDocument)
        {
            var response = new APIResponse();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Action", "INSERT_DOCUMENT");
                    parameters.Add("@UserId", taxDocument.UserId);
                    parameters.Add("@FinancialYear", taxDocument.FinancialYear);
                    parameters.Add("@DocumentType", taxDocument.DocumentType);
                    parameters.Add("@DocumentTitle", taxDocument.DocumentTitle);
                    parameters.Add("@IssuedOn", taxDocument.IssuedOn);
                    parameters.Add("@DownloadUrl", taxDocument.DownloadUrl);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    await connection.ExecuteAsync(
                        "USP_Mobile_TaxDocuments_Operations",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    response.Status = parameters.Get<bool>("@Success");
                    response.ResponseMessage = parameters.Get<string>("@ResponseMessage");
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ResponseMessage = $"An error occurred: {ex.Message}";
                response.Data = null;
            }
            return response;
        }

        public async Task<APIResponse> InsertDeclaration(TaxDeclaration taxDeclaration)
        {
            var response = new APIResponse();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Action", "INSERT_DECLARATION");
                    parameters.Add("@UserId", taxDeclaration.UserId);
                    parameters.Add("@FinancialYear", taxDeclaration.FinancialYear);
                    parameters.Add("@DeclarationType", taxDeclaration.DeclarationType);
                    parameters.Add("@Status", taxDeclaration.Status);
                    parameters.Add("@LastUpdated", taxDeclaration.LastUpdated);
                    parameters.Add("@Remarks", taxDeclaration.Remarks);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    await connection.ExecuteAsync(
                        "USP_Mobile_TaxDocuments_Operations",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    response.Status = parameters.Get<bool>("@Success");
                    response.ResponseMessage = parameters.Get<string>("@ResponseMessage");
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ResponseMessage = $"An error occurred: {ex.Message}";
                response.Data = null;
            }
            return response;
        }

        public async Task<APIResponse> UpdateDeclaration(TaxDeclaration taxDeclaration)
        {
            var response = new APIResponse();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Action", "UPDATE_DECLARATION");
                    parameters.Add("@UserId", taxDeclaration.UserId);
                    parameters.Add("@DeclarationType", taxDeclaration.DeclarationType);
                    parameters.Add("@Status", taxDeclaration.Status);
                    parameters.Add("@Remarks", taxDeclaration.Remarks);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    await connection.ExecuteAsync(
                        "USP_Mobile_TaxDocuments_Operations",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    response.Status = parameters.Get<bool>("@Success");
                    response.ResponseMessage = parameters.Get<string>("@ResponseMessage");
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ResponseMessage = $"An error occurred: {ex.Message}";
                response.Data = null;
            }
            return response;
        }

        public async Task<APIResponse> GetTaxSummary(TaxSummaryVM model)
        {
            var response = new APIResponse();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Action", "GET");
                    parameters.Add("@UserId", model.UserId);
                    parameters.Add("@FinancialYear", model.FinancialYear);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    var result = await connection.QueryAsync<dynamic>(
                        "USP_Mobile_TaxDocuments_Operations",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    response.Status = parameters.Get<bool>("@Success");
                    response.ResponseMessage = parameters.Get<string>("@ResponseMessage");
                    response.Data = result;
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ResponseMessage = $"An error occurred: {ex.Message}";
                response.Data = null;
            }
            return response;
        }
    }
}
