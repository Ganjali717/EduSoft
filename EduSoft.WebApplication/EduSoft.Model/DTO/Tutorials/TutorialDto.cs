using EduSoft.Entities.Tutorials;

namespace EduSoft.Model.DTO.Tutorials;

public class TutorialDto
{
    public Guid? Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public Guid? Tutorial
    {
        get
        {
            if(Guid.TryParse(Title, out var tutorialId)) return tutorialId;
            return null;
        }
    }
    public string TutorialId { get; set; }
    public string Created { get; set; }
    public ICollection<Chapter> Chapters { get; set; }
}