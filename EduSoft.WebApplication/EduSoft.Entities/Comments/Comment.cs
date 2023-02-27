using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSoft.Entities.Discussions;
using EduSoft.Entities.Security;

namespace EduSoft.Entities.Comments
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public AppUser AppUser { get; set; }
        [ForeignKey("AppUser")]
        public Guid AppUserId { get; set; }
        public Discussion Discussion { get; set; }
        [ForeignKey("Discussion")]
        public Guid DiscussionId { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
    }
}
