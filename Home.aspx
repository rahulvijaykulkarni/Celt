<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Home</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_header" runat="Server">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta charset="utf-8" />
   <script src="js/Chart.js"></script>
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
    <link href="dashboard/css/style_grid.css" rel="stylesheet" />
    <link href="dashboard/css/font-awesome.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-2.1.4.min.js"></script>

    <style>





        body {
  overflow: auto; /* Hide scrollbars */
}

        .box1 {
            border: 1px solid #337AB7;
            border-radius: 3px;
        }
            .box1:hover {
                transform: scale(1.1);
            }

        .top_box1 {
            padding: 10px;
            background-color: #337AB7;
        }

        .end_box1 {
            padding: 10px;
            color: #337AB7;
            background-color: #ebebeb;
        }

        .box2 {
            border: 1px solid #5CB85C;
            border-radius: 3px;
        }
         .box2:hover {
                transform: scale(1.1);
            }

        .top_box2 {
            padding: 10px;
            background-color: #5CB85C;
        }

        .end_box2 {
            padding: 10px;
            color: #5CB85C;
            background-color: #ebebeb;
        }

        .box3 {
            border: 1px solid #F0AD4E;
            border-radius: 3px;
        }
         .box3:hover {
                transform: scale(1.1);
            }

        .top_box3 {
            padding: 10px;
            background-color: #F0AD4E;
        }

        .end_box3 {
            padding: 10px;
            color: #F0AD4E;
            background-color: #ebebeb;
        }

        .box4 {
            border: 1px solid #D8514D;
            border-radius: 3px;
        }
        .box4:hover {
                transform: scale(1.1);
            }

        .top_box4 {
            padding: 10px;
            background-color: #D8514D;
        }

        .end_box4 {
            padding: 10px;
            color: #D8514D;
            background-color: #ebebeb;
        }

        .box5 {
            border: 1px solid #52B5D2;
            border-radius: 3px;
        }
        .box5:hover {
                transform: scale(1.1);
            }

        .top_box5 {
            padding: 10px;
            background-color: #52B5D2;
        }

        .end_box5 {
            padding: 10px;
            color: #52B5D2;
            background-color: #ebebeb;
        }

        .box6 {
            border: 1px solid #6a3579;
            border-radius: 3px;
        }
        .box6:hover {
                transform: scale(1.1);
            }

        .top_box6 {
            padding: 10px;
            background-color: #6a3579;
        }

        .end_box6 {
            padding: 10px;
            color: #6a3579;
            background-color: #ebebeb;
        }

        .box7 {
            border: 1px solid #F0AD4E;
            border-radius: 3px;
        }
        .box7:hover {
                transform: scale(1.1);
            }

        .top_box7 {
            padding: 10px;
            background-color: #F0AD4E;
        }

        .end_box7 {
            padding: 10px;
            color: #F0AD4E;
            background-color: #ebebeb;
        }

        .box8 {
            border: 1px solid #D8514D;
            border-radius: 3px;
        }
        .box8:hover {
                transform: scale(1.1);
            }

        .top_box8 {
            padding: 10px;
            background-color: #D8514D;
        }

        .end_box8 {
            padding: 10px;
            color: #D8514D;
            background-color: #ebebeb;
        }

        .box9 {
            border: 1px solid #52B5D2;
            border-radius: 3px;
        }
        .box9:hover {
                transform: scale(1.1);
            }

        .top_box9 {
            padding: 10px;
            background-color: #52B5D2;
        }

        .end_box9 {
            padding: 10px;
            color: #52B5D2;
            background-color: #ebebeb;
        }

        .text1 {
            color: #fff;
            font-size: 14px;
        }

        .text2 {
            color: #fff;
            font-size: 18px;
        }

        .glyap1 {
            color: #fff;
            font-size: 60px;
        }

        .glyap2 {
            font-size: 15px;
        }
        .blinking{
            
    animation:blinkingText 2.5s infinite;
}
@keyframes blinkingText{
    0%{     color: /*#000;*/ #1a75ff;   }
    49%{    color: /*#000;*/ #1a75ff; }
    60%{    color: transparent; }
    99%{    color:transparent;  }
    100%{   color: /*#000;*/ #1a75ff;   }
}
    </style>
    <script type="text/javascript">
        //$(document).ready(function () {
          //  $.fn.dataTable.ext.errMode = 'none';
            //var table = $('#<%=grd_client_pending.ClientID%>').DataTable({
              //   "responsive": true,
                // "sPaginationType": "full_numbers",
               //  buttons: [
                 //    {
                   //      extend: 'csv',
                     //    exportOptions: {
                       //      columns: ':visible'
                       //  }
                     //},
                    // {
                      //   extend: 'print',
                        // exportOptions: {
                          //   columns: ':visible'
                         //}
             //        },
               //      {
                 //        extend: 'copyHtml5',
                   //      exportOptions: {
                     //        columns: ':visible'
                       //  }
                     //},
                     //'colvis'
               //  ]

             //});

             //table.buttons().container()
              //  .appendTo('#<%=grd_client_pending.ClientID%>_wrapper .col-sm-6:eq(0)');
            //
           //  $.fn.dataTable.ext.errMode = 'none';
         //});
          //branch

         // $(document).ready(function () {
              //$.fn.dataTable.ext.errMode = 'none';
            //  var table = $('#<%=location_branch_pending_home.ClientID%>').DataTable({
          //       "responsive": true,
        //         "sPaginationType": "full_numbers",
      //           buttons: [
                     //{
                         //extend: 'csv',
                       //  exportOptions: {
                     //        columns: ':visible'
                   //      }
                 //    },
               //      {
                         //extend: 'print',
                        // exportOptions: {
                      //       columns: ':visible'
                    //     }
                  //   },
                //     {
              //           extend: 'copyHtml5',
            //             exportOptions: {
          //                   columns: ':visible'
        //                 }
      //               },
    //                 'colvis'
  //               ]

//             });

             //table.buttons().container()
                //.appendTo('#<%=location_branch_pending_home.ClientID%>_wrapper .col-sm-6:eq(0)');
              //
          //   $.fn.dataTable.ext.errMode = 'none';
        // });
         //employee_grd_home
         //$(document).ready(function () {
           //  $.fn.dataTable.ext.errMode = 'none';
             //var table = $('#<%=employee_grd_home.ClientID%>').DataTable({
               //  "responsive": true,
                 //"sPaginationType": "full_numbers",
                 //buttons: [
                   //  {
                     //    extend: 'csv',
                       //  exportOptions: {
                         //    columns: ':visible'
                         //}
                     //},
                     //{
                       //  extend: 'print',
                         //exportOptions: {
                           //  columns: ':visible'
                         //}
                     //},
                     //{
                       //  extend: 'copyHtml5',
                         //exportOptions: {
                           //  columns: ':visible'
                         //}
                     //},
                     //'colvis'
                 //]

//             });

             //table.buttons().container()
               // .appendTo('#<%=employee_grd_home.ClientID%>_wrapper .col-sm-6:eq(0)');
             //
          //   $.fn.dataTable.ext.errMode = 'none';
        // });
         //employee compliances
         //$(document).ready(function () {
           //  $.fn.dataTable.ext.errMode = 'none';
             //var table = $('#<%=Grid_compliances_pupop.ClientID%>').DataTable({
               //  "responsive": true,
                 //"sPaginationType": "full_numbers",
                 //buttons: [
                   //  {
                     //    extend: 'csv',
                       //  exportOptions: {
                         //    columns: ':visible'
                         //}
                     //},
                     //{
                         //extend: 'print',
                         //exportOptions: {
                         //    columns: ':visible'
                       //  }
                     //},
                     //{
                         //extend: 'copyHtml5',
                         //exportOptions: {
                       //      columns: ':visible'
                     //    }
                   //  },
                 //    'colvis'
               //  ]

             //});
             
          //   table.buttons().container()
        //        .appendTo('#<%=Grid_compliances_pupop.ClientID%>_wrapper .col-sm-6:eq(0)');

      //    $.fn.dataTable.ext.errMode = 'none';
    //     });

    </script>


    <script type="text/javascript">
        $(window).on('load', function () {

        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <div class="container-fluid">
        <h1><b><center><span class="blinking">Welcome To IHMS</span></center></b></h1>
      
    <asp:Panel ID="Panel1" runat="server" Visible="false" CssClass="body">
        <div id="Div1" class="carousel slide" data-ride="carousel">
            <!-- Indicators -->
            <ol class="carousel-indicators">
                <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                <li data-target="#myCarousel" data-slide-to="1"></li>
                <li data-target="#myCarousel" data-slide-to="2"></li>
            </ol>
            <!-- Wrapper for slides -->
            <div class="carousel-inner" role="listbox">
                <div class="item active">
                    <div class="attendance">
                        <div class="text-center head1"><b>Attend Today, Achieve Tomorrow !</b></div>
                        <div class="row">
                            <div class="col-sm-2 col-xs-12"></div>
                            <div class="col-sm-8 col-xs-12">
                                <asp:Panel ID="Panel0" runat="server" Height="300px" ScrollBars="Auto">
                                    <asp:GridView ID="attendance_gridview" class="table" runat="server" AllowPaging="True" AutoGenerateColumns="False" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <RowStyle ForeColor="#ffffff" />
                                        <Columns>
                                            <asp:CommandField HeaderText="-&gt;" SelectText="-&gt;"
                                                ShowSelectButton="True" />

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Employee Name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Emp_name" runat="server" Text='<%# Eval("EMP_NAME")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Date">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_date" runat="server" Text='<%# Eval("LogDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="InTime">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_intime" runat="server" Text='<%# Eval("C1")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="OutTime">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_outtime" runat="server" Text='<%# Eval("C2")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Work Code">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_workcode" runat="server" Text='<%# Eval("WorkCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <SelectedRowStyle Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                            <div class="col-sm-2 col-xs-12"></div>
                        </div>
                    </div>
                </div>
                <div class="item">
                    <div class="birthday" style="color: #111;">
                        <div class="text-center head2"><b>Hope your Awesome day - Happy Birthday !</b></div>
                        <div class="row">
                            <div class="col-sm-3 col-xs-12"></div>
                            <div class="col-sm-6 col-xs-12">

                                <asp:Panel ID="pnl_bday" runat="server"  Height="300px" Width="100%" ScrollBars="Auto">
                                    <br />
                                    <br />
                                    <asp:GridView ID="gridview_bday" runat="server" CssClass="table" ShowHeader="true"
                                        GridLines="Both" AutoGenerateColumns="true" BackColor="White" BorderColor="#990033" Style="border: 2px solid #990033; box-shadow: 0px 10px 10px 0px #CC6699;"
                                        DataSourceID="" AutoGenerateSelectButton="true"
                                        OnRowDataBound="grdview_bday_RowDataBound"
                                        OnSelectedIndexChanged="gridview_bday_SelectedIndexChanged">
                                        <RowStyle ForeColor="#000066" BorderColor="#990033" />
                                        <AlternatingRowStyle BackColor="#FFCCCC" ForeColor="#333333" BorderColor="#990033" />

                                        <FooterStyle BackColor="White" ForeColor="#999999" />
                                        <PagerStyle BackColor="White" ForeColor="#999999" HorizontalAlign="center" BorderColor="#990033" />
                                        <SelectedRowStyle BackColor="#add8e6" Font-Bold="True" ForeColor="White" BorderColor="#990033" />
                                        <HeaderStyle Font-Bold="True" ForeColor="#111111" BorderColor="#990033" CssClass="notice-success" />
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                            <div class="col-sm-3 col-xs-12"></div>
                        </div>
                    </div>
                </div>
                <div class="item">
                    <div class="policy">
                        <div class="text-center head3"><b>Better Policy, Better Success !</b></div>
                        <div class="row">
                            <div class="col-sm-3 col-xs-12"></div>
                            <div class="col-sm-6 col-xs-12">
                                <asp:Panel ID="pnl_policy" runat="server" Height="300px" Width="100%" ScrollBars="Auto">
                                    <asp:GridView ID="gridview_policy" runat="server" CssClass="table"
                                        ShowHeader="true" GridLines="Both" AutoGenerateColumns="true"
                                        BorderColor="#CCCCCC"
                                        Style="border: 2px solid #333333; box-shadow: 0px 10px 10px 0px #999999;"
                                        OnRowDataBound="grdview_bday_RowDataBound">
                                        <RowStyle ForeColor="#000066" />
                                        <AlternatingRowStyle BackColor="#eeeeee" ForeColor="#666666" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnk_download" Text="Download" CommandArgument='<%# Eval("Attachment") %>' runat="server" OnClick="gridview_policy_SelectedIndexChanged"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#999999" />
                                        <PagerStyle BackColor="White" ForeColor="#999999" HorizontalAlign="center" />
                                        <SelectedRowStyle BackColor="#add8e6" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#cccccc" Font-Bold="True" ForeColor="#333333" CssClass="notice-success2" />
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                            <div class="col-sm-3 col-xs-12"></div>
                        </div>
                    </div>
                </div>
                <div class="item">
                    <div class="career">
                        <div class="text-center head4"><b>Work Together and Explore Opportunities !</b></div>
                        <div class="row">
                            <div class="col-sm-3 col-xs-12"></div>
                            <div class="col-sm-6 col-xs-12">
                                <asp:Panel ID="pnl_vacancy" runat="server" Height="300px" ScrollBars="Auto">
                                    <asp:GridView ID="JobOpeningGridView" runat="server" CssClass="table"
                                        ShowHeader="true" GridLines="Both" AutoGenerateColumns="false"
                                        BorderColor="Black"
                                        Style="border: 2px solid #333333; box-shadow: 0px 10px 10px 0px #999999;"
                                        OnRowDataBound="grdview_bday_RowDataBound">
                                        <RowStyle ForeColor="white" />
                                        <AlternatingRowStyle BackColor="#cccccc" ForeColor="white" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="Role">
                                                <HeaderStyle HorizontalAlign="center" />
                                                <ItemStyle HorizontalAlign="center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_role" runat="server" Style="text-align: center" Text='<%# Eval("ROLE")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="Mandatory Skills">
                                                <HeaderStyle HorizontalAlign="center" />
                                                <ItemStyle HorizontalAlign="center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Mandatory_Skills" runat="server" Style="text-align: center" Text='<%# Eval("Mandatory_Skills")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="Job Location">
                                                <HeaderStyle HorizontalAlign="center" />
                                                <ItemStyle HorizontalAlign="center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_location" runat="server" Style="text-align: center" Text='<%# Eval("LOCATION")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="Start Date">
                                                <HeaderStyle HorizontalAlign="center" />
                                                <ItemStyle HorizontalAlign="center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstartdate" runat="server" Style="text-align: center" Text='<%# Eval("Start_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="End Date">
                                                <HeaderStyle HorizontalAlign="center" />
                                                <ItemStyle HorizontalAlign="center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblenddate" runat="server" Style="text-align: center" Text='<%# Eval("End_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#cccccc" ForeColor="white" />
                                        <PagerStyle BackColor="#cccccc" ForeColor="white" HorizontalAlign="center" />
                                        <SelectedRowStyle BackColor="#cccccc" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#cccccc" Font-Bold="True" ForeColor="white" CssClass="notice-success2" />
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </asp:Panel>

    <div class="container-fluid main_box" style="display: none;">
        <div class="row">
            <div class="col-sm-6 col-xs-12">

                <div class="event">
                    <div class="inner">
                        <h2 class="text-center"><b>Event</b></h2>

                        <div class="row text-center">
                            Event_Name-<asp:Label ID="lbl_Event_Name" runat="server"></asp:Label><br />
                            Description:-<asp:Label ID="lbl_header_description" runat="server"></asp:Label>
                        </div>

                        <asp:Panel ID="Panel4" runat="server" Height="200px">
                            <br />
                            <div id="myCarousel" class="carousel slide" data-ride="carousel">
                                <!-- Indicators -->
                                <ol class="carousel-indicators">
                                    <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                                    <li data-target="#myCarousel" data-slide-to="1"></li>
                                    <li data-target="#myCarousel" data-slide-to="2"></li>
                                </ol>
                                <!-- Wrapper for slides -->
                                <div class="carousel-inner" role="listbox">
                                    <div class="item active">
                                        <asp:Image ID="Image1" Height="185px" Width="100%" runat="server" ImageUrl='<%# Eval("emp_photo") %>' />
                                    </div>
                                    <div class="item">
                                        <asp:Image ID="Image2" Height="185px" Width="100%" runat="server" ImageUrl='<%# Eval("emp_photo") %>' />
                                    </div>
                                    <div class="item">
                                        <asp:Image ID="Image3" Height="185px" Width="100%" runat="server" ImageUrl='<%# Eval("emp_photo") %>' />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>

            </div>
            <%-- swara--%>

            <div class="modal fade" id="Div2">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal">×</a>
                    <h3>Modal header</h3>
                </div>
                <div class="modal-body">
                    <p>One fine body…</p>
                </div>
                <div class="modal-footer">
                    <a href="#" class="btn">Close</a>
                    <a href="#" class="btn btn-primary">Save changes</a>
                </div>
            </div>
            <div class="col-sm-6 col-xs-12">
                <div class="suggest1 text-center">
                    <div class="inner">
                        <h2><b>Suggestions</b></h2>
                        <br />
                        <br />
                        <div class="img_sugg">
                            <img src="Images/envelope.png" height="100" class="img-responsive img_sugg2" data-toggle="modal" data-target="#myModal" />
                        </div>
                        <br />
                        <br />

                        <!-- Modal -->

                        <div class="modal fade" id="myModal" role="dialog">
                            <div class="modal-dialog modal-md">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title" style="color: #000;"><b>Suggestions Box</b></h4>
                                    </div>
                                    <div class="modal-body suggest2">

                                        <div class="row ">
                                            <div class="col-sm-3 col-xs-12 text-right">
                                                Type :
                                            </div>
                                            <div class="col-sm-3 col-xs-12 text-right">
                                                <asp:DropDownList ID="ddl_type_selected" runat="server" class="form-control">
                                                    <asp:ListItem Value="1">-Select Type-</asp:ListItem>
                                                    <asp:ListItem Value="2">feedback</asp:ListItem>
                                                    <asp:ListItem Value="3">Issue-Admin</asp:ListItem>
                                                    <asp:ListItem Value="4">Issue-Attendance</asp:ListItem>
                                                    <asp:ListItem Value="5">Issue-Investment</asp:ListItem>
                                                    <asp:ListItem Value="6">Issue-IT</asp:ListItem>
                                                    <asp:ListItem Value="7">Issue-Salary</asp:ListItem>
                                                    <asp:ListItem Value="8">Request For Letter</asp:ListItem>
                                                    <asp:ListItem Value="9">Suggestion</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-3 col-xs-12 text-right text-right ">
                                                Subject :
                                            </div>
                                            <div class="col-sm-6 col-xs-12">
                                                <asp:TextBox ID="txt_subject" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-3 col-xs-12 text-right">
                                                Description :
                                            </div>
                                            <div class="col-sm-6 col-xs-12">
                                                <asp:TextBox ID="txt_description" runat="server" Rows="2" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-3 col-xs-12 text-right ">
                                                Response :
                                            </div>
                                            <div class="col-sm-6 col-xs-12 ">
                                                <asp:TextBox ID="txt_response" runat="server" TextMode="MultiLine" Rows="2" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-3 col-xs-12 text-right">
                                                Upload Doc :
                                            </div>
                                            <div class="col-sm-6 col-xs-12 ">
                                                <asp:FileUpload ID="upload_file" runat="server" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-3 col-xs-12">
                                            </div>
                                            <div class="col-sm-9 col-xs-12">
                                                <p>
                                                    <i>Note: 1) Maximum file size 500 KB. 
                                                    <br />
                                                        2) File types Allowed to be uploaded: jpg, pdf, docx, doc, txt.</i>
                                                </p>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-3 col-xs-12">
                                            </div>
                                            <div class="col-sm-9 col-xs-12">

                                                <p><i>Note: On Submit click email alert goes to HR Team.</i></p>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="Submit" CssClass="btn btn-success"
                                            OnClientClick="return Req_Validation();" />
                                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>

        </div>
    </div>
    
          <div class="row" id="ddl_id" runat="server">
            <div class="col-sm-2 col-xs-12">
               <br />
               <b> Client Name :</b>
               <asp:DropDownList ID="ddl_client_name" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_client_name_SelectedIndexChanged" >
                    <asp:ListItem Value="Select">Select</asp:ListItem>
               </asp:DropDownList>
            </div>
            <div class="col-sm-2 col-xs-12">
               <br />
               <b> State Name :</b>
               <asp:DropDownList ID="ddl_state" class="form-control" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" >
                  <asp:ListItem Value="Select">Select</asp:ListItem>
               </asp:DropDownList>
            </div>
            <div class="col-sm-2 col-xs-12">
               <br />
               <b> Branch Name :</b>
               <asp:DropDownList ID="ddl_unit" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_unit_SelectedIndexChanged" >
                  <asp:ListItem Value="Select">Select</asp:ListItem>
               </asp:DropDownList>
            </div>
         
         </div>
        <div class="wthree_agile_admin_info" id="temp_hide" runat="server">
            <div class="clearfix"></div>
            <div class="inner_content">
               <div class="inner_content_w3_agile_info">
                  <div class="agile_top_w3_grids">
                     <ul class="ca-menu">
                         <div class="row text-center" style="margin-left:20px;">
                        <li style="background-image:url(dashboard/images/download1.jpg);background-repeat:no-repeat">
                           <a data-toggle="modal" href="#total_link">
                              <i class="fa fa-building-o" aria-hidden="true"></i>
                              <div class="ca-content">
                                 <h4 class="ca-main"><%=total_emp1%></h4>
                                 <h3 class="ca-sub">Total Employee</h3>
                              </div>
                           </a>
                        </li>
                        <li style="background-image:url(dashboard/images/download2.jpg);background-repeat:no-repeat">
                           <a data-toggle="modal" href="#present_link">
                              <i class="fa fa-users" aria-hidden="true"></i>
                              <div class="ca-content">
                                 <h4 class="ca-main one"><%=Present_emp%></h4>
                                 <h3 class="ca-sub one">Present Employee</h3>
                              </div>
                           </a>
                        </li>
                        <li style="background-image:url(dashboard/images/images2.jpg);background-repeat:no-repeat">
                           <a data-toggle="modal" href="#absent_link">
                              <i class="fa fa-database" aria-hidden="true"></i>
                              <div class="ca-content">
                                 <h4 class="ca-main two"><%=Absent_emp1%></h4>
                                 <h3 class="ca-sub two">Absent Employee</h3>
                              </div>
                           </a>
                        </li>
                             </div>
                         <br />
                         <div class="row text-center" style="margin-left:20px;">
                          <li style="background-image:url(dashboard/images/download8.jpg);background-repeat:no-repeat">
                           <a data-toggle="modal" href="#birth_emp">
                              <i class="fa fa-birthday-cake" aria-hidden="true"></i>
                              <div class="ca-content">
                                 <h4 class="ca-main three"><%=birth_emp%></h4>
                                 <h3 class="ca-sub three">Birthday Notification</h3>
                              </div>
                           </a>
                        </li>
                         <li style="background-image:url(dashboard/images/download11.jpg);background-repeat:no-repeat">
                           <a data-toggle="modal" href="#emp_profile">
                              <i class="fa fa-user-plus" aria-hidden="true"></i>
                              <div class="ca-content">
                                 <h4 class="ca-main four"><%=emp_profile%></h4>
                                 <h3 class="ca-sub four"> Employee Profile</h3>
                              </div>
                           </a>
                        </li>
                          <li style="background-image:url(dashboard/images/download6.jpg);background-repeat:no-repeat">
                           <a data-toggle="modal" href="#emp_reliver">
                              <i class="fa fa-user-secret" aria-hidden="true"></i>
                              <div class="ca-content">
                                 <h4 class="ca-main four"><%=emp_reliver%></h4>
                                 <h3 class="ca-sub four"> Employee Reliver</h3>
                              </div>
                           </a>
                        </li>
                             </div>
                        <div class="clearfix"></div>
                     </ul>
                  </div>
               </div>
            </div>
         </div>
        <div class="wthree_agile_admin_info" runat="server" id="chart_show">
            <div class="clearfix"></div>
            <div class="inner_content">
               <div class="inner_content_w3_agile_info">
                  <div class="agile_top_w3_grids">
                     <ul class="chart-menu">
                        <li>
       
          <asp:Panel ID="panel2" runat="server">
                           <%--  <div class="container-fluid">--%>
                                  <div class="row">
                                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
                                   <asp:Label ID="lbl1" runat="server"><b>Total : <img alt="" height="15"  src="Images/Capture1.png" width="15" /></b></asp:Label>
                                   &nbsp;&nbsp;  
                                   <asp:Label ID="Label1" runat="server"><b>Active : <img alt="" height="15"  src="Images/Capture2.png" width="15" /></b></asp:Label>
                                   &nbsp; &nbsp; 
                                   <asp:Label ID="Label2" runat="server"><b>Left :  <img alt="" height="15"  src="Images/Capture3.png" width="15" /></b></asp:Label>
                                </div>
   <asp:chart id="Chart1" runat="server" Height="300px" Width="400px">
                                   <titles>
                                      <asp:Title ShadowOffset="3" Name="Title1" />
                                   </titles>
                                   <legends>
                                      <asp:Legend Alignment="Center" Docking="Bottom"
                                         IsTextAutoFit="False" Name="Default"
                                         LegendStyle="Row" />
                                   </legends>
                                   <series>
                                      <asp:Series Name="chart" />
                                   </series>
                                   <chartareas>
                                      <asp:ChartArea Name="ChartArea1"
                                         BorderWidth="0"  />
                                   </chartareas>
                                </asp:chart>
       
                                <%-- </div>--%>
               <h3 class="ca-sub">Total Employee Strength</h3>
              </asp:Panel>
              </li>
                        <li>
          <asp:Panel ID="panel3" runat="server">
                            <%-- <div class="container-fluid">--%>
                                 <div class="row">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
                                    <asp:Label ID="Label3" runat="server"><b>Total : <img alt="" height="15"  src="Images/Capture1.png" width="15" /></b></asp:Label>
                                    &nbsp;&nbsp;  
                                    <asp:Label ID="Label4" runat="server"><b>Present : <img alt="" height="15"  src="Images/Capture2.png" width="15" /></b></asp:Label>
                                    &nbsp; &nbsp; 
                                    <asp:Label ID="Label5" runat="server"><b>Absent :  <img alt="" height="15"  src="Images/Capture3.png" width="15" /></b></asp:Label>
                                    &nbsp; &nbsp; 
                                </div>
   <asp:chart id="Chart2" runat="server" Height="300px" Width="400px">
                                   <titles>
                                      <asp:Title ShadowOffset="3" Name="Title1" />
                                   </titles>
                                   <legends>
                                      <asp:Legend Alignment="Center" Docking="Bottom"
                                         IsTextAutoFit="False" Name="Default"
                                         LegendStyle="Row" />
                                   </legends>
                                   <series>
                                      <asp:Series Name="chart1" />
                                   </series>
                                   <chartareas>
                                      <asp:ChartArea Name="ChartArea2"
                                         BorderWidth="0"  />
                                   </chartareas>
                                </asp:chart>
       <%-- </div>--%>
                     <h3 class="ca-sub two">Todays Employee Attendance</h3>            
              </asp:Panel>
            </li>
                        <div class="clearfix"></div>
                     </ul>
                  </div>
               </div>
            </div>
         </div>
           
           <div class="modal fade" id="total_link" role="dialog" data-dismiss="modal">
      <div class="modal-dialog">
         <div class="modal-content" >
            <div class="modal-header">
               <button type="button" class="close" data-dismiss="modal">&times;</button>
               <h4 class="modal-title"> Total Employee </h4>
            </div>
            <div class="modal-body">
               <div class="row">
                  <div class="col-sm-12" >
                     <asp:Panel ID="Panel123" runat="server" CssClass="grid-view" Style="max-height:300px;overflow-y:auto">
                        <asp:GridView ID="gv_total_links" class="table"  Width="100%" HeaderStyle-CssClass="FixedHeader" runat="server" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                           <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                           <EditRowStyle BackColor="#999999" />
                           <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                           <HeaderStyle BackColor="#ab0dc8" Font-Bold="True" ForeColor="White" Width="50" />
                           <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                           <RowStyle BackColor="#ffffff" ForeColor="#333333" />
                           <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                           <SortedAscendingCellStyle BackColor="#E9E7E2" />
                           <SortedAscendingHeaderStyle BackColor="#506C8C" />
                           <SortedDescendingCellStyle BackColor="#FFFDF8" />
                           <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                           <Columns>
                            
                             
                             
                                 <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />
                              <asp:BoundField DataField="EMP_MOBILE_NO" HeaderText="EMP MOBILE NO" SortExpression="EMP_MOBILE_NO" />
                           </Columns>
                        </asp:GridView>
                     </asp:Panel>
                  </div>
               </div>
               <div class="modal-footer">
                  <div class="row text-center">
                     <div class="col-sm-4">
                     </div>
                     <div class="col-sm-4">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">CLOSE</button>
                     </div>
                     <div class="col-sm-4">
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </div>
   </div>
               <div class="modal fade" id="present_link" role="dialog" data-dismiss="modal">
      <div class="modal-dialog">
         <div class="modal-content" >
            <div class="modal-header">
               <button type="button" class="close" data-dismiss="modal">&times;</button>
               <h4 class="modal-title">Todays Present Employee </h4>
            </div>
            <div class="modal-body">
               <div class="row">
                  <div class="col-sm-12" >
                     <asp:Panel ID="Panel5" runat="server" CssClass="grid-view" Style="max-height:300px;overflow-y:auto">
                        <asp:GridView ID="gv_present_link" class="table"  Width="100%" HeaderStyle-CssClass="FixedHeader" runat="server" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                           <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                           <EditRowStyle BackColor="#999999" />
                           <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                           <HeaderStyle BackColor="#ab0dc8" Font-Bold="True" ForeColor="White" Width="50" />
                           <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                           <RowStyle BackColor="#ffffff" ForeColor="#333333" />
                           <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                           <SortedAscendingCellStyle BackColor="#E9E7E2" />
                           <SortedAscendingHeaderStyle BackColor="#506C8C" />
                           <SortedDescendingCellStyle BackColor="#FFFDF8" />
                           <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                           <Columns>
                             
                                 <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />
                              <asp:BoundField DataField="EMP_MOBILE_NO" HeaderText="EMP MOBILE NO" SortExpression="EMP_MOBILE_NO" />
                           </Columns>
                        </asp:GridView>
                     </asp:Panel>
                  </div>
               </div>
               <div class="modal-footer">
                  <div class="row text-center">
                     <div class="col-sm-4">
                     </div>
                     <div class="col-sm-4">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">CLOSE</button>
                     </div>
                     <div class="col-sm-4">
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </div>
   </div>
   <div class="modal fade" id="absent_link" role="dialog" data-dismiss="modal">
      <div class="modal-dialog">
         <div class="modal-content" >
            <div class="modal-header">
               <button type="button" class="close" data-dismiss="modal">&times;</button>
               <h4 class="modal-title">Todays Absent Employee </h4>
            </div>
            <div class="modal-body">
               <div class="row">
                  <div class="col-sm-12" >
                     <asp:Panel ID="Panel6" runat="server" CssClass="grid-view" Style="max-height:300px;overflow-y:auto">
                        <asp:GridView ID="gv_absent_link" class="table"  Width="100%" HeaderStyle-CssClass="FixedHeader" runat="server" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                           <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                           <EditRowStyle BackColor="#999999" />
                           <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                           <HeaderStyle BackColor="#ab0dc8" Font-Bold="True" ForeColor="White" Width="50" />
                           <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                           <RowStyle BackColor="#ffffff" ForeColor="#333333" />
                           <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                           <SortedAscendingCellStyle BackColor="#E9E7E2" />
                           <SortedAscendingHeaderStyle BackColor="#506C8C" />
                           <SortedDescendingCellStyle BackColor="#FFFDF8" />
                           <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                           <Columns>
                            
                                 <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />
                              <asp:BoundField DataField="EMP_MOBILE_NO" HeaderText="EMP MOBILE NO" SortExpression="EMP_MOBILE_NO" />
                           </Columns>
                        </asp:GridView>
                     </asp:Panel>
                  </div>
               </div>
               <div class="modal-footer">
                  <div class="row text-center">
                     <div class="col-sm-4">
                     </div>
                     <div class="col-sm-4">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">CLOSE</button>
                     </div>
                     <div class="col-sm-4">
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </div>
   </div>                   
               <div class="modal fade" id="birth_emp" role="dialog" data-dismiss="modal">
      <div class="modal-dialog">
         <div class="modal-content" >
            <div class="modal-header">
               <button type="button" class="close" data-dismiss="modal">&times;</button>
               <h4 class="modal-title">Todays Birthday </h4>
            </div>
            <div class="modal-body">
               <div class="row">
                  <div class="col-sm-12" >
                     <asp:Panel ID="Panel7" runat="server" CssClass="grid-view" Style="max-height:300px;overflow-y:auto">
                        <asp:GridView ID="gv_birthday_link" class="table"  Width="100%" HeaderStyle-CssClass="FixedHeader" runat="server" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                           <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                           <EditRowStyle BackColor="#999999" />
                           <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                           <HeaderStyle BackColor="#ab0dc8" Font-Bold="True" ForeColor="White" Width="50" />
                           <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                           <RowStyle BackColor="#ffffff" ForeColor="#333333" />
                           <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                           <SortedAscendingCellStyle BackColor="#E9E7E2" />
                           <SortedAscendingHeaderStyle BackColor="#506C8C" />
                           <SortedDescendingCellStyle BackColor="#FFFDF8" />
                           <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                           <Columns>
                            
                                 <asp:BoundField DataField="EMP_NAME" HeaderText="Employee Name" SortExpression="EMP_NAME" />
                                <asp:BoundField DataField="BIRTH_DATE" HeaderText="Birth Date" SortExpression="BIRTH_DATE" />
                              <asp:BoundField DataField="EMP_MOBILE_NO" HeaderText="EMP MOBILE NO" SortExpression="EMP_MOBILE_NO" />
                           </Columns>
                        </asp:GridView>
                     </asp:Panel>
                  </div>
               </div>
               <div class="modal-footer">
                  <div class="row text-center">
                     <div class="col-sm-4">
                     </div>
                     <div class="col-sm-4">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">CLOSE</button>
                     </div>
                     <div class="col-sm-4">
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </div>
   </div>                           
                           <div class="modal fade" id="emp_profile" role="dialog" data-dismiss="modal">
      <div class="modal-dialog">
         <div class="modal-content" >
            <div class="modal-header">
               <button type="button" class="close" data-dismiss="modal">&times;</button>
               <h4 class="modal-title"> Employee Profile </h4>
            </div>
            <div class="modal-body">
               <div class="row">
                  <div class="col-sm-12" >
                     <asp:Panel ID="Panel8" runat="server" CssClass="grid-view" Style="max-height:300px;overflow-y:auto">
                        <asp:GridView ID="gv_emp_pro" class="table"  Width="100%" HeaderStyle-CssClass="FixedHeader" runat="server" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                           <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                           <EditRowStyle BackColor="#999999" />
                           <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                           <HeaderStyle BackColor="#ab0dc8" Font-Bold="True" ForeColor="White" Width="50" />
                           <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                           <RowStyle BackColor="#ffffff" ForeColor="#333333" />
                           <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                           <SortedAscendingCellStyle BackColor="#E9E7E2" />
                           <SortedAscendingHeaderStyle BackColor="#506C8C" />
                           <SortedDescendingCellStyle BackColor="#FFFDF8" />
                           <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                           <Columns>
                            
                                 <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />
                              <asp:BoundField DataField="EMP_MOBILE_NO" HeaderText="EMP MOBILE NO" SortExpression="EMP_MOBILE_NO" />
                           </Columns>
                        </asp:GridView>
                     </asp:Panel>
                  </div>
               </div>
               <div class="modal-footer">
                  <div class="row text-center">
                     <div class="col-sm-4">
                     </div>
                     <div class="col-sm-4">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">CLOSE</button>
                     </div>
                     <div class="col-sm-4">
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </div>
   </div>         
          <div class="modal fade" id="emp_reliver" role="dialog" data-dismiss="modal">
      <div class="modal-dialog">
         <div class="modal-content" >
            <div class="modal-header">
               <button type="button" class="close" data-dismiss="modal">&times;</button>
               <h4 class="modal-title"> Reliver Employee </h4>
            </div>
            <div class="modal-body">
               <div class="row">
                  <div class="col-sm-12" >
                     <asp:Panel ID="Panel9" runat="server" CssClass="grid-view" Style="max-height:300px;overflow-y:auto">
                        <asp:GridView ID="gv_reliver" class="table"  Width="100%" HeaderStyle-CssClass="FixedHeader" runat="server" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                           <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                           <EditRowStyle BackColor="#999999" />
                           <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                           <HeaderStyle BackColor="#ab0dc8" Font-Bold="True" ForeColor="White" Width="50" />
                           <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                           <RowStyle BackColor="#ffffff" ForeColor="#333333" />
                           <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                           <SortedAscendingCellStyle BackColor="#E9E7E2" />
                           <SortedAscendingHeaderStyle BackColor="#506C8C" />
                           <SortedDescendingCellStyle BackColor="#FFFDF8" />
                           <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                           <Columns>
                            
                                 <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />
                              <asp:BoundField DataField="EMP_MOBILE_NO" HeaderText="EMP MOBILE NO" SortExpression="EMP_MOBILE_NO" />
                           </Columns>
                        </asp:GridView>
                     </asp:Panel>
                  </div>
               </div>
               <div class="modal-footer">
                  <div class="row text-center">
                     <div class="col-sm-4">
                     </div>
                     <div class="col-sm-4">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">CLOSE</button>
                     </div>
                     <div class="col-sm-4">
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </div>
   </div>   
        <br />
         <div class="container-fluid" id="hide_temp" runat="server">
        <div class="row dash">
            <div class="col-sm-4 col-xs-12">
                <div class="box1">
                    <div class="top_box1">
                        <div class="row">
                            <div class="col-sm-3 col-xs-12">
                                <br />
                                <span class="glyphicon glyphicon-hand-up glyap1 text-left"></span>
                            </div>
                            <div class="col-sm-9 col-xs-12">
                                <div class="text1 text-right">In Time : <b>Not Logged In</b></div>
                                <div class="text1 text-right">Duration: </div>
                                <br />
                                <div class="text2 text-right">Check-In/Out</div>
                            </div>
                        </div>
                    </div>
                    <div class="end_box1">
                        <div class="row">
                            <div class="col-sm-4">View Details</div>
                            <div class="col-sm-6"></div>
                            <div class="col-sm-2 text-right"><span class="glyphicon glyphicon-circle-arrow-right glyap2"></span></div>

                        </div>
                    </div>
                </div>
            </div>
            <%-- pending task--%>
            <div class="col-sm-4 col-xs-12">
                <div class="box2">
                    <div class="top_box2">
                        <div class="row">
                            <div class="col-sm-3 col-xs-12">
                                <br />
                                <span class="glyphicon glyphicon-tasks glyap1 text-left"></span>
                            </div>
                            <div class="col-sm-9 col-xs-12">
                                <div class="text1 text-right">Your Pending Task: <b>9</b></div>
                                <div class="text1 text-right"></div>
                                <br />
                                <br />
                                <div class="text2 text-right">Task</div>
                            </div>
                        </div>
                    </div>
                    <div class="end_box2">
                        <%----- vikas-----%>
                        <div class="row">
                            <div class="col-sm-4"><a data-toggle="modal" href="#attendance">View Details</a></div>
                            <div class="modal fade" id="attendance" role="dialog">
                                <div class="modal-dialog" id="home_dialog" style="width: 58%">
                                    <div class="modal-content" style="width: 811px; color: black;">
                                        <br />
                                        <br />
                                        <h1 style="padding-left: 27%;">Your Pending Task......</h1>
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>

                                        </div>
                                        <div class="modal-body">
                                            <div class="row">
                                                <%--   client--%>
                                                <div class="container">

                                                    <asp:Panel ID="pnl_client" runat="server" Height="300px" Style="overflow-y: auto; width: 64%; overflow: hidden">
                                                        <h3>Client</h3>
                                                        <asp:GridView ID="grd_client_pending" class="table" runat="server" Width="100%" ForeColor="#333333" ShowHeaderWhenEmpty="True" OnPreRender="grd_client_pending_PreRender">
                                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                            <RowStyle BackColor="#EFF3FB" />
                                                        </asp:GridView>

                                                    </asp:Panel>

                                                </div>
                                                <br />
                                                <br />
                                                <!---- branch location----->
                                                <div class="container">

                                                    <asp:Panel ID="pnl_brach" runat="server" Height="300px" Style="overflow: hidden; overflow-y: auto; width: 64%">
                                                        <h3 id="brach_loction_h3">Branch Location</h3>
                                                        <asp:GridView ID="location_branch_pending_home" class="table" runat="server" BackColor="White"
                                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                            AutoGenerateColumns="False" OnRowDataBound="location_branch_pending_home_RowDataBound" OnPreRender="location_branch_pending_home_PreRender">

                                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                            <RowStyle BackColor="#EFF3FB" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                            <SortedDescendingHeaderStyle BackColor="#00547E" />

                                                            <Columns>

                                                                <asp:BoundField DataField="client_name" HeaderText="CLIENT NAME" SortExpression="client_name" />
                                                                <asp:BoundField DataField="UNIT_NAME" HeaderText="BRANCH NAME" SortExpression="UNIT_NAME" />
                                                                <asp:BoundField DataField="UNIT_CITY" HeaderText="BRANCH CITY" SortExpression="UNIT_CITY" />
                                                                <%--<asp:BoundField DataField="DESIGNATION" HeaderText="Designation" SortExpression="DESIGNATION" />--%>
                                                                <asp:BoundField DataField="Client_branch_code" HeaderText="CLIENT BRANCH CODE" SortExpression="Client_branch_code" />
                                                                <asp:BoundField DataField="OPus_NO" HeaderText="OPUS CODE" SortExpression="OPus_NO" />
                                                                <asp:BoundField DataField="txt_zone" HeaderText="ZONE" SortExpression="`txt_zone`" />
                                                                <asp:BoundField DataField="ZONE" HeaderText="REGION NAME" SortExpression="`ZONE`" />
                                                                <asp:BoundField DataField="UNIT_EMAIL_ID" HeaderText="BRANCH_EMAIL_ID" SortExpression="UNIT_EMAIL_ID" />

                                                            </Columns>

                                                        </asp:GridView>
                                                    </asp:Panel>

                                                </div>
                                                <br />
                                                <br />
                                                <%-- employee --%>
                                                <div class="container">

                                                    <asp:Panel ID="pnl_employee" runat="server" Height="300px" Style="overflow-x: auto; overflow-y: auto; width: 64%">
                                                        <h3>Employee</h3>
                                                        <asp:GridView ID="employee_grd_home" class="table" runat="server" BackColor="White"
                                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                            AutoGenerateColumns="False" Width="10%" OnRowDataBound="employee_grd_home_RowDataBound" OnPreRender="employee_grd_home_PreRender">

                                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                            <RowStyle BackColor="#EFF3FB" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                            <Columns>


                                                                <%--                                        <asp:TemplateField HeaderText="Sr.No">
                                       <ItemTemplate>
                                            <asp:Label ID="lbl_GRADE_CODE" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                        </ItemTemplate>
                                </asp:TemplateField>--%>
                                                                <asp:BoundField DataField="client_name" HeaderText="CLIENT NAME" SortExpression="client_name" />
                                                                <asp:BoundField DataField="UNIT_NAME" HeaderText="BRANCH NAME" SortExpression="UNIT_NAME" />
                                                                <asp:BoundField DataField="emp_name" HeaderText="NAME" SortExpression="emp_name" />
                                                                <asp:BoundField DataField="EMP_MOBILE_NO" HeaderText="MOB_NO" SortExpression="EMP_MOBILE_NO" />
                                                                <asp:BoundField DataField="original_bank_account_no" HeaderText="ORIGNAL_A/C_NO" SortExpression="original_bank_account_no" />
                                                                <asp:BoundField DataField="PF_NOMINEE_RELATION" HeaderText="NOMINEE_NAME" SortExpression="PF_NOMINEE_RELATION" />
                                                                <asp:BoundField DataField="PF_NOMINEE_NAME" HeaderText="NOMINEE_RELATION" SortExpression="PF_NOMINEE_NAME" />

                                                                <asp:BoundField DataField="original_photo" HeaderText="ORIGINAL_PHOTO" SortExpression="original_photo" />
                                                                <asp:BoundField DataField="original_adhar_card" HeaderText="ORIGINAL_ADHAR_CARD" SortExpression="original_adhar_card" />
                                                                <asp:BoundField DataField="original_policy_document" HeaderText="ORIGINAL_POLICE_VERIFICATION " SortExpression="original_policy_document" />
                                                                <asp:BoundField DataField="original_address_proof" HeaderText="ORIGINAL_PROOF_OF_ADDRESS" SortExpression="original_address_proof" />
                                                                <asp:BoundField DataField="bank_passbook" HeaderText="BANK_PASSBOOK" SortExpression="bank_passbook" />
                                                                <asp:BoundField DataField="emp_signature" HeaderText="EMPLOYEE_SIGNATURE" SortExpression="emp_signature" />
                                                                <asp:BoundField DataField="UNIFORM" HeaderText="UNIFORM" SortExpression="UNIFORM" />
                                                                <asp:BoundField DataField="ID CARD" HeaderText="ID_CARD" SortExpression="ID CARD" />
                                                                <asp:BoundField DataField="SWEATER" HeaderText="SWEATER" SortExpression="SWEATER" />


                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>

                                                </div>
                                                <br />
                                                <br />
                                                <%--employee compliances--%>
                                                <div class="container">

                                                    <asp:Panel ID="pnl_emp_compliances" runat="server" Height="300px" Style="overflow-x: auto; overflow-y: auto; width: 64%">
                                                        <h3>Employee Compliances</h3>
                                                        <asp:GridView ID="Grid_compliances_pupop" class="table" runat="server" BackColor="black"
                                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                            AutoGenerateColumns="False" Width="10%" OnRowDataBound="Grid_compliances_pupop_RowDataBound" OnPreRender="Grid_compliances_pupop_PreRender">

                                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                            <RowStyle BackColor="#EFF3FB" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                            <%-- <FooterStyle BackColor="White" ForeColor="#000066" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#00547E" />--%>
                                                            <Columns>


                                                                <%--                                        <asp:TemplateField HeaderText="Sr.No">
                                       <ItemTemplate>
                                            <asp:Label ID="lbl_GRADE_CODE" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                        </ItemTemplate>
                                </asp:TemplateField>--%>
                                                                <asp:BoundField DataField="client_name" HeaderText="CLIENT NAME" SortExpression="client_name" />
                                                                <asp:BoundField DataField="UNIT_NAME" HeaderText="BRANCH NAME" SortExpression="UNIT_NAME" />
                                                                <asp:BoundField DataField="emp_name" HeaderText="NAME" SortExpression="emp_name" />
                                                                <%--<asp:BoundField DataField="" HeaderText="UAN NUMBER" SortExpression="" />--%>
                                                                <asp:BoundField DataField="EMP_NEW_PAN_NO" HeaderText="PAN_NUM" SortExpression="EMP_NEW_PAN_NO" />
                                                                <asp:BoundField DataField="ESIC_NUMBER" HeaderText="ESIC_NUM" SortExpression="ESIC_NUMBER" />
                                                                <asp:BoundField DataField="PF_NUMBER" HeaderText="PF_NUM" SortExpression="PF_NUMBER" />

                                                                <asp:BoundField DataField="BANK_HOLDER_NAME" HeaderText="A/C_HOLDER_NAME" SortExpression="BANK_HOLDER_NAME" />
                                                                <asp:BoundField DataField="original_bank_account_no" HeaderText=" BANK_A/C_NUM" SortExpression="original_bank_account_no" />
                                                                <asp:BoundField DataField="PF_IFSC_CODE" HeaderText="BANK_IFSC_CODE" SortExpression="PF_IFSC_CODE" />
                                                                <asp:BoundField DataField="cca" HeaderText="CCA" SortExpression="cca" />
                                                                <asp:BoundField DataField="gratuity" HeaderText="GRATUITY" SortExpression="gratuity" />
                                                                <asp:BoundField DataField="special_allow" HeaderText="SPECIAL_ALLOWANCE" SortExpression="special_allow" />



                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </div>
                                            </div>




                                        </div>
                                        <br />
                                        <br />
                                        <br />

                                        <div class="modal-footer">
                                            <div class="row text-center">
                                                <div class="col-sm-4">
                                                </div>
                                                <div class="col-sm-4">
                                                    <button type="button" class="btn btn-danger" data-dismiss="modal">CLOSE</button>
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6"></div>
                            <div class="col-sm-2 text-right"><span class="glyphicon glyphicon-circle-arrow-right glyap2"></span></div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-4 col-xs-12">
                <div class="box3">
                    <div class="top_box3">
                        <div class="row">
                            <div class="col-sm-3 col-xs-12">
                                <br />
                                <span class="glyphicon glyphicon-bed glyap1 text-left"></span>
                            </div>
                            <div class="col-sm-9 col-xs-12">
                                <div class="text1 text-right">Applied Date: <b>04/12/2015</b></div>
                                <div class="text1 text-right">Leave Status:<b>Request</b></div>
                                <br />
                                <div class="text2 text-right">Leave Applied</div>
                            </div>
                        </div>
                    </div>
                    <div class="end_box3">
                        <div class="row">
                            <div class="col-sm-4">View Details</div>
                            <div class="col-sm-6"></div>
                            <div class="col-sm-2 text-right"><span class="glyphicon glyphicon-circle-arrow-right glyap2"></span></div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
       
        <div class="row dash">
            <div class="col-sm-4 col-xs-12">
                <div class="box4">
                    <div class="top_box4">
                        <div class="row">
                            <div class="col-sm-3 col-xs-12">
                                <br />
                                <span class="glyphicon glyphicon-user glyap1 text-left"></span>
                            </div>
                            <div class="col-sm-9 col-xs-12">
                                <div class="text1 text-right">EmailID : <b>emp1@gmail.com</b></div>
                                <div class="text1 text-right">Contct No:9565443242 </div>
                                <br />
                                <div class="text2 text-right">Employee Profile</div>
                            </div>
                        </div>
                    </div>
                    <div class="end_box4">
                        <div class="row">
                            <div class="col-sm-4">View Details</div>
                            <div class="col-sm-6"></div>
                            <div class="col-sm-2 text-right"><span class="glyphicon glyphicon-circle-arrow-right glyap2"></span></div>

                        </div>
                    </div>
                </div>
            </div>
          <div class="col-sm-4 col-xs-12">
                <div class="box5">
                    <div class="top_box5">
                        <div class="row">
                            <div class="col-sm-3 col-xs-12">
                                <br />
                                <span class="glyphicon glyphicon-envelope glyap1 text-left"></span>
                            </div>
                            <div class="col-sm-9 col-xs-12">
                                <div class="text1 text-right">Current Salary : <b>16234.34</b></div>
                                <div class="text1 text-right"></div>
                                <br />
                                <br />
                                <div class="text2 text-right">Payslip</div>
                            </div>
                        </div>
                    </div>
                    <div class="end_box5">
                        <div class="row">
                            <div class="col-sm-4">View Details</div>
                            <div class="col-sm-6"></div>
                            <div class="col-sm-2 text-right"><span class="glyphicon glyphicon-circle-arrow-right glyap2"></span></div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-4 col-xs-12">
                <div class="box6">
                    <div class="top_box6">
                        <div class="row">
                            <div class="col-sm-3 col-xs-12">
                                <br />
                                <span class="glyphicon glyphicon-bullhorn glyap1 text-left"></span>
                            </div>
                            <div class="col-sm-9 col-xs-12">
                                <div class="text1 text-right">Message : <b>Testing issues...</b></div>
                                <div class="text1 text-right"></div>
                                <br />
                                <br />
                                <div class="text2 text-right">Admin Message</div>
                            </div>
                        </div>
                    </div>
                    <div class="end_box6">
                        <div class="row">
                            <div class="col-sm-4">View Details</div>
                            <div class="col-sm-6"></div>
                            <div class="col-sm-2 text-right"><span class="glyphicon glyphicon-circle-arrow-right glyap2"></span></div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row dash">
            <div class="col-sm-4 col-xs-12">
                <div class="box7">
                    <div class="top_box7">
                        <div class="row">
                            <div class="col-sm-3 col-xs-12">
                                <br />
                                <span class="glyphicon glyphicon-calendar glyap1 text-left"></span>
                            </div>
                            <div class="col-sm-9 col-xs-12">
                                <div class="text1 text-right"></div>
                                <br />
                                <div class="text1 text-right"></div>
                                <br />
                                <br />
                                <div class="text2 text-right">Holidays calender</div>
                            </div>
                        </div>
                    </div>
                    <div class="end_box7">
                        <div class="row">
                            <div class="col-sm-4">View Details</div>
                            <div class="col-sm-6"></div>
                            <div class="col-sm-2 text-right"><span class="glyphicon glyphicon-circle-arrow-right glyap2"></span></div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-4 col-xs-12">
                <div class="box8">
                    <div class="top_box8">
                        <div class="row">
                            <div class="col-sm-3 col-xs-12">
                                <br />
                                <span class="glyphicon glyphicon-gift glyap1 text-left"></span>
                            </div>
                            <div class="col-sm-9 col-xs-12">
                                <div class="text1 text-right">No Birthdays Notified Till Now : <b></b></div>
                                <div class="text1 text-right"></div>
                                <br />
                                <br />
                                <div class="text2 text-right">Birthday Notification</div>
                            </div>
                        </div>
                    </div>
                    <div class="end_box8">
                        <div class="row">
                            <div class="col-sm-4">View Details</div>
                            <div class="col-sm-6"></div>
                            <div class="col-sm-2 text-right"><span class="glyphicon glyphicon-circle-arrow-right glyap2"></span></div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-4 col-xs-12">
                <div class="box9">
                    <div class="top_box9">
                        <div class="row">
                            <div class="col-sm-3 col-xs-12">
                                <br />
                                <span class="glyphicon glyphicon-comment glyap1 text-left"></span>
                            </div>
                            <div class="col-sm-9 col-xs-12">
                                <div class="text1 text-right">Message : <b></b></div>
                                <div class="text1 text-right"></div>
                                <br />
                                <br />
                                <div class="text2 text-right">Comments</div>
                            </div>
                        </div>
                    </div>
                    <div class="end_box9">
                        <div class="row">
                            <div class="col-sm-4">View Details</div>
                            <div class="col-sm-6"></div>
                            <div class="col-sm-2 text-right"><span class="glyphicon glyphicon-circle-arrow-right glyap2"></span></div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>
        
        <div class="row">
            <div class="col-sm-9 col-xs-12">
                <%-- <div id="chartContainer" style="height: 370px; width: 100%;"></div>--%>
            </div>
            <div class="col-sm-3 col-xs-12">
            </div>



        </div>

    </div>
</asp:Content>

