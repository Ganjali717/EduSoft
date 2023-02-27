using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EduSoft.Entities.Tutorials
{
    public class SubChapterIntro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }  
        public string Content { get; set; }
        public Subchapter SubChapter { get; set; }
        [ForeignKey("SubChapter")] 
        public Guid SubChapterId { get; set; }
        public DateTime Created { get; set; }
        public ICollection<IntroImages> Images { get; set; }
    }
}
