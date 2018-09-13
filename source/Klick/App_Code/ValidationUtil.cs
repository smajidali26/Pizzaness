using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for ValidationUtil
/// </summary>
public class ValidationUtil
{
	public ValidationUtil()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static bool IsName(string strToCheck)
    {
        Regex objPattern = new Regex("^+[a-zA-Z\"-'\\s]*$");
        bool result = objPattern.IsMatch(strToCheck);
        return result;
    }
    public static bool IsPositiveInteger(string strToCheck)
    {
        Regex objPattern = new Regex("^\\d+$");
        bool result = objPattern.IsMatch(strToCheck);
        return result;
    }
    public static bool IsEmail(string strToCheck)
    {
        Regex objPattern = new Regex("^(?(\"\")(\"\".+?\"\"@)|(([0-9a-zA-Z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-zA-Z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,6}))$");
        bool result = objPattern.IsMatch(strToCheck);
        return result;
    }
    public static bool IsUrl(string strToCheck)
    {
        Regex objPattern = new Regex("^(ht|f)tp(s?)\\:\\/\\/[0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*(:(0-9)*)*(\\/?)([a-zA-Z0-9\\-\\.\\?\\,\'\\/\\\\+&amp;%\\$#_]*)?$");
        bool result = objPattern.IsMatch(strToCheck);
        return result;
    }
}
