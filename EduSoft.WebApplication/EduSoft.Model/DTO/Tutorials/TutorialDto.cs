using EduSoft.Entities.Tutorials;

namespace EduSoft.Model.DTO.Tutorials;

public class TutorialDto
{
    public Guid? Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public Guid? Chapter
    {
        get
        {
            if(Guid.TryParse(Title, out var chapterId)) return chapterId;
            return null;
        }
    }
    public string ChapterId { get; set; }
    public string Created { get; set; }
    public ICollection<Chapter> Chapters { get; set; }
}