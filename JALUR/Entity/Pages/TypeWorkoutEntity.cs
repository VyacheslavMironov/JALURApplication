namespace JALUR.Entity.Pages
{
    public class TypeWorkoutEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TypeWorkoutEntity(int Id, string Name) { 
            this.Id = Id;
            this.Name = Name;
        }
    }
}
