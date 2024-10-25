using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.DataModels;
using Models.IdentityModels;

namespace Web.Areas.Visitor.Controllers
{
    [Area("Visitor")]
    [Authorize]
    public class FormController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        

        public FormController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<SampleForm> sampleFormList = _unitOfWork.SampleForm.GetAll().ToList();
            return View(sampleFormList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SampleForm sampleForm)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.SampleForm.Add(sampleForm);
                _unitOfWork.Save();
                TempData["success"] = "Form submitted successfully";
                return RedirectToAction(nameof(Success));
            }
            return View();
        }


        public IActionResult Edit(int? id)
        {
            if (id == null | id == 0)
            {
                return NotFound();
            }

            SampleForm? dbSampleForm = _unitOfWork.SampleForm.GetFirstOrDefault(x => x.Id == id);
            return View(dbSampleForm);
        }

        [HttpPost]
        public IActionResult Edit(SampleForm sampleForm)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.SampleForm.Update(sampleForm);
                _unitOfWork.Save();
                TempData["success"] = "Form updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null | id == 0)
            {
                return NotFound();
            }
            SampleForm? dbSampleForm = _unitOfWork.SampleForm.GetFirstOrDefault(x => x.Id == id);
            return View(dbSampleForm);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            SampleForm? category = _unitOfWork.SampleForm.GetFirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _unitOfWork.SampleForm.Delete(category.Id);
            _unitOfWork.Save();
            TempData["success"] = "Form deleted successfully";
            return RedirectToAction(nameof(Index));
        }



        public ActionResult Success()
        {
            return View();
        }
    }
}