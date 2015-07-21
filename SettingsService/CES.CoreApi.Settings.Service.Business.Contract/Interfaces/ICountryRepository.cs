using System.Collections.Generic;
using CES.CoreApi.Settings.Service.Business.Contract.Models;

namespace CES.CoreApi.Settings.Service.Business.Contract.Interfaces
{
    public interface ICountryRepository
    {
        IEnumerable<CountryModel> GetAll(int languageId, string abbreviationList);
        IEnumerable<CountryModel> GetAll(string culture, string abbreviationList);
    }
}