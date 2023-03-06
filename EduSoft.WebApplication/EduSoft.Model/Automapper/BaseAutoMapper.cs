using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EduSoft.Entities;
using EduSoft.Entities.Jobs;
using EduSoft.Entities.Tutorials;
using EduSoft.Model.DTO.Jobs;
using EduSoft.Model.DTO.Tutorials;

namespace EduSoft.Model.Automapper
{
    public class BaseAutoMapper:Profile
    {
        public BaseAutoMapper()
        {
            #region TutorialDTO
            CreateMap<TutorialDto, Tutorial>();
            CreateMap<Tutorial, TutorialDto>();
            #endregion

            #region AccountDTO


            #endregion

            #region Jobs
            CreateMap<JobDTO, Job>();
            CreateMap<Job, JobDTO>();
            #endregion

            #region Chapter
            CreateMap<Chapter, ChapterDto>();
            CreateMap<ChapterDto, Chapter>();
            #endregion

            #region ManagerResultRegion
            CreateMap(typeof(ManagerResult<>), typeof(ManagerResult<>));
            CreateMap(typeof(PagedManagerResult<>), typeof(PagedManagerResult<>));
            #endregion
        }
    }
}
