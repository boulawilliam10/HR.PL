using AutoMapper;
using HR.BLL.Interface;
using HR.DAL.Models;
using HR.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchValue))
                employees = await _unitOfWork.EmployeeRepository.GetAll();
            else
                employees = _unitOfWork.EmployeeRepository.GetEmpByName(SearchValue);

            var MappedEmp = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(employees);
            return View(MappedEmp);
        }
        //------------------
        //Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel EmpVm)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                var MappedEmp = _mapper.Map<EmployeeViewModel,Employee>(EmpVm);

                await _unitOfWork.EmployeeRepository.Add(MappedEmp);
                int Result = await _unitOfWork.Complete();
                if(Result > 0)
                {
                    TempData["Message"] = "Employee is Created";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(EmpVm); // To The Same Input He Just Did
        }
        //Details
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            ViewBag.Departments =  _unitOfWork.DepartmentRepository.GetAll();
            if (id is null)
                return BadRequest();

            var employee = await _unitOfWork.EmployeeRepository.GetById(id.Value);

            if (employee is null)
                return NotFound();

            var MappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);

         
            return View(ViewName, MappedEmp);
        }
        //Update
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Departments =  _unitOfWork.DepartmentRepository.GetAll();
            return await Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel EmpVm)
        {
            if (id != EmpVm.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmpVm);

                    _unitOfWork.EmployeeRepository.Update(MappedEmp);
                    await _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(EmpVm);
        }
        //Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();
            return await Details(id, "Delete");

        }
        public async Task<IActionResult> Delete(EmployeeViewModel EmpVm, [FromRoute] int id)
        {
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();
            if (id != EmpVm.Id)
                return BadRequest();
            try
            {
                var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmpVm);
                _unitOfWork.EmployeeRepository.Delete(MappedEmp);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(EmpVm);
        }
    }
}
