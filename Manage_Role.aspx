<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Manage_Role.aspx.cs" Inherits="Manage_Role" Title="CREATE / UPDATE ROLE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
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
    <style>
        .text-red {
            color: red;
        }

        .container {
            max-width: 99%;
        }

        .label_text {
            font-size: 14px;
            font-weight: bold;
            margin-bottom: 10px;
        }

        .text_box {
            margin-top: 7px;
        }

        .auto-style1 {
            color: #FFFFFF;
        }

        h2 {
            border-radius: 5px;
        }

        .grid-view {
            height: auto;
            overflow: scroll;
        }

        h5 {
            font-weight: bold;
            font-size: 15px;
        }

        .row {
            margin: 0px;
        }
    </style>
    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />

    <head>
        <title>Role Access Form</title>
        <script type="text/javascript">
            function pageLoad(sender, args) {
                $(document).ready(function () {
                    $('[id*=chk_delete_header]').click(function () {
                        $("[id*='chk_delete']").attr('checked', this.checked);
                    });
                });
                $(document).ready(function () {
                    $('[id*=chk_create_header]').click(function () {
                        $("[id*='chk_create']").attr('checked', this.checked);
                    });
                });
                $(document).ready(function () {
                    $('[id*=chk_update_header]').click(function () {
                        $("[id*='chk_update']").attr('checked', this.checked);
                    });
                });
                $(document).ready(function () {
                    $('[id*=chk_read_header]').click(function () {
                        $("[id*='chk_Read']").attr('checked', this.checked);
                    });
                });
            }
            //$(document).ready(function () {
            //    var chk_name = 'ctl00_cph_righrbody_GridView2_ctl';
            //    var chk_delete = '_chk_delete';
            //    var chk_create = '_chk_create';
            //    var chk_update = '_chk_update';
            //    var chk_read = '_chk_Read';
            //    //alert('hi');
            //    for (var p = 2 ; p < 94 ; p++) {
            //        var cnt = p;
            //        if (p < 9)
            //        {
            //            cnt = '0' + p;
            //        }
            //        var chk_id = document.getElementById('ctl00_cph_righrbody_GridView2_ctl' + cnt + '_chk_delete');
            //        alert(chk_id);
            //        if (chk_id) {
            //            $('#' + chk_id).click(function () {
            //                alert(chk_id);
            //            });
            //        }
            //        //if ($("#" + document.getElementById(chk_id)).prop("checked", true))
            //        //{
            //        //    alert(chk_id);

            //        //}
            //            //$("[id*='ctl00_cph_righrbody_GridView2_ctl'" + cnt + "'_chk_create']").attr('checked', this.checked);
            //            //$("[id*='ctl00_cph_righrbody_GridView2_ctl'" + cnt + "'_chk_update']").attr('checked', this.checked);
            //            //$("[id*='ctl00_cph_righrbody_GridView2_ctl'" + cnt + "'_chk_Read']").attr('checked', this.checked);
            //       // alert('ctl00_cph_righrbody_GridView2_ctl' + cnt + '_chk_delete');
            //        //$('[id*='+ chk_id + ']').click(function () {
            //        //    alert('ctl00_cph_righrbody_GridView2_ctl' + cnt + '_chk_delete');
            //        //    $("[id*='ctl00_cph_righrbody_GridView2_ctl'" + cnt + "'_chk_create']").attr('checked', this.checked);
            //        //    $("[id*='ctl00_cph_righrbody_GridView2_ctl'" + cnt + "'_chk_update']").attr('checked', this.checked);
            //        //    $("[id*='ctl00_cph_righrbody_GridView2_ctl'" + cnt + "'_chk_Read']").attr('checked', this.checked);
            //        //});
            //    }
            //});

        </script>
        <script type="text/javascript">

            function GetCheckedCheckBox() {
                var checkedCheckBox;
                var dataGrid = document.all('<%=GridView2.ClientID %>');
                var rows = dataGrid.rows;
                for (var index = 1; index < rows.length; index++) {
                    var checkBox = rows[index].cells[0].childNodes[3];
                    if (checkBox.Checked)
                        checkedCheckBox = checkBox;
                }
                return checkedCheckBox;
            }

            function OnTreeClick(evt) {
                var src = window.event != window.undefined ? window.event.srcElement : evt.target;
                var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
                if (isChkBoxClick) {
                    var parentTable = GetParentByTagName("table", src);
                    var nxtSibling = parentTable.nextSibling;
                    if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
                    {
                        if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
                        {
                            //check or uncheck children at all levels
                            CheckUncheckChildren(parentTable.nextSibling, src.checked);
                        }
                    }
                    //check or uncheck parents at all levels
                    CheckUncheckParents(src, src.checked);
                }
            }

            function CheckUncheckChildren(childContainer, check) {
                var childChkBoxes = childContainer.getElementsByTagName("input");
                var childChkBoxCount = childChkBoxes.length;
                for (var i = 0; i < childChkBoxCount; i++) {
                    childChkBoxes[i].checked = check;
                }
            }

            function CheckUncheckParents(srcChild, check) {
                var parentDiv = GetParentByTagName("div", srcChild);
                var parentNodeTable = parentDiv.previousSibling;

                if (parentNodeTable) {
                    var checkUncheckSwitch;

                    if (check) //checkbox checked
                    {
                        var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                        if (isAllSiblingsChecked)
                            checkUncheckSwitch = true;
                        else
                            checkUncheckSwitch = true;
                    }
                    else //checkbox unchecked
                    {
                        checkUncheckSwitch = false;
                    }

                    var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
                    if (inpElemsInParentTable.length > 0) {
                        var parentNodeChkBox = inpElemsInParentTable[0];
                        parentNodeChkBox.checked = checkUncheckSwitch;
                        //do the same recursively
                        CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
                    }
                }
            }

            function AreAllSiblingsChecked(chkBox) {
                var parentDiv = GetParentByTagName("div", chkBox);
                var childCount = parentDiv.childNodes.length;
                for (var i = 0; i < childCount; i++) {
                    if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
                    {
                        if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                            var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                            //if any of sibling nodes are not checked, return false
                            if (prevChkBox.checked) {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }

            //utility function to get the container of an element by tagname
            function GetParentByTagName(parentTagName, childElementObj) {
                var parent = childElementObj.parentNode;
                while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
                    parent = parent.parentNode;
                }
                return parent;
            }



            function Validate() {

                var role_name = document.getElementById('<%=txt_roleName.ClientID%>');

                if (role_name.value == "") {
                    alert("Please Enter Role Name...!!!");
                    role_name.focus();
                    return false;
                }

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                return true;
            }
            function callwait() {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                return true;
            }

            window.onfocus = function () {
                $.unblockUI();
            }
            function openWindow() {
                window.open("html/Manage_Role.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
            }

        </script>
        <style type="text/css">
            .container {
                max-width: 99%;
            }

            .grid-view {
                height: auto;
                max-height: 500px;
                overflow: scroll;
            }
        </style>
        <style>
            .container {
                max-width: 99%;
            }

            .label_text {
                font-size: 14px;
            }

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

    </head>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                    <ContentTemplate>--%>
        <asp:Panel ID="Panel1" runat="server" CssClass="panel panel-primary">

            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase" font-size="25px"><b>ROLE ACCESS FORM</b></div>
                    </div>
                    <div class="col-sm-2 text-right">

                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />

            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-9">
                        <div style="text-align: left; color:white; font-size: small;"><b>Role Access Form Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:15px 15px 15px 15px; margin-bottom:20px; margin-top:20px">
                        <div class="container">
                            <div class="row">
                                <div class="col-sm-2">
                                    <b>Update Role :</b><span class="text-red"> *</span>
                                    <asp:DropDownList ID="ddl_Update_role" runat="server" AutoPostBack="true" onchange="javascript:callwait();" OnSelectedIndexChanged="ddl_Update_role_SelectedIndexChanged" class="form-control text_box" Width="100%"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                   <b> Role Name :</b><span class="text-red"> *</span>
                                    <asp:TextBox ID="txt_roleName" runat="server" CssClass="form-control text_box"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                   <b> Approval Levels :</b><span class="text-red"> *</span>
                                    <asp:TextBox ID="txt_approval_level" runat="server" CssClass="form-control text_box"></asp:TextBox>
                                </div>
                                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-3 col-xs-12 text-left">
                                            Client Name :<span class="text-red"> *</span>
                                            <asp:ListBox ID="ddl_unit_client" runat="server" class="form-control pr_state js-example-basic-single text_box" SelectionMode="Multiple"  AutoPostBack="true"></asp:ListBox>
                                        </div>
                                        <div class="col-sm-3 col-xs-12">
                                            State :<span class="text-red"> *</span>
                                            <asp:ListBox ID="ddl_clientwisestate" runat="server" class="form-control pr_state js-example-basic-single text_box" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>--%>
                            </div>
                          </div>
                     </div>
                <br />
                
                <br />
                            <asp:Panel ID="gridview" runat="server" align="center">
                                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:15px 15px 15px 15px;  margin-left:100px;margin-right:100px">
                                <asp:GridView ID="GridView2" runat="server" HeaderStyle-BackColor="#3AC0F2" 
                                    OnRowDataBound="OnRowDataBound" HeaderStyle-ForeColor="White" Width="70%" 
                                    class="table" AutoGenerateColumns="false">
                                    <Columns>

                                        <asp:BoundField DataField="Description" HeaderText="Menu Name">
                                            <ItemStyle Width="250px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="permissions" HeaderText="" />
                                        <asp:TemplateField HeaderText="Remove">
                                            <HeaderStyle HorizontalAlign="center" />
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chk_delete_header" runat="server" Text="Delete" />
                                            </HeaderTemplate>
                                            <ItemStyle Width="80px" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_delete" runat="server" Style="text-align: center" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Add">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chk_create_header" runat="server" Text="Create" />
                                            </HeaderTemplate>
                                            <ItemStyle Width="80px" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_create" runat="server" Style="text-align: center" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Change">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chk_update_header" runat="server" Text="Update" />
                                            </HeaderTemplate>
                                            <ItemStyle Width="80px" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_update" runat="server" Style="text-align: center" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chk_read_header" runat="server" Text="Read" />
                                            </HeaderTemplate>
                                            <ItemStyle Width="80px" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_Read" runat="server" Style="text-align: center" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                </asp:GridView>

                            </asp:Panel>

                            <br />
                <br />
                            <div class="row text-center">
                                <asp:Button ID="btnSubmit" runat="server" OnClick="btmSubmit_Click"
                                    Text="Submit" OnClientClick="return Validate();" class="btn btn-primary" />

                                <%--&nbsp&nbsp <asp:Button ID="btn_update" runat="server" Text="Update" OnClick="btn_update_Click"  class="btn btn-primary" />--%>

                                <%-- <asp:Button ID="Button2" runat="server" Text="Search Role"  OnClick="Button2_Click" class="btn btn-primary"/>--%>

                                <asp:Button ID="btn_close" runat="server" class="btn btn-danger" OnClick="btn_close_Click" Text=" Close " />
                            </div>
                <br />
                            <%--<div class="table-responsive">
                <asp:GridView ID="GridView1" class="table" DataKeyNames="role_name" runat="server" CellPadding="4" ForeColor="#333333"
                    ShowHeaderWhenEmpty="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Height="208px"  OnRowDeleting="GridView1_RowDeleting">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#428BCA" Font-Bold="True" ForeColor="White" />

                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    <Columns>
                        <asp:CommandField SelectText="Edit" ShowSelectButton="True"
                            meta:resourcekey="CommandFieldResource1" />
                        <asp:CommandField ShowDeleteButton="true" />
                    </Columns>
                </asp:GridView>
           </div>--%>
                       


                    </asp:Panel>
                </div>
                </div>
            </div>
        </asp:Panel>
        <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
    </div>
</asp:Content>
