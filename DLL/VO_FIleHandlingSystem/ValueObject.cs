using RC_Resources;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace VO_FileHandle
{
    public class ValueObject
    {
        public EmployeeData EmployeeData;
        public string dataToStore;
        public EditMode editMode;
        public ErrorMessage error;
        public caption caption;
        public string userDefineError;

        public ValueObject()
        {
            this.dataToStore = "";
        }

        public ValueObject(string data)
        {
            EmployeeData = new EmployeeData(data);
            this.dataToStore = data;
        }
    }

    public struct EmployeeData
    {
        public long SerialNumber;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string Prefix;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string FirstName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 25)]
        public string MiddleName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string LastName;

        public DateTime DateOfBirth;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
        public int Qualification;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
        public string CurrentCompany;

        public DateTime JoiningDate;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 500)]
        public string CurrentAddress;


        public EmployeeData(string data)
        {
            if (data.Length != 927)
            {
                throw new ArgumentException("Data length is not 927.");
            }

            // Assign substrings to properties
            SerialNumber = Convert.ToInt64(data.Substring(0, 18));
            Prefix = data.Substring(18, 7).TrimEnd();
            FirstName = data.Substring(25, 50).TrimEnd();
            MiddleName = data.Substring(75, 25).TrimEnd();
            LastName = data.Substring(100, 50).TrimEnd();
            DateOfBirth = DateTime.Parse(data.Substring(150, 10));
            Qualification = int.Parse(data.Substring(160, 2));
            CurrentCompany = data.Substring(162, 255).TrimEnd();
            JoiningDate = DateTime.Parse(data.Substring(417, 10));
            CurrentAddress = data.Substring(427, 500).TrimEnd();
        }

    }


    public class ValueLayerObject
    {
        public string[,] userData;
        public long serialNumber;
        public object[] values;
        public string UserFormType = "";
        public static List<string> records;
    }
}
