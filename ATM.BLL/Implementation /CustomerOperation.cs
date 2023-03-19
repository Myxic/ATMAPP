using System;
using System.Collections;
using System.Reflection;
using System.Threading.Tasks;
using ATM.BLL.Interface;
using ATM.BLL.Models;
using ATM.DAL.Data;
using ATM.DAL.Model;
using ATM.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ATM.BLL.Implementation
{
    public class CustomerOperation : ICustomerOperation
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Customer> _CustomerRepo;
        private readonly IRepository<Admin> _AdminRepo;
        private readonly IRepository<Workflow> _WorkflowRepo;
        private readonly IRepository<Complains> _ComplainsRepo;
        private readonly IRepository<TransationRecords> _transationRepo;

        public CustomerOperation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _AdminRepo = _unitOfWork.GetRepository<Admin>();
            _CustomerRepo = _unitOfWork.GetRepository<Customer>();
            _WorkflowRepo = _unitOfWork.GetRepository<Workflow>();
            _ComplainsRepo = _unitOfWork.GetRepository<Complains>();
            _transationRepo = _unitOfWork.GetRepository<TransationRecords>();

        }

        public async Task<(bool successful, string msg)> ChangePinAsync(ChangePinModel changePin)
        {
            try
            {
                var User = await _CustomerRepo.GetSingleByAsync(a => a.PinNo == changePin.CurrentPin);

                if (changePin.Newpin == changePin.ConfirmNewPin)
                {
                    User.PinNo = changePin.Newpin;

                    var savechanges = await _unitOfWork.SaveChangesAsync();
                    return savechanges > 0 ? (true, $"Task:Change of Pin was successfully") : (false, "Failed To save changes!");
                }
                return (false, "Change of Pin failed");
            }
            catch (Exception ex)
            {
                return (false, "Change of Pin failed");
            }
        }

        public async Task<(bool successful, string msg)> ComplainsAsync(Complains complains)
        {
            try
            {
                var Complain = new Complains
                {
                    Subject = complains.Subject,
                    CardNo = complains.CardNo,
                    Reports = complains.Reports
                };
                await _ComplainsRepo.AddAsync(Complain);
                await _unitOfWork.SaveChangesAsync();

                return (true, "Complain sent successfully");

            }
            catch (Exception ex)
            {
                return (false, "Complain not sent");
            }
        }

        public async Task<(bool successful, string msg)> DepositAsync(DepositOrWithdraw deposit)
        {

            var User = await _CustomerRepo.GetSingleByAsync(a => a.PinNo == $"\"{deposit.CustomerPin}\"");

            if (User != null)
            {
                User.Balance = User.Balance + deposit.Amount;

                var TransationSender = new TransationRecords()
                {
                    Customer = User,
                    Date = DateTime.Now.ToLongDateString(),
                    Time = DateTime.Now.ToShortTimeString(),
                    Reports = $"Withdrew #{deposit.Amount} from account"
                };

                await _transationRepo.AddAsync(TransationSender);
                await _unitOfWork.SaveChangesAsync();

                return (true, "Deposit Successful");
            }
            else
            {
                return (false, "Deposit Unsuccessful");
            }

        }

        public Task<IEnumerable<TransationRecords>> GetTransationHistoryAsync(Customer customer)
        {

            var history = _transationRepo.GetByAsync(a => a.Customer == customer);

            return history;

        }
        public async Task<(bool successful, string msg)> TransferAsync(TransferModel transferModel)
        {

            var Sender = await _CustomerRepo.GetSingleByAsync(a => a.PinNo == $"\"{transferModel.UserPin}\"");

            var Reciever = await _CustomerRepo.GetSingleByAsync(a => a.CardNo == $"\"{transferModel.ReceiverCardNo}\"");

            if (Sender != null && Reciever != null)
            {
                Sender.Balance = Sender.Balance - transferModel.Amount;

                Reciever.Balance = Reciever.Balance + transferModel.Amount;

                var TransationSender = new TransationRecords()
                {
                    Customer = Sender,
                    Date = DateTime.Now.ToLongDateString(),
                    Time = DateTime.Now.ToShortTimeString(),
                    Reports = $"Transfered #{transferModel.Amount} to {Reciever.CustomerName}"
                };
                var Transationreciever = new TransationRecords()
                {
                    Customer = Reciever,
                    Date = DateTime.Now.ToLongDateString(),
                    Time = DateTime.Now.ToShortTimeString(),
                    Reports = $"Recieved #{transferModel.Amount} from {Sender.CustomerName}"
                };


                await _transationRepo.AddAsync(TransationSender);
                await _transationRepo.AddAsync(Transationreciever);
                await _unitOfWork.SaveChangesAsync();

                return (true, "Transfer Successful");
            }
            else
            {
                return (false, "Transfer Unsuccessful");
            }

        }

        public async Task<decimal> ViewBalanceAsync(Customer customer)
        {
            using (var context = new AtmDbContext())
            {
                var User = await context.Customers.SingleOrDefaultAsync(a => a.CardNo == $"\"{customer.CardNo}\"");

                if (User != null)
                {
                    return User.Balance;
                }
                else
                {
                    return -1;
                }
            }
        }

        public async Task<(bool successful, string msg)> WithdrawalAsync(DepositOrWithdraw withdraw)
        {
            using (var context = new AtmDbContext())
            {
                var User = await context.Customers.SingleOrDefaultAsync(a => a.PinNo == $"\"{withdraw.CustomerPin}\"");

                if (User != null)
                {
                    User.Balance = User.Balance - withdraw.Amount;

                    var TransationSender = new TransationRecords()
                    {
                        Customer = User,
                        Date = DateTime.Now.ToLongDateString(),
                        Time = DateTime.Now.ToShortTimeString(),
                        Reports = $"Withdrew #{withdraw.Amount} from account"
                    };

                    await _transationRepo.AddAsync(TransationSender);
                    await _unitOfWork.SaveChangesAsync();

                    return (true, "Withdraw Successful");
                }
                else
                {
                    return (false, "Withdraw Unsuccessful");
                }
            }
        }
    }
}

