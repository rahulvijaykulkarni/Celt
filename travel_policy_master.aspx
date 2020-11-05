<%@ Page Title="Travel Policy Master" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="travel_policy_master.aspx.cs" Inherits="InvestmentDeclaration" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    <script src="js/tab.js"></script>
    
    <link href="css/new_stylesheet.css" rel="stylesheet" />
    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .grid-view {
            height: auto;
            max-height: 500px;
            overflow: scroll;
        }
    </style>
    <style>
        .row {
            margin-right: -15px;
            margin-left: -15px;
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


        .panel-disp1, .panel-disp2, .panel-disp3, .panel-disp4, .panel-disp5 .panel-disp6 {
            display: none;
        }
        /*vikas*/
    </style>
    <script>
        $(document).ready(function () {

            $(".check_text1").hide();
            $(".check_text2").hide();

            $(".TextBox_ac").hide();
            $(".TextBox_nonac").hide();
            $(".TextBox_citybus").hide();
            mode_allowed();


            $(".hotel_budget").hide();
            $(".hotel_standard").hide();
            $(".hotel_twostar").hide();
            $(".hotel_threestar").hide();
            $(".hotel_fivestar").hide();
            hotel_accommodation();

            $(".CheckBox_breakfast").hide();
            $(".TextBox_breakfast").hide();
            $(".fooding_breakfast").hide();
            $(".text_breakfast").hide();
            $(".CheckBox_lunch").hide();
            $(".TextBox_lunch").hide();
            $(".fooding_lunch").hide();
            $(".text_lunch").hide();
            $(".CheckBox_dinner").hide();
            $(".TextBox_dinner").hide();
            fooding();


            $(".CheckBox_inside").hide();
            $(".txt_per_day_limit").hide();
            $(".CheckBox_outside").hide();
            $(".outside_city").hide();
            $(".expenses_allowed_inside").hide();
            $(".expenses_allowed_outside").hide();
            expenses_allowed();

            //check_local_convey();
            //check_own_vehicle();
            //check_local_convey();
            //check_approve();
            //check_escation();

        });

    </script>
  <%--  vikas13/11 menu highlight--%>
     <script>
         $(".btn-pref btn-group btn-group-justified btn-group-lg")
         $("#stars").button({
             icons: {
                 primary: "ui-icon-file-image"
             },
             text: false
         });
         $("#favorites").button({
             icons: {
                 primary: "ui-icon-play"
             },
             text: false
         });
         $("#following").button({
             icons: {
                 primary: "ui-icon-locked"
             },
             text: false
         });

         $("#Button1").button({
             icons: {
                 primary: "ui-icon-locked"
             },
             text: false
         });
         $("#Button2").button({
             icons: {
                 primary: "ui-icon-locked"
             },
             text: false
         });

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">
    
    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }
        function pageLoad() {
            //$(".date-picker1").val("");
            // $(".date-picker2").val("");

            $(".date-picker1").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                yearRange: "2016:+2",//vikas 14/11
                //  minDate:0,
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

            check_own_vehicle();
            check_local_convey();
            check_approve();
            check_escation();

            $(".date-picker1").attr("readonly", "true");
            $(".date-picker2").attr("readonly", "true");

        }

        $(document).ready(function () {

            var evt = null;


        });

        var e = null;
        AllowAlphabet(e);
        var evt = null;
        isNumber(evt);


        function isNumber(evt) {
            //alert("hello B");
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
        function expenses_allowed() {

            var expenses_inside_outside = document.getElementById('<%=chk_expenses_allowed.ClientID %>');

            if ($(expenses_inside_outside).is(":checked")) {
                $(".CheckBox_inside").show();
                $(".CheckBox_outside").show();
                $(".expenses_allowed_inside").show();
                $(".expenses_allowed_outside").show();

            } else {
                $(".CheckBox_inside").hide();
                $(".CheckBox_outside").hide();
                $(".expenses_allowed_inside").hide();
                $(".expenses_allowed_outside").hide();
            }
            var check_inside_city = document.getElementById('<%=CheckBox_inside.ClientID %>');

            //check inside city
            if ($(check_inside_city).is(":checked")) {
                $(".txt_per_day_limit").show();
            }
            else {
                $(".txt_per_day_limit").hide();
            }
            var check_outside_city = document.getElementById('<%=CheckBox_outside.ClientID %>');

            //check inside city
            if ($(check_outside_city).is(":checked")) {
                $(".outside_city").show();
            }
            else {
                $(".outside_city").hide();
            }

        }


        function check_own_vehicle() {

            var checkown_vehicle = document.getElementById('<%=chkownedvehicle.ClientID %>');

            //check own vehicle
            if ($(checkown_vehicle).is(":checked")) {
                //$(".check_text1").show(); 
                $(".CheckBox_car").show();
                $(".CheckBox_bike").show();

            } else {
                //$(".check_text1").hide();
                $(".CheckBox_car").hide();
                $(".CheckBox_bike").hide();
            }
            var checkown_car = document.getElementById('<%=CheckBox_car.ClientID %>');

            //check own vehicle
            if ($(checkown_car).is(":checked")) {
                $(".check_text1").show();
            }
            else {
                $(".check_text1").hide();
            }

            var checkown_bike = document.getElementById('<%=CheckBox_bike.ClientID %>');

            //check own vehicle
            if ($(checkown_bike).is(":checked")) {
                $(".check_text2").show();
            }
            else {
                $(".check_text2").hide();
                //$(".CheckBox_car").hide();
                //$(".CheckBox_bike").hide();
            }
        }
        function fooding() {

            var check_breakfast = document.getElementById('<%=chk_fooding.ClientID %>');

            if ($(check_breakfast).is(":checked")) {
                $(".CheckBox_breakfast").show();
                $(".fooding_breakfast").show();
                $(".CheckBox_lunch").show();
                $(".fooding_lunch").show();
                $(".CheckBox_dinner").show();
                $(".fooding_dinner").show();

            } else {

                $(".CheckBox_breakfast").hide();
                $(".fooding_breakfast").hide();
                $(".CheckBox_lunch").hide();
                $(".fooding_lunch").hide();
                $(".CheckBox_dinner").hide();
                $(".fooding_dinner").hide();


            }

            var text_breakfast = document.getElementById('<%=CheckBox_breakfast.ClientID %>');

            if ($(text_breakfast).is(":checked")) {
                $(".TextBox_breakfast").show();
                $(".text_breakfast").show();


            } else {
                $(".TextBox_breakfast").hide();
                $(".text_breakfast").hide();

            }
            var text_lunch = document.getElementById('<%=CheckBox_lunch.ClientID %>');

            if ($(text_lunch).is(":checked")) {
                $(".TextBox_lunch").show();
                $(".text_lunch").show();


            } else {
                $(".TextBox_lunch").hide();
                $(".text_lunch").hide();

            }
            var text_dinner = document.getElementById('<%=CheckBox_dinner.ClientID %>');

            if ($(text_dinner).is(":checked")) {
                $(".TextBox_dinner").show();
                $(".text_dinner").show();


            } else {
                $(".TextBox_dinner").hide();
                $(".text_dinner").hide();

            }
        }
        function hotel_accommodation() {

            var check_budget = document.getElementById('<%=chk_hotel.ClientID %>');

            if ($(check_budget).is(":checked")) {
                $(".hotel_budget").show();
                $(".hotel_standard").show();
                $(".hotel_twostar").show();
                $(".hotel_threestar").show();
                $(".hotel_fivestar").show();




            } else {

                $(".hotel_budget").hide();
                $(".hotel_standard").hide();
                $(".hotel_twostar").hide();
                $(".hotel_threestar").hide();
                $(".hotel_fivestar").hide();

            }

            var text_budget = document.getElementById('<%=hotel_budget.ClientID %>');

            if ($(text_budget).is(":checked")) {
                $(".text_hotel_budget").show();
                $(".text_hotel_budget2").show();
                $(".From_hotel_budget").show();
                $(".to_hotel_budget").show();

                //$(".hotel_to").show();

            } else {

                $(".text_hotel_budget").hide();
                $(".text_hotel_budget2").hide();
                $(".From_hotel_budget").show();
                $(".to_hotel_budget").hide();
                //$(".hotel_to").hide();
            }

            var text_standard = document.getElementById('<%=hotel_standard.ClientID %>');

            if ($(text_standard).is(":checked")) {
                $(".text_hotel_standard").show();
                $(".text_hotel_standard2").show();
                $(".to_hotel_standard").show();

            } else {
                $(".text_hotel_standard").hide();
                $(".text_hotel_standard2").hide();
                $(".to_hotel_standard").hide();
            }

            var text_twostar = document.getElementById('<%=hotel_twostar.ClientID %>');

            if ($(text_twostar).is(":checked")) {
                $(".text_hotel_twostar").show();
                $(".text_hotel_twostar2").show();
                $(".to_hotel_twostar").show();

            } else {
                $(".text_hotel_twostar").hide();
                $(".text_hotel_twostar2").hide();
                $(".to_hotel_twostar").hide();
            }

            var text_threestar = document.getElementById('<%=hotel_threestar.ClientID %>');

            if ($(text_threestar).is(":checked")) {
                $(".text_hotel_threestar").show();
                $(".text_hotel_threestar2").show();
                $(".to_hotel_threestar").show();

            } else {
                $(".text_hotel_threestar").hide();
                $(".text_hotel_threestar2").hide();
                $(".to_hotel_threestar").hide();
            }

            var text_fivestar = document.getElementById('<%=hotel_fivestar.ClientID %>');

            if ($(text_fivestar).is(":checked")) {
                $(".text_hotel_fivestar").show();
                $(".text_hotel_fivestar2").show();
                $(".to_hotel_fivestar").show();

            } else {
                $(".text_hotel_fivestar").hide();
                $(".text_hotel_fivestar2").hide();
                $(".to_hotel_fivestar").hide();
            }


        }

        function mode_allowed() {

            var checkown_bus = document.getElementById('<%=chkbus.ClientID %>');

            //check own vehicle
            if ($(checkown_bus).is(":checked")) {
                $(".CheckBox_ac").show();
                $(".CheckBox_nonac").show();
                $(".CheckBox_citybus").show();


            } else {
                $(".CheckBox_ac").hide();
                $(".CheckBox_nonac").hide();
                $(".CheckBox_citybus").hide();

            }



            var checkown_bus_ac = document.getElementById('<%=CheckBox_ac.ClientID %>');

            //check own vehicle
            if ($(checkown_bus_ac).is(":checked")) {

                $(".TextBox_ac").show();
            } else {

                $(".TextBox_ac").hide();
            }

            var checkown_bus_nonac = document.getElementById('<%=CheckBox_nonac.ClientID %>');

            //check own vehicle
        if ($(checkown_bus_nonac).is(":checked")) {

            $(".TextBox_nonac").show();
        } else {

            $(".TextBox_nonac").hide();
        }

        var checkown_bus_citybus = document.getElementById('<%=CheckBox_citybus.ClientID %>');

            //check own vehicle
        if ($(checkown_bus_citybus).is(":checked")) {

            $(".TextBox_citybus").show();
        } else {

            $(".TextBox_citybus").hide();
        }

    }


    function check_local_convey() {

        var check_local_convey = document.getElementById('<%=chklocalconveyance.ClientID %>');

            //check local convey
            if ($(check_local_convey).is(":checked")) {
                $(".local_convey").show();
            } else {
                $(".local_convey").hide();
            }

        }



        // function check_escation() {


        //if ($(check_escation).is(":checked")) {
        //    $(".chk_esca").show();

        //    $(".auto_Approval").hide();

        //} else {
        //    $(".chk_esca").hide();
        //    $(".auto_Approval").show();
        //}
        //  }

        function uncheckall() {
            var ins = document.getElementsByTagName('input');
            for (var i = 0; i < ins.length; i++) {
                if (ins[i].getAttribute('type') == 'checkbox') {
                    ins[i].checked = true;
                }
            }
        }

        function AllowAlphabet(e) {
            //alert("hello A");
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || keyEntry == '45' || (keyEntry == '8'))
                return true;
            else {
                // alert('Please Enter Only Character values.');
                return false;
            }
        }
        //  function femal_intgration(e) {

        //    if (femalper.value > 100) {
        //        alert("Please Enter Per Less Than 100% ");
        //        txt_female_percent.focus();

        //        return false;
        //    }

        //}
        function validate() {
            var policyname = document.getElementById('<%=txt_policy_name1.ClientID %>');
            if (policyname.value == "") {
                alert("Policy Name cannot be blank !!!");
                policyname.focus();
                return false;
            }
            var txt_start_date = document.getElementById('<%=txt_start_date.ClientID %>');
            if (txt_start_date.value == "") {
                alert("Please Select Start Date !!!");
                txt_start_date.focus();
                return false;
            }
            var txt_end_date = document.getElementById('<%=txt_end_date.ClientID %>');
            if (txt_end_date.value == "") {
                alert("Please Select End Date !!!");
                txt_end_date.focus();
                return false;
            }
            var v_txt_approval_level = document.getElementById('<%=txt_approval_level.ClientID %>');
            if (v_txt_approval_level.value == "") {
                alert("Approval level cannot be blank !!!");
                v_txt_approval_level.focus();
                return false;
            }
            var txt_not_approved_emailid = document.getElementById('<%=txt_not_approved_emailid.ClientID %>');
            if (txt_not_approved_emailid.value == "") {
                alert("Approval Email ID cannot be blank !!!");
                txt_not_approved_emailid.focus();
                return false;
            }
            //vikas 14/11
            var txt_per_day_limit = document.getElementById('<%=txt_per_day_limit.ClientID %>');
            if (txt_per_day_limit.value == "") {
                alert("Please Enter Par Day Limit!!");
                txt_per_day_limit.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }

        function submitItem() {

            if (confirm("Once you Submit you cannot make changes ?")) {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                return true;
            }

            return false;
        }
        function a_confirm() {
            if (confirm("Are you sure You want to delete this policy")) {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                return true;
            }
            return false;
        }

        function openWindow() {
            window.open("html/policy_master.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }
        function email(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry == '32') || (keyEntry == '9') || (keyEntry == '64') || (keyEntry == '46'))

                    return true;
                else {
                    return false;
                }
            }
        }

        function openWindow() {
            window.open("html/travel_policy_master.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }

    </script>

    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
     
                  <asp:Panel ID="Panel1" runat="server" class="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                  
                            <div style="text-align: center; color: #fff; font-size:small;" class="text-center text-uppercase"><b>Travel Policy Master</b></div>

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
                        <div style="text-align: left; color:white; font-size: small;"><b>Travel Policy Master Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>
            <div class="panel-body">
                 <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:25px 25px 25px 25px; margin-bottom:20px; margin-top:20px">
              
                <%--  <asp:Panel ID="panel" runat="server" CssClass="panel panel-primary " >
            <div class="panel-body">
                
                <div class="row">
                   
                      <div class="col-md-3  col-xs-12">
                          <br />
                     <asp:TextBox ID="txt_id" runat="server" class="form-control" placeholder="ID New"></asp:TextBox>
                           </div>
                       <div class="col-md-3  col-xs-12">
                           <br />
                     <asp:Button ID="btn_new" runat="server" CssClass="btn btn-primary" Text=" New" onclick="btn_new_Click" />
                           </div>
                </div>
               <br />
            </div>
      </asp:Panel>--%>
                <asp:Panel ID="panel2" runat="server" >
                 <%--  <asp:UpdatePanel ID="updatepanel1" runat="server" UpdateMode="Conditional">
                       <ContentTemplate>--%>
                        <asp:Panel ID="panel3" runat="server" CssClass="panel panel-primary ">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-2  col-xs-12" style="margin-top: 8px;">
                                        <br />
                                        <b>Existing Policy Name </b>
                                        <asp:Label ID="Label10" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                    </div>
                                    <div class="col-md-2  col-xs-12">
                                        <br />
                                        <%--<asp:DropDownList ID="ddl_paid_through" runat="server" class="form-control">
                             </asp:DropDownList>--%>
                                        <asp:DropDownList AppendDataBoundItems="true" ID="ddl_Existing_policy_name" runat="server" class="form-control text_box"
                                            DataTextField="txt_policy_name" DataValueField="id" AutoPostBack="true" OnSelectedIndexChanged="ddl_Existing_policy_name_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <br />
                                        <asp:TextBox ID="txt_policy_name1" runat="server" class="form-control"
                                           onkeypress="return AllowAlphabet(event)"></asp:TextBox>

                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <br />
                                        <asp:TextBox ID="txt_start_date" runat="server" class="form-control date-picker1"  placeholder="Start Date :"></asp:TextBox>
                                    </div>

                                    <div class="col-sm-2 col-xs-12">
                                        <br />
                                        <asp:TextBox ID="txt_end_date" runat="server" class="form-control date-picker2"  placeholder="End Date :"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </asp:Panel>
                    <br />

                        <div class="btn-pref btn-group btn-group-justified btn-group-lg" role="group" aria-label="...">
                            <div class="btn-group" role="group">
                                <button type="button" id="stars" class="btn btn-primary" href="#tab1" data-toggle="tab">

                                    <div class="hidden-xs"><b>Travel Mode</b></div>
                                </button>
                            </div>
                            <div class="btn-group" role="group">
                                <button type="button" id="favorites" class="btn btn-default" href="#tab2" data-toggle="tab">

                                    <div class="hidden-xs"><b>Approval</b></div>
                                </button>
                            </div>
                            <div class="btn-group" role="group">
                                <button type="button" id="following" class="btn btn-default" href="#tab3" data-toggle="tab">

                                    <div class="hidden-xs"><b>Lodging & Boarding with Food Service</b></div>
                                </button>
                            </div>
                            <div class="btn-group" role="group">
                                <button type="button" id="Button1" class="btn btn-default" href="#tab4" data-toggle="tab">
                                    <div class="hidden-xs"><b>Claims</b></div>
                                </button>
                            </div>
                            <div class="btn-group" role="group">
                                <button type="button" id="Button2" class="btn btn-default" href="#tab5" data-toggle="tab">
                                    <div class="hidden-xs"><b>Payment</b></div>
                                </button>
                            </div>
                            <%--  <div class="btn-group" role="group">
                                    <button type="button" id="Button2" class="btn btn-default" href="#tab5" data-toggle="tab">

                                        <div class="hidden-xs"></div>
                                    </button>
                                </div>--%>
                        </div>
                           <br />
                    <br />
                        <div class="well" style="background-color:white">
                            <div class="tab-content">
                                <div class="tab-pane fade in active" id="tab1">

                                    <div class="row">
                                        <div class="col-sm-12 col-xs-12">
                                            <b>1. Mode Allowed : </b>
                                            <table border="1" class="table table-responsive">
                                                <tr>

                                                    <td>
                                                        <asp:CheckBox ID="chkair" runat="server" />
                                                        <b>Air</b>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkbus" runat="server" CssClass="check_own" onchange="mode_allowed()" checked="true" />
                                                        <b>Bus</b>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chktraint1" runat="server" Text="" />
                                                        <b>Train AC Tier1</b>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chktraint2" runat="server" />
                                                       <b> Train AC Tier2</b>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chktraint3" runat="server" />
                                                       <b> Train AC Tier3</b>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkcabtaxi" runat="server" />
                                                       <b> Cab/Taxi</b>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkauto" runat="server" />
                                                       <b> Auto</b>
                                                    </td>
                                                    <%-- <td>
                                                                         <asp:CheckBox ID="chkownedvehicle" runat="server" /> Owned Vehicle
                                                                    </td>--%>
                                                </tr>



                                            </table>
                                            <div class="row">
                                                <div class="col-sm-1 col-xs-12"></div>
                                                <div class="col-sm-2 col-xs-12 CheckBox_ac ">
                                                    <asp:CheckBox ID="CheckBox_ac" runat="server"  CssClass="check_own" onchange="mode_allowed()" checked="true"  />
                                                   <b> AC</b>
                                                </div>

                                                <div class="col-sm-2 col-xs-12 CheckBox_nonac ">
                                                    <asp:CheckBox ID="CheckBox_nonac" runat="server" CssClass="check_own" onchange="mode_allowed()" checked="true"  />
                                                    <b>Non AC</b>
                                                </div>

                                                <div class="col-sm-2 col-xs-12  CheckBox_citybus">
                                                    <asp:CheckBox ID="CheckBox_citybus" runat="server" CssClass="check_own" onchange="mode_allowed()" checked="true"  />
                                                   <b> City Bus</b>
                                                </div>
                                               
                                                </div><br />
                                             <div class="row">
                                                 <div class="col-sm-1 col-xs-12"></div>
                                                 <div class="col-sm-2 col-xs-12 TextBox_ac" >
                                                    <asp:TextBox ID="TextBox_ac" runat="server" class="form-control" value="0"
                                                         onkeypress="return isNumber(event)" name="coupon_field"></asp:TextBox>
                                                    
                                                    </div>
                                                 <div class="col-sm-2 col-xs-12  TextBox_nonac" >
                                                    <asp:TextBox ID="TextBox_nonac" runat="server" class="form-control" value="0"
                                                         onkeypress="return isNumber(event)" name="coupon_field"></asp:TextBox>
                                                    
                                                    </div>
                                                  <div class="col-sm-2 col-xs-12  TextBox_citybus" >
                                                    <asp:TextBox ID="TextBox_citybus" runat="server" class="form-control" value="0"
                                                        onkeypress="return isNumber(event)" name="coupon_field"></asp:TextBox>
                                                    
                                                    </div>
                                                 </div>
                                            <div class="row">
                                                <div class="col-sm-3 col-xs-12 ">
                                                    <b>2.</b>
                                                    <asp:CheckBox ID="chkownedvehicle" runat="server" CssClass="check_own" onchange="check_own_vehicle()" checked="true"  />
                                                   <b> Owned Vehicle</b>
                              
                                                </div>

                                                 <div class="col-sm-3 col-xs-12  CheckBox_car">
                                                    
                                                    <asp:CheckBox ID="CheckBox_car" runat="server" CssClass="check_own" onchange="check_own_vehicle()" checked="true"  />
                                                    <b>Car</b>
                              
                                                </div>
                                              

                                                 <div class="col-sm-2 col-xs-12 CheckBox_bike ">
                                                    
                                                    <asp:CheckBox ID="CheckBox_bike" runat="server"  CssClass="check_own" onchange="check_own_vehicle()" checked="true"  />
                                                 <b>  Bike</b>
                              
                                                </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-2 col-xs-12"></div>
                                                    <div class="col-sm-1 col-xs-12"></div>
                                                    
                                                <div class="col-sm-2 col-xs-12 check_text1" >
                                                   
                                                    <asp:TextBox ID="txtownedvehiclekms" runat="server" class="form-control" value="0"
                                                         placeholder="Kilometer/PerDay" onkeypress="return isNumber(event)" name="coupon_field"></asp:TextBox>
                                                    

                                                </div>
                                               
                                                <div class="col-sm-2 col-xs-12 check_text2 " >

                                                   
                                                    <asp:TextBox ID="txtownedvehiclekms2" runat="server" class="form-control" value="0"
                                                        placeholder="Kilometer/PerDay" onkeypress="return isNumber(event)" name="coupon_field"></asp:TextBox>
                                                    
                                                    </div>
                                                </div>
                                            
                                            <%--<div class="row">
                                                   <fieldset class="question">
                                                       <label for="coupon_question">Do you have a coupon?</label>
                                                       <input class="coupon_question" type="checkbox" name="coupon_question" value="1" />
                                                       <span class="item-text">Yes</span>
                                                   </fieldset>

                                                   <fieldset class="answer">
                                                       <label for="coupon_field">Your coupon:</label>
                                                       <input type="text" name="coupon_field" id="coupon_field"/>
                                                   </fieldset>
                                            </div>--%>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-3 col-xs-12">
                                          <b>  3.</b>
                                            <asp:CheckBox ID="chklocalconveyance" class="check_allowd" runat="server" onchange="check_local_convey()" checked="true" />
                                           <b>Local Conveyance allowed(Rs)?</b> 
                                        </div>
                                        <div class="col-sm-4 col-xs-12">
                                            <asp:TextBox ID="txt_localconveyancelimit" runat="server" class="form-control local_convey" value="0"  placeholder="local conveyance" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade in" id="tab2">

                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12" style="margin-top:8px">
                                           <b> Approval Level : </b>
                                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                        </div>
                                        <div class="col-sm-2 col-xs-12" style="margin-left: -91px;">
                                            <asp:TextBox ID="txt_approval_level" runat="server" class="form-control" value="0" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3 col-xs-12" style="margin-top:8px;margin-left:89px;">
                                           <b> Approval days Before travel :</b>
                                        </div>
                                        <div class="col-sm-2 col-xs-12" style="margin-left: -143px;">
                                            <asp:TextBox ID="txt_approval_days_before" runat="server" class="form-control" value="0" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                   
                                   <%-- <div class="row auto_Approval">
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:CheckBox ID="chk_auto_approval" onchange="check_approve();" runat="server" />
                                            Auto Approval allowed?
                                        </div>
                                        <div class="col-sm-3 col-xs-12">
                                            <asp:TextBox ID="txt_app_days_before_travel" runat="server" class="form-control auto_app" placeholder="Days before travel" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </div>
                                    </div>--%>
                                    <%--<br />
                                    <div class="row exel">
                                        <div class="col-sm-3 col-xs-12">
                                            <asp:CheckBox ID="chk_escalation_approval" CssClass="check_escalation" onchange="check_escation();" runat="server" />
                                            Escalation for Approval allowed?
                                        </div>
                                        <div class="col-sm-3 col-xs-12">
                                            <asp:TextBox ID="txt_app_escalation_approcal" runat="server" class="form-control chk_esca" placeholder="Days before travel" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                   --%>
                                   <%-- <hr />--%>
                                    <div class="row">
                                      <%--  <div class="col-sm-2 col-xs-12" style="margin-top: 8px;">
                                            </div>--%>
                                        <div class="col-sm-2 col-xs-12" style="margin-left: -91px;">
                                            <asp:TextBox ID="txt_app_escalation_level" runat="server" class="form-control" onkeypress="return isNumber(event)">Escalation level :</asp:TextBox>
                                        </div>
                                          
                                      <%--  <div class="col-sm-4 col-xs-12" style="margin-top: 8px;margin-left: 92px;">
                                           <b> </b>
                                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                        </div>--%>
                                        <div class="col-sm-2 col-xs-12" style="margin-left: -131px;">
                                            <asp:TextBox ID="txt_not_approved_emailid" runat="server" class="form-control" onkeypress="return email(event)">If Not approved send request to which email id :</asp:TextBox>
                                        </div>
                                    </div>

                                    <br />
                                    
                                    <div class="row">
                                        <div class="col-sm-5 col-xs-12">
                                            <asp:CheckBox ID="chk_app_cancel_if_approved" runat="server" />
                                        <%--  <b>  Allow Employee to cancel travel plan if approved?</b>--%>
                                        </div>
                                        <div class="col-sm-5 col-xs-12" style="margin-left:-10px;">
                                            <asp:CheckBox ID="chk_app_cancel_if_ticket_confirmed" runat="server" />
                                         <%--   Allow Employee to cancel travel plan if Ticket confirmed?--%>
                                        </div>
                                    </div>
                                    <br />
                                   
                                    <div class="row">
                                       <%-- <div class="col-sm-4 col-xs-12" style="margin-top: 8px;">
                                            <b></b>
                                        </div>--%>
                                        <div class="col-sm-2 col-xs-12" style="margin-left: -121px;">
                                            <asp:TextBox ID="txt_cancellation_days" runat="server" class="form-control" Text="0" onkeypress="return isNumber(event)">How much lastest cancellation can be done in days :</asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <%--<div class="col-sm-4 col-xs-12" style="margin-top: 8px;">
                                           <b> </b>
                                        </div>--%>
                                        <div class="col-sm-2 col-xs-12"  style="margin-left: -121px;">
                                            <asp:TextBox ID="txt_exception_case_approval_level" runat="server" class="form-control" onkeypress="return email(event)">If Exception, how many level approval needed :</asp:TextBox>
                                        </div>
                                    </div>


                                </div>
                                <div class="tab-pane fade in" id="tab3">
                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:CheckBox ID="chk_hotel" runat="server" CssClass="check_own" onchange="hotel_accommodation()" checked="true" />
                                          <b>  Hotel accommodation?</b>
                                        </div>
                                       
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:CheckBox ID="chk_fooding" runat="server" CssClass="check_own" onchange="fooding()" Checked="true"/>
                                           <b> Fooding</b>
                                        </div>
                                        <div class="col-sm-2 col-xs-12 chk_expenses_allowed">
                                            <asp:CheckBox ID="chk_expenses_allowed" runat="server" CssClass="check_own" onchange="expenses_allowed()" Checked="true"/>
                                          <b>  Expenses allowed?</b>
                                        </div>
                                      
                                    </div>
                                     <div class="row">
                                                <div class="col-sm-2 col-xs-12 hotel_budget ">
                                                    <asp:CheckBox ID="hotel_budget" runat="server" CssClass="check_own" onchange="hotel_accommodation()" checked="true"  />
                                                   <b> Budget:</b>
                                                </div>
                                           <div class="col-sm-2 col-xs-12 to_hotel_budget" style="margin-top: 8px;" >
                                              <b>    From</b>
                                                  </div>
                                         <div class="col-sm-2 col-xs-12 text_hotel_budget" style="margin-left:-140px;">
                                            
                                                    <asp:TextBox ID="text_hotel_budget" runat="server" class="form-control" value="0"
                                                        placeholder="Limit/PerDay" onkeypress="return isNumber(event)" name="coupon_field"></asp:TextBox>
                                                     </div>

                                         <div class="col-sm-2 col-xs-12 to_hotel_budget" style="margin-top: 8px;">
                                              <b>    To</b>
                                                  </div>
                                        
                                         <div class="col-sm-2 col-xs-12  text_hotel_budget2" style="margin-left:-160px;">
                                              
                                                    <asp:TextBox ID="text_hotel_budget2" runat="server" class="form-control" value="0"
                                                        placeholder="Limit/PerDay" onkeypress="return isNumber(event)" name="coupon_field"></asp:TextBox>
                                                    
                                                    </div>
                                       </div>
                                         <div class="row">
                                                <div class="col-sm-2 col-xs-12 hotel_standard ">
                                                    <asp:CheckBox ID="hotel_standard" runat="server" CssClass="check_own" onchange="hotel_accommodation()" checked="true"  />
                                                    <b>Standard:</b>
                                                </div>
                                              <div class="col-sm-2 col-xs-12 to_hotel_standard" style="margin-top: 8px;">
                                          <b>  From</b>
                                         </div>
                                             <div class="col-sm-2 col-xs-12 text_hotel_standard" style="margin-left:-140px;">
                                                    <asp:TextBox ID="text_hotel_standard" runat="server" class="form-control" value="0"
                                                        placeholder="Limit/PerDay" onkeypress="return isNumber(event)" name="coupon_field"></asp:TextBox>
                                                    </div>
                                         
                                         <div class="col-sm-2 col-xs-12 to_hotel_standard" style="margin-top: 8px;">
                                          <b>  To</b>
                                         </div>
                                         
                                         <div class="col-sm-2 col-xs-12 text_hotel_standard2" style="margin-left:-160px;">
                                             <div class="col-sm-2 col-xs-12"></div>
                                                    <asp:TextBox ID="text_hotel_standard2" runat="server" class="form-control" value="0"
                                                        placeholder="Limit/PerDay" onkeypress="return isNumber(event)" name="coupon_field"></asp:TextBox>
                                                    
                                                    </div>
                                         </div>
                                             
                                         <div class="row">

                                                <div class="col-sm-2 col-xs-12 hotel_twostar ">
                                                    <asp:CheckBox ID="hotel_twostar" runat="server" CssClass="check_own" onchange="hotel_accommodation()" checked="true"  />
                                                  <b>  2 Star:</b>
                                                </div> 
                                              <div class="col-sm-2 col-xs-12 to_hotel_twostar " style="margin-top: 8px;">
                                        <b>    From</b>
                                         </div>
                                              <div class="col-sm-2 col-xs-12 text_hotel_twostar" style="margin-left:-140px;">
                                                    <asp:TextBox ID="text_hotel_twostar" runat="server" class="form-control" value="0"
                                                        placeholder="Limit/PerDay" onkeypress="return isNumber(event)" name="coupon_field"></asp:TextBox>
                                                    
                                                    </div>
                                         
                                         <div class="col-sm-2 col-xs-12 to_hotel_twostar " style="margin-top: 8px;">
                                        <b>    To</b>
                                         </div>
                                         
                                         <div class="col-sm-2 col-xs-12 text_hotel_twostar2 " style="margin-left:-160px;">
                                             <div class="col-sm-2 col-xs-12"></div>
                                                    <asp:TextBox ID="text_hotel_twostar2" runat="server" class="form-control" value="0"
                                                        placeholder="Limit/PerDay" onkeypress="return isNumber(event)" name="coupon_field"></asp:TextBox>
                                                    
                                                    </div>
                                            </div>
                                         <div class="row">
                                         <div class="col-sm-2 col-xs-12 hotel_threestar">
                                                    <asp:CheckBox ID="hotel_threestar" runat="server" CssClass="check_own" onchange="hotel_accommodation()" checked="true"  />
                                                  <b>  3 Star:</b>
                                                </div>
                                              <div class="col-sm-2 col-xs-12 to_hotel_threestar" style="margin-top: 8px;">
                                         <b>   From</b>
                                         </div>
                                              <div class="col-sm-2 col-xs-12 text_hotel_threestar" style="margin-left:-140px;">
                                                    <asp:TextBox ID="text_hotel_threestar" runat="server" class="form-control" value="0"
                                                        placeholder="Limit/PerDay" onkeypress="return isNumber(event)" name="coupon_field"></asp:TextBox>
                                                    
                                                    </div>
                                         
                                         <div class="col-sm-2 col-xs-12 to_hotel_threestar" style="margin-top: 8px;">
                                         <b>   To</b>
                                         </div>
                                         
                                         <div class="col-sm-2 col-xs-12 text_hotel_threestar2" style="margin-left:-160px;">
                                             <div class="col-sm-2 col-xs-12"></div>
                                                    <asp:TextBox ID="text_hotel_threestar2" runat="server" class="form-control" value="0"
                                                        placeholder="Limit/PerDay" onkeypress="return isNumber(event)" name="coupon_field"></asp:TextBox>
                                                    
                                                    </div>
                                          </div>
                                         <div class="row">

                                                <div class="col-sm-2 col-xs-12 hotel_fivestar ">
                                                    <asp:CheckBox ID="hotel_fivestar" runat="server" CssClass="check_own" onchange="hotel_accommodation()" checked="true"  />
                                                  <b>  5 Star:</b>
                                                </div> 
                                              <div class="col-sm-2 col-xs-12 to_hotel_fivestar" style="margin-top: 8px;">
                                         <b>   From</b>
                                         </div>     
                                              <div class="col-sm-2 col-xs-12 text_hotel_fivestar " style="margin-left:-140px;">
                                                    <asp:TextBox ID="text_hotel_fivestar" runat="server" class="form-control" value="0"
                                                        placeholder="Limit/PerDay" onkeypress="return isNumber(event)" name="coupon_field"></asp:TextBox>
                                                    
                                                    </div>
                                         
                                         <div class="col-sm-2 col-xs-12 to_hotel_fivestar" style="margin-top: 8px;">
                                         <b>   To</b>
                                         </div>
                                         
                                         <div class="col-sm-2 col-xs-12 text_hotel_fivestar2" style="margin-left:-160px;">
                                             <div class="col-sm-2 col-xs-12"></div>
                                                    <asp:TextBox ID="text_hotel_fivestar2" runat="server" class="form-control" value="0"
                                                        placeholder="Limit/PerDay" onkeypress="return isNumber(event)" name="coupon_field"></asp:TextBox>
                                                    
                                                    </div>
                                             </div>
                                    <br />
                                     <div class="row">
                                                <div class="col-sm-2 col-xs-12 CheckBox_breakfast">
                                                    <asp:CheckBox ID="CheckBox_breakfast" runat="server" CssClass="check_own" onchange="fooding()" checked="true"  />
                                                   <b> BreakFast:</b>
                                                </div>
                                         
                                         <div class="col-sm-2 col-xs-12 TextBox_breakfast" style="margin-left:70px;" >
                                                    <asp:TextBox ID="TextBox_breakfast" runat="server" class="form-control" value="0"
                                                        placeholder="Limit/PerDay" onkeypress="return isNumber(event)" name="coupon_field"></asp:TextBox>
                                                     </div>
                                       </div>
                                    <div class="row">
                                                <div class="col-sm-2 col-xs-12 CheckBox_lunch">
                                                    <asp:CheckBox ID="CheckBox_lunch" runat="server" CssClass="check_own" onchange="fooding()" checked="true"  />
                                                  <b>  Lunch:</b>
                                                </div>
                                         <div class="col-sm-2 col-xs-12 TextBox_lunch" style="margin-left:70px;">
                                                    <asp:TextBox ID="TextBox_lunch" runat="server" class="form-control" value="0"
                                                        placeholder="Limit/PerDay" onkeypress="return isNumber(event)" name="coupon_field"></asp:TextBox>
                                                     </div>

                                         
                                       </div>
                                    <div class="row">
                                                <div class="col-sm-2 col-xs-12 CheckBox_dinner ">
                                                    <asp:CheckBox ID="CheckBox_dinner" runat="server" CssClass="check_own" onchange="fooding()" checked="true"  />
                                                  <b>  Dinner:</b>
                                                </div>
                                         <div class="col-sm-2 col-xs-12 TextBox_dinner" style="margin-left:70px;">
                                                    <asp:TextBox ID="TextBox_dinner" runat="server" class="form-control" value="0"
                                                        placeholder="Limit/PerDay" onkeypress="return isNumber(event)" name="coupon_field"></asp:TextBox>
                                                     </div>

                                         
                                       </div>
                                    <div class="row">
                                                <div class="col-sm-2 col-xs-12 CheckBox_inside">
                                                    <asp:CheckBox ID="CheckBox_inside" runat="server" CssClass="check_own" onchange="expenses_allowed()" checked="true"  />
                                                   <b>  Inside City:  </b>
                                                </div>
                                        <%--   <div class="col-sm-2 col-xs-12" style="margin-top: 8px;">
                                            Per Day Limit(Rs) :<span style="color:red">*</span>
                                        </div>--%>
                                       
                                        <div class="col-sm-2 col-xs-12 txt_per_day_limit" style="margin-left:70px;">
                                            <asp:TextBox ID="txt_per_day_limit" runat="server" class="form-control" placeholder="Limit/PerDay" name="coupon_field"
                                              value="0"   onkeypress="return isNumber(event)"></asp:TextBox>
                                        </div>
                                       </div>
                                    <div class="row">
                                                <div class="col-sm-2 col-xs-12 CheckBox_outside">
                                                    <asp:CheckBox ID="CheckBox_outside" runat="server" CssClass="check_own" onchange="expenses_allowed()" checked="true"  />
                                                 <b>   Outside City:</b>
                                                </div>
                                       
                                         <div class="col-sm-2 col-xs-12 outside_city" style="margin-left:70px;">
                                                    <asp:TextBox ID="Textbox_outside" runat="server" class="form-control" value="0"
                                                        placeholder="Limit/PerDay" onkeypress="return isNumber(event)" name="coupon_field"></asp:TextBox>
                                                     </div>
                                         
                                       </div>
                                    <%--<div class="row">
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:CheckBox ID="chk_femail_upgrade" runat="server" />
                                            Female Upgradation?
                                        </div>
                                        <div class="col-sm-1 col-xs-12" style="margin-top: 8px;">
                                            In %  :
                                        </div>
                                        <div class="col-sm-2 col-xs-12" style="margin-left: -55px;">
                                            <asp:TextBox ID="txt_female_percent" runat="server" class="form-control " onkeypress="return isNumber(event)" onchange="femal_intgration()"></asp:TextBox>
                                        </div>
                                    </div>--%>
                                </div>
                                <div class="tab-pane fade in" id="tab4">
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12" style="margin-top: 8px;">
                                           <b> Claims to be submitted max in days after travel date : </b>
                                        </div>
                                        <div class="col-sm-2 col-xs-12" style="margin-left: -80px;">
                                            <asp:TextBox ID="txt_claim_max_days" runat="server" class="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    </div>
                                    <br />
                                    <%--<div class="row">
                                        <div class="col-sm-4 col-xs-12" style="margin-top: 8px;">
                                            If late claim escalation to how many levels : 
                                        </div>
                                        <div class="col-sm-2 col-xs-12" style="margin-left: -80px;">
                                            <asp:TextBox ID="txt_late_claim_days" runat="server" class="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12" style="margin-top: 8px;">
                                            If exception, claim escalation to how many levels : 
                                        </div>
                                        <div class="col-sm-2 col-xs-12" style="margin-left: -80px;">
                                            <asp:TextBox ID="txt_claim_exception_case" runat="server" class="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </div>
                                    </div>--%>
                                    <%--<br />
                                </div>--%>
                            <div class="tab-pane fade in" id="tab5">
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12" style="margin-top: 8px;">
                                         <b>  Number of days to process payment : </b>
                                        </div>
                                        <div class="col-sm-2 col-xs-12" style="margin-left: -80px;">
                                            <asp:TextBox ID="text_payment_process" runat="server" class="form-control" value="0" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    </div>
                                <%--<div class="tab-pane fade in" id="tab5" >
                                    <table border="1" class="table table-responsive">
                                        <tr>
                                            <td style="width: 20%;">
                                                <b>To be paid by company : </b>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chk_payment_air" runat="server" />
                                                Air
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chk_payment_bus" runat="server" />
                                                Bus
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chk_payment_train" runat="server" />
                                                Train
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chk_payment_taxi" runat="server" />
                                                Taxi
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chk_payment_hotel" runat="server" />
                                                Hotel
                                            </td>
                                        </tr>

                                    </table>

                                    <br />
                                </div>--%>
                            </div>
                        </div>


                           
                        <div class="row text-center">
                           <b>Note: You can save as Draft many times as you want but once you submit you cannot make changes.<br />
                                  After Submit Successfully Then You  Get Details In Apply Travelling Plan.</b> 

                        </div>
                        <br />
                        <div class="row text-center">
                            <asp:Button ID="btnadd" runat="server" class="btn  btn-primary" OnClientClick="return validate();" OnClick="btnadd_Click" Text="Save as Draft" />
                            
            <asp:Button ID="btn_submit" runat="server" class="btn  btn-primary" OnClick="btn_submit_Click" Text="Submit" /> <%--OnClientClick="return submitItem();" --%>
                           
                             <asp:Button ID="btndelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this Policy?');" class="btn  btn-primary" OnClick="btndelete_Click" Text="Delete" />
        
                             <asp:Button ID="btncloselow" runat="server" class="btn btn-danger" OnClick="btncloselow_Click" Text="Close" />

                        </div>
                        <br />
                      
                   <%-- </ContentTemplate>
                   </asp:UpdatePanel>--%>
                </asp:Panel>
            </div>
                </div>
        </asp:Panel>
       
    </div>
</asp:Content>
