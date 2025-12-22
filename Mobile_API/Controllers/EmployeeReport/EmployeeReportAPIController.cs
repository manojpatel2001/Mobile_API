using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobile_API.Services;
using Mobile_Core.CommonClass;
using Mobile_Core.EmployeeAttedance;
using Mobile_Core.ViewModel;
using Mobile_Core.ViewModel.EmployeeReport;
using Mobile_Infrastructure.Interface;
using Mobile_Utility;

namespace Mobile_API.Controllers.EmployeeReport
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeReportAPIController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeReportAPIController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetSalarySalaryDetails")]
        public async Task<APIResponse> GetSalarySalaryDetails(SalarysDetailsParameter vm)
        {
            try
            {
                vmGetSalarySalaryDetails? model = await _unitOfWork.EmployeeReportRepository.GetSalarySalaryDetails(vm);

                if (model == null)
                {
                    return new APIResponse { Status = false, ResponseMessage = "Salary details can not details!" };
                }

                var logoPhysicalPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "default-image",
                    "relaylogo.jpeg"
                );

                var logoUrl = new Uri(logoPhysicalPath).AbsoluteUri;

                var placeholders = new Dictionary<string, string>
                {
                    { "CompanyLogoUrl", logoUrl },
                    { "CompanayName", model.CompanyName },
                    { "CompanyAddress", model.CompanyAddress },
                    { "MonthName", model.MonthName ?? "N/A" },
                    { "Year", model.Year.ToString() },

                    { "EmployeeCode", model.EmployeeCode ?? "N/A" },
                    { "FullName", model.FullName ?? "N/A" },
                    { "BranchName", model.BranchName ?? "N/A" },
                    { "DepartmentName", model.DepartmentName ?? "N/A" },
                    { "DesignationName", model.DesignationName ?? "N/A" },
                    { "GradeName", model.GradeName ?? "N/A" },

                    { "DateOfBirth", model.DateOfBirth.HasValue
                        ? model.DateOfBirth.Value.ToString("dd/MM/yyyy")
                        : "N/A"
                    },
                    { "DateOfJoining", model.DateOfJoining.HasValue
                        ? model.DateOfJoining.Value.ToString("dd/MM/yyyy")
                        : "N/A"
                    },

                    { "PFNo", model.PFNo ?? "N/A" },
                    { "PrimaryAccountNumber", model.PrimaryAccountNumber ?? "N/A" },
                    { "PrimaryBankName", model.PrimaryBankName ?? "N/A" },
                    { "PANNo", model.PANNo ?? "N/A" },
                    { "UANNumber", model.UANNumber ?? "N/A" },
                    { "ESICNo", model.ESICNo ?? "N/A" },

                    { "PresentDays", model.PresentDays.ToString() },
                    { "PaidLeave", model.Leave.ToString() },
                    { "Holiday", model.Holiday.ToString() },
                    { "WeekOff", model.WeekOff.ToString() },
                    { "TotalSalaryDays", model.SalaryDays.ToString() },
                    { "Absent", model.AbsentDays.ToString() },

                    { "BasicSalary", FormatCurrency(model.BasicSalary) },
                    { "HRA", FormatCurrency(model.HRA) },
                    { "ConveyanceAllowance", FormatCurrency(model.ConveyanceAllowance) },
                    { "MedicalAllowance", FormatCurrency(model.MedicalAllowance) },
                    { "ChildEducationAllowance", FormatCurrency(model.ChildEducationAllowance) },
                    { "DeputationAllowance", FormatCurrency(model.DeputationAllowance) },

                    { "ProfessionalTax", FormatCurrency(model.ProfessionalTax) },
                    { "GroupMedical", FormatCurrency(model.GroupMedical) },
                    { "TermInsurance", FormatCurrency(model.TermInsurance) },

                    { "TotalEarnings", FormatCurrency(model.TotalGrossSalary) },
                    { "TotalDeductions", FormatCurrency(model.TotalDeductions) },

                    { "NetSalary", FormatCurrency(model.NetSalary) },
                    { "NetSalaryInWords", ConvertToWords(model.NetSalary) }
                };

                var html = TemplateHelper.ReadAndReplace("Templates/SalaryPdfTemplate.html", placeholders);

                var fileName = $"SalarySlip_{model.EmployeeCode}_{model.MonthName}_{model.Year}.pdf";
                string wkhtmlPath = Path.Combine(
                                 Directory.GetCurrentDirectory(),
                                 "Tools",
                                 "wkhtmltopdf",
                                 "wkhtmltopdf.exe"
                             );

                string pdfPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "uploads",
                    "salary_slips",
                    fileName
                );

                // Ensure directory exists
                var pdfDirectory = Path.GetDirectoryName(pdfPath);
                if (!Directory.Exists(pdfDirectory))
                {
                    Directory.CreateDirectory(pdfDirectory);
                }

                WkhtmltopdfService.GeneratePdf(wkhtmlPath, html, pdfPath);

                model.SalaryPdfUrl = $"/uploads/salary_slips/{fileName}";
                return new APIResponse { Data = model, ResponseMessage = "Fetched Successfully!", Status = true };
            }
            catch (Exception ex)
            {
                return new APIResponse { Status = false, ResponseMessage = "Salary details can not details!" };
            }
        }

        // Helper method for formatting currency
        private string FormatCurrency(decimal? amount)
        {
            if (amount == null || amount == 0)
                return "0.00";
            return amount.Value.ToString("N2");
        }

        // Helper method to convert number to words
        private string ConvertToWords(decimal? amount)
        {
            if (amount == null || amount == 0)
                return "Zero Only";

            return $"{amount.Value:N2} Only";
        }

        [HttpPost("GetTodayBirthdaysByCompany")]
        public async Task<APIResponse> GetTodayBirthdaysByCompany(Common_Parameter model)
        {
            try
            {
                var data = await _unitOfWork.EmployeeReportRepository.GetTodayBirthdaysByCompany(model);
                
                return data;
            }
            catch (Exception err)
            {
                return new APIResponse
                {
                    Status = false,
                    Data = err.Message,
                    ResponseMessage = "Unable to retrieve records, Please try again later!"
                };
            }
        }

        [HttpPost("GetUpcomingHolidays")]
        public async Task<APIResponse> GetUpcomingHolidays(Common_Parameter parameter)
        {
            try
            {
                var data = await _unitOfWork.EmployeeReportRepository.GetUpcomingHolidays(parameter);

                return data;
            }
            catch (Exception err)
            {
                return new APIResponse
                {
                    Status = false,
                    Data = err.Message,
                    ResponseMessage = "Unable to retrieve records, Please try again later!"
                };
            }
        }


        [HttpPost("GetAttendanceCalender")]
        public async Task<APIResponse> GetAttendanceCalender(Common_Parameter commonParameter)
        {
            try
            {
                var data = await _unitOfWork.EmployeeReportRepository.GetAttendanceCalender(commonParameter);

                return data;
            }
            catch (Exception ex)
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to retrieve records. Please try again later." };
            }
        }
        [HttpPost("GetMonthlyAttendanceDetails")]
        public async Task<APIResponse> GetMonthlyAttendanceDetails(Common_Parameter parameter)
        {
            try
            {
                var data = await _unitOfWork.EmployeeReportRepository.GetMonthlyAttendanceDetails(parameter);
                if (data == null || !data.Any())
                    return new APIResponse { Status = false, ResponseMessage = "No records found." };

                return new APIResponse { Status = true, Data = data, ResponseMessage = "Records fetched successfully." };
            }
            catch (Exception ex)
            {
                return new APIResponse { Status = false, Data = ex.Message, ResponseMessage = "Unable to retrieve records. Please try again later." };
            }
        }

      
        [HttpPost("GetAllAprovalApplication")]
        public async Task<APIResponse> GetAllAprovalApplication(Common_Parameter model)
        {
            try
            {
                var data = await _unitOfWork.EmployeeReportRepository.GetAllAprovalApplication(model);

                return data;
            }
            catch (Exception ex)
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to retrieve records. Please try again later." };
            }
        }

        [HttpGet("GetEmployeeDashboardCountDetails/{EmployeeId}")]
        public async Task<APIResponse> GetEmployeeCurrentStatus(int EmployeeId)
        {
            try
            {
                var data = await _unitOfWork.EmployeeReportRepository.GetEmployeeDashboardCountDetails(EmployeeId);

                return data;
            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to retrieve status. Please try again later." };
            }
        }
        [HttpPost("GetEmployeeDashboardDetails")]
        public async Task<APIResponse> GetEmployeeDashboardDetails(Common_Parameter model)
        {
            try
            {
                var data = await _unitOfWork.EmployeeReportRepository.GetEmployeeDashboardDetails(model);

                return data;
            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to retrieve status. Please try again later." };
            }
        }
        [HttpPost("GetLastCheckInDetails")]
        public async Task<APIResponse> GetLastCheckInDetails(Common_Parameter model)
        {
            try
            {
                var data = await _unitOfWork.EmployeeReportRepository.GetLastCheckInDetails(model);

                return data;
            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to retrieve punch details. Please try again later." };
            }
        }
        [HttpPost("CalculateMonthlySalary")]
        public async Task<APIResponse> CalculateMonthlySalary(MonthlySalaryRequestViewModel model)
        {
            try
            {
                var data = await _unitOfWork.EmployeeReportRepository.CalculateMonthlySalary(model);

                return data;
            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to retrieve  details. Please try again later." };
            }
        }

       
    }
}
