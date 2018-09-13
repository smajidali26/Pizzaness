<%@ Page Title="" Language="C#" MasterPageFile="~/templates/main.master" AutoEventWireup="true" CodeFile="AboutUs.aspx.cs" Inherits="AboutUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!--Blog Page Section Start-->
    <section class="blog-page-section">
      <div class="container">
        <div class="row-fluid">
          <div class="span12">
            <div class="heading">
              <h1>About Us</h1>
            </div>
          </div>
        </div>
      </div>
      <!--Blog Post End--> 
      
      <!--About Page Start-->
      <div class="about-page">
        <div class="container">
          <div class="row-fluid about-section-1">
            <div class="span5">
              <div id="myCarousel" class="carousel slide">
                <ol class="carousel-indicators">
                  <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                  <li data-target="#myCarousel" data-slide-to="1"></li>
                  <li data-target="#myCarousel" data-slide-to="2"></li>
                  <li data-target="#myCarousel" data-slide-to="3"></li>
                </ol>
                <!-- Carousel items -->
                <div class="carousel-inner">
                  <div class="active item">
                    <div class="frame"><img src="images/about-img.jpg" alt="img"></div>
                  </div>
                  <div class="item">
                    <div class="frame"><img src="images/about-img.jpg" alt="img"></div>
                  </div>
                  <div class="item">
                    <div class="frame"><img src="images/about-img.jpg" alt="img"></div>
                  </div>
                  <div class="item">
                    <div class="frame"><img src="images/about-img.jpg" alt="img"></div>
                  </div>
                </div>
              </div>
            </div>
            <div class="span7">
              <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent vel velit ullamcorper arcu pretium porttitor. Nunc enim nisi, posuere ac ipsum nec, condimentum mollis velit. Suspendisse dapibus dui imperdiet, consequat est sagittis, tincidunt metus. Cras id consectetur ipsum. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Integer non eleifend mi. Phasellus lacinia, orci sit amet eleifend euismod, nibh quam vestibulum tortor, nec dictum tortor purus nec leo. Aenean nec sodales enim. Aliquam tempor aliquet urna, sit amet malesuada nunc tempus id. Fusce sollicitudin elementum odio ut ornare. Vestibulum aliquet, purus eu laoreet venenatis, tortor metus viverra lectus, nec dapibus nisi nisi ac purus. Ut in eros a urna sollicitudin tempus sed ac sem. Integer diam magna, porttitor id lacinia a, cursus eget nisl.</p>
              <p>Nunc a fermentum urna. Fusce et lectus vel nisl volutpat egestas. Nulla at diam arcu. Proin faucibus quam nulla. Fusce sed lacinia lorem, sed posuere nunc. Vestibulum congue rhoncus orci sit amet adipiscing. Nullam dignissim porta massa vel tristique. Nulla eget interdum risus. Maecenas blandit ipsum sit amet diam semper pharetra. Donec volutpat commodo libero at adipiscing. Phasellus quis nisl vulputate, pellentesque quam ac, iaculis enim. Praesent rutrum eros a tellus feugiat, non ultricies mauris aliquam. Maecenas ut elit massa. </p>
            </div>
          </div>
        </div>
      </div>
      <!--About Page End--> 
    </section>
    <!--Blog Page Section End--> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

