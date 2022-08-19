using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LinqSnippets
{


    public class Snippets
    {
        static public void BasicLinQ()
        {
            string[] cars =
            {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat Leon"
            };

            // 1. SELECT * of cars (SELECT ALL CARS)
            var carList = from car in cars select car;
            foreach (var car in carList)
            {
                Console.WriteLine(car);
            }

            // 2. SELECT WHERE car in Audi (SELECT AUDIs)
            var audiList = from car in cars where car.Contains("Audi") select car;

            foreach (var audi in audiList)
            {
                Console.WriteLine(audi);
            }


        }

        // Number Examples

        static public void LinqNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9};

            // Each number multiplied by 3.
            // Take all numbers, but 9.
            // Order by ascending value.


            var processedNumberList =
                numbers
                    .Select(num => num * 3) // { 3, 6, 9, etc.}
                    .Where(num => num != 9) // { all but the 9}
                    .OrderBy(num => num); // at the end, we order ascending.

        }

        static public void SearchExamples()
        {
            List<string> textList = new List<string>
            {
                "a",
                "bx",
                "c",
                "d",
                "e",
                "cj",
                "f",
                "c",
            };

            // 1. First of all elements.
            var first = textList.First();

            // 2. First element that is "c".
            var cText = textList.First(text => text.Equals("c"));

            // 3. Firs element that contains "j".
            var jText = textList.First(text => text.Contains("j"));

            // 4. First element that contains "z" or default.
            var firstOrDefaultText = textList.FirstOrDefault(text => text.Contains("z")); // "" or first element that contains "z".

            // 5. Last element that contains "z" or default.
            var lastOrDefaultText = textList.LastOrDefault(text => text.Contains("z")); // "" or last element that contains "z".

            // 6. Single Values.
            var uniqueText = textList.Single();
            var uniqueOrDefaultText = textList.SingleOrDefault();


            int[] evenNumbers = { 0, 2, 4, 8 };
            int[] otherEvenNumbers = { 0, 2, 6 };

            // Obtain { 4, 8 }
            var myEvenNumbers = evenNumbers.Except(otherEvenNumbers); // Usa dos secuencias de valores y elimina lo que le pidamos.

        }

        static public void MultipleSelects()
        {
            // SELECT MANY
            string[] myOpinions =
            {
                "Opinion 1, text 1",
                "Opinion 2, text 2",
                "Opinion 3, text 3",
            };

            var myOpinionSelection = myOpinions.SelectMany(opinion => opinion.Split(","));

            var Enterprises = new[]
            {
                new Enterprise()
                {
                    Id = 1,
                    Name = "Enterprise 1",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id = 1,
                            Name = "Borja Iglesias",
                            Email = "elpanda@betis.com",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id = 2,
                            Name = "Juanmi Nazario",
                            Email = "orei@betis.com",
                            Salary = 2500
                        },

                        new Employee
                        {
                            Id =3,
                            Name = "Sergio Canales",
                            Email = "elmago@betis.com",
                            Salary = 2500
                        }
                    }
                },

                new Enterprise()
                {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees = new []
                    {
                        new Employee
                        {
                            Id = 1,
                            Name = "Paquito Navarro",
                            Email = "paquitonavarro@wpt.com",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id = 2,
                            Name = "Juan Lebrón",
                            Email = "lebron@wpt.com",
                            Salary = 3000
                        },

                        new Employee
                        {
                            Id =3,
                            Name = "Alejandro Galán",
                            Email = "alegalan@wpt.com",
                            Salary = 2000
                        }
                    }
                }
            };


            // Obtain all Employees of all Enterprises.
            var employeeList = Enterprises.SelectMany(enterprise => enterprise.Employees);

            // Know if any list is empty.
            bool hasEnterprises = Enterprises.Any();

            bool hasEmployees = Enterprises.Any(enterprise => enterprise.Employees.Any());

            // All Enterprises at least has an employee with more than 1000€ of salary.
            bool hasEmployeeWithSalaryMoreThanOrEqual1000 =
                Enterprises.Any(enterprise =>
                enterprise.Employees.Any(Employee => Employee.Salary > 1000));


        }

        static public void linqCollections()
        {
            var firstList = new List<string>() { "a", "b", "c" };
            var secondList = new List<string>() { "a", "c", "d" };

            // INNER JOIN -> shared elements by two lists: in this case are { "a", "c" }
            var commonResult = from element in firstList
                               join secondElement in secondList
                               on element equals secondElement
                               select new { element, secondElement };


            // One more way for the INNER JOIN.
            var commonResult2 = firstList.Join(
                secondList,
                element => element,
                secondElement => secondElement,
                (element, secondElement) => new { element, secondElement }
            );

            // OUTER JOIN - LEFT -> when we have no interest in the common elements and right list, only in the list on the left.
            var leftOuterJoin = from element in firstList
                                join secondElement in secondList
                                on element equals secondElement
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where element != temporalElement
                                select new { Element = element };

            // SIMPLIFIED - OUTER JOIN - LEFT
            var leftOuterJoin2 = from element in firstList
                                 from secondElement in secondList.Where(s => s == element).DefaultIfEmpty()
                                 select new { Element = element, SecondElement = secondElement };



            // OUTER JOIN - RIGHT
            var rightOuterJoin = from secondElement in secondList
                                 join element in firstList
                                 on secondElement equals element
                                 into temporalList
                                 from temporalElement in temporalList.DefaultIfEmpty()
                                 where secondElement != temporalElement
                                 select new { Element = secondElement };

            // UNION -> The opposite of INNER JOIN: gets both outer joins: left & right
            var unionList = leftOuterJoin.Union(rightOuterJoin);

        }

        static public void SkipTakeLinq()
        {
            var myList = new[]
            {
                1,2,3,4,5,6,7,8,9
            };

                // SKIP ELEMENTS FUNCTIONS

            var skipTwoFirstValues = myList.Skip(2); // {3,4,5,6,7,8,9}

            var skipThreeFirstValues = myList.Skip(3); // {4,5,6,7,8,9}

            var skipLastTwoValues = myList.SkipLast(2); // {1,2,3,4,5,6,7}

            var skipWhile = myList.SkipWhile(num => num <= 4); // {5,6,7,8}


            // TAKE ELEMENTS FUNCTIONS

            var takeFirstTwoValues = myList.Take(2); // {1,2}

            var takeLastTwoValues = myList.TakeLast(2); // {1,2,3,4,5,6,7}

            var takeWhileSmallerThan4 = myList.TakeWhile(num => num < 4); // {1,2,3}

        }
        // TODO:
        // VARIABLES
        // ZIP
        // REPEAT
        // ALL
        // AGGREGATE
        // DISCTINCT
        // GROUP BY



    }
}