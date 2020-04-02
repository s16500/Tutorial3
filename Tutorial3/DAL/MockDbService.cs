using Tutorial3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tutorial3.DAL
{
    public class MockDbService : IDbService
    {

        private static IEnumerable<Student> _students;

        static MockDbService()
        {


            _students = new List<Student> {

            new Student{idStudent=1,firstName="Burak",lastName="Bakir"},
            new Student{idStudent=2,firstName="Cem",lastName="Habiboglu"},
            new Student{idStudent=3,firstName="Mustafa",lastName="Erdem"}

            };
        }
        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }
    }
}
