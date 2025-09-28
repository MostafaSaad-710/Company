using Company.BLL.Interfaces;
using Company.DAL.Models;
using Company.G01.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.G01.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepsitory;
        private readonly IDepartmentRepository _departmentRepository;
        public EmployeeController(IEmployeeRepository employeeRepository , IDepartmentRepository departmentRepository)
        {
            _employeeRepsitory = employeeRepository;
            _departmentRepository = departmentRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var employee = _employeeRepsitory.GetAll();
            //Memory of view is a dictionary
            // To access on this dictionary and add date other than the data sent by model 
            // i can it by 3 property we inhiret it from contoller

            ////1. Viewdate : Transfer Extre Information from controller (Action) To View
            ////ViewData["Message"] = "Hello From ViewDate"; // i Do Set

            ////2. ViewBag : Transfer Extre Information from controller (Action) To View
            //ViewBag.Message = "Hello From ViewBag";

            //3. TempDate


            return View(employee);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var departments = _departmentRepository.GetAll();
            ViewData["departments"] = departments;
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Name = model.Name,
                    Age = model.Age,
                    Address = model.Address,
                    Email = model.Email,
                    Phone = model.Phone,
                    Salary = model.Salary,
                    IsActive = model.IsActive,
                    IsDelete = model.IsDelete,
                    CreatedAt = model.CreateAt,
                    HiringDate = model.HiringDate,
                    DepartmentId = model.DepartmentId
                };
                var count = _employeeRepsitory.Add(employee);
                if (count > 0)
                {
                    TempData["Message"] = "Employee is Created";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }
        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id");
            var employee = _employeeRepsitory.Get(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, Message = $"Employee with id:{id} Not Found" });
            return View(ViewName, employee);
        }
        [HttpGet]

        public IActionResult Edit(int? id)
        {
            var departments = _departmentRepository.GetAll();
            ViewData["departments"] = departments;

            if (id is null) return BadRequest("Invalid Id");
            var employee = _employeeRepsitory.Get(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, Message = $"Department with id:{id} Not Found" });
            var employeeDto = new CreateEmployeeDto()
            {

                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Email = employee.Email,
                Phone = employee.Phone,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                IsDelete = employee.IsDelete,
                CreateAt = employee.CreatedAt,
                HiringDate = employee.HiringDate,
                DepartmentId = employee.DepartmentId,
                
                
            };
            return View(employeeDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                // if (id != employee.Id) return BadRequest();
                var employee = new Employee()
                {
                    Id = id,
                    Name = model.Name,
                    Age = model.Age,
                    Address = model.Address,
                    Email = model.Email,
                    Phone = model.Phone,
                    Salary = model.Salary,
                    IsActive = model.IsActive,
                    IsDelete = model.IsDelete,
                    CreatedAt = model.CreateAt,
                    HiringDate = model.HiringDate
                };
                var count = _employeeRepsitory.Update(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(model);

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
            return Details(id, "Delete");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id != employee.Id) return BadRequest();

                var count = _employeeRepsitory.Delete(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(employee);

        }
    }
}
