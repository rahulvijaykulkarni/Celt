<%@ page title="Project Costing" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" enableeventvalidation="false" inherits="costing_master, App_Web_mstmsd5v" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <br />
    <script src="Scripts/jquery-1.11.3.js"></script>
    <script src="Scripts/bootstrap.js"></script>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/jquery.blockUI.js"></script>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <meta charset="utf-8">
      <link rel="shortcut icon" href="images/avi.ico" type="image/x-icon" />
    <!DOCTYPE html>
    <html>
    <head>
        <title></title>
        <style type="text/css">
            body
            {
                font-family: Arial;
                font-size: 10pt;
                text-align: left;
            }

            td
            {
                cursor: pointer;
            }

            .hover_row
            {
                background-color: #FFFFBF;
            }
        </style>
        <style>
            .container
            {
                max-width: 99%;
            }

            .borderless td, .borderless th
            {
                border: none;
            }
            table th {
                 font-family: Arial;
                font-size: 8pt;
                text-align: left;
            }
           
            body {
                font-family: Arial;
                font-size: 8pt;
                text-align: left;
            }

            .form-control {
                  font-family: Arial;
                font-size: 8pt;
                text-align: left;
            }

        </style>

        <script>
          function hidedockl() {
                $(".td_hide_1").hide();
            }

            function showdockl() {
               $(".td_hide_1").show();
            }
        </script>

        <script type="text/javascript">
            $('body').on('keyup', '.maskedExt', function () {
                var num = $(this).attr("maskedFormat").toString().split(',');
                var regex = new RegExp("^\\d{0," + num[0] + "}(\\.\\d{0," + num[1] + "})?$");
                if (!regex.test(this.value)) {
                    this.value = this.value.substring(0, this.value.length - 1);
                }
            });
            function isNumberQuantity(evt) {
                if (null != evt) {
                    evt = (evt) ? evt : window.event;

                    var charCode = (evt.which) ? evt.which : evt.keyCode;
                    if (charCode > 31 && (charCode < 48 || charCode > 57)) {

                        return false;
                    }

                }
                return true;
            }


            function validateQty(el, evt) {
                if (null != evt) {


                    var charCode = (evt.which) ? evt.which : event.keyCode
                    if (charCode != 45 && charCode != 8 && (charCode != 46) && (charCode < 48 || charCode > 57))
                        return false;
                    if (charCode == 46) {
                        if ((el.value) && (el.value.indexOf('.') >= 0))
                            return false;
                        else
                            return true;
                    }

                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    var number = evt.value.split('.');
                    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                        return false;
                    }
                }
                return true;
            };
            
            function validate(evt) 
            {
                var txt_pf_per = document.getElementById('<%=pf_percent.ClientID %>');
                var txt_pf_disc = document.getElementById('<%=pf_dis.ClientID %>');
                var txt_insuranceper = document.getElementById('<%=insurance_percent.ClientID %>');
                var txt_insurance_dis = document.getElementById('<%=insurance_dis.ClientID %>');

                var txt_parti1_dis = document.getElementById('<%=txt_particular1_dis.ClientID %>');
                var txt_parti2_dis = document.getElementById('<%=txt_particular2_dis.ClientID %>');

                var txt_parti3_dis = document.getElementById('<%=txt_particular3_dis.ClientID %>');
                var txt_cstper = document.getElementById('<%=txt_cst_percent.ClientID %>'); 
                var txt_servicesper = document.getElementById('<%=txt_service_percent.ClientID %>');
              

                if (parseFloat(txt_pf_per.value) >1)  
                {
                    alert("Please Enter value Less Than 1.");
                    txt_pf_per.value = "0";
                    txt_pf_per.focus;
                }

                if( parseFloat(txt_pf_disc.value) >1)
                {   
                    alert("Please Enter value Less Than 1.");
                    txt_pf_disc.value = "0";
                    txt_pf_disc.focus;
                }
                if((txt_insuranceper.value)>1)
                {
                    alert("Please Enter value Less Than 1.");

                    txt_insuranceper.value = "0";
                    txt_insuranceper.focus;
                }
                if((txt_insurance_dis.value)>1)
                {
                    alert("Please Enter value Less Than 1.");
                    txt_insurance_dis.value = "0";
                    txt_insurance_dis.focus;
                }
                if(parseFloat(txt_parti1_dis.value)>1)
                {
                    alert("Please Enter value Less Than 1.");
                    txt_parti1_dis.value = "0";
                    txt_parti1_dis.focus;
                }
                if(parseFloat(txt_parti2_dis.value)>1)
                {
                    alert("Please Enter value Less Than 1.");
                    txt_parti2_dis.value = "0";
                    txt_parti2_dis.focus;
                }
                if(parseFloat(txt_parti3_dis.value)>1)
                {
                    alert("Please Enter value Less Than 1.");
                    txt_parti3_dis.value = "0";
                    txt_parti3_dis.focus;
                }
               
                if(parseFloat(txt_cstper.value)>1)
                {
                    alert("Please Enter value Less Than 1.");
                    txt_cstper.value = "0";
                    txt_cstper.focus;
                }
                if(parseFloat(txt_servicesper.value)>1)
                {
                    alert("Please Enter value Less Than 1.");
                    txt_servicesper.value = "0";
                    txt_servicesper.focus;
                }
              
                     
                       return false;
                   }

              

            function openWindow() {
                window.open("html/project_costing.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
            }

            </script>

    </head>
    <body>
        <asp:Panel ID="Panel10" runat="server">
            <div class="col-sm-12 text-right">
                
              
            </div>
            <div class="panel panel-primary" style="margin-top: 10px">
                <div class="panel-heading">
                      <div class="row">                               
                                    <div class="col-sm-9">
                                    <div  style=" color: #fff; font-size:15px;">
                                            <b>Project Costing </b></div>
                                    </div>
                                    <div class="col-sm-3 text-right">
                                             <asp:LinkButton ID="panImgLnkBtn" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                                                 <asp:Image runat="server" ID="panImg" Width="20" Height="20" ImageUrl="Images/help_ico.png" /></asp:LinkButton>
                                    </div>
                      </div>
                </div>

             </div>

        </asp:Panel>


        <asp:Panel ID="Panel3" runat="server">
            <div class="panel panel-primary" style="margin-top: 10px">
                <div class="panel-heading">
                    <table>
                        <tr>
                            <td>
                                <h4 style="color: white; font-weight: bold">Select Offer </h4>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddl_proposal" OnSelectedIndexChanged="ddl_proposal_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="1">Select Offer</asp:ListItem>
                                    <asp:ListItem Value="2">First Offer</asp:ListItem>
                                    <asp:ListItem Value="3">Discounted Offer</asp:ListItem>
                                    <%--<asp:ListItem Value="4">Final Offer</asp:ListItem>--%>
                                </asp:DropDownList>
                            </td>

                           
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
        <div class="panel panel-primary" style="background-color: white">
            <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Width="100%">
                <div class="container-fluid">
                    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
                    <asp:Table ID="Table3" CssClass="table table-responsive" runat="server">
                        <asp:TableRow>
                            <asp:TableCell BorderWidth="1">
                                <asp:Table ID="Table1" runat="server" BorderWidth="1">
                                    <asp:TableHeaderRow HorizontalAlign="Center">
                                        <asp:TableHeaderCell ColumnSpan="3" HorizontalAlign="Center" VerticalAlign="Middle"><strong>Utility Cost</strong></asp:TableHeaderCell>
                                        <asp:TableHeaderCell>First Proposal Amount : </asp:TableHeaderCell>
                                        <asp:TableHeaderCell>
                                            <strong>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txt_total_amount" Enabled="false"></asp:TextBox></strong>
                                        </asp:TableHeaderCell>
                                        <asp:TableHeaderCell>First Proposal Installation Amount :</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>
                                            <strong>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txt_total_installation" Enabled="false"></asp:TextBox>
                                            </strong>
                                        </asp:TableHeaderCell>
                                    </asp:TableHeaderRow>
                                    <asp:TableHeaderRow>
                                        <asp:TableHeaderCell HorizontalAlign="Center" BorderWidth="1">Particular</asp:TableHeaderCell>
                                        <asp:TableHeaderCell HorizontalAlign="Center" BorderWidth="1">Reason</asp:TableHeaderCell>
                                        <asp:TableHeaderCell HorizontalAlign="Center" BorderWidth="1">In %</asp:TableHeaderCell>
                                        <asp:TableHeaderCell HorizontalAlign="Center" BorderWidth="1">Basic Rates</asp:TableHeaderCell>
                                        <asp:TableHeaderCell HorizontalAlign="Center" BorderWidth="1">Dis. In %</asp:TableHeaderCell>
                                        <asp:TableHeaderCell HorizontalAlign="Center" BorderWidth="1">Final In %</asp:TableHeaderCell>
                                        <asp:TableHeaderCell HorizontalAlign="Center"></asp:TableHeaderCell>
                                        <asp:TableHeaderCell HorizontalAlign="Center"></asp:TableHeaderCell>
                                    </asp:TableHeaderRow>
                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">P&F</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="pf_reason" placeholder="Reason"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control maskedExt" maskedFormat="10,4" ID="pf_percent" placeholder="Percent %" onkeypress='return validateQty(this,event);' onchange="validate(event)"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="pf_basic" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control maskedExt" maskedFormat="10,4" ID="pf_dis" placeholder="Discount %" onkeypress='return validateQty(this,event);' onchange="validate(event)"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="pf_final" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="pf_amount" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">Insurance</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="insurance_reason" placeholder="Reason"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control maskedExt" maskedFormat="10,4" ID="insurance_percent" placeholder="Percent %" onkeypress='return validateQty(this,event);' onchange="validate(event)"></asp:TextBox>
                                        </asp:TableCell>

                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="insurance_basic" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control maskedExt" maskedFormat="10,4" ID="insurance_dis" placeholder="Discount %" onkeypress='return validateQty(this,event);' onchange="validate(event)"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="insurance_final" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="insurance_amount" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">Transportation</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="transportation_reason" placeholder="Reason"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">-</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="transportation_basic" placeholder="Basic Cost" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </asp:TableCell>

                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control maskedExt" maskedFormat="10,2" ID="transportation_dis" placeholder="Discount %" onkeypress='return validateQty(this,event);'></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="transportation_final" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="transportation_amount" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">Unloading Charges</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="unloading_reason" placeholder="Reason"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1"></asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="unloading_basic" placeholder="Basic Cost" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </asp:TableCell>

                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="unloading_dis" placeholder="Discount %" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="unloading_final" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="unloading_amount" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">Scaffolding charges</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="scaffolding_reason" placeholder="Reason"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1"></asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="scaffolding_basic" placeholder="Basic Cost" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </asp:TableCell>

                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="scaffolding_dis" placeholder="Discount %" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="scaffolding_final" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="scaffolding_amount" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">Hydra Charges</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="hydra_reason" placeholder="Reason"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">-</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="hydra_basic" placeholder="Basic Cost" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </asp:TableCell>

                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="hudra_dis" placeholder="Discount %" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="hydra_final" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="hydra_amount" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">Scizzer Lifter Charges</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="scizzer_reason" placeholder="Reason"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1"></asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="scizzer_basic" placeholder="Basic Cost" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </asp:TableCell>

                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="scizzer_dis" placeholder="Discount %" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="scizzer_final" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="scizzer_amount" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">Secured & Covered Storage</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="secured_reason" placeholder="Reason"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1"></asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="secured_basic" placeholder="Basic Cost" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </asp:TableCell>

                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="secured_dis" placeholder="Discount %" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="secured_final" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="secured_amount" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">Power Supply</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="power_reason" placeholder="Reason"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1"></asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="power_basic" placeholder="Basic Cost" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="power_dis" placeholder="Discount %" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="power_final" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="power_amount" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_particular1" placeholder="New Particular1"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_particular1_reason" placeholder="Reason"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">-</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_particular1_basic" placeholder="Basic Cost" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control maskedExt" maskedFormat="10,4" ID="txt_particular1_dis" placeholder="Discount %" onchange="validate(event)" onkeypress='return validateQty(this,event);'></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_particular1_final" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_particular1_amount" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_particular2" placeholder="New Particular2"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_particular2_reason" placeholder="Reason"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">-</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_particular2_basic" placeholder="Basic Cost" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control maskedExt" maskedFormat="10,4" ID="txt_particular2_dis" placeholder="Discount %" onchange="validate(event)" onkeypress='return validateQty(this,event);'></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_particular2_final" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_particular2_amount" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_particular3" placeholder="New Particular3"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_particular3_reason" placeholder="Reason"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1"></asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_particular3_basic" placeholder="Basic Cost" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control maskedExt" maskedFormat="10,2" ID="txt_particular3_dis" onchange="validate(event)" placeholder="Discount %" onkeypress='return validateQty(this,event);'></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_particular3_final" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_particular3_amount" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">Taxes on Supply</asp:TableCell>
                                        <asp:TableCell ColumnSpan="2">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_tax_supply" Enabled="false" />
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control maskedExt" maskedFormat="10,4" ID="txt_cst_percent" onkeypress='return validateQty(this,event);' onchange="validate(event)"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">-</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">-</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="cst_amount" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">Taxes on Installation</asp:TableCell>
                                        <asp:TableCell ColumnSpan="2">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_tax_installation" Enabled="false" />
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control maskedExt" maskedFormat="10,4" ID="txt_service_percent" onchange="validate(event)" onkeypress='return validateQty(this,event);'></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">-</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">-</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="service_amount" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>

                                    </asp:TableRow>
                                    
                                    <asp:TableRow CssClass="td_hide_1">
                                        <asp:TableCell BorderWidth="1">Taxes on Boom Barrier</asp:TableCell>
                                        <asp:TableCell ColumnSpan="2">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_dock_tax" Enabled="false" />
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control maskedExt" maskedFormat="10,4" ID="txt_dock_act_tax" onchange="validate(event)" onkeypress='return validateQty(this,event);'></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">-</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">-</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_dock_amount" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>

                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1"><b>Grand Total</b></asp:TableCell>
                                        <asp:TableCell BorderWidth="1"></asp:TableCell>
                                        <asp:TableCell BorderWidth="1">-</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">-</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">-</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">-</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">

                                            <asp:TextBox runat="server" CssClass="form-control" ID="grand_total" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1"><b>Accessories : </b></asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:DropDownList runat="server" ID="ddl_club_unclub">
                                                <asp:ListItem Value="0">Club</asp:ListItem>
                                                <asp:ListItem Value="1">UnClub</asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">Utility :</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:DropDownList runat="server" ID="ddl_utility_club_unclub" >
                                                 <asp:ListItem Value="1">UnClub</asp:ListItem>
                                                <asp:ListItem Value="0">Club</asp:ListItem>
                                               
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">Installation :</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:DropDownList runat="server" ID="ddl_installation_club_unclub">
                                                 <asp:ListItem Value="1">UnClub</asp:ListItem>
                                                <asp:ListItem Value="0">Club</asp:ListItem>
                                               
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:DropDownList runat="server" ID="ddl_price_inr_doller">
                                                <asp:ListItem Value="0">Indian</asp:ListItem>
                                                <asp:ListItem Value="1">Dollar</asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                    </asp:TableRow>

                                </asp:Table>
                            </asp:TableCell>
                            <asp:TableCell BorderWidth="1">
                                <asp:Table ID="Table2" runat="server">
                                    <asp:TableHeaderRow>
                                        <asp:TableCell ColumnSpan="4" HorizontalAlign="Center"> <b>Total Project Sheet</b></asp:TableCell>
                                    </asp:TableHeaderRow>
                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">P&F Charges</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:DropDownList runat="server" ID="ddl_total_pf">
                                                <asp:ListItem Value="0">0</asp:ListItem>
                                                <asp:ListItem Value="0.01">0.01</asp:ListItem>
                                                <asp:ListItem Value="0.02" Selected="True">0.02</asp:ListItem>
                                                <asp:ListItem Value="0.03">0.03</asp:ListItem>
                                                <asp:ListItem Value="0.04">0.04</asp:ListItem>
                                                <asp:ListItem Value="0.05">0.05</asp:ListItem>
                                                <asp:ListItem Value="0.06">0.06</asp:ListItem>
                                                <asp:ListItem Value="0.07">0.07</asp:ListItem>
                                                <asp:ListItem Value="0.08">0.08</asp:ListItem>
                                                <asp:ListItem Value="0.09">0.09</asp:ListItem>
                                                <asp:ListItem Value="0.1">0.10</asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">Total Utility Cost</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="ddl_total_finance" Width="80"></asp:TextBox>

                                            <%--<asp:DropDownList runat="server" ID="ddl_total_finance">
                                                <asp:ListItem Value="0">0</asp:ListItem>
                                                <asp:ListItem Value="0.01">0.01</asp:ListItem>
                                                <asp:ListItem Value="0.02">0.02</asp:ListItem>
                                                <asp:ListItem Value="0.03">0.03</asp:ListItem>
                                                <asp:ListItem Value="0.04">0.04</asp:ListItem>
                                                <asp:ListItem Value="0.05">0.05</asp:ListItem>
                                                <asp:ListItem Value="0.06">0.06</asp:ListItem>
                                                <asp:ListItem Value="0.07">0.07</asp:ListItem>
                                                <asp:ListItem Value="0.08">0.08</asp:ListItem>
                                                <asp:ListItem Value="0.09">0.09</asp:ListItem>
                                                <asp:ListItem Value="0.1">0.10</asp:ListItem>
                                            </asp:DropDownList>--%>
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">Distance Charges</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:DropDownList runat="server" ID="ddl_total_distance">
                                                <asp:ListItem Value="0">0</asp:ListItem>
                                                <asp:ListItem Value="0.01">0.01</asp:ListItem>
                                                <asp:ListItem Value="0.02">0.02</asp:ListItem>
                                                <asp:ListItem Value="0.03">0.03</asp:ListItem>
                                                <asp:ListItem Value="0.04">0.04</asp:ListItem>
                                                <asp:ListItem Value="0.05">0.05</asp:ListItem>
                                                <asp:ListItem Value="0.06">0.06</asp:ListItem>
                                                <asp:ListItem Value="0.07">0.07</asp:ListItem>
                                                <asp:ListItem Value="0.08">0.08</asp:ListItem>
                                                <asp:ListItem Value="0.09">0.09</asp:ListItem>
                                                <asp:ListItem Value="0.1">0.10</asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">Commercial Cost</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:DropDownList runat="server" ID="ddl_total_commercial" Enabled="false">
                                                <asp:ListItem Value="0">0</asp:ListItem>
                                                <%--<asp:ListItem Value="0.01">0.01</asp:ListItem>
                                                <asp:ListItem Value="0.02">0.02</asp:ListItem>
                                                <asp:ListItem Value="0.03">0.03</asp:ListItem>
                                                <asp:ListItem Value="0.04">0.04</asp:ListItem>
                                                <asp:ListItem Value="0.05">0.05</asp:ListItem>
                                                <asp:ListItem Value="0.06">0.06</asp:ListItem>
                                                <asp:ListItem Value="0.07">0.07</asp:ListItem>
                                                <asp:ListItem Value="0.08">0.08</asp:ListItem>
                                                <asp:ListItem Value="0.09">0.09</asp:ListItem>
                                                <asp:ListItem Value="0.1">0.10</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">Operation Cost</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:DropDownList runat="server" ID="ddl_total_operation">
                                                <asp:ListItem Value="0">0</asp:ListItem>
                                                <asp:ListItem Value="0.05">0.05</asp:ListItem>
                                                <asp:ListItem Value="0.1">0.1</asp:ListItem>
                                                <asp:ListItem Value="0.12">0.12</asp:ListItem>
                                                <asp:ListItem Value="0.15">0.15</asp:ListItem>
                                                <asp:ListItem Value="0.18">0.18</asp:ListItem>
                                                <asp:ListItem Value="0.2">0.2</asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">Extended Warranty Cost</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:DropDownList runat="server" ID="ddl_total_extended">
                                                <asp:ListItem Value="0">0</asp:ListItem>
                                                <asp:ListItem Value="0.05">0.05</asp:ListItem>
                                                <asp:ListItem Value="0.1">0.1</asp:ListItem>
                                                <asp:ListItem Value="0.15">0.15</asp:ListItem>
                                                <asp:ListItem Value="0.02">0.2</asp:ListItem>

                                            </asp:DropDownList>
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">Marketing Cost</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:DropDownList runat="server" ID="ddl_total_marketing">
                                                <asp:ListItem Value="0">0</asp:ListItem>
                                                <asp:ListItem Value="0.01">0.01</asp:ListItem>
                                                <asp:ListItem Value="0.02">0.02</asp:ListItem>
                                                <asp:ListItem Value="0.03">0.03</asp:ListItem>
                                                <asp:ListItem Value="0.04">0.04</asp:ListItem>
                                                <asp:ListItem Value="0.05">0.05</asp:ListItem>
                                                <asp:ListItem Value="0.06">0.06</asp:ListItem>
                                                <asp:ListItem Value="0.07">0.07</asp:ListItem>
                                                <asp:ListItem Value="0.08">0.08</asp:ListItem>
                                                <asp:ListItem Value="0.09">0.09</asp:ListItem>
                                                <asp:ListItem Value="0.1">0.10</asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">Mark Up</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="ddl_total_markup" Width="80"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">Project Co- ordination Cost</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:DropDownList runat="server" ID="ddl_total_project">
                                                <asp:ListItem Value="0">0</asp:ListItem>
                                                <asp:ListItem Value="0.01">0.01</asp:ListItem>
                                                <asp:ListItem Value="0.02">0.02</asp:ListItem>
                                                <asp:ListItem Value="0.03">0.03</asp:ListItem>
                                                <asp:ListItem Value="0.04">0.04</asp:ListItem>
                                                <asp:ListItem Value="0.05">0.05</asp:ListItem>
                                                <asp:ListItem Value="0.06">0.06</asp:ListItem>
                                                <asp:ListItem Value="0.07">0.07</asp:ListItem>
                                                <asp:ListItem Value="0.08">0.08</asp:ListItem>
                                                <asp:ListItem Value="0.09">0.09</asp:ListItem>
                                                <asp:ListItem Value="0.1">0.10</asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>

                                <asp:Table ID="discount_1" runat="server">
                                    <asp:TableHeaderRow>
                                        <asp:TableCell ColumnSpan="4" HorizontalAlign="Center"> <b>Discounted for sales person</b></asp:TableCell>
                                    </asp:TableHeaderRow>
                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">a</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">On only (Product)</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_discount_product" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_discount_product1" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">b</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">On only (Automation)</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_discount_automation" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_discount_automation1" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">c</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">On only (Access)</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_discount_access" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_discount_access1" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">d</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">On Installation</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_discount_installation" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_discount_installation1" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>

                                <asp:Table ID="discount_2" runat="server">
                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">First Offer</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">Actual Dis.</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">Dis. Offer</asp:TableCell>
                                        <asp:TableCell BorderWidth="1">% of dis</asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableHeaderRow>
                                        <asp:TableCell ColumnSpan="4" HorizontalAlign="Center"> <b>Total Discount on Product, Automation Accessories & Installation </b></asp:TableCell>
                                    </asp:TableHeaderRow>

                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_discount_product_first" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_discount_product_actual" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_discount_product_dis" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_discount_product_percent" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableHeaderRow>
                                        <asp:TableCell ColumnSpan="4" HorizontalAlign="Center"> <b>Total Discount on Utility</b></asp:TableCell>
                                    </asp:TableHeaderRow>
                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_discount_utility_first" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_discount_utility_actual" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_discount_utility_dis" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_discount_utility_percent" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableHeaderRow>
                                        <asp:TableCell ColumnSpan="4" HorizontalAlign="Center"> <b>Total Discount on Project</b></asp:TableCell>
                                    </asp:TableHeaderRow>
                                    <asp:TableRow>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_discount_project_first" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_discount_project_actual" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_discount_project_dis" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell BorderWidth="1">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_discount_project_percent" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <div class="container-fluid">
                        <asp:Button ID="btn_project_costing" runat="server" CssClass="btn btn-primary" Text="Calculate" OnClientClick="return send_data();" OnClick="btn_project_costing_Click" BackColor="#428BCA" />
                        <asp:Button ID="btn_first_proposal" runat="server" CssClass="btn btn-primary" Text="First Proposal" OnClientClick="return send_data();" OnClick="btn_first_proposal_Click" BackColor="#428BCA" />
                        <asp:Button ID="btn_discounted_proposal" runat="server" CssClass="btn btn-primary" Text="Discounted Proposal" OnClientClick="return send_data();" OnClick="btn_discounted_proposal_Click" BackColor="#428BCA" />
                        <asp:Button ID="btn_final_proposal" runat="server" CssClass="btn btn-primary" Text="Final Proposal" OnClientClick="return send_data();" OnClick="btn_final_proposal_Click" BackColor="#428BCA" />
                        <asp:Button ID="btn_close" runat="server" CssClass="btn btn-primary" Text="Close" OnClick="btn_close_Click" BackColor="#428BCA" />
                   
                        <asp:Button ID="btn_email" runat="server" CssClass="btn btn-primary" Visible="false" Text="Send Email" BackColor="#428BCA" /> </div>
                   
                </div>
            </asp:Panel>
        </div>

           <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btn_email"
                CancelControlID="Button2" BackgroundCssClass="Background">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none">
                <iframe style="width: 600px; height: 350px; background-color: #fff;" id="irm1" src="p_uploadfile.aspx" runat="server"></iframe>
                <div class="row text-center" style="width: 100%;">
                    <asp:Button ID="Button2" CssClass="btn btn-danger" runat="server" Text="Close" />
                </div>

                <br />

            </asp:Panel>
        <div class="row">
           <div class="col-sm-4">
                <div style="background-color: white">
                <asp:Panel ID="Panel9" runat="server" CssClass="table table-condensed" ScrollBars="Auto" Width="100%">
                    <asp:Label ID="lbl_notes" runat="server" Text="Notes :"></asp:Label>
                    <asp:TextBox ID="txt_notes" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </asp:Panel>
            </div>
            </div>

            <div class="col-sm-4">
                <div style="background-color: white">
                <asp:Panel ID="Panel7" runat="server" CssClass="table table-condensed" ScrollBars="Auto" Width="100%">
                    <asp:Label ID="lbl_payment" runat="server" Text="Payment Terms :"></asp:Label>
                    <asp:TextBox ID="txt_payment_terms" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </asp:Panel>
            </div>
            </div>
            <div class="col-sm-4">
                <div style="background-color: white">
                <asp:Panel ID="Panel8" runat="server" CssClass="table table-condensed" ScrollBars="Auto" Width="100%">
                    <asp:Label ID="lbl_delivery" runat="server" Text="Delivery Terms :"></asp:Label>
                    <asp:TextBox ID="txt_delivery_terms" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </asp:Panel>
            </div>
            </div>
        </div>
             

        <div style="background-color: white">
            <asp:Panel ID="Panel1" runat="server" CssClass="table table-condensed" ScrollBars="Auto" Width="100%">

            </asp:Panel>
        </div>
        <div class="panel panel-primary" style="background-color: white">
            <asp:Panel ID="Panel4" runat="server" ScrollBars="Auto" Width="100%">
                <asp:Table ID="Inter_charges_main" runat="server" class="table table-responsive" >
                    <asp:TableRow>
                        <asp:TableCell BorderWidth="1">
                            <asp:Panel ID="Panel5" runat="server" Style="padding-left: 15px;"></asp:Panel>
                        </asp:TableCell>
                        <asp:TableCell BorderWidth="1">
                            <asp:Panel ID="Panel6" runat="server" Style="padding-left: 15px;"></asp:Panel>
                        </asp:TableCell>
                        <asp:TableCell BorderWidth="1">
                            <asp:Table ID="Inter_charges" runat="server">
                                <asp:TableHeaderRow>
                                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center"> <b>Interlocking Charges</b></asp:TableCell>
                                </asp:TableHeaderRow>
                                <asp:TableRow>
                                    <asp:TableCell BorderWidth="1">P&F Charges</asp:TableCell>
                                    <asp:TableCell BorderWidth="1">
                                        <asp:DropDownList runat="server" ID="ddl_total_inter_pf">
                                            <asp:ListItem Value="0">0</asp:ListItem>
                                            <asp:ListItem Value="0.01">0.01</asp:ListItem>
                                            <asp:ListItem Value="0.02">0.02</asp:ListItem>
                                            <asp:ListItem Value="0.03">0.03</asp:ListItem>
                                            <asp:ListItem Value="0.04">0.04</asp:ListItem>
                                            <asp:ListItem Value="0.05">0.05</asp:ListItem>
                                            <asp:ListItem Value="0.06">0.06</asp:ListItem>
                                            <asp:ListItem Value="0.07">0.07</asp:ListItem>
                                            <asp:ListItem Value="0.08">0.08</asp:ListItem>
                                            <asp:ListItem Value="0.09">0.09</asp:ListItem>
                                            <asp:ListItem Value="0.1">0.10</asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell BorderWidth="1">Total Utility Cost</asp:TableCell>
                                    <asp:TableCell BorderWidth="1">
                                        <asp:DropDownList runat="server" ID="ddl_total_inter_finance">
                                            <asp:ListItem Value="0">0</asp:ListItem>
                                            <asp:ListItem Value="0.01">0.01</asp:ListItem>
                                            <asp:ListItem Value="0.02">0.02</asp:ListItem>
                                            <asp:ListItem Value="0.03">0.03</asp:ListItem>
                                            <asp:ListItem Value="0.04">0.04</asp:ListItem>
                                            <asp:ListItem Value="0.05">0.05</asp:ListItem>
                                            <asp:ListItem Value="0.06">0.06</asp:ListItem>
                                            <asp:ListItem Value="0.07">0.07</asp:ListItem>
                                            <asp:ListItem Value="0.08">0.08</asp:ListItem>
                                            <asp:ListItem Value="0.09">0.09</asp:ListItem>
                                            <asp:ListItem Value="0.1">0.10</asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell BorderWidth="1">Distance Charges</asp:TableCell>
                                    <asp:TableCell BorderWidth="1">
                                        <asp:DropDownList runat="server" ID="ddl_total_inter_distance">
                                            <asp:ListItem Value="0">0</asp:ListItem>
                                            <asp:ListItem Value="0.01">0.01</asp:ListItem>
                                            <asp:ListItem Value="0.02">0.02</asp:ListItem>
                                            <asp:ListItem Value="0.03">0.03</asp:ListItem>
                                            <asp:ListItem Value="0.04">0.04</asp:ListItem>
                                            <asp:ListItem Value="0.05">0.05</asp:ListItem>
                                            <asp:ListItem Value="0.06">0.06</asp:ListItem>
                                            <asp:ListItem Value="0.07">0.07</asp:ListItem>
                                            <asp:ListItem Value="0.08">0.08</asp:ListItem>
                                            <asp:ListItem Value="0.09">0.09</asp:ListItem>
                                            <asp:ListItem Value="0.1">0.10</asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell BorderWidth="1">Total Utility Cost</asp:TableCell>
                                    <asp:TableCell BorderWidth="1">
                                        <asp:DropDownList runat="server" ID="ddl_total_inter_commercial" Enabled="false">
                                            <asp:ListItem Value="0">0</asp:ListItem>
                                            <asp:ListItem Value="0.01">0.01</asp:ListItem>
                                            <asp:ListItem Value="0.02">0.02</asp:ListItem>
                                            <asp:ListItem Value="0.03">0.03</asp:ListItem>
                                            <asp:ListItem Value="0.04">0.04</asp:ListItem>
                                            <asp:ListItem Value="0.05">0.05</asp:ListItem>
                                            <asp:ListItem Value="0.06">0.06</asp:ListItem>
                                            <asp:ListItem Value="0.07">0.07</asp:ListItem>
                                            <asp:ListItem Value="0.08">0.08</asp:ListItem>
                                            <asp:ListItem Value="0.09">0.09</asp:ListItem>
                                            <asp:ListItem Value="0.1">0.10</asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell BorderWidth="1">Operation Cost</asp:TableCell>
                                    <asp:TableCell BorderWidth="1">
                                        <asp:DropDownList runat="server" ID="ddl_total_inter_operation">
                                            <asp:ListItem Value="0">0</asp:ListItem>
                                            <asp:ListItem Value="0.05">0.05</asp:ListItem>
                                            <asp:ListItem Value="0.1">0.1</asp:ListItem>
                                            <asp:ListItem Value="0.12">0.12</asp:ListItem>
                                            <asp:ListItem Value="0.15">0.15</asp:ListItem>
                                            <asp:ListItem Value="0.18">0.18</asp:ListItem>
                                            <asp:ListItem Value="0.2">0.2</asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell BorderWidth="1">Extended Warranty Cost</asp:TableCell>
                                    <asp:TableCell BorderWidth="1">
                                        <asp:DropDownList runat="server" ID="ddl_total_inter_extended">
                                            <asp:ListItem Value="0">0</asp:ListItem>
                                            <asp:ListItem Value="0.05">0.05</asp:ListItem>
                                            <asp:ListItem Value="0.1">0.1</asp:ListItem>
                                            <asp:ListItem Value="0.15">0.15</asp:ListItem>
                                            <asp:ListItem Value="0.2">0.2</asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell BorderWidth="1">Marketing Cost</asp:TableCell>
                                    <asp:TableCell BorderWidth="1">
                                        <asp:DropDownList runat="server" ID="ddl_total_inter_marketing">
                                            <asp:ListItem Value="0">0</asp:ListItem>
                                            <asp:ListItem Value="0.01">0.01</asp:ListItem>
                                            <asp:ListItem Value="0.02">0.02</asp:ListItem>
                                            <asp:ListItem Value="0.03">0.03</asp:ListItem>
                                            <asp:ListItem Value="0.04">0.04</asp:ListItem>
                                            <asp:ListItem Value="0.05">0.05</asp:ListItem>
                                            <asp:ListItem Value="0.06">0.06</asp:ListItem>
                                            <asp:ListItem Value="0.07">0.07</asp:ListItem>
                                            <asp:ListItem Value="0.08">0.08</asp:ListItem>
                                            <asp:ListItem Value="0.09">0.09</asp:ListItem>
                                            <asp:ListItem Value="0.1">0.10</asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell BorderWidth="1">Mark Up</asp:TableCell>
                                    <asp:TableCell BorderWidth="1">
                                        <asp:TextBox runat="server" CssClass="form-control" ID="ddl_total_inter_markup" Width="80" onkeypress="return isNumber(event)"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell BorderWidth="1">Project Co- ordination Cost</asp:TableCell>
                                    <asp:TableCell BorderWidth="1">
                                        <asp:DropDownList runat="server" ID="ddl_total_inter_project">
                                            <asp:ListItem Value="0">0</asp:ListItem>
                                            <asp:ListItem Value="0.01">0.01</asp:ListItem>
                                            <asp:ListItem Value="0.02">0.02</asp:ListItem>
                                            <asp:ListItem Value="0.03">0.03</asp:ListItem>
                                            <asp:ListItem Value="0.04">0.04</asp:ListItem>
                                            <asp:ListItem Value="0.05">0.05</asp:ListItem>
                                            <asp:ListItem Value="0.06">0.06</asp:ListItem>
                                            <asp:ListItem Value="0.07">0.07</asp:ListItem>
                                            <asp:ListItem Value="0.08">0.08</asp:ListItem>
                                            <asp:ListItem Value="0.09">0.09</asp:ListItem>
                                            <asp:ListItem Value="0.1">0.10</asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell BorderWidth="1">Discount</asp:TableCell>
                                    <asp:TableCell BorderWidth="1">
                                        <asp:TextBox runat="server" CssClass="form-control" ID="ddl_total_inter_discount" Width="80" onkeypress="return isNumber(event)"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:Panel>
        </div>
        <%--<asp:HiddenField ID="array_store" ClientIDMode="Static" runat="server" />--%>
       <%-- <input type="hidden" id="array_store" name="ArrayStore" value='<%=this.ArrayStore%>' />
        <input type="hidden" id="drop_down" name="dropdownarray" value='<%=this.dropdownarray%>'/>
        <input type="hidden" id="supporting" name="Supporting_Struct" value='<%=this.Supporting_Struct%>' />
        <input type="hidden" id="support_markup" name="Supporting_markup" value='<%=this.Supporting_markup%>'/>--%>

        <asp:HiddenField ID="array_store" runat="server" />
        <asp:HiddenField ID="drop_down" runat="server" />
        <asp:HiddenField ID="supporting" runat="server" />
        <asp:HiddenField ID="support_markup" runat="server" />
        <asp:HiddenField ID="wicket" runat="server" />
        <asp:HiddenField ID="wicket_markup" runat="server" />
        <asp:HiddenField ID="dock_house" runat="server" />
        <asp:HiddenField ID="dock_house_markup" runat="server" />
         <asp:HiddenField ID="dismantaling" runat="server" />
     
        <script type="text/javascript">
            window.onfocus = function () {
                $.unblockUI();
            }
            $(function () {
                $('#<%=btn_project_costing.ClientID%>').click(function () {
                    if (send_data()) {
                        $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                    }
                });

                $('#<%=ddl_proposal.ClientID%>').change(function () {
                    $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                });

                $('#<%=btn_first_proposal.ClientID%>').click(function () {
                    if (send_data()) {
                        $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                    }
                });

                $('#<%=btn_discounted_proposal.ClientID%>').click(function () {
                    if (send_data()) {
                        $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                    }
                });

                $('#<%=btn_final_proposal.ClientID%>').click(function () {
                    if (send_data()) {
                        $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                    }
                });


            });


            $(document).ready(function () {
                $.unblockUI();
            });



            function isNumber(evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    if (charCode == 46) {
                        return true;
                    }
                    return false;
                }
                return true;
            }
            function send_data() {

                var txt_1_door = document.getElementById('<%=ddl_total_markup.ClientID %>');
                if (txt_1_door.value == "" || parseFloat(txt_1_door.value) < 0) {
                    alert("Markup Should be greater than zero (0)");
                    txt_1_door.focus();
                    return false;
                }

                var idstr = "MainContent";
                var per_unit = "";
                var support_stru = "";
                var wicket_gate = "";
                var dock_house = "";
                var t_dis_man = "";
                var prodId = testArray;
                var array_store = document.getElementById('<%=array_store.ClientID%>');
                var drop_down_list = document.getElementById('<%=drop_down.ClientID%>');
                var support_markup = document.getElementById('<%=support_markup.ClientID%>');
                var supporting1 = document.getElementById('<%=supporting.ClientID%>');
                var dismantaling1 = document.getElementById('<%=dismantaling.ClientID%>');
                var wicket_markup = document.getElementById('<%=wicket_markup.ClientID%>');
                var c_wicket = document.getElementById('<%=wicket.ClientID%>');
                var dock_house_markup = document.getElementById('<%=dock_house_markup.ClientID%>');
                var c_dock_house = document.getElementById('<%=dock_house.ClientID%>'); 
                var rowCount = 1;
                //Product for loop
                for (var p = 0 ; p < testArray.length ; p++) {
                    idstr = "MainContent";

                    idstr = idstr + "_" + testArray[p] + "_"; // + rowCount + "_price_unit_";
                    //alert(idstr);
                    var prodDetailLength = no_of_prod[p];
                    for (var i = 1; i <= prodDetailLength; i++) {
                        var id_wicket = idstr + i + "_pu_wicket";
                        var id_dock_house = idstr + i + "_pu_dock_house";
                        var id1 = idstr + i + "_pu_sups";
                        var dis_man = idstr + i + "_dis_sups";
                        var id = idstr + i + "_price_unit_" + rowCount++;
                       // if ((document.getElementById(id).value == "") || (document.getElementById(id).value == 0))
                            if (document.getElementById(id).value == "")
                        {
                            alert("Please Enter Installation Charge for Product !!!");
                            document.getElementById(id).focus();
                            return false;
                        }
                        if (document.getElementById(id1)) {
                            //if ((document.getElementById(id1).value == "") || (document.getElementById(id1).value == 0)) {
                            if (document.getElementById(id1).value == "") {
                                alert("Please Enter Installation Charge for Supporting Structure !!!");
                                document.getElementById(id1).focus();
                                return false;
                            } else {
                                support_stru = support_stru + document.getElementById(id1).value + "~";
                            }
                        }
                        if (document.getElementById(dis_man)) {
                            //if ((document.getElementById(id1).value == "") || (document.getElementById(id1).value == 0)) {
                            if (document.getElementById(dis_man).value == "") {
                                alert("Please Enter Dismantling and Refixing Installation Charge !!!");
                                document.getElementById(dis_man).focus();
                                return false;
                            } else {
                                t_dis_man = t_dis_man + document.getElementById(dis_man).value + "~";
                            }
                        }
                        if (document.getElementById(id_wicket)) {
                           // if ((document.getElementById(id_wicket).value == "") || (document.getElementById(id_wicket).value == 0)) {
                                if (document.getElementById(id_wicket).value == "") {
                                alert("Please Enter Installation Charges for Wicket Gate !!!");
                                document.getElementById(id_wicket).focus();
                                return false;
                            } else {
                                wicket_gate = wicket_gate + document.getElementById(id_wicket).value + "~";
                            }
                        }
                        if (document.getElementById(id_dock_house)) {
                          //  if ((document.getElementById(id_dock_house).value == "") || (document.getElementById(id_dock_house).value == 0)) {
                                if (document.getElementById(id_dock_house).value == "") {
                                alert("Please Enter Installation Charges for Dock House !!!");
                                document.getElementById(id_dock_house).focus();
                                return false;
                            } else {
                                dock_house = dock_house + document.getElementById(id_dock_house).value + "~";
                            }
                        }
                        if (document.getElementById(id)) {
                            per_unit = per_unit + document.getElementById(id).value + "~";
                        }
                    }
                    dismantaling1.value = t_dis_man;
                    supporting1.value = support_stru;
                    c_wicket.value = wicket_gate;
                    c_dock_house.value = dock_house;
                    array_store.value = per_unit;
                    
                }
                //var id = "";
                var discount = "";
                var support = "";
                var wicket_gate_markup = "";
                var dockhouse_markup = "";
                for (var p = 0 ; p < testArray.length ; p++) {
                    idstr = "MainContent";
                    idstr = idstr + "_" + testArray[p] + "_";
                    
                    for (var i = 1 ; i <= 12 ; i++) {
                        var id = idstr + i;
                        var ddlControl = "";
                        if (document.getElementById(id)) {
                            ddlControl = document.getElementById(id);
                            if (i == 11 || i==12)
                            {
                                support = support + ddlControl.value + "~";
                                wicket_gate_markup = wicket_gate_markup + ddlControl.value + "~";
                                dockhouse_markup = dockhouse_markup + ddlControl.value + "~";
                            }
                            else { discount = discount + ddlControl.value + "~"; }
                        }
                    }
                }
                support_markup.value = support;
                // alert(supporting.value);
                wicket_markup.value = wicket_gate_markup;
                dock_house_markup.value = dockhouse_markup;
                drop_down_list.value = discount;

                //need to check with vaibhav
                var txt1 = document.getElementById('<%=insurance_basic.ClientID %>');
                    var txt2 = document.getElementById('<%=transportation_basic.ClientID %>');
                    var txt3 = document.getElementById('<%=unloading_basic.ClientID %>');
                    var txt4 = document.getElementById('<%=scaffolding_basic.ClientID %>');
                    var txt5 = document.getElementById('<%=hydra_basic.ClientID %>');
                    var txt6 = document.getElementById('<%=scizzer_basic.ClientID %>');
                    var txt7 = document.getElementById('<%=secured_basic.ClientID %>');
                    var txt8 = document.getElementById('<%=power_basic.ClientID %>');
                    var txt9 = document.getElementById('<%=txt_particular1_basic.ClientID %>');
                    var txt10 = document.getElementById('<%=txt_particular2_basic.ClientID %>');
                    var txt11 = document.getElementById('<%=txt_particular3_basic.ClientID %>');




                    if (!txt1.disabled) {
                        if (txt1.value == "0" || txt1.value == "") {
                            alert("Please Enter Utility Insurance Cost.");
                            txt1.focus();
                            return false;
                        }
                    }
                    if (!txt2.disabled) {
                        if (txt2.value == "0" || txt2.value == "") {
                            alert("Please Enter Utility Transportation Cost.");
                            txt2.focus();
                            return false;
                        }
                    }
                    if (!txt3.disabled) {
                        if (txt3.value == "0" || txt3.value == "") {
                            alert("Please Enter Utility Unloading Cost.");
                            txt3.focus();
                            return false;
                        }
                    }
                    if (!txt4.disabled) {
                        if (txt4.value == "0" || txt4.value == "") {
                            alert("Please Enter Utility Scaffolding Cost.");
                            txt4.focus();
                            return false;
                        }
                    }
                    if (!txt5.disabled) {
                        if (txt5.value == "0" || txt5.value == "") {
                            alert("Please Enter Utility Hydra Cost.");
                            txt5.focus();
                            return false;
                        }
                    }
                    if (!txt6.disabled) {
                        if (txt6.value == "0" || txt6.value == "") {
                            alert("Please Enter Utility Scizzer Lifter Cost.");
                            txt6.focus();
                            return false;
                        }
                    }

                    if (!txt7.disabled) {
                        if (txt7.value == "0" || txt7.value == "") {
                            alert("Please Enter Utility Secured & Covered Cost.");
                            txt7.focus();
                            return false;
                        }
                    }
                    if (!txt8.disabled) {
                        if (txt8.value == "0" || txt8.value == "") {
                            alert("Please Enter Power Cost.");
                            txt8.focus();
                            return false;
                        }
                    }
                    if (!txt9.disabled) {
                        if (txt9.value == "0" || txt9.value == "") {
                            alert("Please Enter Particular Cost.");
                            txt9.focus();
                            return false;
                        }
                    }
                    if (!txt10.disabled) {
                        if (txt10.value == "0" || txt10.value == "") {
                            alert("Please Enter Particular Cost.");
                            txt10.focus();
                            return false;
                        }
                    }
                    if (!txt11.disabled) {
                        if (txt11.value == "0" || txt11.value == "") {
                            alert("Please Enter Particular Cost.");
                            txt11.focus();
                            return false;
                        }
                    }
                    return true;

                }
                $(function () {
                    function initDropdown() {
                        var arrylen = testArray.length;
                        for (var i = 1 ; i <= 10 ; i++) {
                            var dropdownNO = "_" + i;
                            for (var p = 0 ; p < arrylen ; p++) {
                                idstr = "#MainContent";
                                idstr = idstr + "_" + testArray[p] + dropdownNO;
                                console.log(idstr);
                                var element = document.getElementById(idstr);
                                $(idstr).change(function () {
                                    //alert('calculate master');
                                    document.getElementById('<%= btn_project_costing.ClientID %>').click();
                                });
                            }
                        }
                    }

                    var inter1 = document.getElementById('<%=ddl_total_inter_pf.ClientID %>');
                    var inter2 = document.getElementById('<%=ddl_total_inter_finance.ClientID %>');
                    var inter3 = document.getElementById('<%=ddl_total_inter_distance.ClientID %>');
                    var inter4 = document.getElementById('<%=ddl_total_inter_commercial.ClientID %>');
                    var inter5 = document.getElementById('<%=ddl_total_inter_operation.ClientID %>');
                    var inter6 = document.getElementById('<%=ddl_total_inter_extended.ClientID %>');
                    var inter7 = document.getElementById('<%=ddl_total_inter_marketing.ClientID %>');
                    var inter8 = document.getElementById('<%=ddl_total_inter_markup.ClientID %>');
                    var inter9 = document.getElementById('<%=ddl_total_inter_project.ClientID %>');
                    var inter10 = document.getElementById('<%=ddl_total_inter_discount.ClientID %>');

                    $("#MainContent_ddl_total_pf").change(function () {
                        var selectedValue = this.value;
                        for (var p = 0 ; p < testArray.length ; p++) {
                            idstr = "#MainContent";
                            idstr = idstr + "_" + testArray[p] + "_1";
                            $(idstr).val(selectedValue).attr("selected", "selected");
                            $(inter1).val(selectedValue).attr("selected", "selected");
                        }
                        document.getElementById('<%= btn_project_costing.ClientID %>').click();
                    });

                    $("#MainContent_ddl_total_finance").change(function () {
                        var selectedValue = this.value;
                        for (var p = 0 ; p < testArray.length ; p++) {
                            idstr = "#MainContent";
                            idstr = idstr + "_" + testArray[p] + "_2";
                            $(idstr).val(selectedValue).attr("selected", "selected");
                            $(inter2).val(selectedValue).attr("selected", "selected");
                        }
                        document.getElementById('<%= btn_project_costing.ClientID %>').click();
                    });

                    $("#MainContent_ddl_total_distance").change(function () {
                        var selectedValue = this.value;
                        for (var p = 0 ; p < testArray.length ; p++) {
                            idstr = "#MainContent";
                            idstr = idstr + "_" + testArray[p] + "_3";
                            $(idstr).val(selectedValue).attr("selected", "selected");
                            $(inter3).val(selectedValue).attr("selected", "selected");
                        }
                        document.getElementById('<%= btn_project_costing.ClientID %>').click();
                    });

                    $("#MainContent_ddl_total_commercial").change(function () {
                        var selectedValue = this.value;
                        for (var p = 0 ; p < testArray.length ; p++) {
                            idstr = "#MainContent";
                            idstr = idstr + "_" + testArray[p] + "_4";
                            $(idstr).val(selectedValue).attr("selected", "selected");
                            $(inter4).val(selectedValue).attr("selected", "selected");
                        }
                        document.getElementById('<%= btn_project_costing.ClientID %>').click();
                    });

                    $("#MainContent_ddl_total_operation").change(function () {
                        var selectedValue = this.value;
                        for (var p = 0 ; p < testArray.length ; p++) {
                            idstr = "#MainContent";
                            idstr = idstr + "_" + testArray[p] + "_5";
                            $(idstr).val(selectedValue).attr("selected", "selected");
                            $(inter5).val(selectedValue).attr("selected", "selected");
                        }
                        document.getElementById('<%= btn_project_costing.ClientID %>').click();
                    });

                    $("#MainContent_ddl_total_extended").change(function () {
                        var selectedValue = this.value;
                        for (var p = 0 ; p < testArray.length ; p++) {
                            idstr = "#MainContent";
                            idstr = idstr + "_" + testArray[p] + "_6";
                            $(idstr).val(selectedValue).attr("selected", "selected");
                            $(inter6).val(selectedValue).attr("selected", "selected");
                        }
                        document.getElementById('<%= btn_project_costing.ClientID %>').click();
                    });

                    $("#MainContent_ddl_total_marketing").change(function () {
                        var selectedValue = this.value;
                        for (var p = 0 ; p < testArray.length ; p++) {
                            idstr = "#MainContent";
                            idstr = idstr + "_" + testArray[p] + "_7";
                            $(idstr).val(selectedValue).attr("selected", "selected");
                            $(inter7).val(selectedValue).attr("selected", "selected");
                        }
                        document.getElementById('<%= btn_project_costing.ClientID %>').click();
                    });
                    var stru_markup = "";

                    $("#MainContent_ddl_total_markup").change(function () {
                        var selectedValue = this.value;
                        for (var p = 0 ; p < testArray.length ; p++) {
                            idstr = "#MainContent";
                            idstr = idstr + "_" + testArray[p] + "_8";
                            $(idstr).val(selectedValue).attr("selected", "selected");
                            $(inter8).val(selectedValue).attr("selected", "selected");
                            stru_markup = "#MainContent";
                            stru_markup = stru_markup + "_" + testArray[p] + "_10";
                            $(stru_markup).val(selectedValue).attr("selected", "selected");
                        }
                        document.getElementById('<%= btn_project_costing.ClientID %>').click();
                    });

                    $("#MainContent_ddl_total_project").change(function () {
                        var selectedValue = this.value;
                        for (var p = 0 ; p < testArray.length ; p++) {
                            idstr = "#MainContent";
                            idstr = idstr + "_" + testArray[p] + "_9";
                            $(idstr).val(selectedValue).attr("selected", "selected");
                            $(inter9).val(selectedValue).attr("selected", "selected");
                        }
                        document.getElementById('<%= btn_project_costing.ClientID %>').click();
                    });

                    initDropdown();

                    $("#MainContent_ddl_total_inter_pf").change(function () { document.getElementById('<%= btn_project_costing.ClientID %>').click(); });
                    $("#MainContent_ddl_total_inter_finance").change(function () { document.getElementById('<%= btn_project_costing.ClientID %>').click(); });
                    $("#MainContent_ddl_total_inter_distance").change(function () { document.getElementById('<%= btn_project_costing.ClientID %>').click(); });
                    $("#MainContent_ddl_total_inter_commercial").change(function () { document.getElementById('<%= btn_project_costing.ClientID %>').click(); });
                    $("#MainContent_ddl_total_inter_operation").change(function () { document.getElementById('<%= btn_project_costing.ClientID %>').click(); });
                    $("#MainContent_ddl_total_inter_extended").change(function () { document.getElementById('<%= btn_project_costing.ClientID %>').click(); });
                    $("#MainContent_ddl_total_inter_marketing").change(function () { document.getElementById('<%= btn_project_costing.ClientID %>').click(); });
                    $("#MainContent_ddl_total_inter_markup").change(function () { document.getElementById('<%= btn_project_costing.ClientID %>').click(); });
                    $("#MainContent_ddl_total_inter_project").change(function () { document.getElementById('<%= btn_project_costing.ClientID %>').click(); });
                    $("#MainContent_ddl_total_inter_discount").change(function () { document.getElementById('<%= btn_project_costing.ClientID %>').click(); });

                });

                "use strict";
                var fn = function (s, l, a, i) { var v5, v6 = l && ('isSelectionDisabled' in l); if (!(v6)) { if (s) { v5 = s.isSelectionDisabled; } } else { v5 = l.isSelectionDisabled; } return v5; }; fn.assign = function (s, v, l) { var v0, v1, v2, v3, v4 = l && ('isSelectionDisabled' in l); v3 = v4 ? l : s; if (!(v4)) { if (s) { v2 = s.isSelectionDisabled; } } else { v2 = l.isSelectionDisabled; } if (v3 != null) { v1 = v; ensureSafeObject(v3.isSelectionDisabled, text); ensureSafeAssignContext(v3, text); v0 = v3.isSelectionDisabled = v1; } return v0; };



        </script>
    </body>
    </html>

</asp:Content>

