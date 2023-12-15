namespace CrossCutting.Security.Models
{
    public class Credential
    {
        public string Hash { get; set; }
        public string Salt { get; set; }
    }
}
