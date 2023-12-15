using AutoMapper;
using CrossCutting.Security.Interfaces;
using CRUD_API.Business.Interfaces;
using CRUD_API.Business.Models;
using CRUD_API.Data;
using CRUD_API.Data.Entities;

namespace CRUD_API.Business
{
    public class UserService : IUserService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        private readonly IHasher _hasher;
        private readonly IMapper _mapper;
        public UserService(IApplicationUnitOfWork unitOfWork, IMapper mapper, IHasher hasher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hasher = hasher;
        }

        public async Task<UserModel> VerifyUserLogin(UserLoginModel loginModel)
        {
            var existingUser = await _unitOfWork.GetRepository<User>().GetOneByQuery(u =>
                            u.UserName == loginModel.UserName);
            if (existingUser is not null)
            {
                var isPasswordValid = _hasher.VerifyHash(loginModel.UserPassword,
                                    existingUser.UserPasswordHash, existingUser.HashSalt);
                if (isPasswordValid)
                {
                    return _mapper.Map<UserModel>(existingUser);
                }
            }
            return default;
        }

        public async Task<UserRegisterResponseModel?> CreateUser(UserRegisterModel registerModel)
        {
            var existingUser = await _unitOfWork.GetRepository<User>().GetOneByQuery(u =>
                            u.UserName == registerModel.UserName ||
                            u.UserEmail == registerModel.UserEmail);
            if (existingUser is null)
            {

                var newUser = _mapper.Map<User>(registerModel);

                SetUserCredentials(registerModel, newUser);

                var userEntity = await _unitOfWork.GetRepository<User>().Insert(newUser);
                await _unitOfWork.Save();

                newUser = userEntity.Entity;
                return _mapper.Map<UserRegisterResponseModel>(newUser);
            }
            return default;
        }

        private void SetUserCredentials(UserRegisterModel registerModel, User newUser)
        {
            var hashCreds = _hasher.Hash(registerModel.UserPassword);
            newUser.HashSalt = hashCreds.Salt;
            newUser.UserPasswordHash = hashCreds.Hash;
        }

    }
}
