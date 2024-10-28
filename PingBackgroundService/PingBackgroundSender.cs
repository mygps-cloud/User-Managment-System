


using System.Data;
using System.Net.NetworkInformation;
using Ipstatuschecker.Dto;
using Ipstatuschecker.Services;

public class PingBackgroundService : BackgroundService
{
      private readonly ILogger<PingBackgroundService> logger;
       private readonly IServiceProvider serviceProvider;
      private readonly List<DateTime> _DataTimeOfline = new List<DateTime>();


    public PingBackgroundService(IServiceProvider serviceProvider, ILogger<PingBackgroundService> logger)
    {
        this.serviceProvider = serviceProvider;
        this.logger = logger;
    }

            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await CheckIpStatuses();
                    await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
                }
     }


      private async Task CheckIpStatuses()
      {
           using (var scope = serviceProvider.CreateScope())
              {
                    var _ipStatusService = scope.ServiceProvider.GetRequiredService<DbPingBackgroundService>();
                        var _PingLogService = scope.ServiceProvider.GetRequiredService<PingLogService>();

                        var DataTimeOfline=new List<DateTime>();
                
                        
                            
               
                                                                
                                                    try
                                                    {
                                                        var tasksUsers = await _ipStatusService.GetAllUsers();
                                                        
                                                        if (tasksUsers.Count > 0)
                                                        {
                                                            foreach (var task in tasksUsers)
                                                            {
                                                                var response = await PingIp(task.IpAddress);
                                                                var pingLog = new PingLogDtoReqvest
                                                                {
                                                                    OnlieTime = DateTime.Now,
                                                                    OflineTime = new List<DateTime>()
                                                                };

                                                                if (response.Equals("Online"))
                                                                {
                                                                    pingLog.OnlieTime = DateTime.Now;
                                                                }
                                                                else
                                                                {
                                                                    DataTimeOfline.Add(DateTime.Now);
                                                                    pingLog.OflineTime = DataTimeOfline;
                                                                }

                                                                _PingLogService.AddNewUser(pingLog);
                                                                DataTimeOfline.Clear();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            
                                                            Console.WriteLine("No users found to ping.");
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        
                                                        Console.WriteLine($"An error occurred: {ex.Message}");
                                                        



                                //     var tasks = tasksUsers.Select(ip => Task.Run(async () =>
                                // {
                                //     using (var newScope = scope.ServiceProvider.CreateScope())
                                //     {
                                //         var _PingLogService = newScope.ServiceProvider.GetRequiredService<PingLogService>();
                                //         var Response = ip.Status = await PingIp(ip.IpAddress) ? "Online" : "Offline";

                                //         var pingLog = new PingLogDtoReqvest
                                //         {
                                          
                                //             OnlieTime = DateTime.Now,
                                //             OflineTime = DataTimeOfline
                                //         };
                                //         _DataTimeOfline.Add(DateTime.Now);

                                //         pingLog.OflineTime.Add(DateTime.Now);
                                //         Console.WriteLine(" xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",DateTime.Now);

                                //         _PingLogService.AddNewUser(pingLog);
                                //         DataTimeOfline.Clear();
                                //     }
                                // }
                                // ));
                                    
                                //     await Task.WhenAll(tasks);



                                    
                             }

                             

                    
            
            }
                            
    } 
            
    

                private async Task<bool> PingIp(string ipAddress)
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
                    //  logger.LogError($"Error pinging {ipAddress}: {ex.Message}");
                        return false;
                    }
                }


 }
        
        


    
