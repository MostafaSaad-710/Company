using AutoMapper;
using Company.BLL.Interfaces;
using Company.DAL.Models;
using Company.G01.PL.Dtos;
using Company.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.G01.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _employeeRepsitory;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        public EmployeeController(
                                   //IEmployeeRepository employeeRepository,
                                   //IDepartmentRepository departmentRepository,
                                   IUnitOfWork unitOfWork,
                                   IMapper mapper)
        {
            //_employeeRepsitory = employeeRepository;
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
             
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Employee> employee;
            if(string.IsNullOrEmpty(SearchInput))
            {
                employee = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                employee = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
            }
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
        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    model.ImageName = DocumentSettings.UplaodFile(model.Image, "images");
                }

                var employee = _mapper.Map<Employee>(model);

                /*var count =*/ await _unitOfWork.EmployeeRepository.AddAsync(employee);
                var count = _unitOfWork.Complete();
                if (count > 0)
                {
                    TempData["Message"] = "Employee is Created";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id");

            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, Message = $"Employee with id:{id} Not Found" });

             

            return View(ViewName, employee);
        }
        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            //var departments = _departmentRepository.GetAll();
            //ViewData["departments"] = departments;

            if (id is null) return BadRequest("Invalid Id");
            var employee =await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, Message = $"Department with id:{id} Not Found" });
            //var employeeDto = new CreateEmployeeDto()
            //{

            //    Name = employee.Name,
            //    Age = employee.Age,
            //    Address = employee.Address,
            //    Email = employee.Email,
            //    Phone = employee.Phone,
            //    Salary = employee.Salary,
            //    IsActive = employee.IsActive,
            //    IsDelete = employee.IsDelete,
            //    CreatedAt = employee.CreatedAt,
            //    HiringDate = employee.HiringDate,
            //    DepartmentId = employee.DepartmentId,
            //};

            var employeeDto = _mapper.Map<CreateEmployeeDto>(employee);

            return View(employeeDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                if(model.ImageName is not null && model.Image is not null)
                {
                    DocumentSettings.DeleteFile(model.ImageName, "images");
                }
                
                if(model.Image is not null)
                {
                    model.ImageName = DocumentSettings.UplaodFile(model.Image, "images");
                }

                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;

                /*var count =*/_unitOfWork.EmployeeRepository.Update(employee);
                var count = _unitOfWork.Complete();
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
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            //if (id is null) return BadRequest("Invalid Id");
            //var department = _departmentRepsitory.Get(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department with id:{id} Not Found" });
            return await Details(id, "Delete");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id != employee.Id) return BadRequest();

                /*var count =*/_unitOfWork.EmployeeRepository.Delete(employee);
                var count = _unitOfWork.Complete();
                if (count > 0)
                {
                    if (employee.ImageName is not null)
                    {
                         DocumentSettings.DeleteFile(employee.ImageName , "images");
                    }
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(employee);

        }
    }
}
