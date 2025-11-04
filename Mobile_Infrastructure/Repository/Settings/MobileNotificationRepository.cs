using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Mobile_Core.DB;
using Mobile_Core.ViewModel.Setting;
using Mobile_Infrastructure.Interface.Settings;
using Mobile_Utility;
using System.Data;

namespace Mobile_Infrastructure.Repository.Settings
{
    public class MobileNotificationRepository : IMobileNotificationRepository
    {
        private readonly string _connectionString;
        private readonly MobileDbcontext _db;

        public MobileNotificationRepository(MobileDbcontext db)
        {
            _db = db;
            _connectionString = db.Database.GetDbConnection().ConnectionString;
        }
        
        public async Task<APIResponse> GetNotifications(int userId)
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
                        "sp_MobileNotifications_CRUD",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    response.Status = parameters.Get<bool>("@Success");
                    response.ResponseMessage = parameters.Get<string>("@ResponseMessage");
                    if (!result.Any() || result == null)
                    {
                        response.Data = null;
                        response.Status = false;
                        response.ResponseMessage = "No Record found!";
                    }
                    else
                    {
                        response.Data = result;
                    }
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

        public async Task<APIResponse> InsertNotification(MobileNotificationVM notificationVM)
        {
            var response = new APIResponse();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Action", "INSERT");
                    parameters.Add("@UserId", notificationVM.UserId);
                    parameters.Add("@CategoryDetailId", notificationVM.CategoryDetailId);
                    parameters.Add("@Message", notificationVM.Message);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    await connection.ExecuteAsync(
                        "sp_MobileNotifications_CRUD",
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

        public async Task<APIResponse> MarkAsRead(MarkAsReadVM model)
        {
            var response = new APIResponse();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Action", "MARK_AS_READ");
                    parameters.Add("@UserId", model.UserId);
                    parameters.Add("@CategoryDetailId", model.CategoryDetailId);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    await connection.ExecuteAsync(
                        "sp_MobileNotifications_CRUD",
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
    }
  }
