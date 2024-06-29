using AutoMapper;
using HR.BLL.Interface;
using HR.BLL.Repository;
using HR.DAL.Models;
using HR.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCCompany.BLL.Repository;

namespace HR.PL.Controllers
{
    [Authorize]
    public class WorkController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
         
        }
        public async Task<IActionResult> Index()
        {
            var works = await _unitOfWork.Works.GetAll();
            var MappedWork = _mapper.Map<IEnumerable<Work_For>, IEnumerable<WorksViewModel>>(works);
            return View(MappedWork);
        }
        //------------------
        //Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Projects = await _unitOfWork.ProjectRepository.GetAll();
            ViewBag.Employees = await _unitOfWork.EmployeeRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(WorksViewModel workVM)
        {
            // Server Side Validation
            if (ModelState.IsValid) 
            {
                var MappedWork = _mapper.Map<WorksViewModel, Work_For>(workVM);
                await _unitOfWork.Works.Add(MappedWork);
                await _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
            // To The Same Input He Just Did
            return View(workVM); 
        }
    }
}
