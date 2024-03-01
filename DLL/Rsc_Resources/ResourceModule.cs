using System;
using System.ComponentModel;
using System.Reflection;

namespace RC_Resources
{
    public enum EditMode
    {
        Create = 0,
        Update = 1,
        Delete = 2
    }
    public enum ColumnNamesEnum
    {
        [Description("Serial Number")]
        SerialNumber,

        [Description("Prefix")]
        Prefix,

        [Description("First Name")]
        FirstName,

        [Description("Middle Name")]
        MiddleName,

        [Description("Last Name")]
        LastName,

        [Description("Date of Birth")]
        DateOfBirth,

        [Description("Qualification")]
        Qualification,

        [Description("Current Company")]
        CurrentCompany,

        [Description("Joining Date")]
        JoiningDate,

        [Description("Current Address")]
        CurrentAddress
    }

public enum Qualification : byte
{
    [Description("Tenth Grade")]
    TenthGrade,
    
    [Description("Twelfth Grade")]
    TwelfthGrade,
    
    [Description("Diploma")]
    Diploma,

    [Description("BSc")]
    BSc,
    
    [Description("BCA")]
    BCA,
    
    [Description("BA")]
    BA,
    
    [Description("BTech")]
    BTech,
    
    [Description("BTech CSE")]
    BTechCSE,
    
    [Description("BTech Civil")]
    BTechCivil,
    
    [Description("BTech IT")]
    BTechIT,
    
    [Description("BE")]
    BE,
    
    [Description("MSc")]
    MSc,
    
    [Description("MCA")]
    MCA
}

    public enum ErrorMessage
    {
        [Description("Value of Date Of Birth show be Less than Today's.")]
        errordateOfBirthGreaterThanToday,

        [Description("Value of Date Of Birth show be Less than Joininng Date")]
        errordateOfBirthGreaterThanjoiningdate,


        [Description("First Name is Required")]
        firstnameEmpty,

        [Description("Qualification is Required")]
        qualificationEmpty,

        [Description("Current Company is Required")]
        currentCompanyEmpty,
        invalidData
    }

    public enum caption
    {
        [Description("Information Required")]
        invalidformdata
    }

    public enum Messages
    {
        [Description("Do you want to delete this row?")]
        deleteRecord,

        [Description("You may lost your changed data")]
        clearForm
    }

    public enum MessageCaptions
    {
        [Description("Delete Record")]
        deleteRecord,

        [Description("Clear Record")]
        clearForm,
    }

    public enum Title
    {
        [Description("Add/Edit User Form")]
        addeditform,

        [Description("View User Data")]
        viewOnlyForm,
    }

    public class Resources
    {        
        public string GetEnumDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

            return attribute == null ? value.ToString() : attribute.Description;
        }
    }

}