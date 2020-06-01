using System;
using Models;
using AutoMapper;

namespace AutomapperDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person()
            {
                FirstName = "Tony",
                LastName = "Stark",
                BirthDate = new DateTime(1990, 01, 01)
            };

            Employee e = new Employee()
            {
                FirstName = "Jean",
                LastName = "Grey",
                Position = "Manager"
            };

            Student s = new Student()
            {
                StudentId = 1,
                FN = "Steve",
                LN = "Rogers",
            };

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Person, Employee>();
                cfg.CreateMap<Person, Student>()
                    .ForMember(dest => dest.FN, opt => opt.MapFrom(src => src.FirstName))
                    .ForMember(dest => dest.LN, opt => opt.MapFrom(src => src.LastName));

                cfg.CreateMap<Employee, Person>();
                cfg.CreateMap<Employee, Student>()
                    .ForMember(dest => dest.FN, opt => opt.MapFrom(src => src.FirstName))
                    .ForMember(dest => dest.LN, opt => opt.MapFrom(src => src.LastName));

                cfg.CreateMap<Student, Person>();
                cfg.CreateMap<Student, Employee>()
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FN))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LN));
            });

            var mapper = config.CreateMapper();

            Employee employee = mapper.Map<Employee>(p);
            Student student = mapper.Map<Student>(p);

            Console.WriteLine($"Person object -> {p.FirstName} {p.LastName} {p.BirthDate:d}");

            Console.WriteLine($"\t Mapping Person to Employee -> {employee.FirstName} {employee.LastName} {employee.Position}");
            Console.WriteLine($"\t Mapping Person to Student -> {student.FN} {student.LN}");

            Person person = mapper.Map<Person>(e);
            student = mapper.Map<Student>(e);

            Console.WriteLine($"Employee object -> {e.FirstName} {e.LastName} {e.Position}");

            Console.WriteLine($"\t Mapping Employee to Person -> {person.FirstName} {person.LastName} {person.BirthDate:d}");
            Console.WriteLine($"\t Mapping Employee to Student -> {student.FN} {student.LN} {student.StudentId}");

            person = mapper.Map<Person>(s);
            employee = mapper.Map<Employee>(s);

            Console.WriteLine($"Student object -> {s.FN} {s.LN} {s.StudentId}");

            Console.WriteLine($"\t Mapping Student to Person -> {person.FirstName} {person.LastName} {person.BirthDate:d}");
            Console.WriteLine($"\t Mapping Student to Employee -> {employee.FirstName} {employee.LastName} {employee.Position}");
        }
    }
}
