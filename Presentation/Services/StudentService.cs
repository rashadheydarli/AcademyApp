using System;
using Core.Entities;
using Core.Extensions;
using Core.Helpers;
using System.Globalization;
using Data.Repositories.Concrete;

namespace Presentation.Services
{
	public class StudentService
	{
        private readonly GroupService _groupService;
        private readonly GroupRepository _groupRepository;
        private readonly StudentRepository _studentRepository;
        public StudentService()
        {
            _groupService = new GroupService();
            _groupRepository = new GroupRepository();          //hem servis hem repository lazimdi
            _studentRepository = new StudentRepository();
        }

        public void GetAll()
        {
            var students = _studentRepository.GetAll();
            ConsoleHelper.WriteWithColor("-- ALL STUDENTS --", ConsoleColor.Cyan);
            foreach (var student in students)
            {
                ConsoleHelper.WriteWithColor($"{student.Id}, Fullname: {student.Name} { student.Surname}, Group: {student.Group?.Name}, Created by: {student.CreatedBy}", ConsoleColor.Cyan);

            }
        }

        public void GetAllByGroup()
        {
            _groupRepository.GetAll();

            GroupDesc: ConsoleHelper.WriteWithColor("Enter group id", ConsoleColor.Cyan);

            int groupId;
            bool isSuccessed = int.TryParse(Console.ReadLine(), out groupId);
            if (!isSuccessed)
            {
                ConsoleHelper.WriteWithColor("Group id is not correct format", ConsoleColor.Red);
                goto GroupDesc;
            }

            var group = _groupRepository.Get(groupId);
            if(group is null)
            {
                ConsoleHelper.WriteWithColor("There is no any group in this id", ConsoleColor.Red);
            }
            if(group.Students.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no student in this group", ConsoleColor.Red);

            }
            else
            {
                foreach (var student in group.Students) 
                {
                    ConsoleHelper.WriteWithColor($"{student.Id}, Fullname: {student.Name} {student.Surname}, Group: {student.Group?.Name}", ConsoleColor.Cyan);
                }
            }
        }


        public void Create(Admin admin)
		{
            if(_groupRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("You must create a group first", ConsoleColor.Red);
                return;
            }
            ConsoleHelper.WriteWithColor("Enter student name", ConsoleColor.Cyan);
            string name = Console.ReadLine();

            ConsoleHelper.WriteWithColor("Enter student surname", ConsoleColor.Cyan);
            string surname = Console.ReadLine();

        EmailDesc: ConsoleHelper.WriteWithColor("Enter student Email", ConsoleColor.Cyan);
            string email = Console.ReadLine();

            if (!(email.IsEmail()))
            {
                ConsoleHelper.WriteWithColor("Email is not correct format!", ConsoleColor.Red);
                goto EmailDesc;
            }
            if(_studentRepository.IsDuplicateEmail(email))
            {
                ConsoleHelper.WriteWithColor("This email already used", ConsoleColor.Red);
                goto EmailDesc;
            }


            BirthDateDescription: ConsoleHelper.WriteWithColor("-- Enter birth date --", ConsoleColor.Cyan);
            DateTime birthDate;
            bool isSuccessed = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out birthDate);
            if (!isSuccessed)
            {
                ConsoleHelper.WriteWithColor("Birth date is not correct format!", ConsoleColor.Red);
                goto BirthDateDescription;
            }
            
        // getall dan sonra bir id vermelidi ki student bu groupda oxuyacaq

            GroupDescription: _groupService.GetAll();
            ConsoleHelper.WriteWithColor("Enter group Id", ConsoleColor.Cyan);

            int groupId;
            isSuccessed = int.TryParse(Console.ReadLine(), out groupId);
            if (!isSuccessed)
            {
                ConsoleHelper.WriteWithColor("Group id is not correct format!", ConsoleColor.Red);
                goto GroupDescription;
            }

            var group = _groupRepository.Get(groupId);  // qrupu tapmaq
            if (group is null)
            {
                ConsoleHelper.WriteWithColor("Group is not exist in this Id", ConsoleColor.Red);
                goto GroupDescription;
            }
            if(group.MaxSize <= group.Students.Count)
            {
                ConsoleHelper.WriteWithColor("This group is full", ConsoleColor.Red);
                goto GroupDescription;

            }
            var student = new Student
            {
                Name = name,
                Surname = surname,
                Email = email,
                BirthDate = birthDate,
                Group = group,
                GroupId = group.Id,     //groupun icinde gedib id cagirmayim deye(student.Group.Id)
                CreatedBy = admin.Username                        // rahat istediyim yerde istf edim
            };

            group.Students.Add(student);
            _studentRepository.Add(student);
            ConsoleHelper.WriteWithColor($"{student.Name} {student.Surname} is successfully added", ConsoleColor.Green);
        }
        public void Update(Admin admin)
        {
            StudentDesc: GetAll();

            ConsoleHelper.WriteWithColor("Enter student Id", ConsoleColor.Cyan);
            int id;
            bool issuccessed = int.TryParse(Console.ReadLine(), out id);
            if (!issuccessed)
            {
                ConsoleHelper.WriteWithColor("Inputted id is not correct format", ConsoleColor.Red);
                goto StudentDesc;
            }
            var student = _studentRepository.Get(id);
            {
                if (student is null)
                {
                    ConsoleHelper.WriteWithColor("There is no any student in this id ", ConsoleColor.Red);
                    goto StudentDesc;
                }
            }

            ConsoleHelper.WriteWithColor("Enter new name", ConsoleColor.Cyan);
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithColor("Enter new surname", ConsoleColor.Cyan);
            string surname = Console.ReadLine();

            BirthDateDescription: ConsoleHelper.WriteWithColor("-- Enter birth date --", ConsoleColor.Cyan);
            DateTime birthDate;
            bool isSuccessed = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out birthDate);
            if (!isSuccessed)
            {
                ConsoleHelper.WriteWithColor("Birth date is not correct format!", ConsoleColor.Red);
                goto BirthDateDescription;
            }

            GroupDesc: _groupService.GetAll();                         //butun grouplari gormek ucun(studente yeni group secmek ucun )

            ConsoleHelper.WriteWithColor("Enter new group id", ConsoleColor.Cyan);
            int groupId;
            isSuccessed = int.TryParse(Console.ReadLine(), out groupId);
            {
                if (!isSuccessed)
                {
                    ConsoleHelper.WriteWithColor("Group id  is not correct format!", ConsoleColor.Red);
                    goto GroupDesc;
                }
            }
            var group = _groupRepository.Get(groupId);         //id ye gore groupu axtarmaq 
            if(group is null)
            {
                ConsoleHelper.WriteWithColor("There is no any group in this Id", ConsoleColor.Red);
                goto GroupDesc;
            }
            
            student.Name = name;
            student.Surname = surname;
            student.BirthDate = birthDate;
            student.Group = group;
            student.GroupId = groupId;
            student.ModifiedBy = admin.Username;

            _studentRepository.Update(student);
            ConsoleHelper.WriteWithColor($"{student.Name} {student.Surname},Group: {student.Group.Name} successfully updated, Modifed by: {student.ModifiedBy}", ConsoleColor.Green);

        }

        public void Delete()
        {
            GetAll();

            IdDesc: ConsoleHelper.WriteWithColor("Enter id", ConsoleColor.Cyan);
            int id;
            bool isSuccessful = int.TryParse(Console.ReadLine(),out id);
            if (!isSuccessful)
            {
                ConsoleHelper.WriteWithColor("Id is not correct format!", ConsoleColor.Red);
                goto IdDesc;
            }
            var student = _studentRepository.Get(id);
            if(student is null)
            {
                ConsoleHelper.WriteWithColor("There is no student is in this id", ConsoleColor.Red);
            }
            _studentRepository.Delete(student);
            ConsoleHelper.WriteWithColor($"{student.Name} {student.Surname}, Group: {student.Group}", ConsoleColor.Green);
        }

    }
}

