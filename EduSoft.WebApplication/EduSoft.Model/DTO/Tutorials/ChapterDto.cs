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
    }
}
