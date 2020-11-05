<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CS.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
    </style>
       <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta charset="utf-8" />
    <script src="js/bootstrap.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js"></script>
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" />
    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />

   <script src='<%=ResolveUrl("~/Webcam_Plugin/jquerycamera.min.js") %>' type="text/javascript"></script>
<script src='<%=ResolveUrl("~/Webcam_Plugin/jquery.webcam.js") %>' type="text/javascript"></script>
    
      <link href="css/select2.min.css" rel="stylesheet" />
    <script src="js/select2.min.js"></script>
<script type="text/javascript">
    $('.select2').select2();
    var pageUrl = '<%=ResolveUrl("~/CS.aspx") %>';
    $(function () {
        jQuery("#webcam").webcam({
            width: 560,
            height: 440,
            mode: "save",
            swffile: '<%=ResolveUrl("~/Webcam_Plugin/jscam.swf") %>',
        debug: function (type, status) {
            //$('#camStatus').append(type + ": " + status + '<br /><br />');
        },
        onSave: function (data) {
            $.ajax({
                type: "POST",
                url: pageUrl + "/GetCapturedImage",
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $("[id*=imgCapture]").css("visibility", "visible");
                    $("[id*=imgCapture]").attr("src", r.d);
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        },
        onCapture: function () {
            webcam.save(pageUrl);
        }
    });
});
function Capture() {
    webcam.capture();
    return false;
}
function pageLoad()
{
    $('#<%=btnCapture.ClientID%>').click(function () {
        $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
    });
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid">
        <br />
        <asp:Panel runat="server" ID="panel_attendance" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="text-align: center; color: #fff; font-size: small;"><b>EMPLOYEE ATTENDANCE</b></div>
                    </div>
                   
                </div>
            </div> 
            <div class="panel-body">
        <div class="row">
           <%-- <div class="col-sm-2 col-xs-12">
                Employee List:
                <asp:DropDownList runat="server" CssClass="form-control text_box select2" ID="ddl_emp_list"></asp:DropDownList>
            </div>--%>
            <div class="col-sm-2 col-xs-12">
                Employee Name:
                  <asp:TextBox ID="txt_emp_name" runat="server" class="form-control"  placeholder="Employee Name :" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="col-sm-2 col-xs-12">
                Unit Name:
                <asp:TextBox ID="txt_unit_name" runat="server" class="form-control"  placeholder="Unit Name :" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="col-sm-2 col-xs-12" style="visibility:hidden ">
                Employee Code:
                  <asp:TextBox ID="txt_employee_code" runat="server" class="form-control"  placeholder="Employee Name :" ReadOnly="true"></asp:TextBox>
            </div> 
        </div>
        <br />
        <div class="row">
             <div class="col-sm-1 col-xs-12"></div>  
            <div class="col-sm-9 col-xs-12">
<table border="1" cellpadding="0" cellspacing="0" class="table table-striped">
    <tr>
        <td align="center">
            <u>Live Camera</u>
        </td>
       
        <td align="center">
            <u>Captured Picture</u>
        </td>
    </tr>
    <tr>
        <td style="width:50%">
            <div id="webcam">
            </div>
        </td>
       
        <td style="width:50%">
            <asp:Image ID="imgCapture" runat="server" Style="visibility: hidden; width: 560px;
                height: 440px" />
        </td>
    </tr>
</table>
                </div>
            </div>
<br />
        <div class="row text-center">
            <asp:Button ID="btnCapture" Text="Capture Image" runat="server" CssClass="btn btn-primary"  OnClientClick="return Capture();" />
            <asp:Button ID="btnsave" Text="Submit" runat="server" CssClass="btn btn-primary"  Visible="true" OnClick="btn_save_details" />
             <asp:Button ID="btnclose" Text="Close" runat="server" CssClass="btn btn-danger"  OnClick="btn_close_details" />
            </div>
<br />
<span id="camStatus"></span>
          <asp:Panel ID="Panel5" runat="server">
                    <asp:GridView ID="GridView1" class="table" runat="server" AutoGenerateColumns="False"
                        CellPadding="1" ForeColor="#333333" Font-Size="X-Small">
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                           
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                    <br />
                    <br />
                </asp:Panel>
                </div>
                    </asp:Panel>
        </div>

    </form>
</body>
</html>


  
    
   

 
    

 
