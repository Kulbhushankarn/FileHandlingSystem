using System;
using System.Collections.Generic;
using DL_FileHandle;
using RC_Resources;
using VO_FileHandle;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace BL_FileHandle
{
    public class BusinessLogic
    {
        private static DLcls_FileHandlingSystem DL;
        private ValueLayerObject vlo;

        private const string UserCredentialsFilePath = "C:\\Users\\lenovo\\Downloads\\user_credentials.txt";


        private enum UserRole
        {
            Admin,
            Guest,
            Self,
            SuperAdmin
        }

        public static bool Initialize()
        {
            try
            {
                ValueLayerObject.records = new List<string>();
                DL = new DLcls_FileHandlingSystem();
                DL.ConnectToSqlServer();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing: {ex.Message}");
                return false;
            }
        }

        public static bool ReleaseObjects()
        {
            try
            {
                DL.DisconnectFromSqlServer();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error releasing objects: {ex.Message}");
                return false;
            }
        }

        public bool AuthenticateUser(string userId, string password)
        {
            try
            {
                // Read user credentials from the file
                string[] lines = File.ReadAllLines(UserCredentialsFilePath);

                foreach (string line in lines)
                {
                    string[] credentials = line.Split(' ');
                    if (credentials.Length == 2)
                    {
                        string storedUserId = credentials[0];
                        string storedPassword = credentials[1];

                        if (userId.Equals(storedUserId, StringComparison.OrdinalIgnoreCase) && password.Equals(storedPassword))
                        {
                            // Authentication successful
                            return true;
                        }
                    }
                }

                // Authentication failed
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error authenticating user: {ex.Message}");
                return false;
            }
        }
       

        public bool Save(ValueObject vo, ValueLayerObject vlo)
        {
            this.vlo = vlo;

            if (!Validate(vo))
            {
                vo.caption = caption.invalidformdata;
                return false;
            }

            string data;
            if (vo.editMode == EditMode.Create)
            {
                if (!DL.FinalSave(vo))
                {
                    vo.caption = caption.invalidformdata;
                    return false;
                }

                ValueLayerObject.records.Add(vo.dataToStore);
                vlo.serialNumber += 1;
            }
            else if (vo.editMode == EditMode.Update)
            {
                data = PadDataAsPerRecord();
                ValueLayerObject.records.Remove(data);
                ValueLayerObject.records.Add(vo.dataToStore);
                DL.FinalSave(vo);
            }
            else if (vo.editMode == EditMode.Delete)
            {
                data = PadDataAsPerRecord();
                ValueLayerObject.records.Remove(data);
                if (!DL.FinalSave(vo))
                {
                    return false;
                }
            }

            return true;
        }

/*        private bool CheckUserPermissions()
        {
            // Retrieve the user role based on authentication
            if (AuthenticateUser("userID", "password", out UserRole userRole))
            {
                // Check user permissions based on the role
                switch (userRole)
                {
                    case UserRole.Admin:
                        // Admin has all permissions
                        return true;
                    case UserRole.Guest:
                        // Guest has read-only permission
                        return false;
                    case UserRole.Self:
                        // Self user has Add, Edit, Delete permission
                        return true;
                    case UserRole.SuperAdmin:
                        // SuperAdmin has all permissions
                        return true;
                    default:
                        return false;
                }
            }

            return false;
        }*/

        private bool Validate(ValueObject vo)
        {
            if (vo.editMode == EditMode.Create)
            {
                if (string.IsNullOrEmpty(vo.EmployeeData.FirstName) ||
                    vo.EmployeeData.Qualification == -1 ||
                    string.IsNullOrEmpty(vo.EmployeeData.CurrentCompany) ||
                    vo.EmployeeData.DateOfBirth >= DateTime.Now ||
                    vo.EmployeeData.DateOfBirth >= vo.EmployeeData.JoiningDate)
                {
                    vo.error = ErrorMessage.invalidData;
                    return false;
                }
            }

            return true;
        }

        private string PadDataAsPerRecord()
        {
            string data = "";
            data += PadString(vlo.values[0].ToString(), 18);
            data += PadString(vlo.values[1].ToString(), 7);
            data += PadString(vlo.values[2].ToString(), 50);
            data += PadString(vlo.values[3].ToString(), 25);
            data += PadString(vlo.values[4].ToString(), 50);
            data += PadString(vlo.values[5].ToString(), 10);
            if (Enum.IsDefined(typeof(Qualification), vlo.values[6].ToString()))
            {
                Qualification qualification = (Qualification)Enum.Parse(typeof(Qualification), vlo.values[6].ToString());
                int enumIndex = (int)qualification;
                data += PadString(enumIndex.ToString(), 2);
            }
            data += PadString(vlo.values[7].ToString(), 255);
            data += PadString(vlo.values[8].ToString(), 10);
            data += PadString(vlo.values[9].ToString(), 500);
            return data;
        }

        private string PadString(string input, int length)
        {
            return input.Length >= length ? input.Substring(0, length) : input.PadRight(length);
            
        }
    }
}
