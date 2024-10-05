using System;
using System.Data;
using ContactsBusinessLayer;
using CountriesBussinessLayer;

namespace ContactsConsoleApp_PresentationLayer
{
    internal class Program
    {
        //testing Contacts Bussiness/Data Layer
        static void testFindContact(int ID)
        {
            clsContact contact1 = clsContact.Find(ID); //returns object if found, else null

            if (contact1 != null )
            {
                Console.WriteLine("ID            : " + contact1.ID);
                Console.WriteLine("Full Name     : " + contact1.FirstName + " " + contact1.LastName);
                Console.WriteLine("Email         : " + contact1.Email);
                Console.WriteLine("Phone         : " + contact1.Phone);
                Console.WriteLine("Address       : " + contact1.Address);
                Console.WriteLine("Date Of Birth : " + contact1.DateOfBirth);
                Console.WriteLine("Country ID    : " + contact1.CountryID);
                Console.WriteLine("Image Path    : " + contact1.ImagePath);
            }
            else
            {
                Console.WriteLine($"Contact With ID = {ID} Not Found!");
            }

        }
        static void testAddNewContact()
        {
            clsContact contact1 = new clsContact();

            //contact info are hard coded just to test
            contact1.FirstName = "reda";
            contact1.LastName = "hilal";
            contact1.Email = "r123@xyz.com";
            contact1.Phone = "91238903";
            contact1.Address = "999 xyz street";
            contact1.DateOfBirth = new DateTime(2004, 8, 6);
            contact1.CountryID = 5;
            contact1.ImagePath = "";

            //save to db using this method
            if (contact1.Save())
                Console.WriteLine("Contact Added Successfully with ID " + contact1.ID);
            else
                Console.WriteLine("Failed To Add New Contact!");

        }
        static void testUpdateContact(int ID)
        {
            clsContact contact = clsContact.Find(ID);

            contact.FirstName = "Mohammad";
            contact.LastName = "Ahmed";
            contact.Email = "email.com";
            contact.Phone = "381903111";
            contact.Address = "000 lol 000";
            contact.DateOfBirth = new DateTime(1999, 4, 6);
            contact.CountryID = 3;
            contact.ImagePath = "";

            if (contact.Save())
                Console.WriteLine("Contact Updated Successfully.");
            else
                Console.WriteLine("Contact Update Failed!");

        }
        static void testDeleteContact(int ID)
        {
            if (clsContact.IsContactExist(ID))
            {
                if (clsContact.DeleteContact(ID))
                    Console.WriteLine($"Client {ID} Deleted Successfully.");
                else
                    Console.WriteLine($"Client {ID} Delete Failed!");
            }
            else
                Console.WriteLine($"Client {ID} Not Found");

        }
        static void testListContacts()
        {
            DataTable dataTable = clsContact.GetAllContacts();

            Console.WriteLine("Contacts Data:");

            foreach(DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["ContactID"]}: {row["FirstName"]} {row["LastName"]}");
            }

        }
        static void testIsContactExist(int ID)
        {
            if (clsContact.IsContactExist(ID))
                Console.WriteLine($"Yes, Contact {ID} Exist.");
            else
                Console.WriteLine($"No, Contact {ID} Does NOT Exist!");
        }

        //testing Countries Bussiness/Data Layer
        static void testFindCountry_ByID(int ID)
        {
            clsCountry country = clsCountry.Find(ID);

            if (country != null)
            {
                Console.WriteLine("Country ID   : " + country.CountryID);
                Console.WriteLine("Country Name : " + country.CountryName);
            }
            else
                Console.WriteLine("Country Not Found!");

        }
        static void testFindCountry_ByName(string CounrtyName)
        {
            clsCountry country = clsCountry.Find(CounrtyName);

            if (country != null)
            {
                Console.WriteLine("Country ID   : " + country.CountryID);
                Console.WriteLine("Country Name : " + country.CountryName);
            }
            else
                Console.WriteLine("Country Not Found!");

        }

        static void testAddNewCountry(string CounrtyName)
        {
            clsCountry country1 = new clsCountry();

            country1.CountryName = CounrtyName;

            if (country1.Save())
                Console.WriteLine($"New Country: \"{CounrtyName}\" Added Successfully.");
            else
                Console.WriteLine($"New Country: \"{CounrtyName}\" Adding Failed!!");
        }

        static void testUpdateCountry(int ID)
        {
            clsCountry country = clsCountry.Find(ID);

            country.CountryName = "Brazil";

            if (country.Save())
                Console.WriteLine("Country Updated Successfully.");
            else
                Console.WriteLine("Country Update Failed!");
        }

        static void testIsCountryExist_ByID(int ID)
        {
            if (clsCountry.IsCountryExist(ID))
                Console.WriteLine($"Yes, Country With ID = {ID}, Does Exist.");
            else
                Console.WriteLine($"No, Country With ID = {ID}, Does not Exist!");
        }
        static void testIsCountryExist_ByName(string CountryName)
        {
            if (clsCountry.IsCountryExist(CountryName))
                Console.WriteLine($"Yes, Country Name = {CountryName}, Does Exist.");
            else
                Console.WriteLine($"No, Country Name = {CountryName}, Does not Exist!");
        }

        static void testDeleteCountry(int ID)
        {
            if (clsCountry.DeleteCountry(ID))
                Console.WriteLine("Country Deleted Successfully.");
            else
                Console.WriteLine("Country Delete Failed.");
        }

        static void testListCountries()
        {
            DataTable dataTable = clsCountry.GetAllCountries();

            Console.WriteLine("Countries Data:");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["CountryID"]}: {row["CountryName"]}");
            }
        }

        static void Main(string[] args)
        {
            //testFindContact(4);
            //testAddNewContact();
            //testUpdateContact(20);
            //testDeleteContact(18);
            //testListContacts();
            //testIsContactExist(5);


            //testFindCountry_ByID(5);
            //testFindCountry_ByName("Canada");
            //testAddNewCountry("Egypt");
            //testUpdateCountry(8);
            //testDeleteCountry(13);
            //testListCountries();
            //testIsCountryExist_ByID(2);
            //testIsCountryExist_ByName("Syria");

            Console.ReadLine();
        }
    }
}
