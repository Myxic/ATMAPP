using System;
using ATM.DAL.Model;

namespace ATM.DAL.Interface
{
    public interface IAdminCurdOperation
    {

        Admin GetAdmin(string Pin);

        IEnumerable<Complains> GetComplains(string Pin);

        
    }
}

