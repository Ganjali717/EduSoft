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
    }
}
