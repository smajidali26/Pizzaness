<%@ Page Title="" Language="C#" MasterPageFile="~/templates/main.master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="ContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--Contact Map Area Start-->
    <section class="map-section">
        <div class="row-fluid">
            <div class="span12">
                <div class="heading">
                    <h1>Contact Us</h1>
                    <span class="flag-icon">Flag</span>
                </div>

            </div>
        </div>
    </section>
    <!--Contact Map Area End-->

    <!--Opening Section Start-->
    <section class="opening-section">
        <div class="row-fluid">
            <div class="span8">
                <div class="row-fluid">
                    <article class="span8 mbtm  first">
                        <div class="woocommerce">
                            <div class="checkout">
                                <h2>Leave Us a Message</h2>
                                <p class="form-row form-row-wide">
                                    <label class="">
                                        Name
                                            <abbr title="required" class="required">*</abbr></label>
                                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="*" ClientIDMode="Static" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                                </p>
                                <p class="form-row form-row-wide">
                                    <label class="">
                                        Telephone
                                            <abbr title="required" class="required">*</abbr></label>
                                    <asp:TextBox ID="txtTelephone" runat="server"></asp:TextBox>
                                </p>
                                <p class="form-row form-row-wide">
                                    <label class="">
                                        Email
                                            <abbr title="required" class="required">*</abbr></label>
                                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="*" ClientIDMode="Static" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                                </p>
                                <p class="form-row form-row-wide">
                                    <label class="">
                                        Enquiry
                                            <abbr title="required" class="required">*</abbr></label>
                                    <asp:TextBox ID="txtEnquiry" runat="server" Columns="40" Rows="10" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEnquiry" runat="server" ErrorMessage="*" ClientIDMode="Static" ControlToValidate="txtEnquiry"></asp:RequiredFieldValidator>
                                </p>
                                <p class="form-row form-row-wide">

                                    <asp:Button ID="buttonSendEmail" runat="server" Text=" Send Email " OnClick="buttonSendEmail_Click" CssClass="button alt" ClientIDMode="Static" />
                                </p>
                                <p>
                                    <asp:Label ID="txtMessage" runat="server" Text=""></asp:Label>
                                </p>
                            </div>
                        </div>
                    </article>
                </div>
            </div>
            <div class="span4">
               <div class="opening-hours-box">
                            <div class="address-box">
                                <div class="head">
                                    <h3>Address</h3>
                                    <span class="location">location</span>
                                </div>
                                <div class="address-area">
                                    <ul>
                                        <li class="location">Glendale Plaza
                                            10829 Lanham Sevem Rd.<br />
                                            Glenn Dale, MD 20796 </li>
                                        <li class="call"><strong class="phone">+1301 464 2600</strong> </li>
                                        <li class="fax"><strong class="phone">+1301 464 1100</strong> </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                <div class="opening-hours-box">
                    <div class="head">
                        <h2>Opening Hours</h2>
                        <span class="watch"></span>
                    </div>
                    <div class="chart-sheet">
                        <ul>
                            <li><strong class="day">Monday</strong> <strong class="time">11:00am - 11:00pm</strong> </li>
                            <li class="opening-active"><strong class="day">Tuesday</strong> <strong class="time">11:00am - 11:00pm</strong> </li>
                            <li><strong class="day">Wednesday</strong> <strong class="time">11:00am - 11:00pm</strong> </li>
                            <li class="opening-active"><strong class="day">Thursday</strong> <strong class="time">11:00am - 11:00pm</strong> </li>
                            <li><strong class="day">Friday</strong> <strong class="time">11:00am - 11:00pm</strong> </li>
                            <li class="opening-active"><strong class="day">Satuarday</strong> <strong class="time">11:00am - 12:00am</strong> </li>
                            <li><strong class="day">Sunday</strong> <strong class="time">11:00am - 11:00pm</strong> </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--Opening Section End-->

    <!--Form Section Start-->



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
