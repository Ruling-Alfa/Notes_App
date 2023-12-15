using CRUD_API.Business.Models;

namespace CRUD_API.Business.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> VerifyUserLogin(UserLoginModel loginModel);
        Task<UserRegisterResponseModel?> CreateUser(UserRegisterModel registerModel);
    }
}