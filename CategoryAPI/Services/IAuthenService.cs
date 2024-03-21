using CategoryAPI.Model;

namespace CategoryAPI.Services
{
    public interface IAuthenService
    {
        public LoginResponseModel Authenticate(LoginModel loginModel);
    }
}
