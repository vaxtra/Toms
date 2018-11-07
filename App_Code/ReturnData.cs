using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ReturnData
/// </summary>
public class ReturnData
{
    public bool success { get; set; }
    public string message { get; set; }
    public dynamic data { get; set; }
    public ReturnData()
    {
    }

    public ReturnData(bool _success, string _message, dynamic _data)
    {
        success = _success;
        message = _message;
        data = _data;
    }

    public static ReturnData MessageFailed(string _descriptionMessage, dynamic _data)
    {
        return (new ReturnData(false, _descriptionMessage, _data));
    }

    public static ReturnData MessageSuccess(string _descriptionMessage, dynamic _data)
    {
        return (new ReturnData(true, _descriptionMessage, _data));
    }
}