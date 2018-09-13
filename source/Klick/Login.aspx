<%@ Page Title="" Language="C#" MasterPageFile="~/templates/main.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register Src="~/controls/UserLogin.ascx" TagName="login" TagPrefix="uc1" %>
<%@ Register Src="~/controls/Navigation.ascx" TagName="navigation" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <!--Blog Page Section Start-->
    <section class="blog-page-section">
        <div class="row-fluid">
            <div class="span12">
                <div class="heading">
                    <h1>Login</h1>
                </div>
            </div>
            <!--Blog Post End-->

        </div>
    </section>
    <!--Blog Page Section End-->
    <uc1:login ID="login1" runat="server" />
</asp:Content>

