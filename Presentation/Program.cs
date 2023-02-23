﻿using Core.Helpers;
using Core.Constants;
using Data.Contexts;
using Core.Entities;
using System.Globalization;
using Data.Repositories.Concrete;
using Data.Repositories.Abstract;
using Presentation.Services;
using Core.Extensions;
using Data;
using System.Text;

namespace Presentation
{
    public static class Program
    {
        
        private readonly static GroupService _groupService;   
        private readonly static StudentService _studentService;         // metodu teyin edirik ve constructor daxilinde ob yaradiriq
        private readonly static TeacherService _teacherService;
        private readonly static AdminService _adminService;           //app run olunanda isleyen ilk sey program classidir ve

                                                                         // onun constructoru call olunur

        static Program()
        {
            Console.OutputEncoding = Encoding.UTF8;

            DbInitializer.SeadAdmins();                         //-app ise dusen zaman bazaya adminleri elave et 
            _groupService = new GroupService();           //medotun isletmek ucun obyekt almaliyiq 
            _studentService = new StudentService();     ///neyin metodunu cagirirsa onun obyektini yaradir 
            _teacherService = new TeacherService();
            _adminService = new AdminService();           
        }
        
        static void Main()
        {
            Authorize: var admin = _adminService.Authorize();
            if(admin is not null)
            {
                while (true)
                {
                    ConsoleHelper.WriteWithColor($"---welcome,{admin.Username}---", ConsoleColor.Cyan);

                    MainMenuDesc: ConsoleHelper.WriteWithColor("1-Groups", ConsoleColor.Yellow);
                    ConsoleHelper.WriteWithColor("2-Students", ConsoleColor.Yellow);
                    ConsoleHelper.WriteWithColor("3-Teachers", ConsoleColor.Yellow);
                    ConsoleHelper.WriteWithColor("0-Logout", ConsoleColor.Yellow);

                    int number;
                    bool isSuccessed = int.TryParse(Console.ReadLine(), out number);
                    if (!isSuccessed)
                    {
                        ConsoleHelper.WriteWithColor("Inputted number is not correct format", ConsoleColor.Cyan);
                        goto MainMenuDesc;
                    }
                    else
                    {
                        switch (number)
                        {
                            case (int)MainMenuOptions.Groups:
                                while (true)
                                {
                                GroupDesc: ConsoleHelper.WriteWithColor("1-Create Group", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("2-Update Group", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("3-Delete Group", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("4-Get All Groups", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("5-Get Group by id", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("6-Get Group by name", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("7-Get All Groups by Teacher", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("0-Back to Main Menu", ConsoleColor.Yellow);


                                    ConsoleHelper.WriteWithColor("--- Select option ---", ConsoleColor.Cyan);

                                    // out edirik ki deyeri sonradan deyise numberin deyeri de deyissin .ref type. addresini de oturur
                                    isSuccessed = int.TryParse(Console.ReadLine(), out number);  //pars ede  bilirse deyeri goturub menimsedir  hemin o variabla ede bilmese 1 bool deyer qayarir ki menimsede bilmedim
                                    if (!isSuccessed)
                                    {
                                        ConsoleHelper.WriteWithColor("Inputted number is not correct format!", ConsoleColor.Red);
                                    }
                                    else
                                    {
                                        switch (number)
                                        {
                                            case (int)GroupOptions.CreateGroup:
                                                _groupService.Create(admin);    //admini bildirik ki hansi admindi uzerinde isleyen
                                                break;
                                            case (int)GroupOptions.UpdateGroup:
                                                _groupService.Update(admin);
                                                break;
                                            case (int)GroupOptions.DeleteGroup:
                                                _groupService.Delete();
                                                break;

                                            case (int)GroupOptions.GetAllGroups:
                                                _groupService.GetAll();

                                                break;
                                            case (int)GroupOptions.GetGroupById:
                                                _groupService.GetGroupById(admin);

                                                break;
                                            case (int)GroupOptions.GetGroupByName:
                                                _groupService.GetGroupByName();
                                                break;
                                            case (int)GroupOptions.GetGroupsByStudentCount:
                                                _groupService.GetGroupsByStudentCount();
                                                break;
                                            case (int)GroupOptions.GetAllGroupsByTeacher:
                                                _groupService.GetAllGroupsByTeacher();
                                                break;
                                            case (int)GroupOptions.BackToMainMenu:
                                                goto MainMenuDesc;
                                                break;

                                            default:
                                                ConsoleHelper.WriteWithColor("Inputted number is not exist!", ConsoleColor.Red);
                                                goto GroupDesc;
                                                break;
                                        }
                                    }

                                }
                            case (int)MainMenuOptions.Students:
                                while (true)
                                {
                                   
                                    ConsoleHelper.WriteWithColor("1-Create Student", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("2-Update Student", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("3-Delete Student", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("4-Get All Students", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("5-Get All Students by Group", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("6-Go to Main Menu", ConsoleColor.Yellow);

                                    isSuccessed = int.TryParse(Console.ReadLine(), out number);  //pars ede  bilirse deyeri goturub menimsedir  hemin o variabla ede bilmese 1 bool deyer qayarir ki menimsede bilmedim
                                    if (!isSuccessed)
                                    {
                                        ConsoleHelper.WriteWithColor("Inputted number is not correct format!", ConsoleColor.Red);
                                    }
                                    else
                                    {
                                        switch (number)
                                        {
                                            case (int)StudentOptions.CreateStudent:
                                                _studentService.Create(admin);
                                                break;
                                            case (int)StudentOptions.UpdateStudent:
                                                _studentService.Update(admin);
                                                break;
                                            case (int)StudentOptions.DeleteStudent:
                                                _studentService.Delete();
                                                break;
                                            case (int)StudentOptions.GetAllStudents:
                                                _studentService.GetAll();
                                                break;
                                            case (int)StudentOptions.GetAllStudentsByGroup:
                                                _studentService.GetAllByGroup();
                                                break;
                                            case (int)StudentOptions.BackToMainMenu:
                                                goto MainMenuDesc;

                                        }
                                    }
                                }
                             case (int)MainMenuOptions.Teachers:
                                while (true)
                                {
                                    ConsoleHelper.WriteWithColor("1-Create Teacher", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("2-Update Teacher", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("3-Delete Teacher", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("4-Get All Teachers", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("0-Go to Main Menu", ConsoleColor.Yellow);

                                    isSuccessed = int.TryParse(Console.ReadLine(), out number);  //pars ede  bilirse deyeri goturub menimsedir  hemin o variabla ede bilmese 1 bool deyer qayarir ki menimsede bilmedim
                                    if (!isSuccessed)
                                    {
                                        ConsoleHelper.WriteWithColor("Inputted number is not correct format!", ConsoleColor.Red);
                                    }
                                    else
                                    {
                                        switch (number)
                                        {
                                            case (int)TeacherOptions.CreateTeacher:
                                                _teacherService.Create();
                                                break;
                                            case (int)TeacherOptions.UpdateTeacher:
                                                _teacherService.Update();
                                                break;
                                            case (int)TeacherOptions.DeleteTeacher:
                                                _teacherService.Delete();
                                                break;
                                            case (int)TeacherOptions.GetAllTeachers:
                                                _teacherService.GetAll();
                                                break;
                                            case (int)StudentOptions.BackToMainMenu:
                                                goto MainMenuDesc;
                                        }
                                    }
                                }
                            case (int)MainMenuOptions.Logout:
                                goto Authorize;

                            default:
                                ConsoleHelper.WriteWithColor("Inputted number is not exist!", ConsoleColor.Red);
                                goto MainMenuDesc;
                        }

                    }

                }
            }
        }
    }
}

