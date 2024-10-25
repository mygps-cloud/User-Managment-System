


using System.Net.NetworkInformation;
using Ipstatuschecker.Dto;
using Ipstatuschecker.Services;

public class PingBackgroundService : BackgroundService
{
    private readonly ILogger<PingBackgroundService> logger;
    private readonly IServiceProvider serviceProvider;

    public PingBackgroundService(IServiceProvider serviceProvider, ILogger<PingBackgroundService> logger)
    {
        this.serviceProvider = serviceProvider;
        this.logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Starting Ping Task...");

            using (var scope = serviceProvider.CreateScope())
            {
               var _ipStatusService = scope.ServiceProvider.GetRequiredService<DbPingBackgroundService>();
                 var _PingLogService = scope.ServiceProvider.GetRequiredService<PingLogService>();

                var DataTimeOfline=new List<DateTime>();
            
                   

           var tasksUsers = await _ipStatusService.GetAllUsers();




            var tasks = tasksUsers.Select(ip => Task.Run(async () =>
             {
                using (var newScope = scope.ServiceProvider.CreateScope())
                {
                    var _PingLogService = newScope.ServiceProvider.GetRequiredService<PingLogService>();
                    var Response = ip.Status = await PingIp(ip.IpAddress) ? "Online" : "Offline";

                    var pingLog = new PingLogDtoReqvest
                    {
                        Id = (int)ip.Id,
                        OnlieTime = DateTime.Now,
                        OflineTime = DataTimeOfline
                    };

                    _PingLogService.AddNewUser(pingLog);
                }
             }));
                  
        //  await Task.WhenAll(tasks);
             }
     


        //         foreach (var task in tasksUsers)
        //         {
        //          var Response= await PingIp( task.IpAddress);
        //          if(Response.Equals("Online"))
        //          { var pingLog = new PingLogDtoReqvest
        //                 {
        //                     Id = 1,
        //                     OnlieTime = DateTime.Now,
        //                     OflineTime =DataTimeOfline
                            
        //                 };
        //                     _PingLogService.AddNewUser(pingLog);
        //        }
                            
        //         else 
        //         {
        //                 DataTimeOfline.Add(DateTime.Now);
        //                    var pingLog = new PingLogDtoReqvest
        //                 {
        //                     Id = 1,
        //                     OnlieTime = DateTime.Now,
        //                     OflineTime =DataTimeOfline
                            
        //                 };
        //                     _PingLogService.AddNewUser(pingLog);
        //             Console.WriteLine("write data");

        //         }
        //  }
        //     }



                        // var tasks = tasksUsers.Select(async ip =>
                        // {
                        //     using (var newScope = scope.ServiceProvider.CreateScope())
                        //     {
                        //         var _PingLogService = newScope.ServiceProvider.GetRequiredService<PingLogService>();
                        //         var response = ip.Status = await PingIp(ip.IpAddress) ? "Online" : "Offline";

                        //         var pingLog = new PingLogDtoReqvest
                        //         {
                        //             Id = 1,
                        //             OnlieTime = DateTime.Now,
                        //             OflineTime = DataTimeOfline 
                        //         };

                        //         await _PingLogService.AddNewUser(pingLog); 
                        //     }
                        // });

                        // await Task.WhenAll(tasks);
                        //    }


                        
            }
        }


    
            
    

    public async Task<bool> PingIp(string ipAddress)
    {
        try
        {
            using (var ping = new Ping())
            {
                var reply = await ping.SendPingAsync(ipAddress);
                return reply.Status == IPStatus.Success;
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"Error pinging {ipAddress}: {ex.Message}");
            return false;
        }
    }

 }
        
        


    
