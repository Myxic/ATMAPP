using System.Diagnostics;
using ATM.BLL.Interface;
using ATM.MVC.Models;
using Microsoft.AspNetCore.Mvc;



// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ATM.MVC.Controllers
{
    public class AdminPagesController : Controller
    {
        // GET: /<controller>/
   private readonly ILogger<AdminPagesController> _logger;

           public AdminPagesController(ILogger<AdminPagesController> logger)
           {
               _logger = logger;
           }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DeleteUserForm()
        {
            return View();
        }

        public IActionResult AddUser()
        {
            return View();
        }

        public IActionResult ViewComplains()
        {
            //var Model = 

            return View();
        }
        public IActionResult ViewWorkFlow()
        {
            //var Model = 
            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> LoginValidation(IAdminOperations AdminOperation)
        //{
        //    var validation = AdminOperation.

        //}

        //[HttpPut]
        //public async Task<IActionResult> Update()
        //    {

        // }


     //   [HttpPost]
     //   public async Task<IActionResult> NewUser()
     //   { 
	    //}



        // GET: /<controller>/
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    
}

