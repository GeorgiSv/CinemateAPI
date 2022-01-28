namespace CinemateAPI.Features.User
{
    public interface ICurrentUserService
    {
        public string GetUesername();

        public string GetId();
    }
}