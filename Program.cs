using System;
using AutoMapper;

namespace AutomapperDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Models.Person p = new Models.Person()
            {
                FirstName = "John",
                LastName = "Cena",
                BirthDate = new DateTime(1990, 07, 04)
            };

            Models.Employee e = new Models.Employee()
            {
                FirstName = "Jane",
                LastName = "Doe",
                Position = "Manager"
            };

            Models.Student s = new Models.Student()
            {
                StudentId = 1,
                FN = "Steve",
                LN = "Rogers",
            };

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.Person, Models.Employee>();
                cfg.CreateMap<Models.Person, Models.Student>()
                    .ForMember(dest => dest.FN, opt => opt.MapFrom(src => src.FirstName))
                    .ForMember(dest => dest.LN, opt => opt.MapFrom(src => src.LastName));

                cfg.CreateMap<Models.Employee, Models.Person>();
                cfg.CreateMap<Models.Employee, Models.Student>()
                    .ForMember(dest => dest.FN, opt => opt.MapFrom(src => src.FirstName))
                    .ForMember(dest => dest.LN, opt => opt.MapFrom(src => src.LastName));

                cfg.CreateMap<Models.Student, Models.Person>();
                cfg.CreateMap<Models.Student, Models.Employee>()
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FN))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LN));
            });

            var mapper = config.CreateMapper();

            Models.Employee employee = mapper.Map<Models.Employee>(p);
            Models.Student student = mapper.Map<Models.Student>(p);

            Console.WriteLine($"Person object -> {p.FirstName} {p.LastName} {p.BirthDate:d}");

            Console.WriteLine($"\t Mapping Person to Employee -> {employee.FirstName} {employee.LastName} {employee.Position}");
            Console.WriteLine($"\t Mapping Person to Student -> {student.FN} {student.LN}");

            Models.Person person = mapper.Map<Models.Person>(e);
            student = mapper.Map<Models.Student>(e);

            Console.WriteLine($"Employee object -> {e.FirstName} {e.LastName} {e.Position}");

            Console.WriteLine($"\t Mapping Employee to Person -> {person.FirstName} {person.LastName} {person.BirthDate:d}");
            Console.WriteLine($"\t Mapping Employee to Student -> {student.FN} {student.LN} {student.StudentId}");

            person = mapper.Map<Models.Person>(s);
            employee = mapper.Map<Models.Employee>(s);

            Console.WriteLine($"Student object -> {s.FN} {s.LN} {s.StudentId}");

            Console.WriteLine($"\t Mapping Student to Person -> {person.FirstName} {person.LastName} {person.BirthDate:d}");
            Console.WriteLine($"\t Mapping Student to Employee -> {employee.FirstName} {employee.LastName} {employee.Position}");
        }
    }
}
