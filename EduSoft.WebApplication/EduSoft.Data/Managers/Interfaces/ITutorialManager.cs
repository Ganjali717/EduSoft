﻿using EduSoft.Entities.Tutorials;
using EduSoft.Entities;

namespace EduSoft.Data.Managers.Interfaces;

public interface ITutorialManager
{
    public Task<ManagerResult<List<Tutorial>>> GetAllTutorials();
    public Task<ManagerResult<Tutorial>> GetTutorial(Guid id);
    public Task<ManagerResult<Tutorial>> CreateOrUpdateTutorial(Tutorial tutorial);
    public Task<ManagerResult> DeleteTutorial(Guid id);
}