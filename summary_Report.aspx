<%@ Page Title="TDS Calculation" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="summary_Report.aspx.cs" Inherits="summary_Report" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cph_header" runat="server">
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
    <script src="js/jquery.blockUI.js"></script>
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" />
    <link href="Scripts/jquery-ui.css" rel="stylesheet" />
    <link href="css/new_stylesheet.css" rel="stylesheet" />

    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <style>
        .grid-view {
            height: auto;
            max-height: 500px;
            overflow: scroll;
        }
    </style>
    <style>
        .auto-style1 {
            color: #FFFFFF;
        }

        h2 {
            border-radius: 5px;
        }

        .grid-view {
            height: auto;
            max-height: 300px;
            overflow: scroll;
        }

        h5 {
            font-weight: bold;
            font-size: 15px;
        }
    </style>

    <script type="text/javascript">

          function pageLoad() {
              $('.date-picker').datepicker({
                  changeMonth: true,
                  changeYear: true,
                  showButtonPanel: true,
                  dateFormat: 'dd/mm/yy',
                  onClose: function (dateText, inst) {
                      var month = $("#ui-datepicker-div .ui-datepicker-date .ui-datepicker-month .ui-datepicker-year :selected").val();

                  }
              });

              $(".date-picker").attr("readonly", "true");
          }

          function AllowAlphabet1(e) {
              if (null != e) {

                  isIE = document.all ? 1 : 0
                  keyEntry = !isIE ? e.which : e.keyCode;
                  if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry < '31'))

                      return true;
                  else {
                      // alert('Please Enter Only Character values.');
                      return false;
                  }
              }
          }


          function isNumber(evt) {
              if (null != evt) {
                  evt = (evt) ? evt : window.event;

                  var charCode = (evt.which) ? evt.which : evt.keyCode;
                  if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                      if (charCode == 46) {
                          return true;
                      }
                      return false;
                  }

              }
              return true;
          }

       
          function openWindow() {

              window.open("html/TDScalculation.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');

          }

          </script>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>



    <div class="container-fluid">
        <%--<asp:UpdatePanel ID ="Updatepanel" runat="server">
                    <ContentTemplate>--%>

        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font: small;" class="text-center text-uppercase"><b>Summary Report</b></div>
                    </div>

                    <div class="col-sm-2 text-right">

                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="panel-body">

                <div class="row">
                    <div class="col-sm-2 col-xs-12 text-left">
                        Client Name :<span class="text-red"> *</span>
                        <asp:DropDownList ID="ddl_unit_client" class="form-control pr_state js-example-basic-single" Width="100%" runat="server" OnSelectedIndexChanged="ddl_clientname_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                        State :<span class="text-red"> *</span>
                        <asp:DropDownList ID="ddl_clientwisestate" runat="server" class="form-control pr_state js-example-basic-single" Width="100%" OnSelectedIndexChanged="get_city_list1" AutoPostBack="true">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                        </asp:DropDownList>

                    </div>
                    <div class="col-sm-2 col-xs-12 text-left">
                        Branch Name : <span class="text-red">*</span>
                        <asp:DropDownList ID="ddl_unitcode" class="form-control pr_state js-example-basic-single" AutoPostBack="true" Width="100%" runat="server" OnSelectedIndexChanged="count_details">
                        </asp:DropDownList>
                    </div>



                    <div class="col-sm-2 col-xs-12 text-left">
                        Total  Employee Count :
                            <asp:TextBox ID="txt_totalemp_count" runat="server" class="form-control text_box" MaxLength="20" meta:resourcekey="txt_eecodeResource1" Text="0" disabled></asp:TextBox>

                    </div>
                    <div class="col-sm-2 col-xs-12 text-left">
                        Attendance Count :
                            <asp:TextBox ID="txt_attendancecount" runat="server" class="form-control text_box" MaxLength="20" meta:resourcekey="txt_eecodeResource1" Text="0" disabled></asp:TextBox>

                    </div>

                </div>
                <br />

                <div class="row text-center">
                    <div class="row">
                        <div class="col-lg-6"></div>
                        <div class="col-sm-2 col-xs-12 text-left">


                            <asp:Button ID="btn_Close" runat="server" CausesValidation="False" CssClass="btn btn-danger" Text="Close" OnClick="btn_Close_Click" />
                        </div>

                    </div>
                </div>
            </div>
        </asp:Panel>
        <%-- </ContentTemplate>      
</asp:UpdatePanel>--%>
    </div>
</asp:Content>







