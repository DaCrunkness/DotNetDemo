using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DataModels;


namespace Web.Areas.Visitor.Controllers
{
    [Area("Visitor")]
    [Authorize]
    public class FileController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public FileController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            List<FileForm> fileFormList = _unitOfWork.FileForm.GetAll().ToList();
            return View(fileFormList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [RequestSizeLimit(512 * 1024 * 1024)]
        public IActionResult Create(FileForm fileForm, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filePath = @"Uploads\";
                    string finalPath = Path.Combine(wwwRootPath, filePath);

                    if (!Directory.Exists(finalPath))
                        Directory.CreateDirectory(finalPath);

                    using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    fileForm.FileField = fileName;

                }
                _unitOfWork.FileForm.Add(fileForm);
                _unitOfWork.Save();
                TempData["success"] = "File submitted successfully";
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

            FileForm? dbFileForm = _unitOfWork.FileForm.GetFirstOrDefault(x => x.Id == id);
            return View(dbFileForm);
        }

        [HttpPost]
        public IActionResult Edit(FileForm fileForm, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filePath = @"Uploads\";
                    string finalPath = Path.Combine(wwwRootPath, filePath);

                    if (!Directory.Exists(finalPath))
                        Directory.CreateDirectory(finalPath);
                    if (fileForm.FileField != "")
                    {
                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileForm.FileField), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                    } 
                    else
                    {
                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        fileForm.FileField = fileName;
                    }
                }
                _unitOfWork.FileForm.Update(fileForm);
                _unitOfWork.Save();
                TempData["success"] = "File updated successfully";
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
            FileForm? dbFileForm = _unitOfWork.FileForm.GetFirstOrDefault(x => x.Id == id);
            return View(dbFileForm);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            FileForm? dbFileForm = _unitOfWork.FileForm.GetFirstOrDefault(x => x.Id == id);
            if (dbFileForm == null)
            {
                return NotFound();
            }
            _unitOfWork.FileForm.Delete(dbFileForm.Id);
            _unitOfWork.Save();
            TempData["success"] = "File deleted successfully";
            return RedirectToAction(nameof(Index));
        }



        public ActionResult Success()
        {
            return View();
        }
    }
}