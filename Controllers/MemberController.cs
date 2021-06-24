using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ASP_CORE_MVC.Models;
using System.Text.Json;
using System.IO;
using OfficeOpenXml;

namespace ASP_CORE_MVC.Controllers
{
    public class MemberController : Controller
    {
        
        static List<Member> memberList = new List<Member>{
            new Member{FirstName = "Ky",
                        LastName = "Nguyen Khac",
                        Gender = "Male",
                        DateOfBirth = new DateTime(1999, 11, 12),
                        PhoneNumber = "0222.222.222",
                        BirthPlace = "Ha Noi",                       
                        IsGraduated = false},
            new Member{FirstName = "Trang",
                        LastName = "Huyen Nguyen",
                        Gender = "Female",
                        DateOfBirth = new DateTime(2002, 05, 21),
                        PhoneNumber = "0222.222.222",
                        BirthPlace = "Hai Phong",                      
                        IsGraduated = false},
            new Member{FirstName = "Tuan",
                        LastName = "Trinh Dat",
                        Gender = "Male",
                        DateOfBirth = new DateTime(1994, 01, 15),
                        PhoneNumber = "0222.222.222",
                        BirthPlace = "Bac Ninh",                   
                        IsGraduated = false},
            new Member{FirstName = "Cong",
                        LastName = "Nguyen Van",
                        Gender = "Male",
                        DateOfBirth = new DateTime(1996, 08, 12),
                        PhoneNumber = "0222.222.222",
                        BirthPlace = "Ha Noi",
                        IsGraduated = false},
            new Member{FirstName = "Phuoc",
                        LastName = "Hoang Nhat",
                        Gender = "Male",
                        DateOfBirth = new DateTime(1998, 06, 18),
                        PhoneNumber = "0222.222.222",
                        BirthPlace = "Bac Ninh",                       
                        IsGraduated = false},
            new Member{FirstName = "Long",
                        LastName = "Thang Bao",
                        Gender = "Male",
                        DateOfBirth = new DateTime(2000, 07, 12),
                        PhoneNumber = "0222.222.222",
                        BirthPlace = "Binh Duong",                       
                        IsGraduated = false},
        };


        private readonly ILogger<HomeController> _logger;

        public MemberController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult MaleMember(){
            List<Member> maleList = memberList.Where(x => x.Gender == "Male").ToList();

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(maleList, options);
            return Content(jsonString);
        }

        public IActionResult OldestMember(){
            Member oldest = memberList.Where(x => x.Age == memberList.Max(x => x.Age)).First();

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(oldest, options);
            return Content(jsonString);
        }

        public IActionResult FullName(){
            List<string> FullNameList = memberList.Select(x => x.FullName()).ToList();

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(FullNameList, options);
            return Content(jsonString);
        }

        public IActionResult Equal(){
           List<Member> equalList  = memberList.Where(x => x.DateOfBirth.Year == 2000).ToList();

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(equalList, options);
            return Content(jsonString);
        }

        public IActionResult Less()
        {
            List<Member> lessList = memberList.Where(x => x.DateOfBirth.Year < 2000).ToList();

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(lessList, options);
            return Content(jsonString);
        }

        public IActionResult More()
        {
            List<Member> moreList = memberList.Where(x => x.DateOfBirth.Year > 2000).ToList();

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(moreList, options);
            return Content(jsonString);
        }

        public IActionResult EveryOne(){
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(memberList, options);
            return Content(jsonString);
        }
        public IActionResult Filter(string type, int year){
            if(type == "equal"){
                return RedirectToAction("equal");
            }
            else if(type == "less"){
                return RedirectToAction("less");
            }
            else if(type == "more"){
                return RedirectToAction("more");
            }
            else{
                return RedirectToAction("EveryOne");
            }
        }
        //Add package!
        //dotnet add package EPPlus --version 5.7.0
        public IActionResult Export(){
            var stream = new MemoryStream();
            
            using (var package = new ExcelPackage(stream)){
                var sheet = package.Workbook.Worksheets.Add("Member");

                sheet.Cells.LoadFromCollection(memberList, true);
                
                for(int i=0; i< memberList.Count; i++){
                    sheet.Cells[i+2, 4].Value = memberList[i].DateOfBirth.ToShortDateString();
                    
                    if(memberList[i].IsGraduated == false){
                        sheet.Cells[i+2, 7].Value = "Not Graduate";
                    }
                    else{
                        sheet.Cells[i+2, 7].Value = "Graduate";
                    }
                }
                
                package.Save();
            };
            stream.Position = 0;

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","excel.xlsx");
        }

        public IActionResult Index()
        {
            return Content($"Nothing here");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
