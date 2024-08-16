// See https://aka.ms/new-console-template for more information

using Automapify.Client;
using Automapify.Client.Models;
using Automapify.Client.Models.Dtos;
using Automappify.Services;
using BenchmarkDotNet.Running;

// Set up student and classroom data
var student = new Student(1, "Adeola", "Aderibigbe", "11/12/2000", "jss1");

var classroom = new Classroom("Jss2");

student.Classroom = classroom;

// Map a list of students to a list of student dtos using mapping configuration
var students = new List<Student>() { student };


var studentDtos = students.Map<List<Student>, List<StudentDto>>(MappingService.StudentConfig());


// Map a student using mapping configuration
var studentDto = new StudentDto();

studentDto.Map(student, MappingService.StudentConfig());


// Display data mapped

studentDto.DisplayFullName();

studentDto.DisplayCLassroom();

studentDto.DisplayAge();

foreach (var item in studentDtos)
{
    item.DisplayFullName();

    item.DisplayCLassroom();

    item.DisplayAge();
}

//var summary = BenchmarkRunner.Run(typeof(BenchmarkTest));

