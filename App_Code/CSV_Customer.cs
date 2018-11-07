using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CSV_Customer
/// </summary>
public class CSV_Customer
{
    public CSV_Customer()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private int _gender;

    public int Gender
    {
        get { return _gender; }
        set { _gender = value; }
    }

    private string _firstName;

    public string FirstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }

    private string _lastName;

    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }

    private string _email;

    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }

    private string _createDate;

    public string CreateDate
    {
        get { return _createDate; }
        set { _createDate = value; }
    }

    private string _birthday;

    public string Birthday
    {
        get { return _birthday; }
        set { _birthday = value; }
    }

    private string _lastUpdateDate;

    public string LastUpdateDate
    {
        get { return _lastUpdateDate; }
        set { _lastUpdateDate = value; }
    }
}