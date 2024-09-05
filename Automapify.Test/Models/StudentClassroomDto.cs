using Automapify.Services.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IgnoreAttribute = Automapify.Services.Attributes.IgnoreAttribute;

namespace Automapify.Test.Models
{
    public class StudentClassroomDto
    {
        [Ignore]
        public string Name { get; set; }

        [MapProperty("NumberOfStudents")]
        public int NoOfStudents { get; set; }

        public bool IsActive { get; set; }

        [MapProperty("NumberOfTeachers")]
        public int NoOfLecturers { get; set; }

        [Ignore]
        public string Code { get; set; }

        public List<string> LeadLecturers { get; set; }
    }
}
