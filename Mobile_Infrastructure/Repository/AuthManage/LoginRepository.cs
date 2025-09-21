using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mobile_Core.AuthManage;
using Mobile_Core.CommonClass;
using Mobile_Core.DB;
using Mobile_Core.EmployeeAttedance;
using Mobile_Core.ViewModel.Employee;
using Mobile_Infrastructure.Interface.AuthManage;
using Mobile_Utility;



namespace Mobile_Infrastructure.Repository.AuthManage
{
    public class LoginRepository : ILoginRepository
    {
        private readonly MobileDbcontext _db;
        private readonly ILogger<LoginRepository> _logger;

        public LoginRepository(MobileDbcontext db, ILogger<LoginRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<SP_Response> AuthenticateUser(UserLogin userLogin)
        {
            try
            {
                _logger.LogInformation("Attempting to authenticate user: {UserName} with DeviceId: {DeviceId}",
                    userLogin.UserName, userLogin.DeviceId);

                var result = await _db.Set<SP_Response>().FromSqlInterpolated($@"
                     EXEC USP_Mobile_UserMaster 
                    @Status = {"Authenticate"},
                    @UserName = {userLogin.UserName},
                    @Password = {userLogin.Password},
                    @DeviceId = {userLogin.DeviceId}
                     ").ToListAsync();

                var data = result.FirstOrDefault();

                if (data?.Success == true)
                {
                    _logger.LogInformation("User authentication successful for: {UserName}", userLogin.UserName);
                }
                else
                {
                    _logger.LogWarning("User authentication failed for: {UserName}", userLogin.UserName);
                }

                return result.FirstOrDefault() ?? new SP_Response { Success = false, Message = "Authentication failed." };
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL error occurred while authenticating user: {UserName}. Error: {ErrorMessage}",
                    userLogin.UserName, sqlEx.Message);
                return new SP_Response { Success = false, Message = "Database error occurred during authentication." };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while authenticating user: {UserName}. Error: {ErrorMessage}",
                    userLogin.UserName, ex.Message);
                return new SP_Response { Success = false, Message = "An unexpected error occurred during authentication." };
            }
        }

        public async Task<GetLoginData?> GetLoginData(UserLogin userLogin)
        {
            try
            {
                _logger.LogInformation("Retrieving login data for user: {UserName}", userLogin.UserName);
                var result = await _db.Set<GetLoginData>().FromSqlInterpolated($@"
            EXEC USP_Mobile_UserMaster 
                @Status = {"GetLoginData"},
                @UserName = {userLogin.UserName},
                @Password = {userLogin.Password}
              ").ToListAsync();

                var loginData = result.FirstOrDefault();

                if (loginData != null)
                {
                    _logger.LogInformation("Login data retrieved successfully for user: {UserName}", userLogin.UserName);
                }
                else
                {
                    _logger.LogWarning("No login data found for user: {UserName}", userLogin.UserName);
                }

                return loginData;
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL error occurred while retrieving login data for user: {UserName}. Error: {ErrorMessage}",
                    userLogin.UserName, sqlEx.Message);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while retrieving login data for user: {UserName}. Error: {ErrorMessage}",
                    userLogin.UserName, ex.Message);
                return null;
            }
        }
        public async Task<SP_Response> UpdateFCMToken (Common_Parameter parameter)
        {
            try
            {
                var result = await _db.Set<SP_Response>().FromSqlInterpolated($@"
                    EXEC USP_MobileFCMToken
                        @Token = {parameter.FCMToken},
                        @User_Id = {parameter.UserId},
                        @DeviceId = {parameter.DeviceId}
                ").ToListAsync();

                return result.FirstOrDefault() ?? new SP_Response { Success = false, Message = "Something went wrong!" };
            }
            catch
            {
                return new SP_Response { Success = false, Message = "Something went wrong!" };
            }
        }
        public async Task<SP_Response> ChangePassword (ChangePassword parameter)
        {
            try
            {
                var result = await _db.Set<SP_Response>().FromSqlInterpolated($@"
                    EXEC USP_Mobile_ChangePassword
                        @Action = {"ChangePassword"},
                        @UserId = {parameter.UserId},
                        @NewPassword = {parameter.NewPassword}
                ").ToListAsync();

                return result.FirstOrDefault() ?? new SP_Response { Success = false, Message = "Something went wrong!" };
            }
            catch (Exception ex)
            {
                return new SP_Response { Success = false, Message = "Something went wrong!" };
            }
        }


    }
}

