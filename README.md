# Automapify
This is a dotnet library used to map from one object to another without setting up services or configuration.

### Basic Usage
There are two ways to use this tool; Attributes and Configuration
#### Attribute

- Specify the source name in the destination object

   ```csharp
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        [MapProperty("DateOfBirth")]
        public DateTime DOB { get; set; }

        public bool IsDeleted { get; set; }
   ```

##### Map to a new object
```csharp
    var studentDto = student1.Map<Student,StudentDtos>();
```

##### Map to an existing object

  ```csharp
    studentDto.Map<Student,StudentDtos>(student1);
  ```

#### Configuration

- Set up configuration to display how values will be mapped

 ```csharp
      public static class MappingService
    {
        public static MapifyConfiguration<Student,StudentDtos> StudentConfig()
        {
            return SettingConfiguration<Student, StudentDtos>
                .CreateConfig()
                .Map(d => d.Name, s => $"{s.FirstName} {s.LastName}")
                .Map(d => d.Age, s => s.DateOfBirth.ToAge())
                .Map(d=>d.DOB, s =>s.DateOfBirth)
                .Map(d=>d.IsDeleted, s => false);
        }
    }
 ```

##### Map to a new object

  ```csharp
    var studentDto = student1.Map<Student,StudentDtos>(MappingService.StudentConfig());
  ```

### Map to an existing object

```csharp
studentDto.Map<Student,StudentDtos>(student1,MappingService.StudentConfig());
```

