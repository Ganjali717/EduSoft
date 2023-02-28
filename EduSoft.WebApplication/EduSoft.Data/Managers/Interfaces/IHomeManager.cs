using EduSoft.Entities;
using EduSoft.Entities.Tutorials;

namespace EduSoft.Data.Managers.Interfaces;

public interface IHomeManager
{
    public ManagerResult<Tutorial> GetTutorial();
}