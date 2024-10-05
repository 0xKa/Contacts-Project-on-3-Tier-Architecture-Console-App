using System;
using System.Data;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{

    public class clsContact
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImagePath { get; set; }
        public int CountryID { get; set; }

        public clsContact()

        {
            this.ID = -1;
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.CountryID = -1;
            this.ImagePath = "";

            Mode = enMode.AddNew;
        }

        private clsContact(int ID, string FirstName, string LastName, 
            string Email, string Phone, string Address, string ImagePath, DateTime DateOfBirth, int CountryID) 
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.ImagePath = ImagePath;
            this.DateOfBirth = DateOfBirth;
            this.CountryID = CountryID;

            Mode = enMode.Update;
        }

        private bool _AddNewContact()
        {
            //call data access layer

            this.ID = clsContactDataAccess.AddNewContact(FirstName, LastName, Email, Phone,
                Address, ImagePath, DateOfBirth, CountryID);

            return this.ID != -1; //return true if ID not -1
        }

        private bool _UpdateContact()
        {
            return clsContactDataAccess.UpdateContact(ID,FirstName,LastName, Email, 
                Phone, Address, ImagePath, DateOfBirth, CountryID);
        }


        //returns a contact object if contact found
        public static clsContact Find(int id)
        {
            string FirstName = string.Empty
            ,LastName = string.Empty
            ,Email = string.Empty
            ,Phone = string.Empty
            ,Address = string.Empty
            ,ImagePath = string.Empty;

            DateTime DateOfBirth = DateTime.Now;
            int CountryID = -1;

            if (clsContactDataAccess.GetContactInfoByID(id, ref FirstName, ref LastName, ref Email,
                ref Phone, ref Address, ref ImagePath, ref DateOfBirth, ref CountryID))
            {
                return new clsContact(id, FirstName, LastName, Email,
                    Phone, Address, ImagePath, DateOfBirth, CountryID);
            }
            else
                return null;
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    Mode = enMode.Update; //must change mode
                    return _AddNewContact();

                case enMode.Update:
                    return _UpdateContact();

            }

            return false;
        }

        public static bool DeleteContact(int id)
        {
            return clsContactDataAccess.DeleteContact(id);

        }

        public static DataTable GetAllContacts()
        {
            return clsContactDataAccess.GetAllContacts();
        }

        public static bool IsContactExist(int ID)
        {
            return clsContactDataAccess.IsContactExist(ID);
        }

    }
}
