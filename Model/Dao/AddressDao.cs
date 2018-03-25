using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public class AddressDao
    {
        OnlineShopEntities db = null;
        public AddressDao()
        {
            db = new OnlineShopEntities();
        }
        public List<Country> getCountries()
        {
            return db.Countries.ToList();
        }
        public List<District> getDistricts(int Id=0)
        {
            return db.Districts.Where(w=>w.ProvinceId==Id).ToList();
        }
        public List<Province> getProvinces(int Id=237)
        {
            return db.Provinces.Where(w=>w.CountryId==Id).ToList();
        }
    }
}
