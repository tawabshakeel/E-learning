using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebApplication1.Models;
using System.Dynamic;
using System.Net.Mail;
using System.Text;


namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {

        Database1Entities7 db = new Database1Entities7();
        public ActionResult Index()
        {
            return View();

        }
        public ActionResult logout()
        {
            Session["id"] = null;
            return RedirectToAction("/");
        }
        public ActionResult basicProfile()
        {
            return View();
        }
        public ActionResult Display()
        {
            return View(db.Student.ToList());

        }
      

        public ActionResult Update(int id)
        {
            Student st = db.Student.Find(id);
            return View(st);

        }
        public ActionResult UpdateTeacher(int id)
        {
            Teacher st = db.Teacher.Find(id);
            return View(st);

        }
        [HttpPost]
        public ActionResult updateConfirm(int id)
        {
            Student st = db.Student.Find(id);
            string rollNo = Request["Email"];
            string fname = Request["First_name"];
            string lname = Request["Last_Name"];
            string Address = Request["Adress"];
            string phone = Request["Phone_number"];

            int phn = Int32.Parse(phone);

            //st.Roll_No_ = rollNo;
            st.First_name = fname;
            st.Last_Name = lname;
            st.Adress = Address;
            st.Phone_number = phn;

            db.SaveChanges();
            return RedirectToAction("admin");

        }

        public ActionResult updateConfirmTeacher(int id)
        {
            Teacher st = db.Teacher.Find(id);
            string rollNo = Request["Roll_ENo_"];
            string fname = Request["First_name"];
            string lname = Request["Last_Name"];
            string Address = Request["Adress"];
            string phone = Request["Phone_number"];

            int phn = Int32.Parse(phone);

            //st.Roll_No_ = rollNo;
            st.First_name = fname;
            st.Last_Name = lname;
            st.Adress = Address;
            st.Phone_number = phn;

            db.SaveChanges();
            return RedirectToAction("admin");

        }

        public ActionResult AddStudent()
        {

            return View();

        }
        public ActionResult upload_vid()
        {

            return View();

        }
        public ActionResult forgetPass()
        {
            return View();
        }

 [HttpPost]
        public ActionResult saveVideo()
        {
              
            if (Request.Files.Count > 0)
            {

                var httpPostedFile = Request.Files[0];
                if (httpPostedFile != null)
                {
                    var uploadFilesDir = System.Web.HttpContext.Current.Server.MapPath("~/Content/Videos");
                    if (!Directory.Exists(uploadFilesDir))
                    {
                        Directory.CreateDirectory(uploadFilesDir);
                    }
                    var fileSavePath = Path.Combine(uploadFilesDir, httpPostedFile.FileName);
                    httpPostedFile.SaveAs(fileSavePath);
                }
                string name = Request.Files[0].FileName;
                string teacher_id = Request["id"];
                string cid = Request["course_id"];
                TeachersVideo tv = new TeachersVideo();
                tv.TID = teacher_id;
                tv.vname = name;
                tv.CID = cid;
                db.TeachersVideo.Add(tv);
                db.SaveChanges();
                Session["check"] = 1;

                return View("teacherProfile", db.TeachersVideo.Where(n => n.TID == teacher_id).ToList());
                //return RedirectToAction("teacherProfile");


               

            }
            else
            {
                return Content("no files");
            }
            
     
    
        }
 //public ActionResult teacherProfile(string teacher_id)
 //   {
      
 //              return View(db.TeachersVideo.Where(n =>n.TID==teacher_id).ToList());


 //   }
    public ActionResult updatePass()
    {
        string email = Request["email"];
        string pass = Request["pass"];
        string type = Request["type"];
        if (type.Equals("0"))
        {
            Teacher result = (from t in db.Teacher
                              where t.Email == email
                              select t).SingleOrDefault();

            result.pass = pass;

            db.SaveChanges();
        }
        else
        {
            Student result = (from s in db.Student
                              where s.Email == email
                              select s).SingleOrDefault();

            result.pass = pass;

            db.SaveChanges();
        }
        return Content("Successfully updated");

     }
    public ActionResult emailcontact()
    {
        string email = Request["email"];
        string message = Request["msg"];
        string phone = Request["phone"];
        string subject = Request["Subject"];
        string body="<body>Message:"+message+"<br>Phone:"+phone+"</body>";
        SmtpClient client = new SmtpClient();
        client.Port = 587;
        client.Host = "smtp.gmail.com";
        client.EnableSsl = true;
        client.Timeout = 10000;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential("otexmailer@gmail.com", "otexmailer123");

        MailMessage mm = new MailMessage(email,"tawab2013@gmail.com",subject,message );
        mm.BodyEncoding = UTF8Encoding.UTF8;
        mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

        client.Send(mm);

        return Content("success");
    }


        [HttpPost]
        public ActionResult addStudentconfirm(Student s)
        {
           string type= Request["type"];
           if (Request.Files.Count > 0)
           {
               var httpPostedFile = Request.Files[0];
               if (httpPostedFile != null)
               {

                   // Validate the uploaded file if you want like content length(optional)

                   // Get the complete file path
                   var uploadFilesDir = System.Web.HttpContext.Current.Server.MapPath("~/Content/profiles");
                   if (!Directory.Exists(uploadFilesDir))
                   {
                       Directory.CreateDirectory(uploadFilesDir);
                   }
                   var fileSavePath = Path.Combine(uploadFilesDir, httpPostedFile.FileName);

                   // Save the uploaded file to "UploadedFiles" folder
                   httpPostedFile.SaveAs(fileSavePath);
               }


           }
           if (type.Equals("1"))
            {
                string email = Request["email"];
                string fname = Request["First_name"];
                string lname = Request["Last_Name"];
                string Address = Request["Adress"];
                string phone = Request["Phone_number"];
                string pass = Request["Passowrd"];
                string img = Request.Files[0].FileName;
                int phn = Int32.Parse(phone);
                Student sc = new Student();
                sc.Email = email;
                sc.First_name = fname;
                sc.Last_Name = lname;
                sc.Adress = Address;
                sc.Phone_number = phn;
                sc.profile = img;
                sc.pass = pass;
                db.Student.Add(sc);
                db.SaveChanges();
                Session["id"] = sc.SID;
              
                Session["profile"] = sc.profile;
                Session["name"] = sc.First_name;

                return View("StudentCourse",db.courses.ToList());
            }
           else
           {

              
            

               string email = Request["email"];
               string fname = Request["First_name"];
               string lname = Request["Last_Name"];
               string Address = Request["Adress"];
               string phone = Request["Phone_number"];
               string pass = Request["Passowrd"];
               string img = Request.Files[0].FileName;
               int phn = Int32.Parse(phone);
               Teacher tc = new Teacher();
               tc.Email = email;
               tc.First_name = fname;
               tc.Last_Name = lname;
               tc.Adress = Address;
               tc.Phone_number = phn;
               tc.profile = img;
               tc.pass = pass;
               db.Teacher.Add(tc);
               db.SaveChanges();
               Session["id"] = tc.TID;
               Session["profile"] = tc.profile;
               Session["name"] = tc.First_name;

               return View("TeacherCourse", db.courses.ToList());
               
           }

           
            
        }
        [HttpPost]
        public ActionResult courseSelected(string[] courses)
        {
            string course_id = Request["course"];
            string student_id = Request["user_id"];
           
           // int[] nums = Array.ConvertAll(course_id.Split(','), int.Parse);
            for (int i = 0; i < courses.Length; i++)
            {
                StudentCourses coursess = new StudentCourses();
                coursess.SID = student_id;
                coursess.CID = courses[i];
                db.StudentCourses.Add(coursess);
                 db.SaveChanges();
                
            }

            return Content(student_id);

        }
        public ActionResult teacherCourse()
        {
            
            string teacher_id = Request["Teacher_id"];
            string course_id = Request["courses"];
            int id = Int32.Parse(teacher_id);

            Teacher result = (from t in db.Teacher
                              where t.TID == id
                              select t).SingleOrDefault();

            result.code = course_id;
            db.SaveChanges();
            Session["code"] = course_id;
            Session["check"] = 0;

            return View("teacherProfile", db.TeachersVideo.Where(n => n.TID == teacher_id).ToList());
        }



        public ActionResult Delete(int id)
        {
            Student st = db.Student.Find(id);
            db.Student.Remove(st);
            db.SaveChanges();


            return RedirectToAction("admin");

        }
        public ActionResult DeleteTeacher(int id)
        {
            Teacher st = db.Teacher.Find(id);
            db.Teacher.Remove(st);
            db.SaveChanges();


            return RedirectToAction("admin");

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Courses()
        {
           //ViewBag.Message = "Your courses page.";

            return View(db.courses.ToList());
        }


        public ActionResult Fees()
        {
            ViewBag.Message = "Your fees page.";

            return View();
        }
        public ActionResult admin()
        {
            dynamic cmp = new ExpandoObject();
            cmp.Student = db.Student.ToList();
            cmp.Teacher = db.Teacher.ToList();

              return View(cmp);
        }
        public ActionResult addCourse(courses c)
        {
            db.courses.Add(c);
            db.SaveChanges();
            return RedirectToAction("admin");
        }
        public ActionResult studentProfile()
        {
            string student_id = Request["student_id"];
            var result = from t1 in db.StudentCourses
                         join t2 in db.TeachersVideo on t1.CID equals t2.CID
                         where t1.SID == student_id
                         select t2;
            List<TeachersVideo> teacherVids = result.ToList<TeachersVideo>();


            if (result.ToList().Count > 0)
            {
                Session["check"] = 1;
            }
            else
            {
                Session["check"] = 0;
            }
            return View(teacherVids);
        }
        
        public ActionResult login()
        {
            string type=Request["type"];
            string email = Request["email"];
            string password = Request["password"];
            if (type.Equals("1"))
            {
                var items = db.Student.Where(i => i.pass==password && i.Email==email );

                if(!items.Any()){
                   return RedirectToAction("/");
                    }
                else
                {
                    
                   
                   var data= db.Student.Where(n => n.Email==email).ToList();
                   if (data.Count == 1)
                   {
                       Session["id"]=data[0].SID;
                       Session["Email"]=data[0].Email;
                       Session["name"]=data[0].First_name;
                       Session["profile"]=data[0].profile;
                       
                   }
                   string s = Convert.ToString(data[0].SID);
                 var result = from t1 in db.StudentCourses
                                join t2 in db.TeachersVideo on t1.CID equals t2.CID
                                where t1.SID == s
                                select t2;
                 List<TeachersVideo> teacherVids = result.ToList<TeachersVideo>();

           
                   if (result.ToList().Count > 0)
                   {
                       Session["check"] = 1;
                   }
                   else
                   {
                       Session["check"] = 0;
                   }
                   return View("studentProfile", teacherVids);
                }

                    
            }
            else{
                var res = db.Teacher.Where(i => i.pass == password && i.Email == email);
                 if (!res.Any())
                {
                    return RedirectToAction("/");
                }
                else
                {
                    var teac = db.Teacher.Where(n => n.Email == email).ToList();
                    if (teac.Count == 1)
                    {   Session["id"]=teac[0].TID;
                        Session["Email"] = teac[0].Email;
                        Session["name"] = teac[0].First_name;
                        Session["profile"] = teac[0].profile;
                        
                       

                    }
                    string s = Convert.ToString(teac[0].TID);
                    var result=db.TeachersVideo.Where(n => n.TID == s).ToList();
                    if (result.Count > 0)
                    {
                        Session["check"] = 1;
                    }
                    else
                    {
                        Session["check"] = 0;
                    }
                    return View("teacherProfile", db.TeachersVideo.Where(n => n.TID == s).ToList());
                }
            }

        }

    }
   
}