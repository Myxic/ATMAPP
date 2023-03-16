using System;
using System.Collections;
using System.Threading.Tasks;
using ATM.BLL.Interface;
using ATM.DAL.Data;
using ATM.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ATM.BLL.Implementation
{
    public class CustomerOperation : ICustomerOperation
    {
        private readonly DAL.Repository.IRepository<Customer> _CustomerRepo;

        public CustomerOperation()
        {
        }

        public async Task<bool> ComplainsAsync(Complains complains)
        {
            try
            {
                using (var context = new AtmDbContext())
                {
                    var Complain = new Complains
                    {
                        Subject = complains.Subject,
                        CardNo = complains.CardNo,
                        Reports = complains.Reports
                    };
                    await context.Complains.AddAsync(Complain);
                    await context.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DepositAsync(string CardNo, decimal Amount)
        {
            using (var context = new AtmDbContext())
            {
                var User = await context.Customers.SingleOrDefaultAsync(a => a.CardNo == $"\"{CardNo}\"");

                if (User != null)
                {
                    User.Balance = User.Balance + Amount;

                    await context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false; 
		        }
	        }
        }

        public  Task<IEnumerable<TransationRecords>> GetTransationHistoryAsync(Customer customer)
        {
            // using (var context = new AtmDbContext())
            // {
            //     Task<IEnumerable<TransationRecords>> GetAllAsync(Func<IQueryable<TransationRecords>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);


            //     var Records = await context.Customers.GetAllAsync(include:  => u.Inclu)

            //     return Records;

            //     //return (await _userRepo.GetAllAsync(include: u => u.Include(t => t.TodoList))).Select(u => new UserWithTaskVM
            //     //{
            //     //    Fullname = u.FullName,
            //     //    Tasks = u.TodoList.Select(t => new TaskVM
            //     //    {
            //     //        Title = t.Title,
            //     //        Description = t.Description,
            //     //        DueDate = t.DueDate.ToString("d"),
            //     //        Priority = t.Priority.ToString(),
            //     //        Status = t.IsDone ? "Done" : "Not Done"
            //     //    })
            //     //});
            // }
            throw new NotImplementedException() ;
        }

            
        

        public async Task<bool> TransferAsync(string CustomerCardNo, decimal Amount, string RecieverCardNo)
        {
            using (var context = new AtmDbContext())
            {
                var Sender = await context.Customers.SingleOrDefaultAsync(a => a.CardNo == $"\"{CustomerCardNo}\"");

                var Reciever = await context.Customers.SingleOrDefaultAsync(a => a.CardNo == $"\"{RecieverCardNo}\"");

                if (Sender != null && Reciever != null)
                {
                    Sender.Balance = Sender.Balance - Amount;

                    Reciever.Balance = Reciever.Balance + Amount;

                    var TransationSender = new TransationRecords()
                    {
                        Customer = Sender,
                        Date = DateTime.Now.ToLongDateString(),
                        Time = DateTime.Now.ToShortTimeString(),
                        Reports = $"Transfered #{Amount} to {Reciever.CustomerName}"
                    };
                    var Transationreciever = new TransationRecords()
                    {
                        Customer = Reciever,
                        Date = DateTime.Now.ToLongDateString(),
                        Time = DateTime.Now.ToShortTimeString(),
                        Reports = $"Recieved #{Amount} from {Sender.CustomerName}"
                    };

                   
                    await context.Transations.AddAsync(TransationSender);
                    await context.Transations.AddAsync(Transationreciever);
                    await context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
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

        public async Task<bool> WithdrawalAsync(string CardNo, decimal Amount)
        {
            using (var context = new AtmDbContext())
            {
                var User = await context.Customers.SingleOrDefaultAsync(a => a.CardNo == $"\"{CardNo}\"");

                if (User != null)
                {
                    User.Balance = User.Balance - Amount;

                    await context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}

