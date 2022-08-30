using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TrainCarAPI.Model.Entity
{
    /// <summary>
    /// ApplicationUser extends the IdentityUser with the required fields (related to task 10)
    /// IdentityUser<int> generate id from 1 to the user, otherwise id will be a guid
    /// </summary>
    public class ApplicationUser: IdentityUser<int>
    {
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public bool IsRailwayWorker { get; set; }

        [MaxLength(100)]
        public string RailwayCompanyName { get; set; }
    }
}
