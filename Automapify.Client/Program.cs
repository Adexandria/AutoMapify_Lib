// See https://aka.ms/new-console-template for more information

using Automapify.Client;
using Automapify.Client.Models;
using Automapify.Client.Models.Dtos;
using Automappify.Services;

var student1 = new Student(1, "Adeola", "Aderibigbe", "11/12/2000", "jss1");

var classroom = new Classroom("Jss2");

var studentDto = new StudentDtos();


studentDto.Map<Student,StudentDtos>(student1,MappingService.StudentConfig());

studentDto.Map<Classroom,StudentDtos>(classroom);

studentDto.DisplayFullName();

studentDto.DisplayCLassroom();

studentDto.DisplayAge();



