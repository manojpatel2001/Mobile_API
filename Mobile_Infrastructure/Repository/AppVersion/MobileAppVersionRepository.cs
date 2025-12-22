using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Mobile_Core.DB;
using Mobile_Core.ViewModel.AppVersion;
using Mobile_Infrastructure.Interface.AppVersion;
using Mobile_Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Infrastructure.Repository.AppVersion
{
    public class MobileAppVersionRepository : IMobileAppVersionRepository
    {

        private readonly string _connectionString;
        private readonly MobileDbcontext _db;

        public MobileAppVersionRepository(MobileDbcontext db)
        {
            _db = db;
            _connectionString = db.Database.GetDbConnection().ConnectionString;
        }


        // ================================
        // INSERT APP VERSION
        // ================================
        public async Task<APIResponse> UploadAppVersion(AppVersionModel model)
        {
            var response = new APIResponse();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();

                    parameters.Add("@Action", "Insert");
                    parameters.Add("@AppVersion", model.AppVersion);
                    parameters.Add("@Description", model.Description);
                    parameters.Add("@FromDate", model.FromDate);
                    parameters.Add("@AppType", model.AppType);
                    parameters.Add("@DocumentName", model.DocumentName);
                    parameters.Add("@FileSize", model.FileSize);
                    parameters.Add("@DocumentPath", model.DocumentPath);

                    // 🔹 OUT params
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync(
                        "USP_Mobile_AppVersion",
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
                response.ResponseMessage = ex.Message;
                response.Data = null;
            }

            return response;
        }

        // ================================
        // GET LATEST VERSION
        // ================================
        public async Task<APIResponse> GetLatestVersion(MobileAppVersionPara model)
        {
            var response = new APIResponse();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();

                    parameters.Add("@Action", "GetVersion");
                    parameters.Add("@AppType", model.AppType);
                    parameters.Add("@AppVersion", model.AppVersion);

                    // 🔹 OUT params
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                    var result = await connection
                        .QueryFirstOrDefaultAsync<dynamic>(
                            "USP_Mobile_AppVersion",
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
                response.ResponseMessage = ex.Message;
                response.Data = null;
            }

            return response;
        }
    }
}
