using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Mobile_Core.CommonClass;
using Mobile_Core.DB;
using Mobile_Core.ViewModel;
using Mobile_Core.ViewModel.EmployeeReport;
using Mobile_Infrastructure.Interface.EmployeeManage;
using Mobile_Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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

        public async Task<List<vmGetSalarySalaryDetails>> GetSalarySalaryDetails(SalarysDetailsParameter vm)
        {
            try
            {

                var result = await _db.Set<vmGetSalarySalaryDetails>().FromSqlInterpolated($"EXEC SP_Mobile_GetSalarySalaryDetails @EmployeeId={vm.EmployeeId},@Month={vm.Month},@Year={vm.Year}").ToListAsync();
                return result;
            }
            catch
            {
                return new List<vmGetSalarySalaryDetails>();
            }
        }

        public async Task<List<vmGetTodayBirthdaysByCompany>> GetTodayBirthdaysByCompany(int CompanyId )
        {
            try
            {

                var result = await _db.Set<vmGetTodayBirthdaysByCompany>().FromSqlInterpolated($"EXEC sp_Mobile_GetTodayBirthdaysByCompany @CompanyId={CompanyId}").ToListAsync();
                return result;
            }
            catch
            {
                return new List<vmGetTodayBirthdaysByCompany>();
            }
        }
        public async Task<List<vmGetUpcomingHolidays>> GetUpcomingHolidays(Common_Parameter parameter )
        {
            try
            {

                var result = await _db.Set<vmGetUpcomingHolidays>().FromSqlInterpolated($"EXEC SP_Mobile_GetUpcomingHolidays @CompanyId={parameter.CompanyId},@EmployeeId={parameter.UserId}").ToListAsync();
                return result;
            }
            catch
            {
                return new List<vmGetUpcomingHolidays>();
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

    }
}
