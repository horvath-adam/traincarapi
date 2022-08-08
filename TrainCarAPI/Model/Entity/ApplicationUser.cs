using Microsoft.AspNetCore.Identity;

namespace TrainCarAPI.Model.Entity
{
    /// <summary>
    /// ApplicationUser extends the IdentityUser with the required fields (related to task 10)
    /// IdentityUser<int> generate id from 1 to the user, otherwise id will be a guid
    /// </summary>
    public class ApplicationUser: IdentityUser<int>
    {
        public DateTime DateOfBirth { get; set; }
        public bool IsRailwayWorker { get; set; }
        public string RailwayCompanyName { get; set; }
    }
}
