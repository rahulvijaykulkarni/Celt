<%@ WebHandler Language="C#" Class="Handler1" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JsonServices;
using JsonServices.Web;

public class Handler1 : JsonHandler
{

    public Handler1()
    {
        this.service.Name = "Celtpayroll";
        this.service.Description = "Copyright @Celtsoft";
        InterfaceConfiguration IConfig = new InterfaceConfiguration("RestAPI", typeof(IServiceAPI), typeof(ServiceAPI));
        this.service.Interfaces.Add(IConfig);
    }
    
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}