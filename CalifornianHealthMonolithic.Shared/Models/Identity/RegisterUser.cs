namespace CalifornianHealthMonolithic.Shared.Models.Identity
{
    public class RegisterUser
    {
        public int? Id { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string FName { get; set; }
        public required string LName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
    }
}