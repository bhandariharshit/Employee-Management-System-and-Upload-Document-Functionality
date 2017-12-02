using AuthenticationDBTest.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace AuthenticationDBTest.Controllers
{
    [Authorize(Roles = "admin")]
    public class UploadController : Controller
    {
        // GET: Upload
        [HttpGet]
        
        public ActionResult UploadFile(int id)
        {
            FileModel fModel = new FileModel();
            fModel.empId = id;
            return View(fModel);
        }

        [HttpPost]
        public ActionResult UploadFile(FileModel model)
        {
            var Message = string.Empty;
            try
            {
                int id = model.empId;
                string val = Convert.ToString(Request.Params["empId"]);
                if (model.file != null)
                {
                    UploadFiles(model.file);
                    Message = "File Uploaded Successfully!";
                }
            }
            catch (Exception ex)
            {
                //Log exception 
                Message = "File Uploaded Failed!";
            }
            finally
            {
                ViewBag.Message = Message;
            }
            return View();
        }

        private void UploadFiles(HttpPostedFileBase[] file)
        {
            foreach (HttpPostedFileBase singleFile in file)
            {
                if (singleFile.ContentLength > 0)
                {
                    string extension = Path.GetExtension(singleFile.FileName);
                    string fileName = singleFile.FileName;
                    string filePath = Path.Combine(Server.MapPath("~/Uploaded Files"), fileName);
                    singleFile.SaveAs(filePath);
                }
            }
        }
    }
}
