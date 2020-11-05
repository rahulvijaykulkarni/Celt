<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="employee_upload.aspx.cs" Inherits="GradeDetails" Title="DESIGNATION MASTER" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Employee Upload</title>
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
    <script src="js/select2.min.js"></script>
    <link href="css/select2.min.css" rel="stylesheet" />
    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <link href="css/new_stylesheet.css" rel="stylesheet" />
    <style type="text/css">
        .tab-section {
            background-color: #fff;
        }

        .text-red {
            color: #f00;
        }
    </style>
    <script>
        function val_process() {
            var FileUpload1 = document.getElementById('<%=FileUpload1.ClientID %>');
        if (FileUpload1.value == "") {
            alert("Please select excel file for upload.");
            FileUpload1.focus();
            return false;
        }
    }

    </script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cph_righrbody" runat="Server">

    <div class="container">
        <br />
        <asp:Panel runat="server" ID="uploadfile" CssClass="panel panel-primary">
            <div class="row  text-center">
                <h5 style="font-weight: bold">Import Employee Excel</h5>
            </div>
            <br />
            <div class="row">
                <div class=" col-sm-4 col-xs-12"></div>
                <div class="col-sm-1">
                    File :<span class="text-red">*</span>
                </div>
                <div class=" col-sm-3 col-xs-12">

                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </div>

                <div class=" col-sm-3 col-xs-12">
                    <asp:Button ID="btn_save" runat="server" CssClass="btn btn-primary"
                        Text="Process" OnClick="btn_save_Click" OnClientClick="return val_process();" />

                    <asp:Button ID="btn_download_template" runat="server" CssClass="btn btn-primary"
                        Text="Download Template" OnClick="btn_download_template_Click" />
                </div>
            </div>
            <br />
        </asp:Panel>
        <br />
    </div>
</asp:Content>


