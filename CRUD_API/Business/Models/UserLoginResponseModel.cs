namespace CRUD_API.Business.Models
{
    public record UserLoginResponseModel : UserModel
    {
        public UserLoginResponseModel(UserModel baseModel) : base(baseModel) { }
        public string Token { get; set; }
    }
}
