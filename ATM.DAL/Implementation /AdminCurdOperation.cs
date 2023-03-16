using System;
using ATM.DAL.Interface;
using ATM.DAL.Data;
using ATM.DAL.Model;

namespace ATM.DAL.Implementation
{
    public class AdminCurdOperation : IAdminCurdOperation
    {
        public AdminCurdOperation()
        {
        }

        public Admin GetAdmin(string Pin)
        {
            using (var context = new AtmDbContext())
            {
                var admin1 = context.Admins.SingleOrDefault(a => a.AdminPin == $"\"{Pin}\"");

                if (admin1 != null)
                {
                    Admin admin = new()
                    {
                        AdminName = admin1.AdminName,
                        AdminPin = admin1.AdminPin,
                        Id = admin1.Id,
                    };

                    return admin;
                }
                else
                {
                    return null;
                }
            }
        }

        public IEnumerable<Complains> GetComplains(string Pin)
        {
            throw new NotImplementedException();
        }
    }
}

