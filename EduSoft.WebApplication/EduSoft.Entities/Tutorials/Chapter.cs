using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduSoft.Entities.Tutorials
{
    public class Chapter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Tutorial? Tutorial { get; set; }
        [ForeignKey("Tutorial")] 
        public Guid TutorialId { get; set; }
        public DateTime Created { get; set; }
    }
}
