using Core.Helpers;
using Core.Constants;
using Data.Contexts;
using Core.Entities;
using System.Globalization;
using Data.Repositories.Concrete;
using Data.Repositories.Abstract;
using Presentation.Services;

namespace Presentation
{
    public static class Program
    {
        private readonly static GroupService _groupService;
        static Program()
        {
            _groupService = new GroupService();
        }
       
        static void Main()
        {
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
                                _groupService.Create();
                                



                                break ;
                            case (int)GroupOptions.UpdateGroup:
                                _groupService.Update();
                                break;
                            case (int)GroupOptions.DeleteGroup:
                                _groupService.Delete();
                                break;

                            case (int)GroupOptions.GetAllGroups:
                                _groupService.GetAll();

                                break;
                            case (int)GroupOptions.GetGroupById:
                                _groupService.GetGroupById();

                                break;
                            case (int)GroupOptions.GetGroupByName:
                                _groupService.GetGroupByName();
                                break;
                            case (int)GroupOptions.Exit:
                                _groupService.Exit();
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

