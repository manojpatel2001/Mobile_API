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
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly string _connectionString;
        private readonly MobileDbcontext _db;

        public AnnouncementRepository(MobileDbcontext db)
        {
            _db = db;
            _connectionString = db.Database.GetDbConnection().ConnectionString;
        }

        public async Task<APIResponse> GetAnnouncements(int userId)
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
                        "USP_Announcements_Mobile_Operations",
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

        public async Task<APIResponse> InsertAnnouncement(AnnouncementVM announcementVM)
        {
            var response = new APIResponse();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Action", "INSERT");
                    parameters.Add("@CategoryDetailsId", announcementVM.CategoryDetailsId);
                    parameters.Add("@Message", announcementVM.Message);
                    parameters.Add("@ValidTill", announcementVM.ValidTill);
                    parameters.Add("@PostedBy", announcementVM.PostedBy);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    await connection.ExecuteAsync(
                        "USP_Announcements_Mobile_Operations",
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

        public async Task<APIResponse> UpdateAnnouncement(AnnouncementVM announcementVM)
        {
            var response = new APIResponse();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Action", "UPDATE");
                    parameters.Add("@AnnouncementsId", announcementVM.AnnouncementId);
                    parameters.Add("@Message", announcementVM.Message);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    await connection.ExecuteAsync(
                        "USP_Announcements_Mobile_Operations",
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

        public async Task<APIResponse> MarkAsRead(MarkAnnouncementAsReadVM markAsReadVM)
        {
            var response = new APIResponse();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Action", "MARK_AS_READ");
                    parameters.Add("@UserId", markAsReadVM.UserId);
                    parameters.Add("@AnnouncementsId", markAsReadVM.AnnouncementId);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    await connection.ExecuteAsync(
                        "USP_Announcements_Mobile_Operations",
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
