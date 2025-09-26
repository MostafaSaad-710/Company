using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Models;
using Company.G01.PL.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Company.G01.PL.Controllers
{
    //MVC Controller
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepsitory;
        public DepartmentController(IDepartmentRepository departmentRepsitory)
        {
            _departmentRepsitory = departmentRepsitory;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentRepsitory.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreatedAt = model.CreatedAt
                };
                var count = _departmentRepsitory.Add(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }
        [HttpGet]
        public IActionResult Details(int? id, string ViewName="Details")
        {
          if(id is null) return BadRequest("Invalid Id");
          var department = _departmentRepsitory.Get(id.Value);  
            if(department is null) return NotFound(new {StatusCode=404, Message= $"Department with id:{id} Not Found" });
            return View(ViewName,department);
        }
        [HttpGet]

        public IActionResult Edit(int? id)
        {
            //if (id is null) return BadRequest("Invalid Id");
            //var department = _departmentRepsitory.Get(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department with id:{id} Not Found" });
            return Details(id,"Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest();

                var count = _departmentRepsitory.Update(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(department);

        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit([FromRoute] int id, updateDepartmentDto model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var department = new Department()
        //        {
        //            Id = id,
        //            Name = model.Name,
        //            Code = model.Code,
        //            CreatedAt = model.CreatedAt
        //        };

        //        var count = _departmentRepsitory.Update(department);
        //        if (count > 0)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }

        //    }
        //    return View(model);

        //}

        [HttpGet]
        public IActionResult Delete([FromRoute] int? id)
        {
            //if (id is null) return BadRequest("Invalid Id");
            //var department = _departmentRepsitory.Get(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department with id:{id} Not Found" });
            return Details(id,"Delete");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest();

                var count = _departmentRepsitory.Delete(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(department);

        }


    }  

}