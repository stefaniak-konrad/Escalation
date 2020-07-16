using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EO.Serwis.Portal.DataAccess;
using EO.Serwis.Portal.DataAccess.Contract.POCO;
using EO.Serwis.Portal.DataAccess.Contract.Repositories;
using EO.Serwis.Portal.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Serilog;

namespace EscalationStatelessService
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class EscalationStatelessService : StatelessService
    {
        public ConfigurationPackage ConfigPackage { get; } // odwo³anie do SF
        public EscalationStatelessService(StatelessServiceContext context)
            : base(context)
        {
            ConfigPackage = Context.CodePackageActivationContext.GetConfigurationPackageObject("Config");

            Log.Logger = new LoggerConfiguration()
                       .WriteTo.File($@"c:\logs\EO.Portal.Payments.ServiceFabricApp\{ConfigPackage.Settings.Sections["ENV"].Parameters["Environment"].Value}\EscalationStatelessService-.txt", rollingInterval: RollingInterval.Day)
                       .CreateLogger();

            Log.Information("Initializing Escalation Stateless Service!");
        }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[0];
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following sample code with your own logic 
            //       or remove this RunAsync override if it's not needed in your service.

            long iterations = 0;
            EscalationModel model = new EscalationModel();
            while (true)
            {
                
                string eonConnectionString = $"Server={ConfigPackage.Settings.Sections["DB"].Parameters["DataSource"].Value};initial catalog={ConfigPackage.Settings.Sections["DB"].Parameters["Name"].Value};" +
                        $"user id={ConfigPackage.Settings.Sections["DB"].Parameters["Login"].Value};password={ConfigPackage.Settings.Sections["DB"].Parameters["Password"].Value};MultipleActiveResultSets=True;App=EntityFramework;";
                var opt = new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<EONDataContext>();

                using (var dataContext = new EONDataContext(opt.UseSqlServer(eonConnectionString).Options))
                {
                    try
                    {
                        LiczbaMinutPomiedzyDatami liczbaMinutPomiedzyDatami = new LiczbaMinutPomiedzyDatami();
                        LicznikProcentowegoWykorzystaniaSLA licznikProcentowegoWykorzystaniaSLA = new LicznikProcentowegoWykorzystaniaSLA();

                        // pobieranie bazy danych
                        var KalendarzDniRepo = new KalendarzeDniRepository(dataContext);
                        var zgloszeniaRepo = new ZgloszenieRepository(dataContext);
                        var czasNaRealizacjeZgloszenia = dataContext.Query<CzasNaRealizacjeZgloszeniaPOCO>().ToList();

                        foreach (var dane in czasNaRealizacjeZgloszenia)
                        {
                            DateTime dataPoczatkowa = dane.poczatek;
                            DateTime dataKoncowa = DateTime.Now;
                            long idKalendarza = 1;
                            var wylicz = liczbaMinutPomiedzyDatami.WyliczLiczbeMinutPomiedzyDatamizKalendarza(dataPoczatkowa, dataKoncowa, idKalendarza, KalendarzDniRepo);
                            Log.Information($"Wyliczono liczbê minut pmiêdzy datami");
                            var przelicz = licznikProcentowegoWykorzystaniaSLA.Przelicz(wylicz, dane.czas);
                            Log.Information($"Wyliczono procêt wykorzystania czasu SLA dla zg³oszenia: {dane.IdZgloszenia}");
                            //var pozostalyCzas = dane.czas - wylicz;
                            var zgloszenie = zgloszeniaRepo.Select(c => c.IdZgloszenia == dane.IdZgloszenia).Single();
                            {
                                zgloszenie.ProcentWykozystaniaCzasuSLA = Convert.ToInt32(przelicz);
                            }
                            zgloszeniaRepo.Modify(zgloszenie);
                            zgloszeniaRepo.Save();
                            Log.Information($"Zg³oszenie {dane.IdZgloszenia} zosta³o zaktualizowane");
                        }


                    }
                    catch (Exception ex)
                    {
                        Log.Fatal($"Podczas przeliczania wyst¹pi³ b³¹d wyst¹pi³ b³¹d: {ex.ToString()}");
                    }

                    cancellationToken.ThrowIfCancellationRequested();

                    //ServiceEventSource.Current.ServiceMessage(this.Context, "Working-{0}", ++iterations);

                    await Task.Delay(TimeSpan.FromMinutes(5), cancellationToken);
                }
            }
        }
    }
}
