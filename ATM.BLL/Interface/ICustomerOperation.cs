using System;
using System.Collections;
using ATM.BLL.Models;
using ATM.DAL.Model;

namespace ATM.BLL.Interface
{
    public interface ICustomerOperation
    {


        Task<(bool successful, string msg)> DepositAsync(DepositOrWithdraw deposit);

        Task<IEnumerable<TransationRecords>> GetTransationHistoryAsync(Customer customer);

        Task<(bool successful, string msg)> TransferAsync(TransferModel transferModel);

        Task<decimal> ViewBalanceAsync(Customer customer);

        Task<(bool successful, string msg)> ChangePinAsync(ChangePinModel changePin);

        Task<(bool successful, string msg)> WithdrawalAsync(DepositOrWithdraw withdraw);

        Task<(bool successful, string msg)> ComplainsAsync(Complains complains);
       
    }
}

