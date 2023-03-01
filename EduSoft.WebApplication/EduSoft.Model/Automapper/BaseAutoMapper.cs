using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EduSoft.Entities.Tutorials;
using EduSoft.Model.DTO.Tutorials;

namespace EduSoft.Model.Automapper
{
    public class BaseAutoMapper:Profile
    {
        public BaseAutoMapper()
        {
            CreateMap<TutorialDto, Tutorial>();
            CreateMap<Tutorial, TutorialDto>();
        }
    }
}
