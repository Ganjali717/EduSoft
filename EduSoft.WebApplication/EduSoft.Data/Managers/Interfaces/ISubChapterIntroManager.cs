using EduSoft.Entities.Tutorials;
using EduSoft.Entities;

namespace EduSoft.Data.Managers.Interfaces;

public interface ISubChapterIntroManager
{
    Task<ManagerResult<List<SubChapterIntro>>> GetAll();
    Task<ManagerResult<SubChapterIntro>> Get(Guid id);
    Task<ManagerResult<SubChapterIntro>> CreateOrUpdate(SubChapterIntro subchapter);
    Task<ManagerResult> Remove(Guid id);
}