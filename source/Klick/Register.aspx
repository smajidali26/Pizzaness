<%@ Page Title="Register" Language="C#" MasterPageFile="~/templates/main.master" AutoEventWireup="true"
    CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--Blog Page Section Start-->
    <section class="blog-page-section">
        <div class="row-fluid">
            <div class="span12">
                <div class="heading">
                    <h1>Register</h1>
                </div>
            </div>
            <!--Blog Post End-->

        </div>
    </section>
    <!--Blog Page Section End-->

    <section class="checkout-page">
        <div class="row-fluid">
            <div class="span2"></div>
            <div class="span8" id="block_content_first">
                <article class="span8 mbtm  first">
                    <div class="woocommerce">
                        <div class="checkout">
                            <asp:Panel ID="Panel1" runat="server" CssClass="col-1" DefaultButton="SubmitButton">

                            
                        
                            <h3>Personal Detail</h3>
                            <p id="billing_title_field" class="form-row form-row-wide address-field update_totals_on_change validate-required woocommerce-validated">
                                <label class="" for="billing_country">
                                    Title
                                            <abbr title="required" class="required">*</abbr></label>
                                <asp:DropDownList ID="txtCbTitle" SkinID="ChosenDropDown" runat="server">
                                    <asp:ListItem Text="Select Title" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Mr." Value="Mr."></asp:ListItem>
                                    <asp:ListItem Text="Mrs." Value="Mrs."></asp:ListItem>
                                    <asp:ListItem Text="Miss." Value="Miss."></asp:ListItem>
                                    <asp:ListItem Text="Dr." Value="Dr."></asp:ListItem>
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ErrorMessage="Select title" ControlToValidate="txtCbTitle" InitialValue="0"></asp:RequiredFieldValidator>
                            </p>
                            <p id="billing_first_name_field" class="form-row form-row-wide">
                                <label class="" for="billing_first_name">
                                    First Name
                                    <abbr title="required" class="required">*</abbr></label>
                                <asp:TextBox SkinID="TextBoxSimple" ID="txtFirstName" runat="server"  MaxLength="30"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="Enter first name" ControlToValidate="txtFirstName"></asp:RequiredFieldValidator>
                            </p>
                            <p id="billing_last_name_field" class="form-row form-row-wide">
                                <label class="" for="billing_last_name">
                                    Last Name
                                            <abbr title="required" class="required">*</abbr></label>
                                <asp:TextBox SkinID="TextBoxSimple" ID="txtLastName" runat="server" MaxLength="30"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="Enter last name" ControlToValidate="txtLastName"></asp:RequiredFieldValidator>
                            </p>
                            <p id="billing_email_field" class="form-row form-row-wide">
                                <label class="" for="billing_email">
                                    Email
                                            <abbr title="required" class="required">*</abbr></label>
                                <asp:TextBox SkinID="TextBoxSimple" ID="txtEmail" runat="server" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter password" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                            </p>
                            <p id="billing_password_field" class="form-row form-row-wide">
                                <label class="" for="billing_password">
                                    Password
                                            <abbr title="required" class="required">*</abbr></label>
                                <asp:TextBox SkinID="TextBoxSimple" ID="txtPassword" runat="server" TextMode="Password" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Enter password" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                            </p>
                            <p id="billing_confirm_password_field" class="form-row form-row-wide">
                                <label class="" for="billing_confirm_password">
                                    Confirm Password
                                            <abbr title="required" class="required">*</abbr></label>
                                <asp:TextBox SkinID="TextBoxSimple" ID="txtConfirmPassword" runat="server" TextMode="Password" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ErrorMessage="Enter confirm password" ControlToValidate="txtConfirmPassword"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvConfirmPassword" runat="server" ErrorMessage="Password and Confirm password does not match." ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword"></asp:CompareValidator>
                            </p>
                            <p id="billing_telephone_field" class="form-row form-row-wide">
                                <label class="" for="billing_Telephone">
                                    Telephone
                                            <abbr title="required" class="required">*</abbr></label>
                                <asp:TextBox SkinID="TextBoxSimple" ID="txtTelephone" runat="server" MaxLength="30"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTelephone" runat="server" ErrorMessage="Enter telephone" ControlToValidate="txtTelephone"></asp:RequiredFieldValidator>
                                 </p>
                            <p id="billing_mobile_field" class="form-row form-row-wide">
                                <label class="" for="billing_mobile">
                                    Mobile
                                            </label>
                                <asp:TextBox SkinID="TextBoxSimple" ID="txtMobile" runat="server" MaxLength="30"></asp:TextBox>
                                </p>
                            <h3>Address</h3>
                             <p id="billing_address_field" class="form-row form-row-wide">
                                <label class="" for="billing_address">
                                    Address
                                            <abbr title="required" class="required">*</abbr></label>
                             <asp:TextBox SkinID="TextBoxSimple" ID="txtAddress" runat="server" MaxLength="200"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ErrorMessage="Enter address" ControlToValidate="txtAddress"></asp:RequiredFieldValidator>    
                             </p>
                             <p id="billing_city_field" class="form-row form-row-wide">
                                <label class="" for="billing_city">
                                    City
                                            <abbr title="required" class="required">*</abbr></label>
                                 <asp:TextBox SkinID="TextBoxSimple" ID="txtCity" runat="server" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCity" runat="server" ErrorMessage="Enter city" ControlToValidate="txtCity"></asp:RequiredFieldValidator>
                                 </p>
                            <p id="billing_state_field" class="form-row form-row-wide">
                                <label class="" for="billing_state">
                                    State
                                            <abbr title="required" class="required">*</abbr></label>
                                <asp:TextBox SkinID="TextBoxSimple" ID="txtState" runat="server" MaxLength="2"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="rfvState" runat="server" ErrorMessage="Enter state" ControlToValidate="txtState"></asp:RequiredFieldValidator>
                                </p>
                            
                            <p id="billing_zip_code_field" class="form-row form-row-wide">
                                <label class="" for="billing_zip_code">
                                    Zip Code
                                            <abbr title="required" class="required">*</abbr></label>
                                <asp:TextBox SkinID="TextBoxSimple" ID="txtZipCode" runat="server" MaxLength="5"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvZipCode" runat="server" ErrorMessage="Enter zip code" ControlToValidate="txtZipCode"></asp:RequiredFieldValidator>
                                </p>
                         <p id="billing_country_field" class="form-row form-row-wide">
                                <label class="" for="billing_country">
                                    Country
                                            <abbr title="required" class="required">*</abbr></label>
                             <asp:DropDownList ID="txtCbCountry" runat="server" CssClass="float-left">

                                                <asp:ListItem Text="Select Country" Value="0" />
                                                <asp:ListItem Text="Afghanistan" Value="Afghanistan" />
                                                <asp:ListItem Text="Aland Islands" Value="Aland Islands" />
                                                <asp:ListItem Text="Albania" Value="Albania" />
                                                <asp:ListItem Text="Algeria" Value="Algeria" />
                                                <asp:ListItem Text="American Samoa" Value="American Samoa" />
                                                <asp:ListItem Text="Andorra" Value="Andorra" />
                                                <asp:ListItem Text="Angola" Value="Angola" />
                                                <asp:ListItem Text="Anguilla" Value="Anguilla" />
                                                <asp:ListItem Text="Antarctica" Value="Antarctica" />
                                                <asp:ListItem Text="Antigua and Barbuda" Value="Antigua and Barbuda" />
                                                <asp:ListItem Text="Argentina" Value="Argentina" />
                                                <asp:ListItem Text="Armenia" Value="Armenia" />
                                                <asp:ListItem Text="Aruba" Value="Aruba" />
                                                <asp:ListItem Text="Australia" Value="Australia" />
                                                <asp:ListItem Text="Austria" Value="Austria" />
                                                <asp:ListItem Text="Azerbaijan" Value="Azerbaijan" />
                                                <asp:ListItem Text="Bahamas" Value="Bahamas" />
                                                <asp:ListItem Text="Bahrain" Value="Bahrain" />
                                                <asp:ListItem Text="Bangladesh" Value="Bangladesh" />
                                                <asp:ListItem Text="Barbados" Value="Barbados" />
                                                <asp:ListItem Text="Belarus" Value="Belarus" />
                                                <asp:ListItem Text="Belgium" Value="Belgium" />
                                                <asp:ListItem Text="Belize" Value="Belize" />
                                                <asp:ListItem Text="Benin" Value="Benin" />
                                                <asp:ListItem Text="Bermuda" Value="Bermuda" />
                                                <asp:ListItem Text="Bhutan" Value="Bhutan" />
                                                <asp:ListItem Text="Bolivia" Value="Bolivia" />
                                                <asp:ListItem Text="Bosnia and Herzegovina" Value="Bosnia and Herzegovina" />
                                                <asp:ListItem Text="Botswana" Value="Botswana" />
                                                <asp:ListItem Text="Bouvet Island" Value="Bouvet Island" />
                                                <asp:ListItem Text="Brazil" Value="Brazil" />
                                                <asp:ListItem Text="British Indian Ocean Territory" Value="British Indian Ocean Territory" />
                                                <asp:ListItem Text="Brunei" Value="Brunei" />
                                                <asp:ListItem Text="Bulgaria" Value="Bulgaria" />
                                                <asp:ListItem Text="Burkina Faso" Value="Burkina Faso" />
                                                <asp:ListItem Text="Burundi" Value="Burundi" />
                                                <asp:ListItem Text="Cambodia" Value="Cambodia" />
                                                <asp:ListItem Text="Cameroon" Value="Cameroon" />
                                                <asp:ListItem Text="Canada" Value="Canada" />
                                                <asp:ListItem Text="Cape Verde" Value="Cape Verde" />
                                                <asp:ListItem Text="Cayman Islands" Value="Cayman Islands" />
                                                <asp:ListItem Text="Central African Republic" Value="Central African Republic" />
                                                <asp:ListItem Text="Chad" Value="Chad" />
                                                <asp:ListItem Text="Chile" Value="Chile" />
                                                <asp:ListItem Text="China" Value="China" />
                                                <asp:ListItem Text="Christmas Island" Value="Christmas Island" />
                                                <asp:ListItem Text="Cocos Islands" Value="Cocos Islands" />
                                                <asp:ListItem Text="Colombia" Value="Colombia" />
                                                <asp:ListItem Text="Comoros" Value="Comoros" />
                                                <asp:ListItem Text="Congo" Value="Congo" />
                                                <asp:ListItem Text="Congo, Democratic Republic of the" Value="Congo, Democratic Republic of the" />
                                                <asp:ListItem Text="Cook Islands" Value="Cook Islands" />
                                                <asp:ListItem Text="Costa Rica" Value="Costa Rica" />
                                                <asp:ListItem Text="Croatia" Value="Croatia" />
                                                <asp:ListItem Text="Cuba" Value="Cuba" />
                                                <asp:ListItem Text="Cyprus" Value="Cyprus" />
                                                <asp:ListItem Text="Czech Republic" Value="Czech Republic" />
                                                <asp:ListItem Text="Denmark" Value="Denmark" />
                                                <asp:ListItem Text="Djibouti" Value="Djibouti" />
                                                <asp:ListItem Text="Dominica" Value="Dominica" />
                                                <asp:ListItem Text="Dominican Republic" Value="Dominican Republic" />
                                                <asp:ListItem Text="East Timor" Value="East Timor" />
                                                <asp:ListItem Text="Ecuador" Value="Ecuador" />
                                                <asp:ListItem Text="Egypt" Value="Egypt" />
                                                <asp:ListItem Text="El Salvador" Value="El Salvador" />
                                                <asp:ListItem Text="Equatorial Guinea" Value="Equatorial Guinea" />
                                                <asp:ListItem Text="Eritrea" Value="Eritrea" />
                                                <asp:ListItem Text="Estonia" Value="Estonia" />
                                                <asp:ListItem Text="Ethiopia" Value="Ethiopia" />
                                                <asp:ListItem Text="Falkland Islands" Value="Falkland Islands" />
                                                <asp:ListItem Text="Faroe Islands" Value="Faroe Islands" />
                                                <asp:ListItem Text="Fiji" Value="Fiji" />
                                                <asp:ListItem Text="Finland" Value="Finland" />
                                                <asp:ListItem Text="France" Value="France" />
                                                <asp:ListItem Text="French Guiana" Value="French Guiana" />
                                                <asp:ListItem Text="French Polynesia" Value="French Polynesia" />
                                                <asp:ListItem Text="French Southern Territories" Value="French Southern Territories" />
                                                <asp:ListItem Text="Gabon" Value="Gabon" />
                                                <asp:ListItem Text="Gambia" Value="Gambia" />
                                                <asp:ListItem Text="Georgia" Value="Georgia" />
                                                <asp:ListItem Text="Germany" Value="Germany" />
                                                <asp:ListItem Text="Ghana" Value="Ghana" />
                                                <asp:ListItem Text="Gibraltar" Value="Gibraltar" />
                                                <asp:ListItem Text="Greece" Value="Greece" />
                                                <asp:ListItem Text="Greenland" Value="Greenland" />
                                                <asp:ListItem Text="Grenada" Value="Grenada" />
                                                <asp:ListItem Text="Guadeloupe" Value="Guadeloupe" />
                                                <asp:ListItem Text="Guam" Value="Guam" />
                                                <asp:ListItem Text="Guatemala" Value="Guatemala" />
                                                <asp:ListItem Text="Guernsey" Value="Guernsey" />
                                                <asp:ListItem Text="Guinea" Value="Guinea" />
                                                <asp:ListItem Text="Guinea-Bissau" Value="Guinea-Bissau" />
                                                <asp:ListItem Text="Guyana" Value="Guyana" />
                                                <asp:ListItem Text="Haiti" Value="Haiti" />
                                                <asp:ListItem Text="Heard Island and McDonald Islands" Value="Heard Island and McDonald Islands" />
                                                <asp:ListItem Text="Honduras" Value="Honduras" />
                                                <asp:ListItem Text="Hong Kong" Value="Hong Kong" />
                                                <asp:ListItem Text="Hungary" Value="Hungary" />
                                                <asp:ListItem Text="Iceland" Value="Iceland" />
                                                <asp:ListItem Text="India" Value="India" />
                                                <asp:ListItem Text="Indonesia" Value="Indonesia" />
                                                <asp:ListItem Text="Iran" Value="Iran" />
                                                <asp:ListItem Text="Iraq" Value="Iraq" />
                                                <asp:ListItem Text="Ireland" Value="Ireland" />
                                                <asp:ListItem Text="Isle of Man" Value="Isle of Man" />
                                                <asp:ListItem Text="Israel" Value="Israel" />
                                                <asp:ListItem Text="Italy" Value="Italy" />
                                                <asp:ListItem Text="Jamaica" Value="Jamaica" />
                                                <asp:ListItem Text="Japan" Value="Japan" />
                                                <asp:ListItem Text="Jersey" Value="Jersey" />
                                                <asp:ListItem Text="Jordan" Value="Jordan" />
                                                <asp:ListItem Text="Kazakhstan" Value="Kazakhstan" />
                                                <asp:ListItem Text="Kenya" Value="Kenya" />
                                                <asp:ListItem Text="Kiribati" Value="Kiribati" />
                                                <asp:ListItem Text="Kuwait" Value="Kuwait" />
                                                <asp:ListItem Text="Kyrgyzstan" Value="Kyrgyzstan" />
                                                <asp:ListItem Text="Laos" Value="Laos" />
                                                <asp:ListItem Text="Latvia" Value="Latvia" />
                                                <asp:ListItem Text="Lebanon" Value="Lebanon" />
                                                <asp:ListItem Text="Lesotho" Value="Lesotho" />
                                                <asp:ListItem Text="Liberia" Value="Liberia" />
                                                <asp:ListItem Text="Libya" Value="Libya" />
                                                <asp:ListItem Text="Liechtenstein" Value="Liechtenstein" />
                                                <asp:ListItem Text="Lithuania" Value="Lithuania" />
                                                <asp:ListItem Text="Luxembourg" Value="Luxembourg" />
                                                <asp:ListItem Text="Macao" Value="Macao" />
                                                <asp:ListItem Text="Macedonia" Value="Macedonia" />
                                                <asp:ListItem Text="Madagascar" Value="Madagascar" />
                                                <asp:ListItem Text="Malawi" Value="Malawi" />
                                                <asp:ListItem Text="Malaysia" Value="Malaysia" />
                                                <asp:ListItem Text="Maldives" Value="Maldives" />
                                                <asp:ListItem Text="Mali" Value="Mali" />
                                                <asp:ListItem Text="Malta" Value="Malta" />
                                                <asp:ListItem Text="Marshall Islands" Value="Marshall Islands" />
                                                <asp:ListItem Text="Martinique" Value="Martinique" />
                                                <asp:ListItem Text="Mauritania" Value="Mauritania" />
                                                <asp:ListItem Text="Mauritius" Value="Mauritius" />
                                                <asp:ListItem Text="Mayotte" Value="Mayotte" />
                                                <asp:ListItem Text="Mexico" Value="Mexico" />
                                                <asp:ListItem Text="Micronesia" Value="Micronesia" />
                                                <asp:ListItem Text="Moldova" Value="Moldova" />
                                                <asp:ListItem Text="Monaco" Value="Monaco" />
                                                <asp:ListItem Text="Mongolia" Value="Mongolia" />
                                                <asp:ListItem Text="Montenegro" Value="Montenegro" />
                                                <asp:ListItem Text="Montserrat" Value="Montserrat" />
                                                <asp:ListItem Text="Morocco" Value="Morocco" />
                                                <asp:ListItem Text="Mozambique" Value="Mozambique" />
                                                <asp:ListItem Text="Myanmar" Value="Myanmar" />
                                                <asp:ListItem Text="Namibia" Value="Namibia" />
                                                <asp:ListItem Text="Nauru" Value="Nauru" />
                                                <asp:ListItem Text="Nepal" Value="Nepal" />
                                                <asp:ListItem Text="Netherlands" Value="Netherlands" />
                                                <asp:ListItem Text="Netherlands Antilles" Value="Netherlands Antilles" />
                                                <asp:ListItem Text="New Caledonia" Value="New Caledonia" />
                                                <asp:ListItem Text="New Zealand" Value="New Zealand" />
                                                <asp:ListItem Text="Nicaragua" Value="Nicaragua" />
                                                <asp:ListItem Text="Niger" Value="Niger" />
                                                <asp:ListItem Text="Nigeria" Value="Nigeria" />
                                                <asp:ListItem Text="Niue" Value="Niue" />
                                                <asp:ListItem Text="Norfolk Island" Value="Norfolk Island" />
                                                <asp:ListItem Text="Northern Mariana Islands" Value="Northern Mariana Islands" />
                                                <asp:ListItem Text="North Korea" Value="North Korea" />
                                                <asp:ListItem Text="Norway" Value="Norway" />
                                                <asp:ListItem Text="Oman" Value="Oman" />
                                                <asp:ListItem Text="Pakistan" Value="Pakistan" />
                                                <asp:ListItem Text="Palau" Value="Palau" />
                                                <asp:ListItem Text="Palestinian Territory" Value="Palestinian Territory" />
                                                <asp:ListItem Text="Panama" Value="Panama" />
                                                <asp:ListItem Text="Papua New Guinea" Value="Papua New Guinea" />
                                                <asp:ListItem Text="Paraguay" Value="Paraguay" />
                                                <asp:ListItem Text="Peru" Value="Peru" />
                                                <asp:ListItem Text="Philippines" Value="Philippines" />
                                                <asp:ListItem Text="Poland" Value="Poland" />
                                                <asp:ListItem Text="PL" Value="PL" />
                                                <asp:ListItem Text="Portugal" Value="Portugal" />
                                                <asp:ListItem Text="Puerto Rico" Value="Puerto Rico" />
                                                <asp:ListItem Text="Qatar" Value="Qatar" />
                                                <asp:ListItem Text="Reunion" Value="Reunion" />
                                                <asp:ListItem Text="Romania" Value="Romania" />
                                                <asp:ListItem Text="Russia" Value="Russia" />
                                                <asp:ListItem Text="Rwanda" Value="Rwanda" />
                                                <asp:ListItem Text="Saint Helena" Value="Saint Helena" />
                                                <asp:ListItem Text="Saint Kitts and Nevis" Value="Saint Kitts and Nevis" />
                                                <asp:ListItem Text="Saint Lucia" Value="Saint Lucia" />
                                                <asp:ListItem Text="Saint Pierre and Miquelon" Value="Saint Pierre and Miquelon" />
                                                <asp:ListItem Text="Saint Vincent and the Grenadines" Value="Saint Vincent and the Grenadines" />
                                                <asp:ListItem Text="Samoa" Value="Samoa" />
                                                <asp:ListItem Text="San Marino" Value="San Marino" />
                                                <asp:ListItem Text="Saudi Arabia" Value="Saudi Arabia" />
                                                <asp:ListItem Text="Senegal" Value="Senegal" />
                                                <asp:ListItem Text="Serbia" Value="Serbia" />
                                                <asp:ListItem Text="Serbia and Montenegro" Value="Serbia and Montenegro" />
                                                <asp:ListItem Text="Seychelles" Value="Seychelles" />
                                                <asp:ListItem Text="Sierra Leone" Value="Sierra Leone" />
                                                <asp:ListItem Text="Singapore" Value="Singapore" />
                                                <asp:ListItem Text="Slovakia" Value="Slovakia" />
                                                <asp:ListItem Text="Slovenia" Value="Slovenia" />
                                                <asp:ListItem Text="Solomon Islands" Value="Solomon Islands" />
                                                <asp:ListItem Text="Somalia" Value="Somalia" />
                                                <asp:ListItem Text="South Africa" Value="South Africa" />
                                                <asp:ListItem Text="South Georgia and the South Sandwich Islands" Value="South Georgia and the South Sandwich Islands" />
                                                <asp:ListItem Text="South Korea" Value="South Korea" />
                                                <asp:ListItem Text="Spain" Value="Spain" />
                                                <asp:ListItem Text="Sri Lanka" Value="Sri Lanka" />
                                                <asp:ListItem Text="Sudan" Value="Sudan" />
                                                <asp:ListItem Text="Suriname" Value="Suriname" />
                                                <asp:ListItem Text="Svalbard and Jan Mayen" Value="Svalbard and Jan Mayen" />
                                                <asp:ListItem Text="Swaziland" Value="Swaziland" />
                                                <asp:ListItem Text="Sweden" Value="Sweden" />
                                                <asp:ListItem Text="Switzerland" Value="Switzerland" />
                                                <asp:ListItem Text="Syria" Value="Syria" />
                                                <asp:ListItem Text="Taiwan" Value="Taiwan" />
                                                <asp:ListItem Text="Tajikistan" Value="Tajikistan" />
                                                <asp:ListItem Text="Tanzania" Value="Tanzania" />
                                                <asp:ListItem Text="Thailand" Value="Thailand" />
                                                <asp:ListItem Text="Togo" Value="Togo" />
                                                <asp:ListItem Text="Tokelau" Value="Tokelau" />
                                                <asp:ListItem Text="Tonga" Value="Tonga" />
                                                <asp:ListItem Text="Trinidad and Tobago" Value="Trinidad and Tobago" />
                                                <asp:ListItem Text="Tunisia" Value="Tunisia" />
                                                <asp:ListItem Text="Turkey" Value="Turkey" />
                                                <asp:ListItem Text="Turkmenistan" Value="Turkmenistan" />
                                                <asp:ListItem Text="Turks and Caicos Islands" Value="Turks and Caicos Islands" />
                                                <asp:ListItem Text="Tuvalu" Value="Tuvalu" />
                                                <asp:ListItem Text="Uganda" Value="Uganda" />
                                                <asp:ListItem Text="Ukraine" Value="Ukraine" />
                                                <asp:ListItem Text="United Arab Emirates" Value="United Arab Emirates" />
                                                <asp:ListItem Text="United Kingdom" Value="United Kingdom" />
                                                <asp:ListItem Text="United States" Value="United States" />
                                                <asp:ListItem Text="United States minor outlying islands" Value="United States minor outlying islands" />
                                                <asp:ListItem Text="Uruguay" Value="Uruguay" />
                                                <asp:ListItem Text="Uzbekistan" Value="Uzbekistan" />
                                                <asp:ListItem Text="Vanuatu" Value="Vanuatu" />
                                                <asp:ListItem Text="Vatican City" Value="Vatican City" />
                                                <asp:ListItem Text="Venezuela" Value="Venezuela" />
                                                <asp:ListItem Text="Vietnam" Value="Vietnam" />
                                                <asp:ListItem Text="Virgin Islands, British" Value="Virgin Islands, British" />
                                                <asp:ListItem Text="Virgin Islands, U.S." Value="Virgin Islands, U.S." />
                                                <asp:ListItem Text="Wallis and Futuna" Value="Wallis and Futuna" />
                                                <asp:ListItem Text="Western Sahara" Value="Western Sahara" />
                                                <asp:ListItem Text="Yemen" Value="Yemen" />
                                                <asp:ListItem Text="Zambia" Value="Zambia" />
                                                <asp:ListItem Text="Zimbabwe" Value="Zimbabwe" />
                                            </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ErrorMessage="Select country" ControlToValidate="txtCbCountry" InitialValue="0"></asp:RequiredFieldValidator>
                             </p>
                             <p class="form-row form-row-wide">
                                <asp:Button ID="SubmitButton" runat="server" Text=" Register " ClientIDMode="Static"
                                                OnClick="SubmitButton_Click" CssClass="button alt" />
                                            <asp:Button ID="CancelButton" runat="server" Text=" Cancel " CausesValidation="false"
                                                OnClick="CancelButton_Click" CssClass="button alt" />
                              </p>
                            <p class="form-row form-row-wide">
                                <asp:Label ID="txtError" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </p>
                            </asp:Panel>
                        </div>
                    </div>
                </article>
            </div>
        </div>
    </section>
    <!--Checkout End-->
   

</asp:Content>
