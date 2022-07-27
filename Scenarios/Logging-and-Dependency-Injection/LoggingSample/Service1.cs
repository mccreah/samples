using CoreWCF;
using System;
using System.Runtime.Serialization;

namespace LoggingSample
{
  [ServiceContract]
  public interface IService
  {
    [OperationContract]
    string GetData(int value);
  }

  public class Service1 : IService
  {
    private ILogger<Service1> _logger;
    private readonly IUtilityService _utilityService;

    // Parameterized constructor will be called by Dependency Injection
    // Logs will be created under the name "LoggingSample.Service" as that's the type name used constructing the logger object
    public Service1(ILogger<Service1> logger, IUtilityService utilityService)
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
