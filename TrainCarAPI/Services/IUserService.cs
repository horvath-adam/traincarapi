namespace TrainCarAPI.Services
{
    public interface IUserService
    {
        public Task InitRoles();
        public Task InitUsers();
    }
}
