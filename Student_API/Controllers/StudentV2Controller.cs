using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Student_API.Models;

namespace Student_API.Controllers
{
    [Route("api/v2/student")]
    public class StudentV2Controller : ApiController
    {
        List<Studentv2> student = new List<Studentv2>
        {
            new Studentv2(){ID=1,FirstName="sundram",LastName="kumar",GMail="test1@gmail.com"},
            new Studentv2(){ID=2,FirstName="Shivam",LastName="kumar",GMail="test2@gmail.com"},
            new Studentv2(){ID=3,FirstName="Satyam",LastName="kumar",GMail="test3@gmail.com"}
        };


        [HttpGet]
        public IEnumerable<Studentv2> StudentList()
        {
            return student;
        }

        [Route("api/v2/student/{Id}")]
        [HttpGet]
        public Studentv2 StudentListByID(int Id)
        {
            return student.FirstOrDefault(x => x.ID == Id);
        }

        [HttpDelete]
        public HttpResponseMessage StudentRecordDelte(int Id)
        {
            var data = student.Where(x => x.ID == Id).FirstOrDefault();
            student.Remove(data);
            return Request.CreateResponse(HttpStatusCode.OK,"Remove Data Succesful");
        }
    }
}
