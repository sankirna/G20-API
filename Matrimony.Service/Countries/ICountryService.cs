﻿using G20.Core.Domain;
using G20.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.Countries
{
    public interface ICountryService
    {
        Task<IPagedList<Country>> GetCountriesAsync(string name, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<Country> GetByIdAsync(int Id);
        Task InsertAsync(Country entity);
        Task UpdateAsync(Country entity);
        Task DeleteAsync(Country entity);
    }
}
