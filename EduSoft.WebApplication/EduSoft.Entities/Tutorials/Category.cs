using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduSoft.Entities.Tutorials
{
    public class Category
    {
       [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
       public Guid Id { get; set; }
       public string Name { get; set; }
       
    }
}
