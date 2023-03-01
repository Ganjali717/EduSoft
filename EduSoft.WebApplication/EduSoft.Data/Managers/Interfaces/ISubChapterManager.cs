using EduSoft.Entities;
using EduSoft.Entities.Tutorials;

namespace EduSoft.Data.Managers.Interfaces;

public interface ISubChapterManager
{
    Task<ManagerResult<List<Subchapter>>> GetAllSubChapterAsync();
    Task<ManagerResult<Subchapter>> GetSubChapterbyIdAsync(Guid id);
    Task<ManagerResult<Subchapter>> CreateOrUpdateSubChapter(Subchapter subchapter);
    Task<ManagerResult> DeleteSubChapterAsync(Guid id);
}