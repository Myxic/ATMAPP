using System.Diagnostics;
using ATM.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using ATM.BLL.Implementation;
using ATM.BLL.Interface;
using ATM.DAL.Model;
using ATM.BLL.Models;

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
           
            return View(new Customer());
        }

        public IActionResult Deposit()
        {
            return View(new DepositOrWithdraw());
        }

        public async Task<IActionResult> View_History(Customer customer)
        {
            var model = await _customerOperation.GetTransationHistoryAsync(customer);

            return View(model);
        }

        public IActionResult TransferForm()
        {
            return View(new TransferModel());
        }

        public IActionResult ChangePin()
        {
            return View(new ChangePinModel());
        }
       

        public IActionResult WithdrawalForm()
        {
            return View(new DepositOrWithdraw());
        }



        [HttpPost]
        public async Task<IActionResult> DepositMoney(Customer customer, DepositOrWithdraw model)
	    {
            if (ModelState.IsValid && customer.PinNo == model.CustomerPin) 
	        {
                var (successful, msg) = await _customerOperation.DepositAsync(model);

                if (successful)
                {

                    TempData["SuccessMsg"] = msg;

                    return RedirectToAction("Deposit");
                }

                // TempData["ErrMsg"] = msg; for both views and redirect to actions

                ViewBag.ErrMsg = msg;

                return View("Deposit");

            }

            return View("Deposit");
        }

        [HttpPost]
        public async Task<IActionResult> WithdrawMoney(Customer customer, DepositOrWithdraw model)
        {
            if (ModelState.IsValid && customer.PinNo == model.CustomerPin)
            {
                var (successful, msg) = await _customerOperation.WithdrawalAsync(model);

                if (successful)
                {

                    TempData["SuccessMsg"] = msg;

                    return RedirectToAction("WithdrawalForm");
                }

                // TempData["ErrMsg"] = msg; for both views and redirect to actions

                ViewBag.ErrMsg = msg;

                return View("WithdrawalForm");

            }

            return View("WithdrawalForm");
        }

        [HttpPost]
        public async Task<IActionResult> TransferMoney(Customer customer, TransferModel model)
        {
            if (ModelState.IsValid && customer.PinNo == model.UserPin)
            {
                var (successful, msg) = await _customerOperation.TransferAsync(model);

                if (successful)
                {

                    TempData["SuccessMsg"] = msg;

                    return RedirectToAction("TransferForm");
                }

                // TempData["ErrMsg"] = msg; for both views and redirect to actions

                ViewBag.ErrMsg = msg;

                return View("TransferForm");

            }

            return View("TransferForm");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePinNo(Customer customer, ChangePinModel model)
        {
            if (ModelState.IsValid && customer.PinNo == model.CurrentPin)
            {
                var (successful, msg) = await _customerOperation.ChangePinAsync(model);

                if (successful)
                {

                    TempData["SuccessMsg"] = msg;

                    return RedirectToAction("ChangePin");
                }

                // TempData["ErrMsg"] = msg; for both views and redirect to actions

                ViewBag.ErrMsg = msg;

                return View("ChangePin");

            }

            return View("ChangePin");
        }

        [HttpPost]
        public async Task<IActionResult> SendComplain(Customer customer, Complains model)
        {
            if (ModelState.IsValid && customer.CardNo == model.CardNo)
            {
                var (successful, msg) = await _customerOperation.ComplainsAsync(model);

                if (successful)
                {

                    TempData["SuccessMsg"] = msg;

                    return RedirectToAction("Index");
                }

                // TempData["ErrMsg"] = msg; for both views and redirect to actions

                ViewBag.ErrMsg = msg;

                return View("Index");

            }

            return View("Index");
        }



        // GET: /<controller>/
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    
}

