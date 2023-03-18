using System;
using System.Collections;
using System.Text;
using ATM.BLL.Interface;
using ATM.BLL.Models;
using ATM.DAL.Data;
using ATM.DAL.Model;
using ATM.DAL.Repository;

namespace ATM.BLL.Implementation
{
    public class AdminOperation : IAdminOperations
    {
        public static StringBuilder stringBuilder = new();
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Customer> _CustomerRepo;
        private readonly IRepository<Admin> _AdminRepo;
        private readonly IRepository<Workflow> _WorkflowRepo;
        private readonly IRepository<Complains> _ComplainsRepo;

        public AdminOperation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _AdminRepo = _unitOfWork.GetRepository<Admin>();
            _CustomerRepo = _unitOfWork.GetRepository<Customer>();
            _WorkflowRepo = _unitOfWork.GetRepository<Workflow>();
            _ComplainsRepo = _unitOfWork.GetRepository<Complains>();

        }

        public async Task<(bool successful, string msg)> CreateNewCustomerRecordAsync(Admin admin, Customer customer)
        {
            try
            {

                var workflow = new Workflow
                {
                    Name = admin.AdminName,
                    Date = DateTime.Now.ToLongDateString(),
                    Time = DateTime.Now.ToShortTimeString(),
                    WorkTimeLine = $"Added {customer.CustomerName} into the Database"
                };

                await _WorkflowRepo.AddAsync(workflow);
                await _CustomerRepo.AddAsync(customer);
                await _unitOfWork.SaveChangesAsync();


                return (true, "Created successful");
            }
            catch (Exception ex)
            {
                return (false, "Failed to create");
            }
        }

        public async Task<(bool successful, string msg)> UpdateCustomerRecordsAsync(Admin admin, UpdateCustomer User)
        {

            var customer = await _CustomerRepo.GetSingleByAsync(a => a.CardNo == $"\"{User.CustomerCardNo}\"");

            if (customer != null)
            {

                if (User.CustomerName != null)
                {
                    stringBuilder.Append("CustomerName, ");
                    customer.CustomerName = User.CustomerName;
                }

                if (User.PhoneNumber != null)
                {
                    stringBuilder.Append("PhoneNumber, ");
                    customer.PhoneNumber = User.PhoneNumber;
                }

                if (User.PinNo != null)
                {
                    stringBuilder.Append("PinNo");
                    customer.PinNo = User.PinNo;
                }

                var workflow = new Workflow
                {
                    Name = admin.AdminName,
                    Date = DateTime.Now.ToLongDateString(),
                    Time = DateTime.Now.ToShortTimeString(),
                    WorkTimeLine = $"Updated {customer.CustomerName}'s Records in the Database Changes include {stringBuilder}."
                };

                await _WorkflowRepo.AddAsync(workflow);
                await _unitOfWork.SaveChangesAsync();

                return (true, "Updated successful");
            }

            return (false, "Failed to Update");


        }

        public async Task<(bool successful, string msg)> DeleteCustomerRecordsAsync(Admin admin, string CustomerCardNo)
        {
            var customer = await  _CustomerRepo.GetSingleByAsync(a => a.CardNo == $"\"{CustomerCardNo}\"");

            if (customer != null)
            {
                var workflow = new Workflow
                {
                    Name = admin.AdminName,
                    Date = DateTime.Now.ToLongDateString(),
                    Time = DateTime.Now.ToShortTimeString(),
                    WorkTimeLine = $"Deleted {customer.CustomerName} from the Database"
                };

                await _WorkflowRepo.AddAsync(workflow);

                await _CustomerRepo.DeleteAsync(customer);
                await _unitOfWork.SaveChangesAsync();

                return (true, "Deleted successful");
            }

            return (false, "Failed to Delete");

        }

        public async Task<IEnumerable<Workflow>> GetAdminWorkFlowAsync(Admin admin)
        {
            var workflow = await _WorkflowRepo.GetByAsync(w => w.Name == $"\"{admin.AdminName}\"" );

            return workflow;
        }

        public async Task<IEnumerable<Complains>> GetComplainsAsync()
        {
            var complains = await _ComplainsRepo.GetAllAsync();
            

            return  complains;
        }


    }
}

