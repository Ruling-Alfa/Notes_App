namespace CRUD_API.Business.Models
{
    public record UserRegisterModel
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
    }
}
