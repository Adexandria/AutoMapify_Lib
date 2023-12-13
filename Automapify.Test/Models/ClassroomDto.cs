using Automapify.Services.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automapify.Test.Models
{
    public class ClassroomDto
    {
        public string Name { get; set; }

        [MapProperty("NumberOfStudents")]
        public int NoOfStudents { get; set; }

        public bool IsActive { get; set; }

        [MapProperty("NumberOfTeachers")]
        public int NoOfLecturers { get; set; }

        public string Code { get; set; }

        public List<string> LeadLecturers { get; set; }
    }
}
