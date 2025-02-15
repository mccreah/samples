﻿using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace LoggingSampleNoMinimalApis
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddServiceModelServices();
      services.AddServiceModelMetadata();
      services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();
      services.AddSingleton<IUtilityService, UtilityService>();
      services.AddSingleton<Service3>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app)
    {
      app.UseServiceModel(serviceBuilder =>
      {
        serviceBuilder.AddService<Service3>();
        serviceBuilder.AddServiceEndpoint<Service3, IService>(new BasicHttpBinding(BasicHttpSecurityMode.Transport), "/Service3.svc");
        var serviceMetadataBehavior = app.ApplicationServices.GetRequiredService<ServiceMetadataBehavior>();
        serviceMetadataBehavior.HttpsGetEnabled = true;
      });
    }
  }
}
