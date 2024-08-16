﻿using Automapify.Client.Models;
using Automapify.Client.Models.Dtos;
using Automappify.Services;
using BenchmarkDotNet.Attributes;

namespace Automapify.Client
{
    public class BenchmarkTest
    {
        public BenchmarkTest()
        {
            _student = new Student(1, "Adeola", "Aderibigbe", "11/12/2000", "jss1")
            {
                Classroom = new Classroom("Jss2")
            };

            _students = new List<Student> { _student };
        }

        [Benchmark]
        public void MapToAnExistingObject()
        {
            var studentDto = new StudentDto();
            var classroom = new Classroom("Jss2");
            studentDto.Map(classroom);
        }

        [Benchmark]
        public void MapToExistingObjects()
        {
            var studentDto = new List<StudentDto>();
            var classroom = new List<Classroom> 
            {
                new("Jss2"),
                new("Jss3")
            };

            studentDto.Map(classroom);
        }

        [Benchmark]
        public void MapToAnExistingObjectWithConfig()
        {
            var studentDto = new StudentDto();
            studentDto.Map(_student, MappingService.StudentConfig());
        }

        [Benchmark]
        public void MapToAnExistingObjectsWithConfig()
        {
            var studentDto = new List<StudentDto>();
            studentDto.Map(_students, MappingService.StudentConfig());
        }

        [Benchmark]
        public StudentDto MapToNewObject()
        {
            return _student.Map<Student, StudentDto>();
        }

        [Benchmark]
        public List<StudentDto> MapToNewObjects()
        {
            return _students.Map<List<Student>, List<StudentDto>>();
        }


        [Benchmark]
        public StudentDto MapToNewObjectWithConfig()
        {
            return _student.Map<Student, StudentDto>(MappingService.StudentConfig());
        }

        [Benchmark]
        public List<StudentDto> MapToNewObjectsWithConfig()
        {
            return _students.Map<List<Student>, List<StudentDto>>(MappingService.StudentConfig());
        }

        private readonly List<Student> _students;

        private readonly Student _student;
    }
}
