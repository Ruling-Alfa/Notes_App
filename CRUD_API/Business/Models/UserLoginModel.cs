namespace CRUD_API.Business.Models
{
    public record UserLoginModel
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }
}
