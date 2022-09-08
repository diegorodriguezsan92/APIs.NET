using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Tracing;
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

            var takeLastTwoValues = myList.TakeLast(2); // {9,10}

            var takeWhileSmallerThan4 = myList.TakeWhile(num => num < 4); // {1,2,3}

        }
        // TODO:
        // PAGING with Skip & Take:
        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPerPage) // T is a generic value (bools, number, strings). This funcionality we can set a collection, page number and results per page.
        {
            int startIndex = (pageNumber - 1) * resultsPerPage; // takes page 1 and multiply by 10. Shows the first 10 elements.
            return collection.Skip(startIndex).Take(resultsPerPage); // skips the first 10 elements and shows the second 10 elements, from 11th to 20th.

        }


        // VARIABLES
        static public void LinqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var aboveAverage = from number in numbers // obtén todos los números de la lista de númreos siempre y cuando su cuadrado sea mayor que la media de la lista de números.
                               let average = numbers.Average()    // almacenamos variables
                               let nSquare = Math.Pow(number, 2)
                               where nSquare > average
                               select number; // obtenemos dichos números que cumplan las condiciones dadas

            Console.WriteLine("Average: {0}", numbers.Average()); // volvemos a declarar numbers.Average aunque ya lo habíamos declarado antes porque estaba en un let y significa que está en una variable local dentro de una consulta.

            foreach (int number in aboveAverage)
            {
                Console.WriteLine("Number: {0} Square: {1} ", number, Math.Pow(number, 2));
            }
        }

        // ZIP

        static public void ZipLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] stringNumbers = { "one", "two", "three", "four", "five" };

            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => number + "=" + word);   // { "1 = one", "2 = two" ...}

        }

        // REPEAT & RANGE

        static public void repeatRangeLinq()
        {
            // Generate a collection of values from 1 to 1000 --> Range
            IEnumerable<int> first1000 = Enumerable.Range(0, 1000);

            // var aboveAverage = from number in first1000
            // select number;

            // Repeat a value N times
            IEnumerable<string> fiveXs = Enumerable.Repeat("X", 5); // {"X", "X", "X", "X", "X"}

        }

        static public void studentsLinq()
        {
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Martin",
                    Grade = 90,
                    Certified = false,
                },
                new Student
                {
                    Id = 2,
                    Name = "Amparo",
                    Grade = 60,
                    Certified = true,
                },
                new Student
                {
                    Id = 3,
                    Name = "Rosa",
                    Grade = 45,
                    Certified = true,
                },
                new Student
                {
                    Id = 4,
                    Name = "Juan",
                    Grade = 95,
                    Certified = true,
                },
                new Student
                {
                    Id = 5,
                    Name = "Pilar",
                    Grade = 86,
                    Certified = false,
                }

            };

            var certifiedStudents = from student in classRoom
                                     where student.Certified
                                     select student;

            var notCertifiedStudents = from student in classRoom
                                       where student.Certified == false
                                       select student;

            var approvedStudentsName = from student in classRoom
                                   where student.Grade >= 50 && student.Certified == true
                                   select student.Name; // solo nos muestra el nombre del alumno, no su nota y certificación.

        }

        // ALL

        static public void AllLinq()
        {
            var numbers = new List<int>() { 1, 2, 3, 4, 5 };

            bool allAreSmallerThan10 = numbers.All(x => x < 10);    // consultamos si todos son menores que 10 // true

            bool allAreBiggerOrEqualThan2 = numbers.All(x => x >= 2); // consultamos si todos son mayores que 2 // false


            var emptyList = new List<int>();
            bool allNumbersAreBiggerThan0 = numbers.All(x => x >= 0);   // true porque cuando una lista está vacía nos devuelve que todos los números de la lista son mayores que 0, porque no hay ningún número que rompa la regla, de hecho no hay ningún número en la lista.

        }

            // AGGREGATE
        static public void aggregateQueries()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Sum all numbers
            int sum = numbers.Aggregate((prevSum, current) => prevSum + current);
            // 0, 1 -> 1 almacenado
            // 1, 2 -> 3 almacenado
            // 3, 4 -> 7 almacenado
            // etc.


            string[] words = { "Hello, ", "my ", "name ", "is ", "Dongle." }; // Hello, my name is Diego.
            string greeting = words.Aggregate((prevGreeting, current) => prevGreeting + current);
            // Hello,
            // Hello, my
            // Hello, my name
            // etc

        }


        // DISCTINCT
        static public void distinctValues()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 4, 3, 2, 1 };
            IEnumerable<int> distinctValues = numbers.Distinct();
        }

        // GROUP BY (it shows first the list that DOESN'T meet the condition.
        static public void groupByExamples()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Obtain only even numbers and generate two groups
            var grouped = numbers.GroupBy(x => x % 2 == 0);

            foreach (var group in grouped)
            {
                foreach (var value in group)
                {
                    Console.WriteLine(value);   // 1, 3, 5, 7, 9 ... 2, 4, 6, 8 (first the odds and then the even numbers). We will have two groups: The gruoup that doesnt fits the condition and the group that does fits the condition.
                }
            }


            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Martin",
                    Grade = 90,
                    Certified = false,
                },
                new Student
                {
                    Id = 2,
                    Name = "Amparo",
                    Grade = 60,
                    Certified = true,
                },
                new Student
                {
                    Id = 3,
                    Name = "Rosa",
                    Grade = 45,
                    Certified = true,
                },
                new Student
                {
                    Id = 4,
                    Name = "Juan",
                    Grade = 95,
                    Certified = true,
                },
            };

            var certifiedQuery = classRoom.GroupBy(student => student.Certified); // this query asks for the certified students.
            var certifiedQueryAndMoreThan70 = classRoom.GroupBy(student => student.Certified && student.Grade >= 70); // this query asks for the certified students who has 70 or more grades.

            // We obtain two groups:
            // 1. Not certified students.
            // 2. Certified students.

            foreach (var group in certifiedQuery)
            {
                Console.WriteLine("___________ {0} ___________" + group.Key);
                foreach (var student in group)
                {
                    Console.WriteLine(student.Name);
                }
            }


        }


        static public void relationsLinq()
        {
            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Title = "My first post",
                    Content = "My first content",
                    Created = DateTime.Now,
                    Comment = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 1,
                            Created = DateTime.Now,
                            Title ="My first comment",
                            Content = "My first content"
                        },
                        new Comment()
                        {
                            Id = 2,
                            Created = DateTime.Now,
                            Title ="My second comment",
                            Content = "My second content"
                        }
                    }
                },

                new Post()
                {
                    Id = 2,
                    Title = "My second post",
                    Content = "My second content",
                    Created = DateTime.Now,
                    Comment = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 3,
                            Created = DateTime.Now,
                            Title ="My other comment",
                            Content = "My new content"
                        },
                        new Comment()
                        {
                            Id = 4,
                            Created = DateTime.Now,
                            Title ="My other comment",
                            Content = "My new content"
                        }
                    }


                }
            };


        var comentsContent = posts.SelectMany(
            post => post.Comment, 
            (post, comment) => new { PostId = post.Id, CommentContent = comment.Content });     // deploys an Id and its comment: Id 1 --> Comment 1, etc.






        }









    }
}