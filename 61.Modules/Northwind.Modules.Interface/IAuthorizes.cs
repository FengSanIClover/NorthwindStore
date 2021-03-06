﻿using Northwind.Domain.Models;
using Northwind.Entities.Models;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Modules.Interface
{
   public interface IAuthorizes : IService<Authorizes>
    {
        Task<UserInfo> CreateUserInfo(string userAccount, string userPassword);
    }
}
