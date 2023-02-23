using System;
using System.Globalization;
using Core.Entities;
using Core.Helpers;
using Data.Repositories.Abstract;
using Data.Repositories.Concrete;

namespace Presentation.Services
{
    public class TeacherService
    {
        private readonly StudentRepository _studentRepository;
        private readonly TeacherRepository _teacherRepository;
        
        public TeacherService()
        {
            _studentRepository = new StudentRepository();
            _teacherRepository = new TeacherRepository();
        }

        public void GetAll()
        {
            var teachers = _teacherRepository.GetAll();
            if (teachers.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no any teacher", ConsoleColor.Red);
            }

            foreach (var teacher in teachers)
            {
                ConsoleHelper.WriteWithColor($" Id: {teacher.Id},Fullname: {teacher.Name} {teacher.Surname}, Speciality: {teacher.Speciality}", ConsoleColor.Cyan);

                if (teacher.Groups.Count == 0)
                {
                    ConsoleHelper.WriteWithColor("There is no any group in this teacher", ConsoleColor.Red);
                }

                foreach (var group in teacher.Groups)   //teacherin grouplarina baxmaq 
                {
                    ConsoleHelper.WriteWithColor($" Id: {group.Id}, Name: {group.Name}", ConsoleColor.Cyan);
                }
                Console.WriteLine();
            }
        }

        public void Create()
        {
            ConsoleHelper.WriteWithColor("Enter teacher name", ConsoleColor.Cyan);
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithColor("Enter teacher surname", ConsoleColor.Cyan);
            string surname = Console.ReadLine();

        BirthDateDescription: ConsoleHelper.WriteWithColor("Enter birth date", ConsoleColor.Cyan);
            DateTime birthDate;
            bool isSuccessed = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out birthDate);
            if (!isSuccessed)
            {
                ConsoleHelper.WriteWithColor("Birth date is not correct format!", ConsoleColor.Red);
                goto BirthDateDescription;
            }

            ConsoleHelper.WriteWithColor("Enter teacher Speciality", ConsoleColor.Cyan);
            string speciality = Console.ReadLine();

            var teacher = new Teacher
            {
                Name = name,
                Surname = surname,
                Birthday = birthDate,
                Speciality = speciality,
                CreatedAt = DateTime.Now,
            };

            _teacherRepository.Add(teacher);
            string teacherBirthDate = teacher.Birthday.ToString("dddd, dd MMMM yyyy");
            ConsoleHelper.WriteWithColor($"Name: {teacher.Name}, Surname: {teacher.Surname}, Speciality: {teacher.Speciality}, Birth date:{teacherBirthDate}", ConsoleColor.Cyan);
        }

        public void Delete()
        {
            List: GetAll();
            if (_studentRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no teacher", ConsoleColor.Red);
            }
            else
            {
                ConsoleHelper.WriteWithColor("Enter teacher Id", ConsoleColor.Cyan);
                int id;
                bool isSuccessed = int.TryParse(Console.ReadLine(), out id);
                if (!isSuccessed)
                {
                    ConsoleHelper.WriteWithColor("Id is not correct format!", ConsoleColor.Red);
                    goto List;
                }
                var teacher = _teacherRepository.Get(id);
                if (teacher is null)
                {
                    ConsoleHelper.WriteWithColor("There is no any teacher in this id", ConsoleColor.Red);
                }
                _teacherRepository.Delete(teacher);
                ConsoleHelper.WriteWithColor($"{teacher.Name}{teacher.Surname} is successfully deleted", ConsoleColor.Green);
               
            } 
        }

        public void Update()
        {
            UpdateDesc:  GetAll();
            ConsoleHelper.WriteWithColor("Enter  teacher id", ConsoleColor.Cyan);
            int id;
            bool isSuccessed = int.TryParse(Console.ReadLine(), out id);
            if (!isSuccessed)
            {
                ConsoleHelper.WriteWithColor("Inputted Id is not correct format!", ConsoleColor.Red);
                goto UpdateDesc;
            }
            var teacher = _teacherRepository.Get(id);
            {
                if (teacher is null)
                {
                    ConsoleHelper.WriteWithColor("There is no any teacher in this id ", ConsoleColor.Red);
                    goto UpdateDesc;
                }
            }

            ConsoleHelper.WriteWithColor("Enter new  teacher name", ConsoleColor.Cyan);
            string name = Console.ReadLine();

            ConsoleHelper.WriteWithColor("Enter new teacher surname", ConsoleColor.Cyan);
            string surname = Console.ReadLine();

            BirthDateDescription: ConsoleHelper.WriteWithColor("Enter teacher birth date", ConsoleColor.Cyan);
            DateTime birthDate;
            isSuccessed = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out birthDate);
            if (!isSuccessed)
            {
                ConsoleHelper.WriteWithColor("Birth date is not correct format!", ConsoleColor.Red);
                goto BirthDateDescription;
            }
            ConsoleHelper.WriteWithColor("Enter new  speciality", ConsoleColor.Cyan);
            string speciality= Console.ReadLine();


            //hansi serviceREpository cagirlir


            teacher.Name = name;
            teacher.Surname = surname;
            teacher.Birthday = birthDate;
            teacher.Speciality = speciality;
           
            _teacherRepository.Update(teacher);

            ConsoleHelper.WriteWithColor($"{teacher.Name} {teacher.Surname} is successfully updated", ConsoleColor.Green);



        }
    }
}
