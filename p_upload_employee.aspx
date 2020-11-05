<%@ Page Language="C#" AutoEventWireup="true" CodeFile="p_upload_employee.aspx.cs" Inherits="Default2" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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


    <script type="text/javascript">

        function Req_validation() {

            var ddl_upload_lg_client = document.getElementById('<%=ddlunitclient1.ClientID %>');
            var client_name = ddl_upload_lg_client.options[ddl_upload_lg_client.selectedIndex].text;

            if (client_name == "Select") {
                alert("Please Select Client Name");
                ddl_upload_lg_client.focus();
                return false;
            }

            var ddl_upload_lg_state = document.getElementById('<%=ddl_gv_statewise.ClientID %>');
        var state_name = ddl_upload_lg_state.options[ddl_upload_lg_state.selectedIndex].text;

        if (state_name == "Select") {
            alert("Please Select State Name");
            ddl_upload_lg_state.focus();
            return false;
        }

        var ddl_upload_lg_unit = document.getElementById('<%=ddl_gv_branchwise.ClientID %>');
            var branch_name = ddl_upload_lg_unit.options[ddl_upload_lg_unit.selectedIndex].text;

            if (branch_name == "Select") {
                alert("Please Select Branch Name");
                ddl_upload_lg_unit.focus();
                return false;
            }

            var FileUpload1 = document.getElementById('<%=FileUpload1.ClientID %>');

            if (FileUpload1.value == "") {
                alert("Please Upload the File ");
                FileUpload1.focus();
                return false;
            }
            return true;

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <br />
                <div class="row">
                    <div class="col-sm-4 col-xs-12">
                        <b>Client :</b>
                        <asp:DropDownList ID="ddlunitclient1" runat="server" DataValueField="client_Code" DataTextField="client_name" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddlunitclient1_SelectedIndexChanged" />
                    </div>

                    <div class="col-sm-4 col-xs-12">
                       <b> State :</b>
                        <asp:DropDownList ID="ddl_gv_statewise" runat="server" DataValueField="STATE_CODE" DataTextField="STATE_NAME" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddl_gv_statewise_SelectedIndexChanged" />
                    </div>
                    <div class="col-sm-4 col-xs-10">
                       <b> Branch :</b>
                        <asp:DropDownList ID="ddl_gv_branchwise" runat="server" DataValueField="UNIT_CODE" DataTextField="UNIT_NAME" class="form-control text_box" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <div class="row" border="1">

            <div class=" col-sm-2 col-xs-12" style="margin-top: 9px;">
                File :<span class="text-red">*</span>
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </div>

            <%--<asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>--%>
            <div class=" col-sm-1 col-xs-12" style="margin-top: 14px;">
            <asp:LinkButton ID="lnk_download" runat="server" CausesValidation="false" Text="Download" Style="color: white" OnCommand="lnk_download_Command" CommandArgument='<%# Eval("stamp_copy")%>' CssClass="btn btn-primary"></asp:LinkButton>
            </div>
                <%--  </ItemTemplate>
                                    </asp:TemplateField>--%>

            <div class=" col-sm-2 col-xs-12" style="margin-top: 14px;">
                <asp:Button ID="btn_upload" runat="server" CssClass="btn btn-primary convience"
                    Text="Employee" OnClick="btn_upload_Click" OnClientClick="return Req_validation();" />
            </div>

        </div>
    </form>
</body>
</html>
