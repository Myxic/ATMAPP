using System;
using System.Collections;
using System.Text;
using ATM.BLL.Interface;
using ATM.DAL.Data;
using ATM.DAL.Model;

namespace ATM.BLL.Implementation
{
    public class AdminOperation : IAdminOperations
    {   public static StringBuilder stringBuilder = new();
        

        public bool CreateNewCustomerRecord(Admin admin, Customer customer)
        {
            try
            {

                using (var context = new AtmDbContext())
                {
                    var workflow = new Workflow
                    {
                        Name = admin.AdminName,
                        Date = DateTime.Now.ToLongDateString(),
                        Time = DateTime.Now.ToShortTimeString(),
                        WorkTimeLine = $"Added {customer.CustomerName} into the Database"
                    };

                    context.Workflows.Add(workflow);
                    context.Customers.Add(customer);
                    context.SaveChanges();

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateCustomerRecords(Admin admin, string CustomerCardNo, Customer User)
        {
            using (var context = new AtmDbContext())
            {
                var customer = context.Customers.SingleOrDefault(a => a.CardNo == $"\"{CustomerCardNo}\"");

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

                    context.Workflows.Add(workflow);
                    context.SaveChanges();

                    return true;
                }

                return false;

            }
        }

        public bool DeleteCustomerRecords(Admin admin, string CustomerCardNo)
        {
            using (var context = new AtmDbContext())
            {
                var customer = context.Customers.SingleOrDefault(a => a.CardNo == $"\"{CustomerCardNo}\"");

                if (customer != null)
                {
                    var workflow = new Workflow
                    {
                        Name = admin.AdminName,
                        Date = DateTime.Now.ToLongDateString(),
                        Time = DateTime.Now.ToShortTimeString(),
                        WorkTimeLine = $"Deleted {customer.CustomerName} from the Database"
                    };

                    context.Workflows.Add(workflow);
                    context.Customers.Remove(customer);
                    context.SaveChanges();
                     
                    return true;
                }
               
                return false;

            }
        }

        public IEnumerable GetAdminWorkFlow()
        {
            throw new NotImplementedException();


        }

        public IEnumerable GetComplains()
        {
            throw new NotImplementedException();
        }

       
    }
}

