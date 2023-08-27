using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JALUR.Entity.Pages
{
    public class WorkoutEntity
    {
        public int Id { get; set; }
        public int TypeWorkoutId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public WorkoutEntity(int Id, int TypeWorkoutId, string Name, string Description, string Image) {
            this.Id = Id;
            this.TypeWorkoutId = TypeWorkoutId;
            this.Name = Name;
            this.Description = Description;
            this.Image = Image;
        }
    }
}
