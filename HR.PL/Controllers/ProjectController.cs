using AutoMapper;
using HR.BLL.Interface;
using HR.BLL.Repository;
using HR.DAL.Models;
using HR.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.PL.Controllers
{
    [Authorize]

    public class ProjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var projects = await _unitOfWork.ProjectRepository.GetAll();
            return View(projects);
        }
        //------------------
        //Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Project projectVM)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                await _unitOfWork.ProjectRepository.Add(projectVM);
                await _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
            return View(projectVM); // To The Same Input He Just Did
        }
        //Details
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAll();
            if (id is null)
                return BadRequest();
            var project = await _unitOfWork.ProjectRepository.GetById(id.Value);

            if (project is null)
                return NotFound();

            return View(ViewName, project);
        }
        //Update
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAll();
            return await Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, Project projectVM)
        {
            if (id != projectVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {

                    _unitOfWork.ProjectRepository.Update(projectVM);
                    await _unitOfWork.Complete();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(projectVM);
        }
        //Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAll();
            return await Details(id, "Delete");

        }
        public async Task<IActionResult> Delete(Project projectVM, [FromRoute] int id)
        {
            if (id != projectVM.Id)
                return BadRequest();
            try
            {

                _unitOfWork.ProjectRepository.Delete(projectVM);
                await _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(projectVM);
        }
    }
}
