using System.Diagnostics;
using ATM.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using ATM.BLL.Implementation;
using ATM.BLL.Interface;
using ATM.DAL.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ATM.MVC.Controllers
{
    public class ATMPagesController : Controller
    {
        private readonly ILogger<ATMPagesController> _logger;
        private readonly ICustomerOperation _customerOperation;

           public ATMPagesController(ILogger<ATMPagesController> logger, ICustomerOperation customerOperation)
           {
            _customerOperation = customerOperation;
               _logger = logger;
           }

        public  IActionResult Index()
        {
            //var model = await _customerOperation.TransferAsync();

            return View();
        }

        public IActionResult Deposit()
        {
            return View();
        }

        public IActionResult View_History()
        {
            return View();
        }

        public IActionResult TransferForm()
        {
            return View();
        }

        public IActionResult ChangePin()
        {
            return View();
        }
       

        public IActionResult WithdrawalForm()
        {
            return View();
        }

        //[HttpGet]
     //   public async Task<IActionResult> LoginValidation(ICustomerOperation customerOperation)
     //   {
     //       var validation = _customerOperation.
	    //}
        //   [HttpPost]
        //   public async Task<IActionResult> DepositAsync(CustomerOperation())
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

