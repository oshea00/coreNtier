using System.ComponentModel.DataAnnotations;

namespace Todo.Data.Models {
    public class Todos {
        [Key]
        public int ID { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
    }
}