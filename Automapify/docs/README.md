# Automapify
This dotnet library facilitates object mappings without the need for service setup or configuration.

### Basic Usage
There are two methods for utilizing this tool: Attributes and Configuration
#### **Attributes**

- Specify the property name from the source in the destination object using the `MapProperty` attribute

   ```csharp
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        [MapProperty("DateOfBirth")]
        public DateTime DOB { get; set; }

        public bool IsDeleted { get; set; }

        public Classroom Room {get; set; }
   ```

##### Map to a new object
```csharp
    var studentDto = student1.Map<Student,StudentDto>();
```

##### Map to an existing object

  ```csharp
    studentDto.Map<Student,StudentDto>(student1);
  ```

#### **Configuration**

- Establish a configuration to illustrate the mapping of values.

 ```csharp
      public static class MappingService
    {
        public static MapifyConfiguration<Student,StudentDtos> StudentConfig()
        {
            return new MapifyConfigurationBuilder<Student, StudentDto>()
                .Map(d => d.Name, s => $"{s.FirstName} {s.LastName}")
                .Map(d => d.Age, s => s.DateOfBirth.ToAge())
                .Map(d=>d.DOB, s => s.DateOfBirth)
                .Map(d=>d.IsDeleted, s => false)
                .Map(d=>d.Classroom, s=> s.Room)
                .CreateConfig();
        }
    }
 ```

##### Map to a new object

  ```csharp
    var studentDto = student.Map<Student,StudentDto>(MappingService.StudentConfig());
  ```

##### Map to an existing object

```csharp
   studentDto.Map<Student,StudentDto>(student1,MappingService.StudentConfig());
```

#### Many to one/many mapping
Automapify now supports many to many and many to one object mapping. You can utilize that by either using the conifguration or attributes.

##### Using `MapProperty` attribute

- Specify the property name from the source in the destination object using the `MapProperty` attribute

   ```csharp
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        [MapProperty("DateOfBirth")]
        public DateTime DOB { get; set; }

        public bool IsDeleted { get; set; }

        public Classroom Room {get; set; }
   ```

###### Map from objects to  objects (Many-to-many mapping)
- Map to a new object
   ```csharp
   var studentDtos = student.Map<List<Student>, List<StudentDto>>();
   ```
- Map to an existing object
  ```csharp
  studentDtos.Map(students); 
  ```
###### Map from object to objects (One-to-many mapping)
- Map to a new object
   ```csharp
   var studentDtos = student.Map<Student, List<StudentDto>>();
   ```
- Map to an existing object
  ```csharp
  studentDtos.Map(student); 
  ```

##### Using Configuration
- Establish a configuration to illustrate the mapping of values.

 ```csharp
      public static class MappingService
    {
        public static MapifyConfiguration<Student,StudentDtos> StudentConfig()
        {
            return new MapifyConfigurationBuilder<Student, StudentDto>()
                .Map(d => d.Name, s => $"{s.FirstName} {s.LastName}")
                .Map(d => d.Age, s => s.DateOfBirth.ToAge())
                .Map(d=>d.DOB, s => s.DateOfBirth)
                .Map(d=>d.IsDeleted, s => false)
                .Map(d=>d.Classroom, s=> s.Room)
                .CreateConfig();
        }
    }
 ```

###### Map from objects to  objects (Many-to-many mapping)
- Map to a new object
   ```csharp
   var studentDtos = student.Map<List<Student>, List<StudentDto>>(MappingService.StudentConfig());
   ```
- Map to an existing object
  ```csharp
  studentDtos.Map(students,MappingService.StudentConfig()); 
  ```
###### Map from object to objects (One-to-many mapping)
- Map to a new object
   ```csharp
   var studentDtos = student.Map<Student, List<StudentDto>>(MappingService.StudentConfig());
   ```
- Map to an existing object
    ```csharp
  studentDtos.Map(student,MappingService.StudentConfig()); 
  ```
