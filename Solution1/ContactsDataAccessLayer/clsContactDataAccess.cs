using System;
using System.Data;
using System.Data.SqlClient;

namespace ContactsDataAccessLayer
{
    public class clsContactDataAccess
    {
        public static bool GetContactInfoByID(int ID, ref string FirstName, ref string LastName,
            ref string Email, ref string Phone, ref string Address, ref string ImagePath,
            ref DateTime DateOfBirth, ref int CountryID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Contacts WHERE ContactID = @id";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    //The Record was Found
                    isFound = true;

                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    ImagePath = Convert.ToString(reader["ImagePath"]); //we use Convert because the ImagePath value can be NULL
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    CountryID = (int)reader["CountryID"];
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                // finally Code will always executed, whether an exception occurs or not
                connection.Close();
            }

            return isFound;
        }

        //add the contact to the db, and returns the ID AutoNumber, if failed id will be -1
        public static int AddNewContact(string FirstName, string LastName,
            string Email, string Phone, string Address, string ImagePath,
            DateTime DateOfBirth, int CountryID)
        {
            int ContactID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [dbo].[Contacts]
                            ([FirstName], [LastName], [Email], [Phone], [Address], [DateOfBirth], [CountryID], [ImagePath]) 
                            VALUES (@fname, @lname, @email, @phone, @address, @DoB, @countryID, @imagepath);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@fname", FirstName);
            command.Parameters.AddWithValue("@lname", LastName);
            command.Parameters.AddWithValue("@email", Email);
            command.Parameters.AddWithValue("@phone", Phone);
            command.Parameters.AddWithValue("@address", Address);
            command.Parameters.AddWithValue("@DoB", DateOfBirth);
            command.Parameters.AddWithValue("@countryID", CountryID);

            // if there is not image path we save it in the DB as NULL
            if (ImagePath == "") 
                command.Parameters.AddWithValue("@imagepath", DBNull.Value);
            else
                command.Parameters.AddWithValue("@imagepath", ImagePath);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ContactID = insertedID; //get the new AutoNumber ID
                }

            }
            catch(Exception ex)
            {
                //
            }
            finally
            {
                connection.Close(); 
            }

            return ContactID;
        }


        public static bool UpdateContact(int ID, string FirstName, string LastName,
            string Email, string Phone, string Address, string ImagePath,
            DateTime DateOfBirth, int CountryID)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[Contacts]
                               SET [FirstName] = @fname
                                  ,[LastName] = @lname
                                  ,[Email] = @email
                                  ,[Phone] = @phone
                                  ,[Address] = @address
                                  ,[DateOfBirth] = @DoB
                                  ,[CountryID] = @countryID
                                  ,[ImagePath] = @imagepath
                             WHERE ContactID = @id;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@id", ID);
            command.Parameters.AddWithValue("@fname", FirstName);
            command.Parameters.AddWithValue("@lname", LastName);
            command.Parameters.AddWithValue("@email", Email);
            command.Parameters.AddWithValue("@phone", Phone);
            command.Parameters.AddWithValue("@address", Address);
            command.Parameters.AddWithValue("@DoB", DateOfBirth);
            command.Parameters.AddWithValue("@countryID", CountryID);

            if (ImagePath == "")
                command.Parameters.AddWithValue("@imagepath", DBNull.Value);
            else
                command.Parameters.AddWithValue("@imagepath", ImagePath);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (RowsAffected > 0); //means update successfully, true
        }

        public static bool DeleteContact(int ID)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"DELETE FROM [dbo].[Contacts]
                              WHERE ContactID = @id";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", ID);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (RowsAffected > 0);
        }

        public static DataTable GetAllContacts()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM Contacts";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dt.Load(reader);

                reader.Close();
            }
            catch (Exception ex)
            {
                //
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool IsContactExist(int ID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT 1 FROM Contacts WHERE ContactID = @id";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", ID);

            try
            {
                connection.Open();

                isFound = (command.ExecuteScalar() != null);

            }
            catch( Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close(); 
            }

            return isFound;
        }



    }
}
