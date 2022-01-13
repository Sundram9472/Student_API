using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Build.Tasks;
using StudentDataAccess;

namespace Student_API.Controllers
{
    [EnableCorsAttribute("http://localhost:58176", "*", "*")]
    [BasicAuthentication]
    [RoutePrefix("api/Student")]
    public class StudentController : ApiController
    {
      
        public HttpResponseMessage Get()
        {
            String userName = Thread.CurrentPrincipal.Identity.Name;
            using (Intern_DBEntities _DB = new Intern_DBEntities())
            {
               switch(userName.ToLower())
                {
                    case "cgc":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            _DB.Student_Details_Sundram.Where(x=>x.Student_UniversityName.ToLower()=="cgc").ToList());
                    case "cec":
                        return Request.CreateResponse(HttpStatusCode.OK,
                              _DB.Student_Details_Sundram.Where(x => x.Student_UniversityName.ToLower() == "cec").ToList());

                    default:
                        //return Request.CreateResponse(HttpStatusCode.BadRequest,
                        //                "College Not Found From Specified College");
                        return Request.CreateResponse(HttpStatusCode.OK, _DB.Student_Details_Sundram.ToList());
                        
                }
            }
        }
        [Route("{id}",Name ="GetStudentById")]
        [HttpGet]
        public HttpResponseMessage  GetStudentByID(int ID)
        {
            using (Intern_DBEntities _DB = new Intern_DBEntities())
            {
                var Data = _DB.Student_Details_Sundram.Where(x => x.Student_ID == ID).FirstOrDefault();
                if(Data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Data);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Student With ID ="+ID.ToString()+" Not Found");
                }
            }
        }

        
        [HttpPost]
        public HttpResponseMessage AddedStudent([FromBody] Student_Details_Sundram model,[FromUri] Student_Details_Sundram model1)
        {
            try
            {
                using (Intern_DBEntities DB = new Intern_DBEntities())
                {
                    DB.Student_Details_Sundram.Add(model);
                    DB.SaveChanges();
                    var Message = Request.CreateResponse(HttpStatusCode.Created,"Student Added !");
                    Message.Headers.Location = new Uri(Url.Link("GetStudentById",new { id = model.Student_ID }));
                    return Message;
                }
            }
            catch(Exception Ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Ex);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public HttpResponseMessage DeleteStudent(int ID)
        {
            try
            {
                using (Intern_DBEntities DB = new Intern_DBEntities())
                {
                    var Data = DB.Student_Details_Sundram.Where(x => x.Student_ID == ID).FirstOrDefault();
                    if (Data != null)
                    {
                        DB.Student_Details_Sundram.Remove(Data);
                        DB.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Student ID :-" + ID + " Remove Succefully".ToString());
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Student Data Not Found With ID " + ID.ToString());
                    }
                }
            }
            catch(Exception Ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Ex);
            }
        }

        [HttpPut]
        public HttpResponseMessage AddedStudent(int ID,[FromBody]Student_Details_Sundram model)
        {
            try
            {
                using (Intern_DBEntities DB = new Intern_DBEntities())
                {
                    var Data = DB.Student_Details_Sundram.Where(x => x.Student_ID == ID).FirstOrDefault();
                    if(Data != null)
                    {
                        Data.Student_FirstName = model.Student_FirstName;
                        Data.Student_LastName = model.Student_LastName;
                        Data.Student_MailId = model.Student_MailId;
                        Data.Student_Roll_Number = model.Student_Roll_Number;
                        Data.Student_UniversityName = model.Student_UniversityName;
                        Data.Student_Preffered_ProgrammingLanguage = model.Student_Preffered_ProgrammingLanguage;
                        DB.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Student ID :-" + ID + " Update Succefully".ToString());
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Student Data Not Found With ID " + ID.ToString());
                    }
                }
            }
            catch(Exception Ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Ex);
            }
        }
    }
}
