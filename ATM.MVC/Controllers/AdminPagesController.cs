using System.Diagnostics;
using ATM.BLL.Interface;
using ATM.BLL.Models;
using ATM.DAL.Model;
using ATM.MVC.Models;
using Microsoft.AspNetCore.Mvc;



// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ATM.MVC.Controllers
{
    public class AdminPagesController : Controller
    {
        // GET: /<controller>/
        private readonly ILogger<AdminPagesController> _logger;
        private readonly ICustomerOperation _customerService;
        private readonly IAdminOperations _adminService;

        public AdminPagesController(ILogger<AdminPagesController> logger, ICustomerOperation customerOperation, IAdminOperations adminOperations)
           {
               _logger = logger;
            _adminService = adminOperations;
            _customerService = customerOperation;
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
            
            return View(new Customer());
        }

        public async Task<IActionResult> ViewComplains()
        {
            var Model = await _adminService.GetComplainsAsync();

            return View(Model);
        }
        public async Task<IActionResult> ViewWorkFlow(Admin admin)
        {
            var Model = await _adminService.GetAdminWorkFlowAsync(admin);

            return View(Model);
        }

        public IActionResult UpdateUser()
        {
            return View(new UpdateCustomer()); 
	    }

        //[HttpGet]
        //public async Task<IActionResult> LoginValidation(IAdminOperations AdminOperation)
        //{
        //    var validation = AdminOperation.

        //}

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCustomer model, Admin admin)
        {
            if (ModelState.IsValid)
            {

                var (successful, msg) = await _adminService.UpdateCustomerRecordsAsync(admin, model);

                if (successful)
                {

                    TempData["SuccessMsg"] = msg;

                    return RedirectToAction("Index");
                }

                // TempData["ErrMsg"] = msg; for both views and redirect to actions

                ViewBag.ErrMsg = msg;

                return View("UpdateUser");

            }

            return View("UpdateUser");
        }


        [HttpPost]
        public async Task<IActionResult> NewUser(Customer model, Admin admin)
        {
            if (ModelState.IsValid)
            {

                var (successful, msg) = await _adminService.CreateNewCustomerRecordAsync(admin, model);

                if (successful)
                {

                    TempData["SuccessMsg"] = msg;

                    return RedirectToAction("Index");
                }

                // TempData["ErrMsg"] = msg; for both views and redirect to actions

                ViewBag.ErrMsg = msg;

                return View("DeleteUserForm");

            }

            return View("DeleteUserForm");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Admin admin, string model)
        {

            if (ModelState.IsValid)
            {

                var (successful, msg) = await _adminService.DeleteCustomerRecordsAsync(admin, model);

                if (successful)
                {

                    TempData["SuccessMsg"] = msg;

                    return RedirectToAction("Index");
                }

                // TempData["ErrMsg"] = msg; for both views and redirect to actions

                ViewBag.ErrMsg = msg;

                return View("AddUser");

            }

            return View("AddUser");
        }

        // GET: /<controller>/
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    
}

