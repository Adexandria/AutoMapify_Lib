// See https://aka.ms/new-console-template for more information

using Automapify.Client;
using Automapify.Client.Models;
using Automapify.Client.Models.Dtos;
using Automappify.Services;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;

// Set up student and classroom data
var student = new Student(1, "Adeola", "Aderibigbe", "11/12/2000", "jss1");

var classroom = new Classroom("Jss2");

student.Classroom = classroom;
student.AddClassroom("Jss3");

student.AddTeacher("Violet", "James");

// Map a list of students to a list of student dtos using mapping configuration
var students = new List<Student>() { student };


var studentDtos = student.Map<Student, List<StudentDto>>(MappingService.StudentConfig());


// Maps data from a student object to a list of student dtos using mapping configuration
var newStudentDtos = students.Map<List<Student>, List<StudentDto>>(MappingService.StudentConfig());


// Map a student using mapping configuration
var studentDto = new StudentDto();

studentDto.Map(student, MappingService.StudentConfig());


// Display data mapped for one to one mapping

Console.WriteLine("One to one mapping values:");

studentDto.DisplayFullName();

studentDto.DisplayCLassroom();

studentDto.DisplayAge();


Console.WriteLine("\nMany to many mapping values:");

foreach (var item in studentDtos)
{
    item.DisplayFullName();

    item.DisplayCLassroom();

    item.DisplayAge();
}


Console.WriteLine("\n One to many mapping values:");
foreach (var item in newStudentDtos)
{
    item.DisplayFullName();

    item.DisplayCLassroom();

    item.DisplayAge();
}

var config = DefaultConfig.Instance
    .AddJob(Job
         .MediumRun
         .WithLaunchCount(1)
         .WithToolchain(InProcessEmitToolchain.Instance));

var summary = BenchmarkRunner.Run(typeof(BenchmarkTest), config);

