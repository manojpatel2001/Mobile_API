using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mobile_Core.AuthManage
{
    // Model for individual geo location
    public class GeoLocationItem
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Range { get; set; }
        public string LocationName { get; set; }
    }
    public class GetLoginData
    {
        public int? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
        public int? DesignationId { get; set; }
        public string? UserName { get; set; }
        public string? MobileNo { get; set; }
        public int? BranchId { get; set; }
        public int? CompanyId { get; set; }
        public string? BranchName { get; set; }
        public Boolean? IsReset { get; set; }
        public int? IsGeofencing { get; set; }
        public int? IsSelfiRequired { get; set; }
        public string? Designation { get; set; }

        // Store JSON string from database
        public string? GeoLocationData { get; set; }

        // Parsed list property - computed property that deserializes the JSON
        public List<GeoLocationItem> GeoLocationList
        {
            get
            {
                if (string.IsNullOrEmpty(GeoLocationData))
                    return new List<GeoLocationItem>();

                try
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    return JsonSerializer.Deserialize<List<GeoLocationItem>>(GeoLocationData, options) ?? new List<GeoLocationItem>();
                }
                catch
                {
                    return new List<GeoLocationItem>();
                }
            }
        }

        public string? EmployeeProfile { get; set; }
    }

}
