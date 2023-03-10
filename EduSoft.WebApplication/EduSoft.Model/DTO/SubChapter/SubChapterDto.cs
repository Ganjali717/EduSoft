using EduSoft.Entities.Tutorials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduSoft.Model.DTO.SubChapter
{
    public class SubChapterDto
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid? Chapter
        {
            get
            {
                if (Guid.TryParse(ChapterId, out var chapterId)) return chapterId;
                return null;
            }
        }
        public string ChapterId { get; set; }
        public string Created { get; set; }
        public ICollection<SubChapterIntro> SubchapterIntro { get; set; }
    }
}
