using System;
using System.Collections;
using ATM.BLL.Models;
using ATM.DAL.Model;
using Microsoft.VisualBasic;

namespace ATM.BLL.Interface
{
    public interface IAdminOperations
    {
        Task<(bool successful, string msg)> UpdateCustomerRecordsAsync(Admin admin, UpdateCustomer User);

        Task<(bool successful,string msg)> CreateNewCustomerRecordAsync(Admin admin, Customer customer);

        Task<(bool successful, string msg)> DeleteCustomerRecordsAsync(Admin admin, string CustomerCardNo);

        Task<IEnumerable<Workflow>> GetAdminWorkFlowAsync(Admin admin);

        Task<IEnumerable<Complains>> GetComplainsAsync();
    }
}

