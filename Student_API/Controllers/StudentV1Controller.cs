using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Student_API.Models;

namespace Student_API.Controllers
{
    [Route("api/v1/student")]
    public class StudentV1Controller : ApiController
    {
        List<Studentv1> student = new List<Studentv1>
        {
            new Studentv1(){ID=1,FirstName="sundram",LastName="kumar"},
            new Studentv1(){ID=2,FirstName="Shivam",LastName="kumar"},
            new Studentv1(){ID=3,FirstName="Satyam",LastName="kumar"}
        };

        
        [HttpGet]
        public IEnumerable<Studentv1> StudentList()
        {
            return student;
        }

        [Route("api/v1/student/{Id}")]
        [HttpGet]
        public Studentv1 StudentListByID(int Id)
        {
            return student.FirstOrDefault(x =>x.ID==Id);
        }

    }
}
