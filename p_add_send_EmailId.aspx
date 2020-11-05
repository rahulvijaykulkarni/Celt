<%@ Page Language="C#" AutoEventWireup="true" CodeFile="p_add_send_EmailId.aspx.cs" Inherits="p_add_send_EmailId" EnableEventValidation="false" %>

<html>
<head>
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

    <link href="datatable/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="datatable/buttons.bootstrap.min.css" rel="stylesheet" />
    <script src="datatable/jquery.dataTables.min.js"></script>
    <script src="datatable/dataTables.bootstrap.min.js"></script>
    <script src="datatable/dataTables.buttons.min.js"></script>
    <script src="datatable/buttons.bootstrap.min.js"></script>
    <script src="datatable/vfs_fonts.js"></script>
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>
    <script type="text/javascript">
        function ClientCheck() {
            var valid = false;
            var gv = document.getElementById("gv_send_email");
            for (var i = 0; i < gv.getElementsByTagName("input").length; i++) {
                var node = gv.getElementsByTagName("input")[i];
                if (node != null && node.type == "checkbox" && node.checked) {
                    valid = true;
                    break;
                }
            }
            if (!valid) {
                alert("Please select at list one Email Id.");
            }
            return valid;
        }
        $(document).ready(function () {
            $("#txt_cc").change(function () {
                var EmailId = $("#txt_cc").val();
                if (validateEmailAddress(EmailId)) {
                    return true;
                }
                else {
                    alert('Invalid Email Address.Please enter valid email e.g abc@domain.com');

                    return false;
                }
            });
        });
        function validateEmailAddress(EmailId) {
            var expr = /^(\s?[^\s,]+@[^\s,]+\.[^\s,]+\s?,)*(\s?[^\s,]+@[^\s,]+\.[^\s,]+)$/
            if (expr.test(EmailId)) {
                return true;
            }
            else {
                return false;
            }
        }


    </script>

    <script>
        function unblock() {
            $.unblockUI();
        }
        $(document).ready(function () {

            $('#<%=btn_upload.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=btn_mailsend.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_send_email.ClientID%>').DataTable({
            "responsive": true,
            "sPaginationType": "full_numbers",
            buttons: [
                {
                    extend: 'csv',
                    exportOptions: {
                        columns: ':visible'
                    }
                },
                {
                    extend: 'print',
                    exportOptions: {
                        columns: ':visible'
                    }
                },
                {
                    extend: 'copyHtml5',
                    exportOptions: {
                        columns: ':visible'
                    }
                },
                'colvis'
            ]

        });

        table.buttons().container()
           .appendTo('#<%=gv_send_email.ClientID%>_wrapper .col-sm-6:eq(0)');

        });

       function pageLoad() {

           $(".date-picker1").datepicker({
               changeMonth: true,
               changeYear: true,
               showButtonPanel: true,
               dateFormat: 'dd/mm/yy',
               minDate: 0,
               onSelect: function (selected) {
                   $(".date-picker2").datepicker("option", "minDate", selected)
               }
           });


           $(".date-picker2").datepicker({
               changeMonth: true,
               changeYear: true,
               showButtonPanel: true,
               dateFormat: 'dd/mm/yy',
               minDate: 0,
               onSelect: function (selected) {
                   $(".date-picker1").datepicker("option", "maxDate", selected)
               }
           });


           $(".date-picker1").attr("readonly", "true");
           $(".date-picker2").attr("readonly", "true");
       }





       function AllowAlphabet1(e) {
           if (null != e) {

               isIE = document.all ? 1 : 0
               keyEntry = !isIE ? e.which : e.keyCode;
               if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry < '31') || (keyEntry == '32') || (keyEntry == '46'))

                   return true;
               else {
                   // alert('Please Enter Only Character values.');
                   return false;
               }
           }
       }
       function AllowAlphabet_itemname(e) {
           if (null != e) {

               isIE = document.all ? 1 : 0
               keyEntry = !isIE ? e.which : e.keyCode;
               if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry < '31') || !(keyEntry <= '122') || (keyEntry = '45') || (keyEntry = '46'))

                   return true;
               else {
                   // alert('Please Enter Only Character values.');
                   return false;
               }
           }
       }
       function openWindow() {
           window.open("html/DepartmentMaster.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
       }
    </script>
    <style>
        body {
            font-family: Verdana;
            font-size: 10px;
        }

        .form-control {
            font-size: 12px;
        }

        .grid-view {
            max-height: 400px;
            height: auto;
            overflow-y: auto;
            overflow-x: hidden;
            text-align: center;
        }

        th {
            text-align: center;
            font-size: 12px;
        }
    </style>
    <script>
        function validate() {
            var inp = document.getElementById("<%= document1_file.ClientID %>");

            if (inp.files.length === 0) {
                alert("Attachment Required");
                inp.focus();

                return false;
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
        <br />
        <div class="container">
            <div class="row" style="display: none">
                <div class="col-sm-2  col-xs-12"><span>File to Upload :</span></div>
                <div class="col-sm-3 col-xs-12">
                    <asp:FileUpload ID="document1_file" runat="server" meta:resourcekey="photo_uploadResource1" />
                </div>
                <div class="col-sm-3 col-xs-12">
                    <asp:Button ID="btn_upload" runat="server" class="btn btn-primary" OnClick="btn_upload_Click" Text="Upload" />
                    <asp:Button ID="btn_mailsend" runat="server" Text="Send_Email" class="btn btn-primary" OnClick="btn_mailsend_click" />

                </div>



            </div>
            <br />

            <br />
            <asp:Panel ID="Panel2" runat="server">

                <asp:GridView ID="gv_send_email" class="table" runat="server" CssClass="grid-view" Width="100%" Height="20%"
                    AutoGenerateColumns="False" BackColor="White" BorderColor="#000" OnRowDataBound="gv_emailid_RowDataBound"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="10px" OnPreRender="gv_send_email_PreRender">
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
                        <asp:BoundField DataField="Id" HeaderText="ID" />
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk_id" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="head_type" HeaderText="Head Type"
                            SortExpression="head_type" />
                        <asp:BoundField DataField="head_name" HeaderText="Head Name"
                            SortExpression="head_name" />
                        <asp:BoundField DataField="head_email_id" HeaderText="Head Email"
                            SortExpression="head_email_id" />


                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
                <div class="row">
                    <div class="col-sm-4 col-xs-12">
                        CC EmailID:
                                    <asp:TextBox ID="txt_cc" runat="server" class="form-control text_box"></asp:TextBox>
                        <%-- <asp:RegularExpressionValidator
            ID="RegularExpressionValidator1"
            runat="server"
            ControlToValidate="txt_cc"
            ErrorMessage="Invalid Email Address"
            

            ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*"></asp:RegularExpressionValidator>

                        --%>
                    </div>
                </div>
            </asp:Panel>
            <br />
            <br />
            <div class="row">
                <div class="col-sm-5">
                </div>
                <div class="col-lg-2">
                    <asp:Button ID="Button1" runat="server" Text="Send_Email" class="btn btn-primary" OnClick="btn_mailsend_click" OnClientClick="return ClientCheck();" />
                </div>
            </div>
            <div class="row text-center">
            </div>



        </div>




        <%--</ContentTemplate> </asp:UpdatePanel>--%>
    </form>
</body>
</html>
