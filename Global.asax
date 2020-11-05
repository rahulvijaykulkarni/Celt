<%@ Application Language="C#" %>
<%@ Import namespace="System.Diagnostics" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        Application["BIRTHDAY"] = "";
        Application["policy_id"] = "";
    Application["comp1emp_code"] = "";
    Application["comp2emp_code"] = "";
        log4net.Config.XmlConfigurator.Configure();
        // Code that runs on application startup

        //Application_End(null, null);
        //ProcessStartInfo pro = new ProcessStartInfo();
        //Process proStart = new Process();
        //pro.FileName = "C:\\inetpub\\wwwroot\\CeltPayroll\\Ticketing_console\\Ticketing\\bin\\Debug\\Ticketing.exe";
        //pro.CreateNoWindow = true;
        //proStart.StartInfo = pro;
        //proStart.Start();


    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
        Application["BIRTHDAY"] = "";
        Application["policy_id"] = "";
    Application["comp1emp_code"] = "";
    Application["comp2emp_code"] = "";
        var process = System.Diagnostics.Process.GetProcessesByName("Ticketing");
        foreach (var p in process)
        {
            if (!string.IsNullOrEmpty(p.ProcessName))
            {
                try
                {
                    p.Kill();
                }
                catch { }
            }
        }

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        Session["comp_code"] = "";
        Session["COMP_NAME"] = "";
        Session["CLIENT_CODE"] = "";
        Session["UNIT_NAME"]="";
        Session["UNIT_CODE"] = "";
        Session["EXPENSE_ID"] = "";

        Session["expfrom_date"]= "";
        Session["expTo_date"] = "";

        Session["ReportPath"] = "";
        Session ["USERNAME"]="";

        Session["system_curr_date"] = "";

        //-- Salary Sessions --
        Session["SalaryProcessMonth"] = "";
        Session["SalaryProcessYear"] = "";
        Session["SalaryProcessUnit"] = "";
        Session["SalaryProcessClient"] = "";
        Session["SalarySteps"] = "";
        //ACOUNTS REPORTS-----------------
        Session["FOR_CODE"] = "";
        Session["FROM_DATE"] = "";
        Session["TO_DATE"] = "";

        Session["FROM_INVOICE"] = "";
        Session["LOGIN_ID"] = "";
        //--------------------------
        Session["CURRENT_PERIOD"] = "";
        Session["CURRENT_MONTH"] = "";
        Session["CURRENT_YEAR"] = "";
        //--------------------------
        //all excel outs sessions-------------------
        Session["AllExcelOut"] = "";
        Session["DOC_NO"] = "";
        Session["AC_REPORT_NO"] = "";
        Session["PAY_REPORT_NO"] = "";
        Session["ReportMonthNo"] = "";
        Session["ExlColCnt"] = "";
        Session["UnitCode_Name"] = "";
        Session["Service_Tax"] = "";
        Session["ROLE"] = "";
        Session["MOVE_TO_INVOICE"] = "";
        Session["MOVE_TO_PROFORMA"] = "";
        Session["MOVE_PROFORMA_INVOICE"] = "";
        Session["MOVE_PO_PI"] = "";
        Session["Ticket_ID"] = "";
        Session["EMP_CODE"] = "";
        Session["LEAVE_MASTER_ID"] = "";

        Session["MAP_ADDRESS"] = "";
        Session["MAP_LONGITUDE"] = "";
        Session["MAP_LATTITUDE"] = "";
        Session["MAP_AREA"] = "";
        Session["MAP_ID"] = "";

        Session["Approval_Id"] = "";
        Session["Approval_Code"] = "";
        Session["SERVICE"] = "";
        Session["YEAR_MONTH"] = "";
	 Session["UNIT_CODE1"] = "";
	 Session["ZONE"] = "";
     Session["unit_code_addemp"] = "";//vikas 12/11
     Session["REPORTING_EMP_SERIES"] = "";
     Session["add_email_client"] = "";
     Session["add_email_state"] = "";
     Session["state"] = "";
     Session["state_new_emp"] = "";
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        Session ["comp_code"]=null ;
        Session ["COMP_NAME"]=null ;
        Session["UNIT_NAME"] = null;
        Session["UNIT_CODE"] = null;
        Session ["REPORT_TYPE"]=null ;
        Session["ReportPath"] = "";
        Session["USERNAME"] = "";
        Session["Atten_Unit_Code"] = "";
        Session["LOGIN_ID"] = "";
        Session["ROLE"] = "";
        Session["system_curr_date"] = "";
        Session["EXPENSE_ID"] = "";

        Session["expfrom_date"]= "";
        Session["expTo_date"] = "";
        Session["MOVE_TO_INVOICE"] = "";
        Session["MOVE_TO_PROFORMA"] = "";
        Session["MOVE_PROFORMA_INVOICE"] = "";
        Session["MOVE_PO_PI"] = "";
        Session["Ticket_ID"] = "";
        Session["EMP_CODE"] = "";
        Session["LEAVE_MASTER_ID"] = "";
        //--- Salary Processing ----
        Session["SalaryProcessMonth"] = "";
        Session["SalaryProcessYear"] = "";
        Session["SalaryProcessUnit"] = "";
        Session["SalarySteps"] = "";

        Session["MAP_ADDRESS"] = "";
        Session["MAP_LONGITUDE"] = "";
        Session["MAP_LATTITUDE"] = "";
        Session["MAP_AREA"] = "";
        Session["MAP_ID"] = "";
        Session["CLIENT_CODE"] = "";
        Session["Approval_Id"] = "";
        Session["Approval_Code"] = "";
        Session["SERVICE"] = "";
        Session["YEAR_MONTH"] = "";
	 Session["UNIT_CODE1"] = "";
	 
	  //MD change
        Session["ZONE_NAME"] = "";
        Session["REGION_NAME"] = "";
        Session["unit_code_addemp"] = "";//vikas 12/11
        Session["REPORTING_EMP_SERIES"] = "";
        Session["add_email_client"] = "";
        Session["add_email_state"] = "";
        Session["state"] = "";
        Session["state_new_emp"] = "";
    }

</script>
