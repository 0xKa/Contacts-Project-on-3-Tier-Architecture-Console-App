using System;
using System.Data;
using CountriesDataAccessLayer;

namespace CountriesBussinessLayer
{
    public class clsCountry
    {

        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public string Code { get; set; }
        public string PhoneCode { get; set; }

        public clsCountry() 
        {
            this.CountryID = -1;
            this.CountryName = string.Empty;
            this.Code = string.Empty;
            this.PhoneCode = string.Empty;
            
            Mode = enMode.AddNew;
        }

        public clsCountry(int countryID, string countryName, string countryCode, string countryPhoneCode)
        {
            this.CountryID = countryID;
            this.CountryName = countryName;
            this.Code = countryCode;
            this.PhoneCode = countryPhoneCode;
            
            Mode = enMode.Update;
        }

        public static clsCountry Find(int countryID) 
        {
            string CountryName = string.Empty;
            string Code = string.Empty;
            string PhoneCode = string.Empty;

            if (clsCountryDataAccess.GetCountryInfoByID(countryID,ref CountryName, ref Code, ref PhoneCode))
            {
                return new clsCountry(countryID, CountryName, Code, PhoneCode);
            }
            else
                return null;
        }       
        public static clsCountry Find(string countryName) 
        {
            int CountryID = -1;
            string Code = string.Empty;
            string PhoneCode = string.Empty;

            if (clsCountryDataAccess.GetCountryInfoByName(countryName,ref CountryID, ref Code, ref PhoneCode))
            {
                return new clsCountry(CountryID, countryName, Code, PhoneCode);
            }
            else
                return null;
        }

        private bool _AddNewCountry()
        {
            if (IsCountryExist(this.CountryName))
                return false;

            this.CountryID = clsCountryDataAccess.AddNewCountry(CountryName,Code,PhoneCode);

            return this.CountryID != -1;
        }

        private bool _UpdateCountry()
        {
            return clsCountryDataAccess.UpdateCountry(CountryID, CountryName, Code, PhoneCode) ;
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    Mode = enMode.Update;
                    return _AddNewCountry();
                    
                case enMode.Update: 
                    return _UpdateCountry();

            }
            return false;
        }
        public static bool DeleteCountry(int CountryID)
        {
            if (IsCountryExist(CountryID))
                return clsCountryDataAccess.DeleteCountry(CountryID);
            else
                return false;
        }

        public static DataTable GetAllCountries()
        {
            return clsCountryDataAccess.GetAllCountries();
        }

        public static bool IsCountryExist(int CountryID)
        {
            return clsCountryDataAccess.IsCountryExist(CountryID);
        }
        public static bool IsCountryExist(string CountryName)
        {
            return clsCountryDataAccess.IsCountryExist(CountryName);
        }

    }
}
