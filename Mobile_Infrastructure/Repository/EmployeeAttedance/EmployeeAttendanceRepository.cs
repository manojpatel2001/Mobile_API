using Microsoft.EntityFrameworkCore;
using Mobile_Core.CommonClass;
using Mobile_Core.DB;
using Mobile_Core.EmployeeAttedance;
using Mobile_Infrastructure.Interface.EmployeeAttedance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Infrastructure.Repository.EmployeeAttedance
{
    public class EmployeeAttendanceRepository : IEmployeeAttendanceRepository
    {
        private readonly MobileDbcontext _db;

        public EmployeeAttendanceRepository(MobileDbcontext db)
        {
            _db = db;
        }

        public async Task<SP_Response> InsertAttendance(EmployeeAttendanceVM attendance)
        {
            try
            {
                var result = await _db.Set<SP_Response>().FromSqlInterpolated($@"
                    EXEC USP_Mobile_EmployeeAttendance
                        @Status = {"Insert"},
                        @CompanyId = {attendance.CompanyId},
                        @EmployeeId = {attendance.EmployeeId},
                        @Lat = {attendance.Lat},
                        @Long = {attendance.Long},
                        @LocationName = {attendance.LocationName},
                        @PunchTypeId = {attendance.PunchTypeId},
                        @DocumentName = {attendance.DocumentName},
                        @FileSize = {attendance.FileSize},
                        @DocumentPath = {attendance.DocumentPath}
                ").ToListAsync();

                return result.FirstOrDefault() ?? new SP_Response { Success = false, Message = "Something went wrong!" };
            }
            catch
            {
                return new SP_Response { Success = false, Message = "Something went wrong!" };
            }
        }

        public async Task<EmployeeAttendanceStatusVM> GetEmployeeCurrentStatus(int employeeId)
        {
            try
            {
                var result = await _db.Set<EmployeeAttendanceStatusVM>().FromSqlInterpolated($@"
                    EXEC USP_Mobile_EmployeeAttendance
                        @Status = {"GetEmployeeCurrentStatus"},
                        @EmployeeId = {employeeId}
                ").ToListAsync();

                return result.FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AutoListVM>> GetAutoList()
        {
            try
            {
                return await _db.Set<AutoListVM>().FromSqlInterpolated($@"
                    EXEC USP_Mobile_EmployeeAttendance
                        @Status = {"GetAutoList"}
                ").ToListAsync();
            }
            catch
            {
                return new List<AutoListVM>();
            }
        }

        public async Task<SP_Response> InsertLiveLocation(MobileUserLiveLocation location)
        {
            try
            {
                var result = await _db.Set<SP_Response>().FromSqlInterpolated($@"
                    EXEC USP_Mobile_EmployeeAttendance
                        @Status = {"InsertLiveLocation"},
                        @EmployeeId = {location.UserId},
                        @LocationDatetime = {location.LocationDatetime},
                        @Lat = {location.Lat},
                        @Long = {location.Long},
                        @DeviceName = {location.DeviceName}
                ").ToListAsync();

                return result.FirstOrDefault() ?? new SP_Response { Success = false, Message = "Something went wrong!" };
            }
            catch
            {
                return new SP_Response { Success = false, Message = "Something went wrong!" };
            }
        }
    }
}
