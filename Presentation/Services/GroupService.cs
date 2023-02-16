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
        
        public GroupService()
        {
            _groupRepository = new GroupRepository();
        }

        public void GetAll()
        {
            var groups = _groupRepository.GetAll();

            ConsoleHelper.WriteWithColor("--All Groups --", ConsoleColor.Cyan);
            foreach (var group in groups)
            {
                ConsoleHelper.WriteWithColor($"Id: {group.Id}, Name: {group.Name}, Max size: {group.MaxSize}, start date : {group.StartDate}, end date: {group.EndDate}", ConsoleColor.Magenta);
            }
        }
        public void GetGroupById()
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
                    Create();
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
                ConsoleHelper.WriteWithColor($"Id: {group.Id}, Name: {group.Name}, Max size: {group.MaxSize}, start date : {group.StartDate}, end date: {group.EndDate}", ConsoleColor.Magenta);



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
        public void Update()
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
            if (!(number==1 || number==2))
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
                    _groupRepository.Update(group); 
                }
            }
            else
            {
            EnterGroupNameDesc: ConsoleHelper.WriteWithColor("Enter group name", ConsoleColor.DarkCyan);
            string  name=Console.ReadLine();
                
                
            }
        }
        public void Create()                   // creategroup yazmiriq onsuz groupservicein icindeyik create deyende groupu nezerde tutacaq
		{
            ConsoleHelper.WriteWithColor("-- Enter name --", ConsoleColor.Cyan);

        MaxSizeDescription: ConsoleHelper.WriteWithColor("-- Enter group max size --", ConsoleColor.Cyan);
            string name = Console.ReadLine();
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
                ConsoleHelper.WriteWithColor("start is not chosen right", ConsoleColor.Red);
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
            var group = new Group
            {
                Name = name,
                MaxSize = maxSize,
                StartDate = startDate,
                EndDate = endDate

            };
            _groupRepository.Add(group);
            ConsoleHelper.WriteWithColor($"Group successfully created with Name : {group.Name}\n Max size:{group.MaxSize}\n Start Date : {group.StartDate.ToShortDateString()}\n End date: {group.EndDate.ToShortDateString()}\n");
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
                _groupRepository.Delete(dbGroup);
                ConsoleHelper.WriteWithColor("Group successfully deleted", ConsoleColor.Green);


            }
        }
        public void Exit()
        {
        AreYouSureDescription: ConsoleHelper.WriteWithColor("Are you sure?--y or n--", ConsoleColor.DarkRed);
            char decision;
            bool isSuccessded = char.TryParse(Console.ReadLine(), out decision);
            if (!isSuccessded)
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
                return;
            }
        }
        
    }
}

