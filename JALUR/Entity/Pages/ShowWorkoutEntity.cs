using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JALUR.Entity.Pages
{
    public class ShowWorkoutEntity
    {
        public int Id { get; set; }
        public int TypeWorkoutId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }
}
