using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduSoft.Entities.Tutorials
{
    public class Subchapter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Chapter Chapter { get; set; }
        [ForeignKey("Chapter")] 
        public Guid ChapterId { get; set; }
        public DateTime Created { get; set; }
    }
}
