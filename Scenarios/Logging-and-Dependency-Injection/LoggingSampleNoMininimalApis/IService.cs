using CoreWCF;
using System;
using System.Runtime.Serialization;

namespace LoggingSampleNoMinimalApis
{
  [ServiceContract]
  public interface IService
  {
    [OperationContract]
    string GetData(int value);
  }

  [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
  public class Service3 : IService
  {
    private ILogger<Service3> _logger;
    private readonly IUtilityService _utilityService;

    // Parameterized constructor will be called by Dependency Injection
    // Logs will be created under the name "LoggingSample.Service" as that's the type name used constructing the logger object
    public Service3(ILogger<Service3> logger, IUtilityService utilityService)
    {
      _logger = logger;
      _utilityService = utilityService;
    }

    public string GetData(int value)
    {
      _logger.LogInformation("GetData called with value {value}", value);
      return string.Format("You entered: {0}", value);
    }
  }
}
