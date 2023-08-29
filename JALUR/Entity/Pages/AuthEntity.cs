namespace JALUR.Entity.Pages
{
    public class AuthEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string AccessKey { get; set; }

        public AuthEntity(int Id, string FirstName, string LastName, string Phone, string Role, string AccessKey) { 
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Phone = Phone;
            this.Role = Role;
            this.AccessKey = AccessKey;
        }
    }
}
