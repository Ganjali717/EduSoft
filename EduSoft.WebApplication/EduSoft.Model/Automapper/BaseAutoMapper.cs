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
using EduSoft.Model.DTO.SubChapter;
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

            #region JobsDTO
            CreateMap<JobDTO, Job>();
            CreateMap<Job, JobDTO>();
            #endregion

            #region ChapterDTO
            CreateMap<Chapter, ChapterDto>();
            CreateMap<ChapterDto, Chapter>();
            #endregion

            #region SubChapterDTO
            CreateMap<Subchapter,SubChapterDto>();
            CreateMap<SubChapterDto, Subchapter>();
            CreateMap<SubChapterIntro, SubChapterIntroDto>();
            CreateMap<SubChapterIntroDto, SubChapterIntro>();
            #endregion

            #region ManagerResultRegion
            CreateMap(typeof(ManagerResult<>), typeof(ManagerResult<>));
            CreateMap(typeof(PagedManagerResult<>), typeof(PagedManagerResult<>));
            #endregion
        }
    }
}
