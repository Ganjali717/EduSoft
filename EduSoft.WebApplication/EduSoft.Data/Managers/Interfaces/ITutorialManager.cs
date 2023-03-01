using EduSoft.Entities.Tutorials;
using EduSoft.Entities;

namespace EduSoft.Data.Managers.Interfaces;

public interface ITutorialManager
{
    Task<ManagerResult<List<Tutorial>>> GetAllTutorials();
    Task<ManagerResult<Tutorial>> GetTutorial(Guid id);
    Task<ManagerResult<Tutorial>> CreateOrUpdateTutorial(Tutorial tutorial);
    Task<ManagerResult> DeleteTutorial(Guid id);

}