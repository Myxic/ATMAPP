using System;
using System.Diagnostics.Metrics;
using ATM.DAL.Model;

namespace ATM.DAL.Interface
{
    public interface ICustomerCurdOperation
    {


        Customer? GetUser(string CardNo);

        bool UpdateUserDetails(string CardNo);

        bool CheckCardNo(string CardNo);

        bool CheckPin(Customer customer, string GetPin);



        IEnumerator<TransationRecords> GetRecords(Customer customer);
    }
}

