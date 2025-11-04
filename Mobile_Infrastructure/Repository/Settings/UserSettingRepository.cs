using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Mobile_Core.DB;
using Mobile_Core.ViewModel.Setting;
using Mobile_Infrastructure.Interface.Settings;
using Mobile_Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Infrastructure.Repository.Settings
{
    public class UserSettingRepository : IUserSettingRepository
    {
        private readonly string _connectionString;
        private readonly MobileDbcontext _db;

        public UserSettingRepository(MobileDbcontext db)
        {
            _db = db;
            _connectionString = db.Database.GetDbConnection().ConnectionString;
        }

        public async Task<APIResponse> GetUserSettings(int userId)
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
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);

                    var result = await connection.QueryAsync<dynamic>(
                        "sp_MobileUserSettings",
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

        public async Task<APIResponse> UpdateUserSetting(UserSettingVM userSettingVM)
        {
            var response = new APIResponse();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Action", "UPDATE");
                    parameters.Add("@UserId", userSettingVM.UserId);
                    parameters.Add("@NotificationTypeId", userSettingVM.NotificationTypeId);
                    parameters.Add("@IsActive", userSettingVM.IsActive);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);

                    await connection.ExecuteAsync(
                        "sp_MobileUserSettings",
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
