using EduSoft.Entities;
using EduSoft.Entities.Tutorials;

namespace EduSoft.Data.Managers.Interfaces;

public interface ISubChapterManager
{
    Task<ManagerResult<List<Subchapter>>> GetAllSubChapterAsync();
    Task<ManagerResult<Subchapter>> GetSubChapterbyIdAsync(int id);
    Task<ManagerResult<Subchapter>> CreateOrUpdateSubChapter(Subchapter subchapter);
    Task<ManagerResult> DeleteSubChapterAsync(int id);
}