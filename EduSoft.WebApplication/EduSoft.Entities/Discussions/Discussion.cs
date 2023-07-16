using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EduSoft.Entities.Security;

namespace EduSoft.Entities.Discussions
{
    public class Discussion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public AppUser AppUser { get; set; }
        [ForeignKey("AppUser")]
        public Guid AppUserId { get; set; }
        public DateTime Created { get; set; }
    }
}
