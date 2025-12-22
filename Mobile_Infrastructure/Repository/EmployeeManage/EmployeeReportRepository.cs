using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Mobile_Core.CommonClass;
using Mobile_Core.DB;
using Mobile_Core.EmployeeAttedance;
using Mobile_Core.ViewModel;
using Mobile_Core.ViewModel.EmployeeReport;
using Mobile_Core.ViewModel.Intraction;
using Mobile_Infrastructure.Interface.EmployeeManage;
using Mobile_Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Mobile_Infrastructure.Repository.EmployeeManage
{
    public class EmployeeReportRepository: IEmployeeReportRepository
    {
        private readonly MobileDbcontext _db;
        private readonly string _connectionString;

        public EmployeeReportRepository(MobileDbcontext db)
        {
            _db = db;
            _connectionString = db.Database.GetDbConnection().ConnectionString;
        }

        public async Task<vmGetSalarySalaryDetails?> GetSalarySalaryDetails(SalarysDetailsParameter vm)
        {
            try
            {

                var result = await _db.Set<vmGetSalarySalaryDetails>().FromSqlInterpolated($"EXEC SP_Mobile_GetSalarySalaryDetails @EmployeeId={vm.EmployeeId},@Month={vm.Month},@Year={vm.Year}").ToListAsync();
                return result.FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public async Task<APIResponse> GetTodayBirthdaysByCompany(Common_Parameter model)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@CompanyId", model.CompanyId);
                    parameters.Add("@UserId", model.UserId);

                    // Execute the stored procedure and get JSON result
                    var jsonResult = await connection.ExecuteScalarAsync<string>(
                        "sp_Mobile_GetTodayBirthdaysByCompany",
                        parameters,
                        commandType: CommandType.StoredProcedure);

                    // Deserialize the JSON result
                    var birthdayGroup = JsonSerializer.Deserialize<List<BirthdayGroup>>(jsonResult);
                    if (birthdayGroup == null)
                    {
                        return new APIResponse
                        {
                            Status = false,
                            ResponseMessage = "No record found!",

                        };
                    }

                    if (birthdayGroup.Any())
                    {
                        return new APIResponse
                        {
                            Status = true,
                            ResponseMessage = "Comments fetched successfully",
                            Data = birthdayGroup
                        };
                    }
                    else
                    {
                        return new APIResponse
                        {
                            Status = false,
                            ResponseMessage = "No record found!",

                        };
                    }
                }
            }

            catch (Exception ex)
            {
                // Log general errors
                return new APIResponse
                {
                    Status = false,
                    ResponseMessage = "Some thing went wrong!",

                };
            }
        }
      
        public async Task<APIResponse> GetUpcomingHolidays(Common_Parameter model)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@CompanyId", model.CompanyId);
                    parameters.Add("@EmployeeId", model.UserId);

                    // Execute the stored procedure and get JSON result
                    var jsonResult = await connection.ExecuteScalarAsync<string>(
                        "SP_Mobile_GetUpcomingHolidays",
                        parameters,
                        commandType: CommandType.StoredProcedure);

                    // Deserialize the JSON result
                    var holidays = JsonSerializer.Deserialize<List<HolidaySummary>>(jsonResult);
                    if (holidays == null)
                    {
                        return new APIResponse
                        {
                            Status = false,
                            ResponseMessage = "No record found!",

                        };
                    }

                    if (holidays.Any())
                    {
                        return new APIResponse
                        {
                            Status = true,
                            ResponseMessage = "Comments fetched successfully",
                            Data = holidays
                        };
                    }
                    else
                    {
                        return new APIResponse
                        {
                            Status = false,
                            ResponseMessage = "No record found!",

                        };
                    }
                }
            }

            catch (Exception ex)
            {
                // Log general errors
                return new APIResponse
                {
                    Status = false,
                    ResponseMessage = "Some thing went wrong!",

                };
            }
        }
      
      

        public async Task<APIResponse> GetAttendanceCalender(Common_Parameter commonParameter)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var queryParameters = new DynamicParameters();
                    queryParameters.Add("@EmployeeId", commonParameter.UserId);
                    queryParameters.Add("@Year", commonParameter.Year);
                    queryParameters.Add("@Month", commonParameter.Month);

                    using (var multi = await connection.QueryMultipleAsync(
                        "sp_EmployeeMonthlyAttendanceCalendar",
                        queryParameters,
                        commandType: CommandType.StoredProcedure))
                    {
                        try
                        {
                            var AttedanceCalanderDays = (await multi.ReadAsync<vmAttedanceCalanderDays>()).AsList();
                            var AttedanceCalanderDaysSummary = (await multi.ReadAsync<vmAttedanceCalanderDaysSummary>()).AsList();
                            var newData = new
                            {
                                AttedanceCalanderDays = AttedanceCalanderDays,
                                AttedanceCalanderDaysSummary = AttedanceCalanderDaysSummary.FirstOrDefault()
                            };
                            return new APIResponse { Status = true, Data = newData, ResponseMessage = "Records fetched successfully." };

                            //return (AttedanceCalanderDays, AttedanceCalanderDaysSummary);
                        }
                        catch (Exception ex)
                        {
                            return new APIResponse { Status = false, ResponseMessage = "Unable to retrieve records. Please try again later." };

                        }
                    }


                }
            }
            catch (Exception ex)
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to retrieve records. Please try again later." };

            }
        }

        public async Task<List<vmGetMonthlyAttendanceDetails>> GetMonthlyAttendanceDetails(Common_Parameter parameter)
        {
            try
            {
                var result = await _db.Set<vmGetMonthlyAttendanceDetails>()
                                .FromSqlInterpolated($"EXEC SP_Mobile_GetMonthlyAttendanceDetails  @MonthNumber={parameter.Month}, @Year={parameter.Year},  @EmployeeId = {parameter.UserId},@CompanyId={parameter.CompanyId}")
                                .ToListAsync();
                return result;
            }
            catch (Exception)
            {
                return new List<vmGetMonthlyAttendanceDetails>();
            }
        }


      
        public async Task<APIResponse> GetAllAprovalApplication(Common_Parameter commonParameter)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var queryParameters = new DynamicParameters();
                    queryParameters.Add("@UserId", commonParameter.UserId);
                    queryParameters.Add("@Status", commonParameter.Status);

                    using (var multi = await connection.QueryMultipleAsync(
                        "SP_Mobile_GetAllAprovalApplication",
                        queryParameters,
                        commandType: CommandType.StoredProcedure))
                    {
                        var leaveApplications = (await multi.ReadAsync<LeaveApplicationViewModel>()).ToList();
                        var attendanceRegularizations = (await multi.ReadAsync<AttendanceRegularizationViewModel>()).ToList();

                        var responseData = new 
                        {
                            LeaveApplications = leaveApplications,
                            AttendanceRegularizations = attendanceRegularizations
                        };

                        return new APIResponse
                        {
                            Status = true,
                            Data = responseData,
                            ResponseMessage = "Records fetched successfully."
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return new APIResponse
                {
                    Status = false,
                    ResponseMessage = $"Error retrieving records: {ex.Message}"
                };
            }
        }

        public async Task<APIResponse> GetEmployeeDashboardCountDetails(int EmployeeId)
        {
            try
            {
                var result = await _db.Set<EmployeeDashboardCountModel>()
                    .FromSqlInterpolated($@"
                  EXEC SP_Mobile_GetEmployeeDashboardCountDetails
                    @EmployeeId = {EmployeeId}
              ")
                    .ToListAsync();

                if (!result.Any())
                {
                    return new APIResponse { Status = false, ResponseMessage = "No record found!" };
                }
                else
                {
                    return new APIResponse { Status = true, Data = result, ResponseMessage = "Record fetched successfully!" };

                }
            }
            catch (Exception ex)
            {

                return new APIResponse { Status = false, ResponseMessage = "Some thing went wrong!" };
            }
        }

        public async Task<APIResponse> GetEmployeeDashboardDetails(Common_Parameter model)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var parameters = new DynamicParameters();
                    parameters.Add("@CompanyId", model.CompanyId);
                    parameters.Add("@EmployeeId", model.UserId);

                    // Execute the stored procedure and get JSON result
                    var jsonResult = await connection.ExecuteScalarAsync<string>(
                        "SP_Mobile_GetEmployeeDashboardDetails",
                        parameters,
                        commandType: CommandType.StoredProcedure);

                    if (string.IsNullOrEmpty(jsonResult))
                    {
                        return new APIResponse
                        {
                            Status = false,
                            ResponseMessage = "No record found!"
                        };
                    }

                    // Parse the JSON manually and create the object
                    var dashboardData = new EmployeeDashboardViewModel();

                    try
                    {
                        var jsonObject = JObject.Parse(jsonResult);

                        // Deserialize Holidays
                        if (jsonObject["Holidays"] != null)
                        {
                            dashboardData.Holidays = jsonObject["Holidays"].ToObject<List<HolidayViewModel>>();
                        }

                        // Deserialize Birthdays
                        if (jsonObject["Birthdays"] != null)
                        {
                            dashboardData.Birthdays = jsonObject["Birthdays"].ToObject<List<BirthdayViewModel>>();
                        }

                        // Deserialize DashboardCounts
                        if (jsonObject["DashboardCounts"] != null)
                        {
                            dashboardData.DashboardCounts = jsonObject["DashboardCounts"].ToObject<DashboardCountsViewModel>();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"JSON parsing error: {ex.Message}");
                        return new APIResponse
                        {
                            Status = false,
                            ResponseMessage = "Error parsing JSON data"
                        };
                    }

                    return new APIResponse
                    {
                        Status = true,
                        ResponseMessage = "Records fetched successfully",
                        Data = dashboardData
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new APIResponse
                {
                    Status = false,
                    ResponseMessage = "Something went wrong!"
                };
            }
        }

        public async Task<APIResponse> GetLastCheckInDetails(Common_Parameter model)
        {
            try
            {
                var result = await _db.Set<LastCheckInDetailsVM>().FromSqlInterpolated($@"
                 EXEC SP_Mobile_GetLastCheckInDetails
                    @UserId = {model.UserId}
            ").ToListAsync();

                var data = result.FirstOrDefault();
                if (data!=null)
                {
                    return new APIResponse { Status = true, Data = data, ResponseMessage = "Record fetch sucessfully!" };
                }
                else
                {
                    return new APIResponse { Status = false, ResponseMessage = "No record found" };
                }

            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Something went wrong" };
            }
        }


        public async Task<APIResponse> CalculateMonthlySalary(MonthlySalaryRequestViewModel model)
        {
            var response = new APIResponse();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@StartDate", model.StartDate);
                    parameters.Add("@EndDate", model.EndDate);
                    parameters.Add("@CompanyId", model.CompanyId);
                    parameters.Add("@EmployeeCodes", model.EmployeeCodes);
                    parameters.Add("@BranchIds", model.BranchIds);


                    var result = await connection.QueryAsync<dynamic>(
                        "USP_Mobile_CalculateMonthlySalary",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    if (result == null||!result.Any())
                    {
                        response.Status = false;
                        response.ResponseMessage = "No records found.";
                        return response;
                    }

                    response.Status = true;
                    response.ResponseMessage = "Success!";
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
