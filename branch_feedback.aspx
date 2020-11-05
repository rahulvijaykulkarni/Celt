<%@ Page Language="C#" AutoEventWireup="true" CodeFile="branch_feedback.aspx.cs" Inherits="branch_feedback" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Branch Feedback</title>
      <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta charset="utf-8" />
    <script src="js/jquery-1.12.3.min.js"></script>
    <script src="Scripts/jquery-1.11.3.js"></script>
    <script src="js/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/datetimepicker.js"></script>
    <script src="Scripts/jquery-ui-1.8.20.min.js"></script>
    <script src="Scripts/jquery-ui-1.8.20.js"></script>
    <script src="Scripts/jquery-1.7.1.js"></script>
    <script src="Scripts/jquery-ui.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" />
    <link href="Scripts/jquery-ui.css" rel="stylesheet" />
    <link href="css/new_stylesheet.css" rel="stylesheet" />
    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <link href="css/select2.min.css" rel="stylesheet" />
    <script src="js/select2.min.js"></script>

    <script type="text/javascript">
        function pageLoad() {
            employee_groom_percent();
        }
        function employee_groom_percent() {
            var ddl_employee_groom = document.getElementById('<%=ddl_employee_groom.ClientID %>');
            var selected_ddl_employee_groom = ddl_employee_groom.options[ddl_employee_groom.selectedIndex].text;
            var text1 = ddl_employee_groom.options[ddl_employee_groom.selectedIndex].value;

            var ddl_employee_hygiene = document.getElementById('<%=ddl_employee_hygiene.ClientID %>');
            var selected_ddl_employee_hygiene = ddl_employee_hygiene.options[ddl_employee_hygiene.selectedIndex].text;
            var text2 = ddl_employee_hygiene.options[ddl_employee_hygiene.selectedIndex].value;

            var ddl_employee_duty = document.getElementById('<%=ddl_employee_duty.ClientID %>');
            var selected_ddl_employee_duty = ddl_employee_duty.options[ddl_employee_duty.selectedIndex].text;
            var text3 = ddl_employee_duty.options[ddl_employee_duty.selectedIndex].value;

            var ddl_employee_behaviour = document.getElementById('<%=ddl_employee_behaviour.ClientID %>');
            var selected_ddl_employee_behaviour = ddl_employee_behaviour.options[ddl_employee_behaviour.selectedIndex].text;
            var text4 = ddl_employee_behaviour.options[ddl_employee_behaviour.selectedIndex].value;

            var ddl_employee_support = document.getElementById('<%=ddl_employee_support.ClientID %>');
            var selected_ddl_employee_support = ddl_employee_support.options[ddl_employee_support.selectedIndex].text;
            var text5 = ddl_employee_support.options[ddl_employee_support.selectedIndex].value;

            var lbl_percentage = document.getElementById('<%=lbl_percentage.ClientID %>');

            lbl_percentage.innerHTML = ((parseInt(text1) + parseInt(text2) + parseInt(text3) + parseInt(text4) + parseInt(text5)) / 5) + "%";
        }
    </script>
  
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
        <asp:Panel ID="Panel3"  runat="server"  Style="border:1px solid gray;">
            <div class="panel-heading" style="font-family:Arial;color:black;">
               
                <div class="row text-center">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-4" style="border:1px solid gray">
                        <div style="color: #fff; font-size: large;color:black;font-weight:bold;" class="text-center text-uppercase"> <b><%=month_year %> </b></div>
                    </div>
                </div>
                    </div>
           
            <br />
            <br />
            <asp:Panel id="hide_panel" runat="server">
            <div class="container-fluid" style="border:1px solid gray;width:99%">
                <br />
                 <div class="row">
                    <div class="col-sm-2 col-xs-12">
                        <span style="font-family:Arial;font-weight:bold;font-size:14px;">CLIENT NAME : </span>
                    </div> 
                      <div class="col-sm-4 col-xs-12" style="font-family:Arial;margin-left:-7em;font-weight:bold;font-size:14px;"><table ><tr><th style="text-align:center"><%=comp_name %></th></tr></table></div>
                       
                      <div class="col-sm-2 col-xs-12">
                          <span style="font-family:Arial;font-weight:bold;font-size:14px;">BRANCH : </span>
                      </div> 
                      <div class="col-sm-4 col-xs-12" style="font-family:Arial;font-weight:bold;font-size:14px;margin-left:-7em"><table><tr><th style="text-align:center"><%=branch %></th></tr></table></div> 
                </div>
                <br />
            </div>
                <br /><br />
            <div class="container-fluid" style="font-family:Verdana;font-weight:bold; border-radius: 10px; border: 1px solid white;width:99%;font-size:12px;">
                <br />
                        <div class="row" style="text-align:right;">
                 <div class="col-sm-2 col-xs-12"></div>
                    <div class="col-sm-4 col-xs-12">
                ARE THE EMPLOYEES GROOMED PROPERLY
                        </div>
                <div class="col-sm-2 col-xs-12">
                                <asp:DropDownList ID="ddl_employee_groom" runat="server"  class="form-control" onchange="employee_groom_percent();">
                                    <asp:ListItem Value="100">Excellent</asp:ListItem>
                                      <asp:ListItem Value="95">Very Good</asp:ListItem>
                                      <asp:ListItem Value="90">Good</asp:ListItem>
                                      <asp:ListItem Value="80">Satisfactory</asp:ListItem>
                                      <asp:ListItem Value="70">Poor</asp:ListItem>
                                </asp:DropDownList>
                </div>
                </div>
                <br />
            <div class="row" style="text-align:right;">
                  <div class="col-sm-2 col-xs-12"></div>
                     <div class="col-sm-4 col-xs-12">
                         CLEANING AND HYGIENE STANDARDS MAINTAINED AT SITE
                     </div>
                  <div class="col-sm-2 col-xs-12">
                        <asp:DropDownList ID="ddl_employee_hygiene" runat="server"  class="form-control" onchange="employee_groom_percent();">
                            <asp:ListItem Value="100">Excellent</asp:ListItem>
                                      <asp:ListItem Value="95">Very Good</asp:ListItem>
                                      <asp:ListItem Value="90">Good</asp:ListItem>
                                      <asp:ListItem Value="80">Satisfactory</asp:ListItem>
                                      <asp:ListItem Value="70">Poor</asp:ListItem>
                                </asp:DropDownList>
                  </div>
                    </div>
                <br />
            <div class="row" style="text-align:right;">
                  <div class="col-sm-2 col-xs-12"></div>
                     <div class="col-sm-4 col-xs-12">
                         DO THE EMPLOYEES KNOW THEIR DUTIES
                     </div>
                  <div class="col-sm-2 col-xs-12">
                        <asp:DropDownList ID="ddl_employee_duty" runat="server"  class="form-control" onchange="employee_groom_percent();">
                             <asp:ListItem Value="100">Excellent</asp:ListItem>
                                      <asp:ListItem Value="95">Very Good</asp:ListItem>
                                      <asp:ListItem Value="90">Good</asp:ListItem>
                                      <asp:ListItem Value="80">Satisfactory</asp:ListItem>
                                      <asp:ListItem Value="70">Poor</asp:ListItem>
                                </asp:DropDownList>
                  </div>
                    </div>
                <br />
            <div class="row" style="text-align:right;">
                  <div class="col-sm-2 col-xs-12"></div>
                     <div class="col-sm-4 col-xs-12">
                         BEHAVIOR AND ATTITUDE OF EMPLOYEE
                     </div>
                  <div class="col-sm-2 col-xs-12">
                        <asp:DropDownList ID="ddl_employee_behaviour" runat="server"  class="form-control" onchange="employee_groom_percent();">
                             <asp:ListItem Value="100">Excellent</asp:ListItem>
                                      <asp:ListItem Value="95">Very Good</asp:ListItem>
                                      <asp:ListItem Value="90">Good</asp:ListItem>
                                      <asp:ListItem Value="80">Satisfactory</asp:ListItem>
                                      <asp:ListItem Value="70">Poor</asp:ListItem>
                                </asp:DropDownList>
                  </div>
                    </div>
                <br />
            <div class="row" style="text-align:right;">
                  <div class="col-sm-2 col-xs-12"></div>
                       <div class="col-sm-4 col-xs-12">
                           SUPPORT FROM TEAM (FO/ADMIN)
                       </div>
                  <div class="col-sm-2 col-xs-12">
                        <asp:DropDownList ID="ddl_employee_support" runat="server"  class="form-control"  onchange="employee_groom_percent();">
                             <asp:ListItem Value="100">Excellent</asp:ListItem>
                                      <asp:ListItem Value="95">Very Good</asp:ListItem>
                                      <asp:ListItem Value="90">Good</asp:ListItem>
                                      <asp:ListItem Value="80">Satisfactory</asp:ListItem>
                                      <asp:ListItem Value="70">Poor</asp:ListItem>
                                </asp:DropDownList>
                  </div> 
                    </div>
                <br />
            <div class="row">
                  <div class="col-sm-2 col-xs-12"></div>
                      <div class="col-sm-4 col-xs-12" style="text-align:right;">
                          <span style="font-family:Verdana;font-weight:bold;font-size:14px;">Percentage : </span>
                      </div>
                 <div class="col-sm-2 col-xs-12">
                     <table><tr><th>  <asp:Label runat="server" ID="lbl_percentage" style="font-family:Arial;font-weight:bold;font-size:12px;">100%</asp:Label></th></tr></table>
                    
                  </div> 
            </div>
               <br />    
               </div>
              
            <br />
            <div class="container">
                <div class="row text-center">
                    <asp:Button ID="btn_submit" runat="server" CssClass="btn btn-primary" OnClick="btn_submit_Click" Text="Submit" /> 
                     <asp:Button ID="btn_close" runat="server" CssClass="btn btn-danger" Text="Close" /> 
                </div> 
            </div>
            <br />
                </asp:Panel>
            </asp:Panel>
        </div>
        </form>
        </body>
    </html>
    


     
