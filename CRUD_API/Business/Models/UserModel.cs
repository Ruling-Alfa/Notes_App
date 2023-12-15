namespace CRUD_API.Business.Models
{
    public record UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        //public string UserPasswordHash { get; set; }
        //public string HashSalt { get; set; }
    }
}
