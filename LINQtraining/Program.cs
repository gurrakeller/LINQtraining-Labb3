using LINQtraining.Data;
using LINQtraining.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Identity.Client;
using System;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LINQtraining
{
    internal class Program
    {
        static void Main(string[] args) //IMPORTANT FOR ANYONE WHO FOUND THIS THROUGH GITHUB! When youre reading this its important to me that you know that I am aware that this is not the most efficient way to structure a program.
                                        //Infact program should only hold the entry point of the programme and the rest should be in separate classes/helper classes, but due to the nature of the 
                                        //assignment and for simplicity i have kept everything in one file.
        {
            using var context = new LINQtrainingContext();
            string MenuChoice = "";
            MainMenu(context, MenuChoice);
        }

        private static void MainMenu(LINQtrainingContext context, string MenuChoice)
        {
            Console.WriteLine("1. Hämta alla studenter\n2. Hämta alla studenter i en viss klass\n3. Lägga till nya studenter\n4. Hämta personal\n5. Lägga till ny personal\n6. Labb 4\n7. EXIT");
            MenuChoice = Console.ReadLine();

            switch (MenuChoice)
            {
                case "1":
                    Console.Clear();
                    GetStudents(context);
                    Console.WriteLine("press any key to continue..");
                    Console.ReadKey();
                    Console.Clear();
                    MainMenu(context, MenuChoice);
                    break;

                case "2":
                    Console.Clear();
                    GetFilteredStudents(context);
                    Console.WriteLine("press any key to continue..");
                    Console.ReadKey();
                    Console.Clear();
                    MainMenu(context, MenuChoice);
                    break;

                case "3":
                    Console.Clear();
                    Console.WriteLine("press any key to continue..");
                    Console.ReadKey();
                    Console.Clear();
                    MainMenu(context, MenuChoice);
                    break;

                case "4":
                    Console.Clear();
                    JobFilterMenuMethod(context);
                    Console.WriteLine("press any key to continue..");
                    Console.ReadKey();
                    Console.Clear();
                    MainMenu(context, MenuChoice);
                    break;

                case "5":
                    Console.Clear();
                    AddEmployee(context);
                    Console.WriteLine("press any key to continue..");
                    Console.ReadKey();
                    Console.Clear();
                    MainMenu(context, MenuChoice);
                    break;

                case "6":
                    Console.Clear();
                    //TODO: Labb 4 metoder här
                    Labb4Menu(context, MenuChoice);
                    Console.WriteLine("press any key to continue..");
                    Console.ReadKey();
                    Console.Clear();
                    MainMenu(context, MenuChoice);
                    break;

                case "7":
                    Console.Clear();
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Unexpected input");
                    Console.WriteLine("press any key to continue..");
                    Console.ReadKey();
                    Console.Clear();
                    MainMenu(context, MenuChoice);
                    break;
            }
        }

        //some of these methods are called "get" but they also print to console, which is their primary use for clarification.
        private static void JobFilterMenuMethod(LINQtrainingContext context)
        {
            Console.WriteLine("1. Print all Personell\n2. Print by job post");
            string jobChoice = Console.ReadLine();
            switch (jobChoice)
            {
                case "1":
                    GetEmployees(context);
                    Console.ReadKey();
                    break;
                case "2":
                    Console.WriteLine("Job post: ");
                    string jobPost = Console.ReadLine();
                    var filteredEmployees = context.Employees
                        .Where(e => e.EmployeePost == jobPost)
                        .ToList();
                    foreach (var fe in filteredEmployees)
                    {
                        Console.WriteLine($"{fe.FirstName} {fe.LastName} - {fe.EmployeePost}");
                    }
                    break;
                default:
                    Console.WriteLine("Ogiltigt val");
                    break;
            }
        }

        private static void GetStudents(LINQtrainingContext context)
        {
            var allstudents = context.Students.ToList();

            foreach (var s in allstudents)
            {
                Console.WriteLine(s.FirstName);
                Console.WriteLine();
            }
        }

        private static void GetFilteredStudents(LINQtrainingContext context)
        {
            Console.WriteLine("Class: ");
            int classChoice = int.Parse(Console.ReadLine());
            var filteredStudents = context.Classes
                .Include(c => c.Students)
                .Where(fs => fs.ClassId == classChoice)
                .ToList();

            foreach (var fs in filteredStudents)
            {
                Console.WriteLine(fs.ClassName);
                foreach (var s in fs.Students)
                {
                    Console.WriteLine($" - {s.FirstName} {s.LastName}");
                }
            }
        }

        private static void GetEmployees(LINQtrainingContext context)
        {
            var allemployees = context.Employees.ToList();
            foreach (var e in allemployees)
            {
                Console.WriteLine(e.FirstName);
                Console.WriteLine();
            }
        }

        private static void AddEmployee(LINQtrainingContext context)
        {
            Console.WriteLine("First Name: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Last Name: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Job post: ");
            string jobPost = Console.ReadLine();
            Employee newEmployee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                EmployeePost = jobPost
            };
            context.Employees.Add(newEmployee);
            context.SaveChanges();
            Console.WriteLine("Employee added successfully.");
        }

        //------------------------------LABB 4------------------------------------------
        private static void Labb4Menu(LINQtrainingContext context, string Labb4MenuChoice)
        {
            Console.WriteLine("1. Count teachers per post (i assume this is what you mean by avdelning?) " +
                "\n2. Students in specific class\n3. Show active courses\n4. Add student grade\n5. Back to main menu\n6. EXIT");
            Labb4MenuChoice = Console.ReadLine();
            switch (Labb4MenuChoice)
            {
                case "1":
                    Console.Clear();
                    CountTeachersPerPost(context);
                    Console.WriteLine("press any key to continue..");
                    Console.ReadKey();
                    Console.Clear();
                    Labb4Menu(context, Labb4MenuChoice);
                    break;
                case "2":
                    Console.Clear();
                    ClassSpecificStudents(context);
                    Console.WriteLine("press any key to continue..");
                    Console.ReadKey();
                    Console.Clear();
                    Labb4Menu(context, Labb4MenuChoice);
                    break;
                case "3":
                    Console.Clear();
                    PrintActiveCourses(context);
                    break;

                case "4":
                    Console.Clear();
                    SetStudentGradeFromConsoleAsync(context).Wait();
                    Console.WriteLine("press any key to continue..");
                    Console.ReadKey();
                    Labb4Menu(context, Labb4MenuChoice);
                    Console.Clear();
                    break;

                case "5":
                    string MenuChoice = "";
                    Console.Clear();
                    MainMenu(context, MenuChoice);
                    break;

                case "6":
                    Console.Clear();
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Unexpected input");
                    Console.WriteLine("press any key to continue..");
                    Console.ReadKey();
                    Console.Clear();
                    Labb4Menu(context, Labb4MenuChoice);
                    break;
            }
        }

        private static void CountTeachersPerPost(LINQtrainingContext context)
        {
            var teacherCounts = context.Employees
                .GroupBy(e => e.EmployeePost)
                .Select(g => new
                {
                    JobPost = g.Key,
                    Count = g.Count()
                })
                .ToList();
            foreach (var tc in teacherCounts)
            {
                Console.WriteLine($"{tc.JobPost}: {tc.Count}");
            }
        }

        private static void ClassSpecificStudents(LINQtrainingContext context)
        {
            Console.WriteLine("What class would you like to check?");
            int classId = int.Parse(Console.ReadLine());

            var students = context.Students
                .Where(s => s.Classes.Any(c => c.ClassId == classId))
                .Select(s => new
                {
                    StudentName = s.FirstName + " " + s.LastName, //<-- matar bara in hela namnet ist så slipper jag krånga med cw'n här nere, ser dock lite konstigt ut för man måste göra ett manuellt mellanrun X(
                    Courses = s.Grades.Select(g => new
                    {
                        CourseName = g.Course.CourseName,
                        Grade = g.Grade1
                    })
                })
                .ToList();

            foreach (var student in students)
            {
                Console.WriteLine(student.StudentName);

                foreach (var course in student.Courses)
                {
                    Console.WriteLine($"  {course.CourseName}: {course.Grade}");
                }

                Console.WriteLine();
            }
        }

        private static void PrintActiveCourses(LINQtrainingContext context)
        {
            var activeCourses = context.Courses
                .Where(c => c.Students.Any())
                .ToList();

            Console.WriteLine("Current courses with students enrolled (i assume this is what you mean by active (?):");
            foreach (var ac in activeCourses)
                Console.WriteLine($"-{ac.CourseName}\n");
        }

        private static async Task SetStudentGradeFromConsoleAsync(LINQtrainingContext context)
        {
            Console.Write("Student ID: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Invalid Student ID.");
                return;
            }

            Console.Write("Course ID: ");
            if (!int.TryParse(Console.ReadLine(), out int courseId))
            {
                Console.WriteLine("Invalid Course ID.");
                return;
            }

            Console.Write("Grade: ");
            if (!int.TryParse(Console.ReadLine(), out int grade))
            {
                Console.WriteLine("Invalid grade.");
                return;
            }
            await AddGradeWithTransaction(context, studentId, courseId, grade);
        }

        private static async Task AddGradeWithTransaction(LINQtrainingContext context, int StudentID, int courseID, int Grade1)
        {
            //made it async so we can use await to check if there already exists a grade, and in that case edit that one instead of creating a new one risking douplicate grades.
            //also so i can seperate the info gathering from console and the actual transaction logic. And yes i realise i shoudev done this in the entire thing
            //but realistically only you will be seeing this anyways Aldor so i guess its fine x(.
            //Its now 03:00 and im tired ok leave me alone. Atleast with this i hope im finally caught up again.
            using var transaction = context.Database.BeginTransaction();

            try
            {
                var newGrade = await context.Grades
                    .FirstOrDefaultAsync(g => g.StudentId == StudentID && g.CourseId == courseID);
                if (newGrade != null)
                {
                    newGrade.Grade1 = Grade1;
                }
                else
                {
                    context.Grades.Add(new Grade
                    {
                        StudentId = StudentID,
                        CourseId = courseID,
                        Grade1 = Grade1,
                        EmployeeId = 1,
                        GradeDate = DateOnly.FromDateTime(DateTime.Now)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                transaction.Rollback();
                return;
            }
        }
    }
}
