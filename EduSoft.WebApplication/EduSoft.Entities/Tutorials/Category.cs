using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EduSoft.Entities.Tutorials
{
    public class Category
    {
       [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
       public Guid Id { get; set; }
       public string Name { get; set; }
        
    }
}
