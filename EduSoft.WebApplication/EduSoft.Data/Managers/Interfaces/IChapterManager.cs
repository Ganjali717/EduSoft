using EduSoft.Entities.Tutorials;
using EduSoft.Entities;

namespace EduSoft.Data.Managers.Interfaces;

public interface IChapterManager
{
    Task<ManagerResult<List<Chapter>>> GetAllChapters();
    Task<ManagerResult<Chapter>> GetChapter(Guid id);
    Task<ManagerResult<Chapter>> CreateOrUpdateChapter(Chapter chapter);
    Task<ManagerResult> DeleteChapter(Guid id);
}