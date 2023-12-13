// See https://aka.ms/new-console-template for more information

using Automapify.Client;
using Automapify.Client.Models;
using Automapify.Client.Models.Dtos;
using Automappify.Services;

var student = new Student(1, "Adeola", "Aderibigbe", "11/12/2000", "jss1");

var classroom = new Classroom("Jss2");

var studentDto = new StudentDto();


studentDto.Map<Student,StudentDto>(student,MappingService.StudentConfig());

studentDto.Map<Classroom,StudentDto>(classroom);

studentDto.DisplayFullName();

studentDto.DisplayCLassroom();

studentDto.DisplayAge();



