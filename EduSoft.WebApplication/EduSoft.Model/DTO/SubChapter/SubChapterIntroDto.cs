namespace EduSoft.Model.DTO.SubChapter
{
    public class SubChapterIntroDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public Guid? SubChapter
        {
            get
            {
                if (Guid.TryParse(SubChapterId, out var subChapterId)) return subChapterId;
                return null;
            }
        }
        public string SubChapterId { get; set; }
        public string Created { get; set; }
    }
}
