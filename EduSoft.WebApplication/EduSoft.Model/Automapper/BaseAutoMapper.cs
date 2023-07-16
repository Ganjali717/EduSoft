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
            CreateMap<Tutorial, TutorialDto>();
            CreateMap<TutorialDto, Tutorial>();
            #endregion

            #region AccountDTO


            #endregion

            #region JobsDTO
            CreateMap<Job, JobDTO>();
            CreateMap<JobDTO, Job>();
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
