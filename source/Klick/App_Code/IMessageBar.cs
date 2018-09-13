using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IMessageBar
/// </summary>

public interface IMessageBar
{
    void ShowMessage(String message, MessageType type);
}

