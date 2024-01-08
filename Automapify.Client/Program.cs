// See https://aka.ms/new-console-template for more information

using Automapify.Client;
using Automapify.Client.Models;
using Automapify.Client.Models.Dtos;
using Automappify.Services;
using BenchmarkDotNet.Running;

var student = new Student(1, "Adeola", "Aderibigbe", "11/12/2000", "jss1");

var classroom = new Classroom("Jss2");

var studentDto = new StudentDto();


studentDto.Map(student,MappingService.StudentConfig());

studentDto.Map(classroom);

studentDto.DisplayFullName();

studentDto.DisplayCLassroom();

studentDto.DisplayAge();

var summary = BenchmarkRunner.Run(typeof(BenchmarkTest));

