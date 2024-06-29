using AutoMapper;
using HR.BLL.Interface;
using HR.DAL.Models;
using HR.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAll();
            var MappedDepartment = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);

            return View(MappedDepartment);
        }
        //------------------
        //Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                var MappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                await _unitOfWork.DepartmentRepository.Add(MappedDepartment);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(departmentVM); // To The Same Input He Just Did
        }
        //Details
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var department = await _unitOfWork.DepartmentRepository.GetById(id.Value);

            if (department is null)
                return NotFound();
            var MappedDepartment = _mapper.Map<Department, DepartmentViewModel>(department);

            return View(ViewName, MappedDepartment);
        }
        //Update
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                    _unitOfWork.DepartmentRepository.Update(MappedDepartment);
                    await _unitOfWork.Complete();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(departmentVM);
        }
        //Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");

        }
        public async Task<IActionResult> Delete(DepartmentViewModel departmentVM, [FromRoute] int id)
        {
            if (id != departmentVM.Id)
                return BadRequest();
            try
            {
                var MappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _unitOfWork.DepartmentRepository.Delete(MappedDepartment);
                await _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(departmentVM);
        }
    }
}
