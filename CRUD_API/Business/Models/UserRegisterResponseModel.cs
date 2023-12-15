namespace CRUD_API.Business.Models
{
    public record UserRegisterResponseModel : UserRegisterModel
    {
        public int Id { get; set; }
    }
}
