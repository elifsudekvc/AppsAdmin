using AppsServices.Models;
using AppsServices.Services;
using Microsoft.AspNetCore.Mvc;
using System.Configuration.Provider;

namespace AppsServices.Controllers
{
    public class APPSController : Controller
    {
        
        private readonly IConfiguration _configuration;
        private readonly IAPPSService _aPPSService;
        public APPSController(IConfiguration configuration, IAPPSService APPSService)
        {
            _configuration = configuration;
            _aPPSService= APPSService;
        }
        //Action metodu controller'dan gelen bilgiyi alır işler ve sonuç olarak kullanıcıya bir Http Response döndürür. 
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult APPSList()
        {
            AllModels model = new AllModels();

             model.APPSList = _aPPSService.GetAPPSList().ToList();

            return View(model);
        }



        [HttpPost]
        public IActionResult InsertUpdateAPPSRecord(APPS formData)
        {
            AllModels model = new AllModels();

            string url = Request.Headers["Referer"].ToString();
            string resault = String.Empty;

            if (formData.AppID > 0)
            {
                resault = _aPPSService.UpdateAPPSRecord(formData);
            }
            else
            {
                resault = _aPPSService.InsertAPPS(formData);
            }

           
            if (resault == "recorded")
            {
                TempData["SuccessMsg"] = "recorded";
                return Redirect(url);
            }
            else
            {
                TempData["ErrorMsg"] = resault;
                return View(url);
            }
        }

        [HttpPost]
        public JsonResult DeleteAPPSRecord(int AppID)
        {
            
            string url = Request.Headers["Referer"].ToString();

            string resault = _aPPSService.DeleteAPPSRecord(AppID);
            if (resault == "DELETED")
            {
               
                return Json(new { message = "DELETED" });
            }
            else
            {
                
                return Json(new { message = "ERROR" });
            }
        }

    }
}
