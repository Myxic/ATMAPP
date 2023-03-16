using System;
using ATM.DAL.Data;
using ATM.DAL.Interface;
using ATM.DAL.Model;

namespace ATM.DAL.Implementation
{
    public class CustomerCurdOperation : ICustomerCurdOperation
    {
      
        public bool CheckCardNo(string CardNo)
        {
            using (var context = new AtmDbContext())
            {
                var User = context.Customers.FirstOrDefault(a => a.CardNo == $"\"{CardNo}\"");

                if (User != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool CheckPin(Customer customer, string GetPin)
        {
            using (var context = new AtmDbContext())
            {
                var User = context.Customers.FirstOrDefault(a => a.CardNo == $"\"{customer.CardNo}\"");

                if (User != null)
                {

                    switch (User.PinNo == GetPin)
                    {
                        case true:
                            return true;

                        default:
                            return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public Customer? GetUser(string CardNo)
        {
            using (var context = new AtmDbContext())
            {
                var User = context.Customers.FirstOrDefault(a => a.CardNo == $"\"{CardNo}\"");

                if (User != null)
                {
                    Customer customer = new()
                    {
                        Id = User.Id,
                        CustomerName = User.CustomerName,
                        CardNo = User.CardNo,
                        PinNo = User.PinNo,
                        PhoneNumber = User.PhoneNumber,
                        Balance = User.Balance,
                        TransationHistory = User.TransationHistory
                    };

                    return customer;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool UpdateUserDetails(string CardNo)
        {
            throw new NotImplementedException();
        }


        public IEnumerator<TransationRecords> GetRecords(Customer customer)
        {
            throw new NotImplementedException();
        }

    }
}

