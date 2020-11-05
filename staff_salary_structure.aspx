<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="staff_salary_structure.aspx.cs" Inherits="staff_salary_structure" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Staff Salary Structure</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_header" runat="Server">
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
    <script type="text/javascript">

        //save button
        function Req_validation() {
            var txt_head1_per = document.getElementById('<%=txt_head1_per.ClientID %>');


            if (txt_head1_per.value == "" || txt_head1_per.value == "0") {

                alert("Please Enter Head1(%) !!");
                txt_head1_per.value == "0";
                txt_head1_per.focus();
                return false;
            }
            var txt_head2 = document.getElementById('<%=txt_head2.ClientID %>');


            if (txt_head2.value == "" || txt_head2.value == "0") {

                alert("Please Enter Head2 !!");
                txt_head2.value == "0";
                txt_head2.focus();
                return false;
            }
            var txt_head2_per = document.getElementById('<%=txt_head2_per.ClientID %>');


            if (txt_head2_per.value == "" || txt_head2_per.value == "0") {

                alert("Please Enter Head2(%) !!");
                txt_head2_per.value == "0";
                txt_head2_per.focus();
                return false;
            }
            var txt_pfemployee = document.getElementById('<%=txt_pfemployee.ClientID %>');


            if (txt_pfemployee.value == "" || txt_pfemployee.value == "0") {

                alert("Please Enter PF Employee !!");
                txt_pfemployee.value == "0";
                txt_pfemployee.focus();
                return false;
            }
            var txt_pfemployer = document.getElementById('<%=txt_pfemployer.ClientID %>');


            if (txt_pfemployer.value == "" || txt_pfemployer.value == "0") {

                alert("Please Enter PF Employer !!");
                txt_pfemployer.value == "0";
                txt_pfemployer.focus();
                return false;
            }
            var txt_esicemployee = document.getElementById('<%=txt_esicemployee.ClientID %>');


            if (txt_esicemployee.value == "" || txt_esicemployee.value == "0") {

                alert("Please Enter ESIC Employee !!");
                txt_esicemployee.value == "0";
                txt_esicemployee.focus();
                return false;
            }    
            var txt_esicemployer = document.getElementById('<%=txt_esicemployer.ClientID %>');


            if (txt_esicemployer.value == "" || txt_esicemployer.value == "0") {

                alert("Please Enter ESIC Employer !!");
                txt_esicemployer.value == "0";
                txt_esicemployer.focus();
                return false;
            }
        }
        function openWindow() {
            window.open("html/staff_salary_structure.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }

    </script>
    <style>
        .text_box {
            margin-top: 7px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Staff Salary Structure</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />
          <%--  <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-9">
                        <div style="text-align: left; color:white; font-size: small;"><b>Staff Salary Structure Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>--%>
            <div class="panel-body">
                <div class="container-fluid">
                    <div class="container-fluid" style="border: 1px solid #ddd9d9; background: #f3f1fe; padding-bottom:15px; margin-left:-10px; margin-right:-10px; border-radius: 10px;margin-bottom:20px;margin-top:20px">
                    <br />
                    <br />
                     <div class="row">
                          <div class="col-sm-2 col-xs-12">
                           <b> Client Name:</b>
                            <asp:DropDownList runat="server" ID="ddl_client" CssClass="form-control text_box" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged1" AutoPostBack="true" meta:resourceKey="ddl_emp_liststatusResource1"></asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                          <b>  Unit Name:</b>
                            <asp:DropDownList runat="server" ID="ddl_unit" CssClass="form-control text_box" meta:resourceKey="ddl_emp_liststatusResource1"></asp:DropDownList>
                        </div>
                        </div>
                    <br />
                   </div>
                    <div class="row">
                        <div class="container-fluid" style="border: 1px solid #ddd9d9; background: #f3f1fe; padding-top:25px; padding-bottom:15px; border-radius: 10px;margin-bottom:20px;margin-top:25px">
                        <div class="col-sm-6 col-xs-12">
                            <table border="1" style="border-color: gray" class="table table-striped">
                                <tr>
                                    <th class="text-center">
                                        <h4>Earning Heads</h4>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                        <div class="row">

                                            <div class="col-sm-3 col-xs-12">Head1:<asp:TextBox runat="server" ID="txt_head1" CssClass="form-control text_box" CausesValidation="true" ReadOnly="true">BASIC</asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12">Head1(%):<asp:TextBox runat="server" ID="txt_head1_per" CssClass="form-control text_box">0</asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12">Head2:<asp:TextBox runat="server" ID="txt_head2" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12">Head2(%):<asp:TextBox runat="server" ID="txt_head2_per" CssClass="form-control text_box">0</asp:TextBox></div>

                                        </div>
                                    </th>
                                </tr>

                                <tr>
                                    <th>
                                        <div class="row">

                                            <div class="col-sm-3 col-xs-12">Head3:<asp:TextBox runat="server" ID="txt_head3" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12">Head3(%):<asp:TextBox runat="server" ID="txt_head3_per" CssClass="form-control"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12">Head4:<asp:TextBox runat="server" ID="txt_head4" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12">Head4(%):<asp:TextBox runat="server" ID="txt_head4_per" CssClass="form-control text_box"></asp:TextBox></div>

                                        </div>
                                    </th>
                                </tr>

                                <tr>
                                    <th>
                                        <div class="row">

                                            <div class="col-sm-3 col-xs-12">Head5:<asp:TextBox runat="server" ID="txt_head5" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12">Head5(%):<asp:TextBox runat="server" ID="txt_head5_per" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12">Head6:<asp:TextBox runat="server" ID="txt_head6" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12">Head6(%):<asp:TextBox runat="server" ID="txt_head6_per" CssClass="form-control text_box"></asp:TextBox></div>
                                        </div>
                                    </th>
                                </tr>

                                <tr>
                                    <th>
                                        <div class="row">
                                            <div class="col-sm-3 col-xs-12">Head7:<asp:TextBox runat="server" ID="txt_head7" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12">Head7(%):<asp:TextBox runat="server" ID="txt_head7_per" CssClass="form-control"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12">Head8:<asp:TextBox runat="server" ID="txt_head8" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12">Head8(%):<asp:TextBox runat="server" ID="txt_head8_per" CssClass="form-control text_box"></asp:TextBox></div>
                                        </div>
                                    </th>
                                </tr>

                                <tr>
                                    <th>
                                        <div class="row">
                                            <div class="col-sm-3 col-xs-12">Head9:<asp:TextBox runat="server" ID="txt_head9" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12">Head9(%):<asp:TextBox runat="server" ID="txt_head9_per" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12">Head10:<asp:TextBox runat="server" ID="txt_head10" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12">Head10(%):<asp:TextBox runat="server" ID="txt_head10_per" CssClass="form-control text_box"></asp:TextBox></div>
                                        </div>
                                    </th>
                                </tr>

                                <tr>
                                    <th>
                                        <div class="row">
                                            <div class="col-sm-3 col-xs-12">Head11:<asp:TextBox runat="server" ID="txt_head11" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12">Head11(%):<asp:TextBox runat="server" ID="txt_head11_per" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12">Head12:<asp:TextBox runat="server" ID="txt_head12" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12">Head12(%):<asp:TextBox runat="server" ID="txt_head12_per" CssClass="form-control text_box"></asp:TextBox></div>
                                        </div>
                                    </th>
                                </tr>

                                <tr>
                                    <th>
                                        <div class="row">
                                            <div class="col-sm-3 col-xs-12">Head13:<asp:TextBox runat="server" ID="txt_head13" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12">Head13(%):<asp:TextBox runat="server" ID="txt_head13_per" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12">Head14:<asp:TextBox runat="server" ID="txt_head14" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12">Head14(%):<asp:TextBox runat="server" ID="txt_head14_per" CssClass="form-control text_box"></asp:TextBox></div>
                                        </div>
                                    </th>
                                </tr>

                                <tr>
                                    <th>
                                        <div class="row">
                                            <div class="col-sm-3 col-xs-12">Head15:<asp:TextBox runat="server" ID="txt_head15" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12">Head15(%):<asp:TextBox runat="server" ID="txt_head15_per" CssClass="form-control text_box"></asp:TextBox></div>
                                        </div>
                                    </th>
                                </tr>
                            </table>
                        </div>
                        <div class="col-sm-6 col-xs-12">
                            <div class="row">
                                  <div class="col-sm-4 col-xs-12">
                            <table border="1" style="border-color: gray" class="table table-striped">
                                <tr>
                                    <th>
                                        <h4>Lumpsum Heads</h4>
                                    </th>
                                </tr>
                                <tr>
                                    <th>L_Head1 :
                                    <asp:TextBox ID="txt_lhead1" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <th>L_Head2 :
                                   <asp:TextBox ID="txt_lhead2" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <th>L_Head3 :
                                                       
                                 <asp:TextBox ID="txt_lhead3" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <th>L_Head4 :
                                                       
                                   <asp:TextBox ID="txt_lhead4" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <th>L_Head5 :
                                                        
                                 <asp:TextBox ID="txt_lhead5" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                </tr>
                            </table>
</div>
                                   <div class="col-sm-8 col-xs-12">
                            <table border="1" style="border-color: gray" class="table table-striped">
                                <tr>
                                    <th colspan="2" class="text-center">
                                        <h4>Deduction Heads</h4>
                                    </th>
                                </tr>
                                <tr>
                                    <th>D_Head1 :
                                                       
                                          <asp:TextBox ID="txt_dhead1" runat="server" class="form-control text_box" Width="100%" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                    <th>D_Head2 :
                                                       
                                       <asp:TextBox ID="txt_dhead2" runat="server" class="form-control text_box" Width="100%" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                </tr>

                                <tr>
                                    <th>D_Head3 :
                                                       
                                      <asp:TextBox ID="txt_dhead3" runat="server" class="form-control text_box" Width="100%" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                    <th>D_Head4 :
                                                       
                                    <asp:TextBox ID="txt_dhead4" runat="server" class="form-control text_box" Width="100%" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                </tr>

                                <tr>
                                    <th>D_Head5 :
                                                        
                               <asp:TextBox ID="txt_dhead5" runat="server" class="form-control text_box" Width="100%" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                    <th>D_Head6 :
                                                       
                                    <asp:TextBox ID="txt_dhead6" runat="server" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15" class="form-control text_box" Width="100%"></asp:TextBox>
                                    </th>
                                </tr>

                                <tr>
                                    <th>D_Head7 :
                                                       
                                         <asp:TextBox ID="txt_dhead7" runat="server" class="form-control text_box" Width="100%" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                    <th>D_Head8 :
                                                      
                                     <asp:TextBox ID="txt_dhead8" runat="server" class="form-control text_box" Width="100%" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>

                                    </th>
                                </tr>

                                <tr>
                                    <th>D_Head9 :
                                                       
                                 <asp:TextBox ID="txt_dhead9" runat="server" class="form-control text_box" Width="100%" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                    <th>D_Head10 :
                                                       
                                       <asp:TextBox ID="txt_dhead10" runat="server" class="form-control text_box" Width="100%" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                </tr>

                            </table>
                                       </div>
                                </div>
                            <div class="row">
  <table border="1" style="border-color: gray;margin-top:-5px;" class="table table-striped">
                                <tr>
                                    <th colspan="2" class="text-center">
                                        <h4>Tax Deductions</h4>
                                    </th>
                                </tr>
                                <tr>
                                   <th>
                                       PF Employee:
                                       <asp:TextBox runat="server" ID="txt_pfemployee" CssClass="form-control text_box">0</asp:TextBox>
                                   </th>
                                     <th>
                                       PF Employer:
                                       <asp:TextBox runat="server" ID="txt_pfemployer" CssClass="form-control text_box">0</asp:TextBox>
                                   </th>
                                    </tr>
       <tr>
                                   <th>
                                       PF Formula Employee:
                                       <asp:TextBox runat="server" ID="txt_pfemployee_formula" CssClass="form-control text_box">0</asp:TextBox>
                                   </th>
                                     <th>
                                       PF Formula Employer:
                                       <asp:TextBox runat="server" ID="txt_pfemployer_formula" CssClass="form-control text_box">0</asp:TextBox>
                                   </th>
                                    </tr>
                                     <tr>
                                   <th>
                                       ESIC Employee:
                                       <asp:TextBox runat="server" ID="txt_esicemployee" CssClass="form-control text_box">0</asp:TextBox>
                                   </th>
                                     <th>
                                       ESIC Employer:
                                       <asp:TextBox runat="server" ID="txt_esicemployer" CssClass="form-control text_box">0</asp:TextBox>
                                   </th>
                                    </tr>

                                    <tr>
                                   <th>
                                       ESIC Formula Employee:
                                       <asp:TextBox runat="server" ID="txt_esicemployee_formula" CssClass="form-control text_box">0</asp:TextBox>
                                   </th>
                                     <th>
                                       ESIC Formula Employer:
                                       <asp:TextBox runat="server" ID="txt_esicemployer_formula" CssClass="form-control text_box">0</asp:TextBox>
                                   </th>
                                    </tr>
      
      </table>
                            </div>
                    </div>
                        </div>
                        </div>
                    <br />
                    <div class="row text-center">
                        <asp:Button ID="btn_add" runat="server" class="btn btn-primary" Text="Save" OnClick="btn_save_click" OnClientClick="return Req_validation()"/>
                        <asp:Button ID="btnclose" runat="server" class="btn btn-danger" Text="Close" OnClick="btn_close_click"/>
                    </div>
                    <br />
                    
                    <div class="container-fluid" style="border: 1px solid #ddd9d9; background: #f3f1fe; padding-top:25px; padding-bottom:15px; border-radius: 10px;margin-bottom:20px;margin-top:25px">
                    <asp:Panel ID="gv_staffsalary" runat="server" ScrollBars="Auto" CssClass="grid-view">
                        <asp:GridView ID="gv_salary_structure" class="table" runat="server"
                           Font-Size="X-Small"
                            ForeColor="#333333" OnPreRender="GridView1_files_PreRender" OnRowDataBound="gv_salary_structure_RowDataBound"
                            OnSelectedIndexChanged="gv_salary_structure_SelectedIndexChanged">

                             <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#424D7A" Font-Bold="True" ForeColor="White" Width="50" CssClass="text-uppercase" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <%--<RowStyle BackColor="#ffffff" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />--%>
                                           <%-- <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />--%>

                        </asp:GridView>
                    </asp:Panel>
                        </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
