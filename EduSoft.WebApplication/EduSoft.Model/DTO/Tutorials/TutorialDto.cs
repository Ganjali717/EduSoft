using EduSoft.Entities.Tutorials;

namespace EduSoft.Model.DTO.Tutorials;

public class TutorialDto
{
    public Guid? Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public Guid? Category
    {
        get
        {
            if(Guid.TryParse(CategoryId, out var categoryId)) return categoryId;
            return null;
        }
    }
    public string CategoryId { get; set; }
    public string Created { get; set; }
}