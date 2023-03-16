using System;
using System.Collections;
using ATM.DAL.Model;

namespace ATM.BLL.Interface
{
    public interface ICustomerOperation
    {
       

        Task<bool> DepositAsync(string CardNo, decimal Amount);


        Task<IEnumerable<TransationRecords>> GetTransationHistoryAsync(Customer customer);


        Task<bool> TransferAsync(string CustomerCardNo, decimal Amount, string RecieverCardNo);



        Task<decimal> ViewBalanceAsync(Customer customer);


        Task<bool> WithdrawalAsync(string CardNo, decimal Amount);

        Task<bool> ComplainsAsync(Complains complains);
       
    }
}

