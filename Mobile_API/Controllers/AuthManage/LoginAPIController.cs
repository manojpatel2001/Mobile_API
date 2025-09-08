using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Mobile_Core.AuthManage;
using Mobile_Infrastructure.Interface;
using Mobile_Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mobile_API.Controllers.AuthManage
{
    [Route("api/[controller]")]
    [ApiController]
   

    public class LoginAPIController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginAPIController> _logger;

        public LoginAPIController(IUnitOfWork unitOfWork,
                                 IConfiguration configuration,
                                 ILogger<LoginAPIController> logger)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("Login")]
        public async Task<APIResponse> Login(UserLogin user)
        {
            try
            {
                _logger.LogInformation("Login attempt started for user: {UserName} with DeviceId: {DeviceId}",
                    user?.UserName ?? "null", user?.DeviceId ?? "null");

                // Validate user input
                var response = await ValidationUserLogin(user);
                if (response.Status != true)
                {
                    _logger.LogWarning("Login validation failed for user: {UserName}. Reason: {Message}",
                        user?.UserName ?? "null", response.ResponseMessage);
                    return response;
                }

                _logger.LogInformation("User validation successful for: {UserName}", user.UserName);

                // Authenticate user
                var login = await _unitOfWork.LoginRepository.AuthenticateUser(user);
                if (!login.Success)
                {
                    _logger.LogWarning("User authentication failed for: {UserName}. Reason: {Message}",
                        user.UserName, login.Message);
                    return new APIResponse { Status = false, ResponseMessage = login.Message };
                }

                _logger.LogInformation("User authentication successful for: {UserName}", user.UserName);

                // Get login details
                GetLoginData? loginDetails = await _unitOfWork.LoginRepository.GetLoginData(user);
                if (loginDetails == null)
                {
                    _logger.LogError("Failed to retrieve login data for authenticated user: {UserName}", user.UserName);
                    return new APIResponse { Status = false, ResponseMessage = "Something went wrong" };
                }

                _logger.LogInformation("Login data retrieved successfully for user: {UserName}", user.UserName);

                // Generate JWT token
                var token = await BuildToken(loginDetails, _configuration);

                var finalData = new
                {
                    Token = token,
                    Data = loginDetails
                };

                _logger.LogInformation("Login process completed successfully for user: {UserName}", user.UserName);

                return new APIResponse { Status = login.Success, ResponseMessage = login.Message, Data = finalData };
            }
            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "Argument null exception during login for user: {UserName}",
                    user?.UserName ?? "null");
                return new APIResponse { Status = false, ResponseMessage = "Invalid request parameters" };
            }
            catch (SecurityTokenException tokenEx)
            {
                _logger.LogError(tokenEx, "JWT token generation failed for user: {UserName}",
                    user?.UserName ?? "null");
                return new APIResponse { Status = false, ResponseMessage = "Token generation failed" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during login process for user: {UserName}. Error: {ErrorMessage}",
                    user?.UserName ?? "null", ex.Message);
                return new APIResponse { Status = false, ResponseMessage = "An unexpected error occurred during login" };
            }
        }

        private async Task<string> BuildToken(GetLoginData? userInfo, IConfiguration _config)
        {
            try
            {
                _logger.LogInformation("Building JWT token for user");

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

                var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                  _config["Jwt:Audience"],
                  claims,
                  expires: DateTime.Now.AddMinutes(60),
                  signingCredentials: credentials);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                _logger.LogInformation("JWT token generated successfully");

                return tokenString;
            }
            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "JWT configuration missing or invalid. Check Jwt:Key, Jwt:Issuer, Jwt:Audience in appsettings");
                throw new SecurityTokenException("JWT configuration is invalid", argEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while building JWT token: {ErrorMessage}", ex.Message);
                throw new SecurityTokenException("Token generation failed", ex);
            }
        }

        private async Task<APIResponse> ValidationUserLogin(UserLogin data)
        {
            try
            {
                _logger.LogInformation("Validating user login parameters");

                var response = new APIResponse { Status = true, ResponseMessage = "" };

                if (data == null)
                {
                    _logger.LogWarning("Login validation failed: UserLogin object is null");
                    response.Status = false;
                    response.ResponseMessage = "Incorrect Parameter Value";
                    return response;
                }

                if (string.IsNullOrEmpty(data.UserName))
                {
                    _logger.LogWarning("Login validation failed: UserName is missing");
                    response.Status = false;
                    response.ResponseMessage = "User Name is required";
                    response.Data = "UserName";
                }
                else if (string.IsNullOrEmpty(data.Password))
                {
                    _logger.LogWarning("Login validation failed: Password is missing for user: {UserName}", data.UserName);
                    response.Status = false;
                    response.ResponseMessage = "Password is required";
                    response.Data = "Password";
                }
                else if (string.IsNullOrEmpty(data.DeviceId))
                {
                    _logger.LogWarning("Login validation failed: DeviceId is missing for user: {UserName}", data.UserName);
                    response.Status = false;
                    response.ResponseMessage = "Device ID is required";
                    response.Data = "DeviceId";
                }

                if (response.Status)
                {
                    _logger.LogInformation("User login validation successful for: {UserName}", data.UserName);
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during login validation: {ErrorMessage}", ex.Message);
                return new APIResponse
                {
                    Status = false,
                    ResponseMessage = "Validation error occurred"
                };
            }
        }
    }
}
