using RC_Resources;
using System;
using System.Data.SqlClient;
using System.Text;
using VO_FileHandle;

namespace DL_FileHandle
{
    public class DLcls_FileHandlingSystem
    {
        private SqlConnection sqlConnection;

        public void ConnectToSqlServer()
        {
            try
            {
                string connectionString = "Server=DESKTOP-26MMTP5;Database=FileHandling;Integrated Security=True;TrustServerCertificate=True";

                sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to SQL Server: " + ex.Message);
            }
        }

        public void DisconnectFromSqlServer()
        {
            try
            {
                // Close the SQL connection
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error disconnecting from SQL Server: " + ex.Message);
            }
        }

        public bool FinalSave(ValueObject vo)
        {
            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    if (vo.dataToStore.Length == 927 && vo.editMode == EditMode.Create)
                    {
                        Insert(vo.dataToStore);
                    }
                    else
                    {
                        Update();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                vo.userDefineError = ex.ToString();
                return false;
            }
        }

        private bool Insert(string data)
        {
            try
            {
                string insertQuery = "INSERT INTO Employee (Prefix, FirstName, MiddleName, LastName, BirthDate, Qualification, CurrentCompany, JoiningDate, CurrentAddress ) VALUES (@Prefix, @FirstName, @MiddleName, @LastName, @BirthDate, @Qualification, @CurrentCompany, @JoiningDate, @CurrentAddress)";

                using (SqlCommand command = new SqlCommand(insertQuery, sqlConnection))
                {
                    EmployeeData record = new EmployeeData(data);

                    // Set parameter values
                    command.Parameters.AddWithValue("@Prefix", record.Prefix);
                    command.Parameters.AddWithValue("@FirstName", record.FirstName);
                    command.Parameters.AddWithValue("MiddleName", record.MiddleName);
                    command.Parameters.AddWithValue("@LastName", record.LastName);
                    command.Parameters.AddWithValue("@BirthDate", record.DateOfBirth);
                    command.Parameters.AddWithValue("Qualification", record.Qualification);
                    command.Parameters.AddWithValue("CurrentCompany", record.CurrentCompany);
                    command.Parameters.AddWithValue("JoiningDate", record.JoiningDate);
                    command.Parameters.AddWithValue("CurrentAddress", record.CurrentAddress);
                    

                    // Execute the command
                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during INSERT operation: " + ex.Message);
                return false;
            }
        }

        private bool Update()
        {
            try
            {
                string updateQuery = "UPDATE Employee SET Prefix = @Prefix, FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, BirthDate = @BirthDate, Qualification = @Qualification, CurrentCompany = @CurrentCompany, JoiningDate = @JoiningDate, CurrentAddress = @CurrentAddress";

                using (SqlCommand command = new SqlCommand(updateQuery, sqlConnection))
                {
                    foreach (string currentRecordString in ValueLayerObject.records)
                    {
                        EmployeeData record = new EmployeeData(currentRecordString);

                        // Set parameter values
                        command.Parameters.AddWithValue("@Prefix", record.Prefix);
                        command.Parameters.AddWithValue("@FirstName", record.FirstName);
                        command.Parameters.AddWithValue("MiddleName", record.MiddleName);
                        command.Parameters.AddWithValue("@LastName", record.LastName);
                        command.Parameters.AddWithValue("@BirthDate", record.DateOfBirth);
                        command.Parameters.AddWithValue("Qualification", record.Qualification);
                        command.Parameters.AddWithValue("CurrentCompany", record.CurrentCompany);
                        command.Parameters.AddWithValue("JoiningDate", record.JoiningDate);
                        command.Parameters.AddWithValue("CurrentAddress", record.CurrentAddress);

                        // Execute the command
                        command.ExecuteNonQuery();

                        // Clear parameters for the next iteration
                        command.Parameters.Clear();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during UPDATE operation: " + ex.Message);
                return false;
            }
        }
    }
}
