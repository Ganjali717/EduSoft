using EduSoft.Entities.Tutorials;
using EduSoft.Entities;

namespace EduSoft.Data.Managers.Interfaces;

public interface ISubChapterIntroManager
{
    Task<ManagerResult<List<SubChapterIntro>>> GetAllSubChapterIntrorAsync();
    Task<ManagerResult<SubChapterIntro>> GetSubChapterIntrobyIdAsync(Guid id);
    Task<ManagerResult<SubChapterIntro>> CreateOrUpdateSubChapterIntro(SubChapterIntro subchapter);
    Task<ManagerResult> DeleteSubChapterIntroAsync(Guid id);
}