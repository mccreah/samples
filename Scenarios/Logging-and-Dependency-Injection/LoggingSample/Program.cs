// Project created using the CoreWCF.Templates project template

var builder = WebApplication.CreateBuilder();

builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelMetadata();
builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();

// custom utility service
builder.Services.AddSingleton<IUtilityService, UtilityService>();

//Register the `Service` class with Dependency Injection, based on it being a single instance
builder.Services.AddSingleton<Service1>();

//Register the `Service` class with Dependency Injection, based on it being a scoped instance
builder.Services.AddScoped<Service2>();

var app = builder.Build();

app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<Service1>();
    serviceBuilder.AddServiceEndpoint<Service1, IService>(new BasicHttpBinding(BasicHttpSecurityMode.Transport), "/Service1.svc");

    serviceBuilder.AddService<Service2>();
    serviceBuilder.AddServiceEndpoint<Service2, IService2>(new BasicHttpBinding(BasicHttpSecurityMode.Transport), "/Service2.svc");

    var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
    serviceMetadataBehavior.HttpsGetEnabled = true;
});

app.Run();
