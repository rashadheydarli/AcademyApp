using Core.Helpers;
using Core.Constants;
using Data.Contexts;
using Core.Entities;
using System.Globalization;
using Data.Repositories.Concrete;
using Data.Repositories.Abstract;



namespace Presentation
{
    public static class Program
    {
       
        static void Main()
        {
            GroupRepository _groupRepository = new GroupRepository();



            ConsoleHelper.WriteWithColor("---welcome---", ConsoleColor.Cyan);
            while(true)
            {
                ConsoleHelper.WriteWithColor("1-Create Group", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor("2-Update Group", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor("3-Delete Group", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor("4-Get All Groups", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor("5-Get Group by id", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor("6-Get Group by name", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor("0-Exit", ConsoleColor.Yellow);


                ConsoleHelper.WriteWithColor("--- Select option ---", ConsoleColor.Cyan);

                int number;     // out edirik ki deyeri sonradan deyise numberin deyeri de deyissin .ref type. addresini de oturur
                bool isSuccessed = int.TryParse(Console.ReadLine(), out number);  //pars ede  bilirse deyeri goturub menimsedir  hemin o variabla ede bilmese 1 bool deyer qayarir ki menimsede bilmedim
                if (!isSuccessed)
                {
                    ConsoleHelper.WriteWithColor("Inputted number is not correct format!", ConsoleColor.Red);
                }
                else
                {
                    if(!(number>=0 && number <= 6))
                    {
                        ConsoleHelper.WriteWithColor("Inputted number is not exist!", ConsoleColor.Red);
                    }
                    else
                    {
                        switch (number)
                        {
                            case (int)GroupOptions.CreateGroup:
                                ConsoleHelper.WriteWithColor("-- Enter name --", ConsoleColor.Cyan);

                                MaxSizeDescription:  ConsoleHelper.WriteWithColor("-- Enter group max size --", ConsoleColor.Cyan);
                                string name = Console.ReadLine();
                                int maxSize;
                                isSuccessed = int.TryParse(Console.ReadLine(), out maxSize);
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



                                break ;
                            case (int)GroupOptions.UpdateGroup:
                                break;
                            case (int)GroupOptions.DeleteGroup:
                                var groupsss = _groupRepository.GetAll();

                                ConsoleHelper.WriteWithColor("--All Groups --", ConsoleColor.Cyan);
                                foreach (var group_ in groupsss)
                                {
                                    ConsoleHelper.WriteWithColor($"Id: {groupsss.Id}, Name: {groupsss.Name}, Max size: {groupsss.MaxSize}, start date : {groupsss.StartDate}, end date: {groupsss.EndDate}", ConsoleColor.Magenta);
                                }
                            IdDescription: ConsoleHelper.WriteWithColor("-- Enter ID --", ConsoleColor.Cyan);

                                int id;
                                isSuccessed = int.TryParse(Console.ReadLine(), out id);
                                if (!isSuccessed)
                                {
                                    ConsoleHelper.WriteWithColor("ID is not correct format!", ConsoleColor.Red);
                                    goto IdDescription;
                                }
                                var dbGroup = _groupRepository.Get(id);
                                if(dbGroup is null)
                                {
                                    ConsoleHelper.WriteWithColor("there is no any group in this id", ConsoleColor.Red);
                                    
                                }
                                else
                                {
                                    _groupRepository.Delete(dbGroup);
                                    ConsoleHelper.WriteWithColor("Group successfully deleted", ConsoleColor.Green);


                                }
                                break;

                            case (int)GroupOptions.GetAllGroups:
                                var groups = _groupRepository.GetAll();

                                ConsoleHelper.WriteWithColor("--All Groups --", ConsoleColor.Cyan);
                                foreach( var group_ in groups)
                                {
                                    ConsoleHelper.WriteWithColor($"Id: {group_.Id}, Name: {group_.Name}, Max size: {group_.MaxSize}, start date : {group_.StartDate}, end date: {group.EndDate}", ConsoleColor.Magenta);
                                }

                                break;
                            case (int)GroupOptions.GetGroupById:
                                break;
                            case (int)GroupOptions.GetGroupByName:
                                break;
                            case (int)GroupOptions.Exit:
                                return;
                                break;

                            default:
                                break;
                        }

                   
                    }
                }

            }



        }
    }
    
}

