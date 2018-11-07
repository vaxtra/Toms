﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Web.Routing;
using System.Web.UI;

/// <summary>
/// Summary description for CustomRouteHandler
/// </summary>
public class CustomRouteHandler : IRouteHandler
{
    public CustomRouteHandler(string virtualPath)
    {
        this.VirtualPath = virtualPath;
    }

    public string VirtualPath { get; private set; }

    public IHttpHandler GetHttpHandler(RequestContext
          requestContext)
    {
        var page = BuildManager.CreateInstanceFromVirtualPath
             (VirtualPath, typeof(Page)) as IHttpHandler;

        foreach (var urlParam in requestContext.RouteData.Values)
        {
            requestContext.HttpContext.Items[urlParam.Key] = urlParam.Value;
        }

        return page;

    }
}