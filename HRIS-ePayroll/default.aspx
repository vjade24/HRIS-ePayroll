<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="HRIS_ePayroll.defualt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <form runat="server" enctype="multipart/form-data">
        
        <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
          <ol class="carousel-indicators">
            <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
            <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
            <li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
            <li data-target="#carouselExampleIndicators" data-slide-to="3"></li>
            <li data-target="#carouselExampleIndicators" data-slide-to="4"></li>
            <li data-target="#carouselExampleIndicators" data-slide-to="5"></li>
            <li data-target="#carouselExampleIndicators" data-slide-to="6"></li>
          </ol>
          <div class="carousel-inner">
            <div class="carousel-item active">
                <img class="d-block w-100" src="ResourceImages/Landing%20Page/1.jpg" alt="First slide" style="width:50%;height:10%">
                <%--<div class="carousel-caption d-none d-md-block">
                    <h5>WELCOME TO</h5>
                    <p>Payroll Application</p>
                </div>--%>
            </div>
            
            <div class="carousel-item">
              <img class="d-block w-100" src="ResourceImages/Landing%20Page/2.jpg" alt="Second slide" style="width:50%;height:25%">
                <%--<div class="carousel-caption d-none d-md-block">
                    <h5>Human Resource Information System</h5>
                    <p>...</p>
                </div>--%>
            </div>
            <div class="carousel-item">
              <img class="d-block w-100" src="ResourceImages/Landing%20Page/3.jpg" alt="Third slide" style="width:50%;height:25%">
                <%--<div class="carousel-caption d-none d-md-block">
                    <h5>Human Resource Information System</h5>
                    <p>...</p>
                </div>--%>
            </div>
            <div class="carousel-item">
              <img class="d-block w-100" src="ResourceImages/Landing%20Page/4.jpg" alt="Third slide" style="width:50%;height:25%">
                <div class="carousel-caption d-none d-md-block">
                    <img class="round-circle" src="ResourceImages/HRIS_TEAM/HRIS%20LOGO.png" width="150" height="150"  />
                    <h1 class="mb-0 pb-0"><b>HUMAN RESOURCE</b></h1>
                    <p class="mt-0 pt-0" style="letter-spacing:10px">INFORMATION SYSTEM</p>
                </div>
            </div>
            <div class="carousel-item">
              <img class="d-block w-100" src="ResourceImages/Landing%20Page/5.jpg" alt="Third slide" style="width:50%;height:25%">
                <%--<div class="carousel-caption d-none d-md-block">
                    <h2>OUR TEAM</h2>
                    <p>We are the Human Resource Information System</p>
                </div>--%>
            </div>
            <div class="carousel-item">
              <img class="d-block w-100" src="ResourceImages/Landing%20Page/6.jpg" alt="Third slide" style="width:50%;height:25%">
            </div>
            <div class="carousel-item">
              <img class="d-block w-100" src="ResourceImages/Landing%20Page/7.jpg" alt="Third slide" style="width:50%;height:25%">
            </div>
          </div>
          <a class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="sr-only">Previous</span>
          </a>
          <a class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="sr-only">Next</span>
          </a>
        </div>

        <div class="bg-light">
            <%--<div class="col-xl-12 text-center" style="padding-top:10px">
                <h3 class="font-weight-bold">YOU CAN DO</h3>
            </div>--%>
            <div class="form-group row" style="padding-top:10px">
                <div class="col-xl-4 text-center">
                    <img class="round-circle" src="ResourceImages/Landing%20Page/configuration.png" width="150" height="150" style="border:1px solid #e2e2e2;border-radius:50%;padding:10px" />
                    <h6 class="text-uppercase font-weight-bold">Generate Payroll</h6>
                    <p class="pr-5 pl-5 small">Generate Monthly payroll, RATA, Overtime, etc.</p>
                </div>
                <div class="col-xl-4 text-center">
                    <img class="round-circle" src="ResourceImages/Landing%20Page/statistics.png" width="150" height="150" style="border:1px solid #e2e2e2;border-radius:50%;padding:10px" />
                    <h6 class="text-uppercase font-weight-bold">View/Print Payroll Reports</h6>
                    <p class="pr-5 pl-5 small">You can preview the Reports and Print.</p>
                </div>
                <div class="col-xl-4 text-center">
                    <img class="round-circle" src="ResourceImages/Landing%20Page/upload.png" width="150" height="150" style="border:1px solid #e2e2e2;border-radius:50%;padding:10px" />
                    <h6 class="text-uppercase font-weight-bold">Contributions and Loan Ledger</h6>
                    <p class="pr-5 pl-5 small">You can Add/Update employees contributions and loans. </p>
                </div>
            </div>
        </div>
        

<%--        <div class="row mt-2">
            <div class="col-xl-12">
                <div class="form-group row">
                    <div class="col-xl-4 mt-1">
                      <div class="card-text text-white bg-warning o-hidden h-100" style="border-radius:5px">
                          <div class="card-body">
                              <div class="card-body-icon">
                                  <i class="fa fa-qrcode"></i>
                              </div>
                              <div class="mr-5">Generate Payroll</div>
                          </div>
                          <div class="card-footer text-white clearfix small z-1">
                              <span class="float-left">View Details</span>
                              <span class="float-right">
                                  <i class="fa fa-angle-right"></i>
                              </span>
                          </div>
                      </div>
                    </div>
                    
                    <div class="col-sm-4 mt-1">
                      <div class="card-text text-white bg-success o-hidden h-100" style="border-radius:5px">
                          <div class="card-body">
                              <div class="card-body-icon">
                                  <i class="fa fa-print"></i>
                              </div>
                              <div class="mr-5">Payroll Report</div>
                          </div>
                          <div class="card-footer text-white clearfix small z-1">
                              <span class="float-left">View Details</span>
                              <span class="float-right">
                                  <i class="fa fa-angle-right"></i>
                              </span>
                          </div>
                      </div>
                    </div>
                    <div class="col-sm-4 mt-1">
                      <div class="card-text text-white bg-danger o-hidden h-100" style="border-radius:5px">
                          <div class="card-body">
                              <div class="card-body-icon">
                                  <i class="fa fa-dashboard"></i>
                              </div>
                              <div class="mr-5">Daghag Chats</div>
                          </div>
                          <div class="card-footer text-white clearfix small z-1">
                              <span class="float-left">View Details</span>
                              <span class="float-right">
                                  <i class="fa fa-angle-right"></i>
                              </span>
                          </div>
                      </div>
                    </div>
                </div>
            </div>
            
        </div>

        <div class="row mt-2">
            <img class="d-block w-100" src="ResourceImages/Landing%20Page/6.jpg" alt="Third slide" style="width:50%;height:25%">
        </div>--%>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="specific_scripts" runat="server">

</asp:Content>