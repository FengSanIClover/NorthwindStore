﻿using Northwind.Entities.Models;
using Northwind.Modules.Interface;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Modules.Service
{
   public class TerritoriesService : Service<Territories>, ITerritories
    {
        public TerritoriesService(IRepositoryAsync<Territories> repository) : base(repository)
        {

        }
    }
}
