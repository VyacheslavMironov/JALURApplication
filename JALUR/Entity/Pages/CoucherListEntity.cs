namespace JALUR.Entity.Pages
{
    public class CoucherListEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }

        public CoucherListEntity(int Id, string FirstName, string LastName, string Image)
        { 
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Image = Image;
        }
    }
}
