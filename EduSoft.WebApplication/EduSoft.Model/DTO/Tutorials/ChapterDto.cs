using EduSoft.Entities.Tutorials;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduSoft.Model.DTO.Tutorials
{
    public class ChapterDto
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public Guid? Tutorial
        {
            get
            {
                if (Guid.TryParse(TutorialId, out var tutorialId)) return tutorialId;
                return null;
            }
        }
        public string TutorialId { get; set; }
        public DateTime Created { get; set; }
        public ICollection<Subchapter> Subchapters { get; set; }
    }
}
