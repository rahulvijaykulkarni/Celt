<%@ Page Title="TDS Calculation" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Document_Details.aspx.cs" Inherits="Document_Details" EnableEventValidation="false" %>

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
    <script>

        
          </script>
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

              $(".date-picker1").datepicker({
                  changeMonth: true,
                  changeYear: true,
                  showButtonPanel: true,
                  dateFormat: 'dd/mm/yy',
                  yearRange: '1950',
                  onSelect: function (selected) {
                      $(".date-picker2").datepicker("option", "minDate", selected)
                  }
              });


              $(".date-picker2").datepicker({
                  changeMonth: true,
                  changeYear: true,
                  showButtonPanel: true,
                  dateFormat: 'dd/mm/yy',
                  onSelect: function (selected) {
                      $(".date-picker1").datepicker("option", "maxDate", selected)
                  }
              });
              $(".date-picker1").attr("readonly", "true");
              $(".date-picker2").attr("readonly", "true");

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

       
          function openWindow()
          {

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
                        <div style="color: #fff; font: small;" class="text-center text-uppercase"><b>DOCUMENT DETAILS</b></div>
                    </div>
                    <div class="col-sm-2 text-right">

                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="col-sm-2 col-xs-12"></div>
                <div class="row">

                    <div class="col-sm-2 col-xs-12">
                        Sr.NO : 
                  
                        <asp:TextBox ID="txt_Sr_No" runat="server" class="form-control  text_box" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                        Employee Name : 
                   
                       <asp:DropDownList ID="ddl_employeename" runat="server" class="form-control text_box" OnSelectedIndexChanged="reporting_to_drtails" AutoPostBack="true" Width="100%"></asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                        Reporting To : 
                    
                        <asp:TextBox ID="txt_reporting_to" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet1(event,this);"></asp:TextBox>
                    </div>

                </div>
                <br />
                <div class="row">
                    <div class="col-sm-2 col-xs-12"></div>

                    <div class="col-sm-2 col-xs-12">
                        Product Type : 
                   
                        <asp:DropDownList ID="ddl_document_list" runat="server" class="form-control text_box">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="1">Uniform</asp:ListItem>
                            <asp:ListItem Value="2">Shoes</asp:ListItem>
                            <asp:ListItem Value="3">Police Varification Document</asp:ListItem>
                            <asp:ListItem Value="4">Present Address Proof</asp:ListItem>

                        </asp:DropDownList>
                    </div>


                    <div class="col-sm-2 col-xs-12">
                        Issu Date :
                        <asp:TextBox ID="txt_start_date" runat="server" class="form-control date-picker1" placeholder="Start Date :"></asp:TextBox>
                    </div>

                    <div class="col-sm-2 col-xs-12">
                        Expiry Date :
                                        <asp:TextBox ID="txt_end_date" runat="server" class="form-control date-picker2" placeholder="End Date :"></asp:TextBox>
                    </div>

                </div>
                <br />



                <br />
                <asp:Panel ID="pnl_document_detials" runat="server" ScrollBars="Auto" CssClass="grid-view" class="panel-body">
                    <asp:GridView ID="gv_Tds" runat="server" class="table" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" OnRowDataBound="gv_Tds_RowDataBound"
                        OnSelectedIndexChanged="gv_Tds_SelectedIndexChanged">

                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Wrap="True" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <EditRowStyle BackColor="#2461BF" />

                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        <Columns>
                            <asp:CommandField HeaderText="-&gt;" SelectText="-&gt;" ShowSelectButton="True" />
                            <asp:TemplateField HeaderText="Sr. No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Eval("Id")%>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("emp_code")%>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reporting To">
                                <ItemTemplate>
                                    <asp:Label ID="lblreportinfto" runat="server" Text='<%# Eval("reporting_to")%>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Document Type">
                                <ItemTemplate>
                                    <asp:Label ID="lbldocumenttype" runat="server" Text='<%# Eval("document_type")%>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblstartdate" runat="server" Text='<%# Eval("start_date")%>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblenddate" runat="server" Text='<%# Eval("end_date")%>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="50px" />
                            </asp:TemplateField>


                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <br />

                <div class="row text-center">

                    <asp:Button ID="btn_Save" runat="server" class="btn btn-primary" Text="Save" OnClick="btn_Save_Click" OnClientClick="return Req_Validation();" />
                    <asp:Button ID="btn_Update" runat="server" class="btn btn-primary" Text="Update" OnClick="btn_Update_Click" />
                    <asp:Button ID="btn_Delete" runat="server" class="btn btn-primary" Text="Delete" OnClick="btn_Delete_Click" />


                    <asp:Button ID="btn_Close" runat="server" CausesValidation="False" CssClass="btn btn-danger" Text="Close" OnClick="btn_Close_Click" />
                </div>

            </div>
        </asp:Panel>
        <%-- </ContentTemplate>      
</asp:UpdatePanel>--%>
    </div>
</asp:Content>







