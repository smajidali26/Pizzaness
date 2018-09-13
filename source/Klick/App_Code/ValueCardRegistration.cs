
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

[DebuggerStepThrough]
[GeneratedCode("System.Xml", "2.0.50727.5476")]
[DesignerCategory("code")]
[XmlType(Namespace = "https://ws.valutec.net/")]
[Serializable]
public class ValueCardRegistration
{
    public bool Authorized { get; set; }
    public string ErrMessage { get; set; }
    public string CardNumber { get; set; }
    public string Name { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string Telephone { get; set; }
    public string EmailAddress { get; set; }
    public string DOB { get; set; }
    public string Misc1 { get; set; }
    public string Misc2 { get; set; }
    public string Misc3 { get; set; }
    public string Misc4 { get; set; }
    public string Misc5 { get; set; }
}
