using System.ComponentModel.DataAnnotations.Schema;

namespace APIConnection.Model
{
    [Table("Department")]
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
