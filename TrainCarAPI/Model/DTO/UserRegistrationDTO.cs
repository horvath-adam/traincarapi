namespace TrainCarAPI.Model.DTO
{
    public class UserRegistrationDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsRailwayWorker { get; set; }
        public string? RailwayCompanyName  { get; set; }
    }
}
