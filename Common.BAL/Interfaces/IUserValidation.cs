using Common.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.BAL.Interfaces
{
    public interface IUserValidation
    {
        bool PassWordValidation(LoginDTO Login);
        void RegisterUser(LoginDTO Login);
    }
}
