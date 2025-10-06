using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Models;
using Company.G01.PL.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.G01.PL.Controllers
{
    //MVC Controller
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepository _departmentRepsitory;
        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_departmentRepsitory = departmentRepsitory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreatedAt = model.CreatedAt
                };

                /*var count =*/await _unitOfWork.DepartmentRepository.AddAsync(department);
                var count = _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id, string ViewName="Details")
        {
          if(id is null) return BadRequest("Invalid Id");
          var department =await _unitOfWork.DepartmentRepository.GetAsync(id.Value);  
            if(department is null) return NotFound(new {StatusCode=404, Message= $"Department with id:{id} Not Found" });
            return View(ViewName,department);
        }
        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            //if (id is null) return BadRequest("Invalid Id");
            //var department = _departmentRepsitory.Get(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department with id:{id} Not Found" });
            return await Details(id,"Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest();


                 /*var count =*/ _unitOfWork.DepartmentRepository.Update(department);
                var count = _unitOfWork.Complete();
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
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            //if (id is null) return BadRequest("Invalid Id");
            //var department = _departmentRepsitory.Get(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department with id:{id} Not Found" });
            return await Details(id,"Delete");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            //if (ModelState.IsValid)
            //{
                if (id != department.Id) return BadRequest();

             
            /*var count =*/_unitOfWork.DepartmentRepository.Delete(department);
            var count = _unitOfWork.Complete();
            if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            //}
            return View(department);

        }


    }  

}