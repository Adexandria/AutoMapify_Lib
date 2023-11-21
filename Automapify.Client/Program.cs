// See https://aka.ms/new-console-template for more information

using Automapify.Client.Models;
using Automapify.Client.Models.Dtos;
using Automappify.Services;

var student1 = new Student(1, "testName", "lastTest", "11/12/2010", "jss1");

var studentDto = student1.Map<Student, StudentDtos>();

studentDto.DisplayName();

studentDto.DisplayCLassroom();


