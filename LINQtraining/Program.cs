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
        static void Main(string[] args)
        {
            using var context = new LINQtrainingContext();
            Console.WriteLine("1. Hämta alla studenter\n2. Hämta alla studenter i en viss klass\n3. Lägga till nya studenter\n4. Hämta personal\n5. Lägga till ny personal");
            string MenuChoice = Console.ReadLine();

            switch (MenuChoice)
            {
                case "1":
                    GetStudents(context);
                    Console.ReadKey();
                    break;

                case "2":
                    GetFilteredStudents(context);
                    Console.ReadKey();
                    break;

                case "3":
                    AddStudent(context);
                    Console.ReadKey();
                    break;

                case "4":
                    JobFilterMenuMethod(context);
                    break;

                case "5":
                    AddEmployee(context);
                    break;

                default:
                    Console.WriteLine("Ogiltigt val");
                    break;
            }
        }

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
                    Console.ReadKey();
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

            foreach (var fs in filteredStudents) { 
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

        private static void AddStudent(LINQtrainingContext context)
        {
            Console.WriteLine("First Name: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Last Name: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Ssn: ");
            int ssn = int.Parse(Console.ReadLine());
            Student newStudent = new Student
            {
                FirstName = firstName,
                LastName = lastName,
                Ssn = ssn
            };
            context.Students.Add(newStudent);
            context.SaveChanges();
            Console.WriteLine("Student added successfully.");
        }
    }
}

