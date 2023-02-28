using EduSoft.Entities;
using EduSoft.Entities.Tutorials;

namespace EduSoft.Data.Managers.Interfaces;

public interface ISubChapterManager
{
    public Task<ManagerResult<List<Subchapter>>> GetAllSubChapterAsync();
    public Task<ManagerResult<Subchapter>> GetSubChapterbyIdAsync(int id);
    public Task<ManagerResult<Subchapter>> CreateOrUpdateSubChapter(Subchapter subchapter);
    public Task<ManagerResult> DeleteSubChapterAsync(int id);
}