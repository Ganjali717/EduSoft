﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EduSoft.Entities.Tutorials;

namespace EduSoft.Entities.Cources
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        [ForeignKey("Category")] 
        public Guid CategoryId { get; set; }
        public DateTime Created { get; set; }
    }
}
