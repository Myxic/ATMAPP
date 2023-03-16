using System;
using System.Collections;
using ATM.DAL.Model;

namespace ATM.BLL.Interface
{
    public interface IAdminOperations
    {
        bool UpdateCustomerRecords(Admin admin, string CustomerCardNo, Customer User);

        bool CreateNewCustomerRecord(Admin admin, Customer customer);

        bool DeleteCustomerRecords(Admin admin, string CustomerCardNo);

        IEnumerable GetAdminWorkFlow();

        IEnumerable GetComplains();
    }
}

