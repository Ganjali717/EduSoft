using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduSoft.Entities.Tutorials
{
    public class IntroImages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public SubChapterIntro SubChapterIntro { get; set; }
        [ForeignKey("SubChapterIntro")]
        public Guid SubChapterIntroId { get; set; }
    }
}
