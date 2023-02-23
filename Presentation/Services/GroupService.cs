using System;
using Core.Helpers;
using Data.Repositories.Concrete;
using System.Globalization;
using Core.Entities;

//bunun icinde bir obyekt yaradib butun medotlari call edeceyik

namespace Presentation.Services
{
    public class GroupService
    {
        private readonly GroupRepository _groupRepository; // security cehetden edirik gr rep bir construktorda teyin olunacaq onu basqa yerde deyismek olmayacaq.(main de student rep ede biler deye) 
        private readonly StudentRepository _studentRepository;
        private readonly TeacherRepository _teacherRespository;
        public GroupService()
        {
            _groupRepository = new GroupRepository();  // repository medonunu isf etdiyi ocun rep obyekti yaradilir
            _studentRepository = new StudentRepository();
            _teacherRespository = new TeacherRepository();
        }

        public void GetAll()
        {
            var groups = _groupRepository.GetAll();

            ConsoleHelper.WriteWithColor("--All Groups --", ConsoleColor.Cyan);
            foreach (var group in groups)
            {
                ConsoleHelper.WriteWithColor($"Id: {group.Id}, Name: {group.Name}, Max size: {group.MaxSize}, start date : {group.StartDate}, end date: {group.EndDate}, Created by: {group.CreatedBy}", ConsoleColor.Magenta);
            }
        }

        public void GetAllGroupsByTeacher()    
        {
            var teachers = _teacherRespository.GetAll();    //butun muellimleri gostermek
            {
                foreach (var teacher in teachers)
                {
                    ConsoleHelper.WriteWithColor($"Id: {teacher.Id}, Fullname: {teacher.Name} {teacher.Surname}", ConsoleColor.Cyan);
                }
            }

            TeacherIdDesc: ConsoleHelper.WriteWithColor(" Enter teacher id", ConsoleColor.Cyan);
            int id;
            bool isSuccessed = int.TryParse(Console.ReadLine(), out id);                    //muellimin hansi grouplarini gormek isteyirikse
            if (!isSuccessed)
            {
                ConsoleHelper.WriteWithColor(" Id is not correct format!", ConsoleColor.Red);
                goto TeacherIdDesc;
            }

            var dbteacher = _teacherRespository.Get(id);
            if (dbteacher is null)
            {
                ConsoleHelper.WriteWithColor(" There is no any teacher in this id", ConsoleColor.Red);
            }
            else
            {
                foreach (var group in dbteacher.Groups)
                {
                    ConsoleHelper.WriteWithColor($" Id: {group.Id}, Name: {group.Name} ", ConsoleColor.Cyan);
                }
            }
        }

        public void GetGroupById(Admin admin)
        {
            var groups = _groupRepository.GetAll();

            if (groups.Count == 0)
            {
            AreYouSureDescription: ConsoleHelper.WriteWithColor("There is no any group.Do you want to create new group?", ConsoleColor.DarkRed);
                char decision;
                bool isSuccessdedResult = char.TryParse(Console.ReadLine(), out decision);
                if (!isSuccessdedResult)
                {

                    ConsoleHelper.WriteWithColor("your choice is not correct format", ConsoleColor.Red);
                    goto AreYouSureDescription;
                }
                if (!(decision == 'y' || decision == 'n'))
                {
                    ConsoleHelper.WriteWithColor("your choice is not correct", ConsoleColor.Red);
                    goto AreYouSureDescription;
                }
                if (decision == 'y')
                {
                    Create(admin);
                }

            }
            else
            {
                GetAll();
            EnterIdDescription: ConsoleHelper.WriteWithColor("--- Enter id ---", ConsoleColor.Cyan);
                int id;
                bool isSuccessed = int.TryParse(Console.ReadLine(), out id);
                if (!isSuccessed)
                {
                    ConsoleHelper.WriteWithColor("Inputted id is not correct format", ConsoleColor.Red);
                    goto EnterIdDescription;
                }
                var group = _groupRepository.Get(id);
                if (group is null)
                {
                    ConsoleHelper.WriteWithColor("There is no any group in this id", ConsoleColor.Red);

                }

                ConsoleHelper.WriteWithColor($"Id: {group.Id}, Name: {group.Name}, Max size: {group.MaxSize}, start date : {group.StartDate}, end date: {group.EndDate}, Created by: {group.CreatedBy}", ConsoleColor.Magenta);

            }

        }

        public void GetGroupByName()
        {
            GetAll();

            ConsoleHelper.WriteWithColor("Enter groupname", ConsoleColor.Cyan);
            string name = Console.ReadLine();

            var group = _groupRepository.GetByName(name);
            if (group is null)
            {
                ConsoleHelper.WriteWithColor("there is no any group in this name", ConsoleColor.Red);
            }
            ConsoleHelper.WriteWithColor($"Id: {group.Id}, Name: {group.Name}, Max size: {group.MaxSize}, start date : {group.StartDate}, end date: {group.EndDate}", ConsoleColor.Magenta);


        }

        public void GetGroupsByStudentCount()
        {
            CountDesc: ConsoleHelper.WriteWithColor("Enter minimum student count", ConsoleColor.Cyan);
            int studentCount;
            bool isSuccessed = int.TryParse(Console.ReadLine(), out studentCount);
            if (!isSuccessed)
            {
                ConsoleHelper.WriteWithColor("Studnet count is not in a correct format", ConsoleColor.Red);
                goto CountDesc;
            }
            if (studentCount<0)
            {
                ConsoleHelper.WriteWithColor("Do not enter the number that is less than 0", ConsoleColor.Red);
                goto CountDesc;
            }
            
            var groups= _groupRepository.GetGroupsByStudentCount(studentCount);
            foreach (var group in groups)
            {
                ConsoleHelper.WriteWithColor($"{group.Name}", ConsoleColor.Cyan);
            }
        }
        public void Update(Admin admin)
        {
            GetAll();
        EnterGroupDesc: ConsoleHelper.WriteWithColor("Enter group \n 1. id or \n 2. name", ConsoleColor.DarkCyan);
            int number;
            bool isSuccessed = int.TryParse(Console.ReadLine(), out number);
            if (!isSuccessed)
            {
                ConsoleHelper.WriteWithColor("Inputted number is not correct format", ConsoleColor.Red);
                goto EnterGroupDesc;
            }
            if (!(number == 1 || number == 2))
            {
                ConsoleHelper.WriteWithColor("inputted number is not correct", ConsoleColor.Red);
                goto EnterGroupDesc;
            }
            if (number == 1)
            {
            EnterGroupIdDesc: ConsoleHelper.WriteWithColor("Enter group id", ConsoleColor.DarkCyan);

                int id;
                isSuccessed = int.TryParse(Console.ReadLine(), out id);
                if (!isSuccessed)
                {
                    ConsoleHelper.WriteWithColor("Inputted id is not correct format", ConsoleColor.Red);
                    goto EnterGroupIdDesc;
                }

                var group = _groupRepository.Get(id);
                if (group is null)
                {
                    ConsoleHelper.WriteWithColor("There is no any group in this id", ConsoleColor.Red);
                }
                else
                {
                    ConsoleHelper.WriteWithColor("enter new name");
                    string name = Console.ReadLine();

                MaxSizeDesc: ConsoleHelper.WriteWithColor("enter new max size");
                    int maxSize;
                    isSuccessed = int.TryParse(Console.ReadLine(), out maxSize);
                    if (!isSuccessed)
                    {
                        ConsoleHelper.WriteWithColor("Max size is not correct format", ConsoleColor.Red);
                        goto MaxSizeDesc;
                    }

                StartDateDesc: ConsoleHelper.WriteWithColor("enter new Start date");
                    DateTime startDate;
                    isSuccessed = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out startDate);  // sirf exact bir format gotursun deye
                    if (!isSuccessed)
                    {
                        ConsoleHelper.WriteWithColor("Start date is not correct format!", ConsoleColor.Red);
                        goto StartDateDesc;
                    }
                EndDateDesc: ConsoleHelper.WriteWithColor("enter new End date");
                    DateTime endDate;
                    isSuccessed = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out endDate);  // sirf exact bir format gotursun deye
                    if (!isSuccessed)
                    {
                        ConsoleHelper.WriteWithColor("End date is not correct format!", ConsoleColor.Red);
                        goto EndDateDesc;
                    }
                    group.Name = name;
                    group.MaxSize = maxSize;
                    group.StartDate = startDate;
                    group.EndDate = endDate;
                    group.ModifiedBy = admin.Username;
                    _groupRepository.Update(group);
                }
            }
            //else
            //{
            //    EnterGroupNameDesc: ConsoleHelper.WriteWithColor("Enter group name", ConsoleColor.DarkCyan);
            //    string  name=Console.ReadLine();
            //}
        }

        public void Create(Admin admin)                   // creategroup yazmiriq onsuz groupservicein icindeyik create deyende groupu nezerde tutacaq
        {
            if (_teacherRespository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("You must create a teacher before", ConsoleColor.Yellow);
            }
            else
            {
                NameDesc: ConsoleHelper.WriteWithColor("-- Enter name --", ConsoleColor.Cyan);
                string name = Console.ReadLine();
                var group = _groupRepository.GetByName(name);
                if (group is not null)
                {
                    ConsoleHelper.WriteWithColor("This group is already added", ConsoleColor.Cyan);
                    goto NameDesc;
                }

                MaxSizeDescription: ConsoleHelper.WriteWithColor("-- Enter group max size --", ConsoleColor.Cyan);
                int maxSize;
                bool isSuccessed = int.TryParse(Console.ReadLine(), out maxSize);
                if (!isSuccessed)
                {
                    ConsoleHelper.WriteWithColor("Max size is not correct format!", ConsoleColor.Red);
                    goto MaxSizeDescription;
                }
                if (maxSize > 18)
                {
                    ConsoleHelper.WriteWithColor("Max size should be less than or equals to 18", ConsoleColor.Red);
                    goto MaxSizeDescription;
                }

                StartDateDescription: ConsoleHelper.WriteWithColor("-- Enter start date --", ConsoleColor.Cyan);
                DateTime startDate;
                isSuccessed = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out startDate);  // sirf exact bir format gotursun deye
                if (!isSuccessed)
                {
                    ConsoleHelper.WriteWithColor("Start date is not correct format!", ConsoleColor.Red);
                    goto StartDateDescription;
                }

                DateTime boundaryDate = new DateTime(2015, 1, 1);
                if (startDate < boundaryDate)
                {
                    ConsoleHelper.WriteWithColor("start date is not chosen right", ConsoleColor.Red);
                    goto StartDateDescription;
                }

                 EndDateDescription: ConsoleHelper.WriteWithColor("-- Enter end date --", ConsoleColor.Cyan);
                DateTime endDate;
                isSuccessed = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out endDate); // sirf exact bir format gotursun deye
                if (!isSuccessed)
                {
                    ConsoleHelper.WriteWithColor("End date is not correct format!", ConsoleColor.Red);
                    goto EndDateDescription;
                }
                if (startDate > endDate)
                {
                    ConsoleHelper.WriteWithColor("End date must be bigger than start date", ConsoleColor.Red);
                    goto EndDateDescription;
                }
                var teachers = _teacherRespository.GetAll();    //butun muellimleri gostermek
                {
                    foreach (var teacher in teachers)
                    {
                        ConsoleHelper.WriteWithColor($"Id: {teacher.Id}, Fullname: {teacher.Name} {teacher.Surname}", ConsoleColor.Cyan);
                    }
                }

                TeacherIdDesc:  ConsoleHelper.WriteWithColor("Enter teacher id", ConsoleColor.Cyan);
                int teacherId;
                isSuccessed = int.TryParse(Console.ReadLine(),out teacherId);
                if (!isSuccessed)
                {
                    ConsoleHelper.WriteWithColor("Teacher id is not correct format!", ConsoleColor.Red);
                    goto TeacherIdDesc;
                }
                var dbteacher = _teacherRespository.Get(teacherId);
                if (dbteacher is null)
                {
                    ConsoleHelper.WriteWithColor("There is no any teacher in this id ", ConsoleColor.Red);
                    goto TeacherIdDesc;
                }
                  
                group = new Group                   
                {
                    Name = name,
                    MaxSize = maxSize,
                    StartDate = startDate,
                    EndDate = endDate,
                    CreatedBy = admin.Username,
                    Teacher =dbteacher,
                };

                dbteacher.Groups.Add(group);
                _groupRepository.Add(group);
                ConsoleHelper.WriteWithColor($"Group successfully created with Name : {group.Name}\n Max size:{group.MaxSize}\n Start Date : {group.StartDate.ToShortDateString()}\n End date: {group.EndDate.ToShortDateString()}\n", ConsoleColor.Green);

            }
        }
        public void Delete()
        {
            GetAll();

            IdDescription: ConsoleHelper.WriteWithColor("-- Enter ID --", ConsoleColor.Cyan);

            int id;
            bool isSuccessed = int.TryParse(Console.ReadLine(), out id);
            if (!isSuccessed)
            {
                ConsoleHelper.WriteWithColor("ID is not correct format!", ConsoleColor.Red);
                goto IdDescription;
            }
            var dbGroup = _groupRepository.Get(id);
            if (dbGroup is null)
            {
                ConsoleHelper.WriteWithColor("there is no any group in this id", ConsoleColor.Red);

            }
            else
            {
                foreach (var student in dbGroup.Students)
                {
                    student.Group = null;       // groupu silende studenti update etmek
                    _studentRepository.Update(student);
                }
                _groupRepository.Delete(dbGroup);
                ConsoleHelper.WriteWithColor("Group successfully deleted", ConsoleColor.Green);
            }
        }


        
    }
}

