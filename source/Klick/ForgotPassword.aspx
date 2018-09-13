<%@ Page Title="" Language="C#" MasterPageFile="~/templates/main.master" AutoEventWireup="true" CodeFile="forgotpassword.aspx.cs" Inherits="forgotpassword" %>

<%@ Register Src="~/controls/Navigation.ascx" TagName="navigation" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--Blog Page Section Start-->
    <section class="blog-page-section">
        <div class="row-fluid">
            <div class="span12">
                <div class="heading">
                    <h1>Forgot Password</h1>
                </div>
            </div>
            <!--Blog Post End-->

        </div>
    </section>
    <section class="checkout-page">
        <div class="row-fluid">
            <div class="span2"></div>
            <div class="span8" id="block_content_first">
                <article class="span8 mbtm  first">
                    <div class="woocommerce">
                        <div class="checkout">
                              <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSubmit">
                                  <p class="form-row form-row-wide">
                                      <asp:Label ID="txtMessage" runat="server" Text="" ViewStateMode="Disabled"></asp:Label>
                                  </p>
                                     <p id="billing_password_field" class="form-row form-row-wide">
                                <label class="" for="billing_password">
                                     Email
                                            <abbr title="required" class="required">*</abbr></label>
                                         <asp:TextBox ID="txtEmail" runat="server" SkinID="TextBoxSimple"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="*" ClientIDMode="Static" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                                         </p>
                                  <p class="form-row form-row-wide">
                                      <asp:Button ID="btnSubmit" runat="server" Text="  Get Password  " CssClass="button alt"
                                                OnClick="btnSubmit_Click" ClientIDMode="Static" /> <a href="/Login.aspx" class="lost_password">Login</a>
                                  </p>
                              </asp:Panel>
                        </div>
                    </div>
                </article>
            </div>
        </div>
    </section>
    <!--Blog Page Section End-->
    
</asp:Content>

