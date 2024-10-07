using ContactsDataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CountriesDataAccessLayer
{
    public class clsCountryDataAccess
    {
        public static bool GetCountryInfoByID(int ID, ref string CountryName, ref string Code, ref string PhoneCode)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM Countries WHERE CountryID = @id";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    CountryName = Convert.ToString(reader["CountryName"]);
                    Code = Convert.ToString(reader["Code"]);
                    PhoneCode = Convert.ToString(reader["PhoneCode"]);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static bool GetCountryInfoByName(string Name, ref int CountryID, ref string Code, ref string PhoneCode) 
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM Countries WHERE CountryName = @name";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", Name);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    CountryID = Convert.ToInt32(reader["CountryID"]);
                    Code = Convert.ToString(reader["Code"]);
                    PhoneCode = Convert.ToString(reader["PhoneCode"]);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static int AddNewCountry(string CountryName, string Code, string PhoneCode)
        {
            int CountryID = -1;

            SqlConnection connection = new SqlConnection(ContactsDataAccessLayer.clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [dbo].[Countries]
                                       ([CountryName] ,[Code] ,[PhoneCode])
                                 VALUES
                                       (@CountryName, @Code, @PhoneCode);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            //checks if null before inserting to database
            if (Code == string.Empty)
                command.Parameters.AddWithValue("@Code", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Code", Code);

            if (Code == string.Empty)
                command.Parameters.AddWithValue("@PhoneCode", DBNull.Value);
            else
                command.Parameters.AddWithValue("@PhoneCode", PhoneCode);

            try
            {
                connection.Open();
                
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    CountryID = insertedID;
                }

            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }
            
            return CountryID;
        }

        public static bool UpdateCountry(int CountryID, string CountryName, string Code, string PhoneCode)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(ContactsDataAccessLayer.clsDataAccessSettings.ConnectionString);;

            string query = @"UPDATE [dbo].[Countries]
                               SET [CountryName] = @CountryName
                                  ,[Code] = @Code
                                  ,[PhoneCode] = @PhoneCode
                             WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            if (Code == string.Empty)
                command.Parameters.AddWithValue("@Code", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Code", Code);

            if (Code == string.Empty)
                command.Parameters.AddWithValue("@PhoneCode", DBNull.Value);
            else
                command.Parameters.AddWithValue("@PhoneCode", PhoneCode);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally { connection.Close(); }

            return RowsAffected > 0;
        }

        public static bool DeleteCountry(int CountryID)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(ContactsDataAccessLayer.clsDataAccessSettings.ConnectionString);

            string query = @"DELETE FROM [dbo].[Countries]
                                WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally { connection.Close(); }

            return RowsAffected > 0;
        }

        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ContactsDataAccessLayer.clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM Countries";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch
            {
                //
            }
            finally { connection.Close(); }

            return dt;
        }

        public static bool IsCountryExist(int CountryID) 
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT 1 FROM Countries WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {
                connection.Open();
                isFound = (command.ExecuteScalar() != null); 
                
            }
            catch(Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close() ;
            }

            return isFound;
        }
        public static bool IsCountryExist(string CountryName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT 1 FROM Countries WHERE CountryName = @CountryName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();
                isFound = (command.ExecuteScalar() != null);

            }
            catch (Exception ex)
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
