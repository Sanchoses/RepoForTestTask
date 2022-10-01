using University.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using University.Data;
using University.Models;
using University.ViewModels;

namespace University.Controllers;
[ApiController]
[Route("[controller]")]
public class UniversityController:ControllerBase {
    private readonly UniversityDbContext _db;

    public UniversityController(UniversityDbContext context)
    {
        _db = context;
    }

    //  Перший таск не розумію умови


    [HttpGet]
    [Route("teachersbyMaxSallary/{maxSallary:decimal}")]
    //query
    //  https://localhost:7125/University/teachersbyMaxSallary/100
    public async Task<ActionResult> GetTeachersByMaxSallaryPerHour(decimal maxSallary){
        
        var result = _db.Teachers.Where(t=>t.Position.SallaryPerHour <= maxSallary);
        return Ok(result);
    }
    
    [HttpGet]
    [Route("teachersbyAddressStreet/{street}")]
    //query
    //  https://localhost:7125/University/teachersbyAddressStreet/Yangelya
    public async Task<ActionResult> GetTeachersByStreet(string street){
        
        var result = _db.Teachers.Where(t=>t.Address.Street == street );
        if(result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet]
    [Route("teachersByAbc/{a}-{b}")]
    //query
    //  https://localhost:7125/University/teachersByAbc/A-R
    public async Task<ActionResult> TeachersAbcInitials(char a, char b){

        var teachers = _db.Teachers.ToList();
        var result = new List<Teacher>();

        foreach(var item in teachers)
        {
            if( a <= item.SecondName.ToUpper()[0] && item.SecondName.ToUpper()[0] <= b)
                result.Add(item);
            
        }
        result = result.OrderBy(i=>i.SecondName).ToList();
        return Ok(result);
    }

    [HttpGet]
    [Route("SallaryForTeachers")]
    //query
    //  https://localhost:7125/University/SallaryForTeachers
    public async Task<ActionResult> GetSallaryForTeachers(){

         var result = new List<SallaryModel>();
        var teachers = _db.Teachers.ToList();
        var subjects = _db.Subjects.ToList();
        var positions = _db.Positions.ToList();
        var schedules = _db.Schedules.ToList();
       

        foreach(var item in schedules){
            var tmp = new SallaryModel();

            var teacher = _db.Teachers.Where(t=>t.Id == item.TeacherId).FirstOrDefault();

            tmp.Name = teacher.FirstName;
            tmp.SecondName = teacher.SecondName;
            tmp.MiddleName = teacher.MiddleName;
            tmp.Position = teacher.Position.Name;

            
            var countHours = item.CountHours;
            tmp.Subject = subjects.Where(s=>s.Id==item.SubjectId).FirstOrDefault().Name;
            tmp.Sallary = teacher.Position.SallaryPerHour * countHours;
            
            var flag = result.Where(v=>v.Subject == tmp.Subject && v.SecondName == tmp.SecondName && v.Name == tmp.Name).FirstOrDefault();
          
            if(flag != null && result.Count >= 1)
                result.Where(v=>v.Subject == tmp.Subject && v.SecondName == tmp.SecondName).FirstOrDefault()
                    .Sallary += tmp.Sallary;
            else
                result.Add(tmp);

        }


        return Ok(result); 
    }



    [HttpGet]
    [Route("sallaryPerSubject/{subject}/{dollarCourse}")]
    //query
    //  https://localhost:7125/University/sallaryPerSubject/Api development/36.93
    public async Task<ActionResult> TeachersAbcInitials(string subject,decimal dollarCourse){

        var subj = _db.Subjects.Where(s=>s.Name == subject).FirstOrDefault();
        var teachers = _db.Teachers.ToList();
        var positions = _db.Positions.ToList();
        var schedules = _db.Schedules.Where(s=>s.SubjectId==subj.Id).ToList();
        var result = new List<SallaryModel>();
        
        foreach(var item in schedules){
            
            var tmp = new SallaryModel();

            var teacher = _db.Teachers.Where(t=>t.Id == item.TeacherId).FirstOrDefault();

            tmp.Name = teacher.FirstName;
            tmp.SecondName = teacher.SecondName;
            tmp.MiddleName = teacher.MiddleName;
            tmp.Position = teacher.Position.Name;

            
            var countHours = item.CountHours;
            tmp.Subject = subject;
            tmp.Sallary = Decimal.Round((teacher.Position.SallaryPerHour * countHours) / dollarCourse,2);
            
            var flag = result.Where(v=>v.SecondName == tmp.SecondName && v.Name == tmp.Name).FirstOrDefault();
            if(flag != null && result.Count >= 1)
                result.Where(v=>v.Subject == tmp.Subject && v.SecondName == tmp.SecondName).FirstOrDefault()
                    .Sallary += tmp.Sallary;
            else
                result.Add(tmp);
        }
        
        return Ok(result);
    }

    [HttpGet]
    [Route("allTeachers")]
        //query
    // https://localhost:7125/University/allTeachers
    public async Task<ActionResult> GetAllTeachers(){
        var result = new List<SimpleTeacherViewModel>();

        foreach(var item in _db.Teachers)
        {
            var tmp = new SimpleTeacherViewModel();
            tmp.FirstName = item.FirstName;
            tmp.MiddleName = item.MiddleName.ToUpper()[0];
            tmp.SecondName = item.SecondName.ToUpper()[0];

            result.Add(tmp);
        }
        
        return Ok(result);
    }

    [HttpGet]
    [Route("changeRegisterFor/{secondName}/{choice}")]
        //query
    // https://localhost:7125/University/changeRegisterFor/Turulko/up
    public async Task<ActionResult> ChangeRegisterPositionBy(string secondName, string choice){
        
        
        var teacher = _db.Teachers.Where(t => t.SecondName == secondName).FirstOrDefault();
        var position = _db.Positions.Where(p=>p.Id == teacher.PositionId).FirstOrDefault().Name;
        string res = "";

        
        if(choice.ToLower() == "up")
            res = position.ToUpper(); 
        else if(choice.ToLower() == "low")
            res = position.ToLower(); 

        return Ok(res);
    }
    
    [HttpGet]
    [Route("minimalSallary")]
        //query
    // https://localhost:7125/University/minimalSallary
    public async Task<ActionResult> MinimalSallary(){
        return Ok(_db.Positions.Min(s=>s.SallaryPerHour));
    }

    [HttpGet]
    [Route("countHourses")]
        //query
    // https://localhost:7125/University/minimalSallary
    public async Task<ActionResult> CountHours(){
        var teachers = _db.Teachers.ToList();
        var schedules = _db.Schedules.ToList();

        var res = new List<TeachersTime>();

        foreach(var item in schedules){
            var tmp = new TeachersTime();
            var teacher = teachers.Where(t=>t.Id == item.TeacherId).FirstOrDefault();

            tmp.Name = teacher.FirstName;
            tmp.SecondName = teacher.SecondName;
            tmp.MiddleName = teacher.MiddleName;
            
            var resItem = res.Where(v=>v.SecondName == tmp.SecondName && v.Name == tmp.Name).FirstOrDefault();

            if(resItem == null){
                tmp.Time = item.CountHours;
                res.Add(tmp);
            }
            else
                resItem.Time += item.CountHours;
        }
        return Ok(res);
    }
    

}

    