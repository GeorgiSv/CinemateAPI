namespace CinemateAPI.Features.Identity
{
    using CinemateAPI.Data.Models;

    public interface IIdentityService
    {
        public string GenerateJwtToken(User user);
    }
}
