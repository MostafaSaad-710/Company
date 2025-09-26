using Company.BLL.Interfaces;
using Company.DAL.Models;
using Company.G01.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.G01.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepsitory;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepsitory = employeeRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var employee = _employeeRepsitory.GetAll();
            return View(employee);
        }
        [HttpGet]
        public IActionResult Create()
        {
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
                    HiringDate = model.HiringDate
                };
                var count = _employeeRepsitory.Add(employee);
                if (count > 0)
                {
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
                HiringDate = employee.HiringDate
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
