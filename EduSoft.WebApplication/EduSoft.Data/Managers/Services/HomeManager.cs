using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EduSoft.Data.DatabaseContext;
using EduSoft.Data.Managers.Interfaces;
using EduSoft.Entities;
using EduSoft.Entities.Tutorials;
using Microsoft.Extensions.DependencyInjection;

namespace EduSoft.Data.Managers.Services
{
    public class HomeManager:IHomeManager
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        public HomeManager(IServiceProvider serviceProvider, IMapper mapper)
        {
            _serviceProvider = serviceProvider;
            _mapper = mapper;
        }

        public ManagerResult<List<Tutorial>> GetAllTutorials()
        {
            var result = new ManagerResult<List<Tutorial>>();
            try
            {
                using var dbContext = _serviceProvider.GetRequiredService<AppDbContext>();
                var context = dbContext.Tutorials.ToList();
                result.Success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result.Message = ex.GetBaseException().Message;
            }

            return result;
        }
    }
}
