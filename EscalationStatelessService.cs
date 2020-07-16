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
                //try
                //{
                //    string eonConnectionString = $"Server={ConfigPackage.Settings.Sections["DB"].Parameters["DataSource"].Value};initial catalog={ConfigPackage.Settings.Sections["DB"].Parameters["Name"].Value};" +
                //                            $"user id={ConfigPackage.Settings.Sections["DB"].Parameters["Login"].Value};password={ConfigPackage.Settings.Sections["DB"].Parameters["Password"].Value};MultipleActiveResultSets=True;App=EntityFramework;";
                //    var opt = new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<EONDataContext>();

                //    using (var dataContext = new EONDataContext(opt.UseSqlServer(eonConnectionString).Options))
                //    {
                //        // pobieranie bazy danych
                //        var zgloszeniaRepo = new ZgloszenieRepository(dataContext);
                //        var kalendarzRepo = new KalendarzeRepository(dataContext);
                //        var JednostkiOrganizacyjneRepo = new JednostkiOrganizacyjneRepository(dataContext);
                //        var kalendarzeRepo = new KalendarzeRepository(dataContext);
                //        var KalendarzDniRepo = new KalendarzeDniRepository(dataContext);
                //        var IdSLAKomponentuUslugiRepo = new SLAKomponentowUslugiRepository(dataContext);
                //        var slaRepo = new SlaRepository(dataContext);

                //        DateTime DataZakonczniaNaliczenia;

                //        //poberanie zegara sla oraz pobranie jednostki organizacyjnej
                //        var zgloszenia = zgloszeniaRepo.Select(c => c.IdZgloszenia == 96465);
                //        Log.Information($"Poberanie zegara sla oraz pobranie jednostki organizacyjnej zakoñczone powodzeniem");

                //        foreach (var daneZgloszenia in zgloszenia)
                //        {
                //            //pobieranie kalendarza
                //            var idKalendarzasla = slaRepo.Select(c => c.IdSLA == daneZgloszenia.IdSLA).ToList();
                //            model.kalendarzSla = idKalendarzasla.Select(c => c.IdSchematuGodzinowegoPracySerwisu).FirstOrDefault();
                //            Log.Information($"Pobieranie SLA zakoñczone powidzeniem");

                //            var jednostkiOrganizacyjne = JednostkiOrganizacyjneRepo.Select(c => c.ID == daneZgloszenia.IdJednostkaOrganizacyjna);
                //            var IdKalendarzaJednostkiOrganizacyjnej = jednostkiOrganizacyjne.Select(c => c.IdKalendarza).FirstOrDefault();
                //            Log.Information($"Pobrano kalendarz jednostki organizacyjnej");
                //            if (IdKalendarzaJednostkiOrganizacyjnej == null)
                //            { model.kalendarzSla = IdKalendarzaJednostkiOrganizacyjnej; }

                //            //w przypadku braku zdefiniowania innych kalendarzy wybór kalednarza standardowego
                //            if (model.kalendarzSla == null)
                //            {
                //                var standardowyKalendarz = kalendarzeRepo.Select(c => c.NazwaKalendarza == "Standardowy");
                //                var IDStandardowegoKalendarza = standardowyKalendarz.Select(c => c.IdKalendarza).First();
                //                model.kalendarzSla = IDStandardowegoKalendarza;
                //                if (standardowyKalendarz == null)
                //                {
                //                    Log.Error($"Nie odnaleziono kalendarza o nazwie standardowy");
                //                }
                //                Log.Information($"Przypisano standardowy kalendarz pracy serwisu");
                //            }

                //            //jeœli data zamkniêcia zadania serwiskowego
                //            if (daneZgloszenia.DataZamknieciaZadaniaSerwisowego == null)
                //            {
                //                DataZakonczniaNaliczenia = DateTime.Now;
                //                model.DataZakonczniaNaliczenia = DataZakonczniaNaliczenia;
                //            }
                //            else
                //            {
                //                model.DataZakonczniaNaliczenia = daneZgloszenia.DataZamknieciaZadaniaSerwisowego.Value;
                //            }


                //            //sprawdzenie kiedy nale¿y rozpocz¹æ liczenie czasu
                //            //czy rejestracja odby³a siê w dniu roboczym w godzinach pracy
                //            model.DataRejestracji = daneZgloszenia.DataZgloszenia;
                //            var CzasRejestracji = model.DataRejestracji.Value.ToString("hh:mm");
                //            int DzienRejestracji =(int) daneZgloszenia.DataZgloszenia.Value.DayOfWeek;

                //            //czy kalendarz jest zdefiniowany
                //            //poroba odnalezienia definicji dla danej daty
                //            var kalendarzDni = KalendarzDniRepo.Select(c => c.IdKalendarza == model.kalendarzSla && c.Data == daneZgloszenia.DataZgloszenia.Value.Date);
                //            model.IdDnia = kalendarzDni.Select(c => c.IdDniaWKalendarzu).FirstOrDefault();

                //            //proba odnalezienia definicji dal danego dnia tygodnia
                //            if (model.IdDnia == 0)
                //            {
                //                var dniWKalendarzu = KalendarzDniRepo.Select(c => c.IdKalendarza == model.kalendarzSla && c.NazwaDnia == DzienRejestracji.ToString());
                //                model.IdDnia = dniWKalendarzu.Select(c => c.IdDniaWKalendarzu).First();

                //                //dzien w kalendarzu nie zosta³ odnaleziony - wstawienie wartosci domyslnych
                //                if (model.IdDnia == 0)
                //                {
                //                    Log.Error($"Nie odnaleziono wartoœci domyœlnych dnia w kalendarzu");
                //                }
                //            }

                //            var MozliwaGodzinaRozpoczecia = KalendarzDniRepo.Select(c => c.IdDniaWKalendarzu == model.IdDnia);
                //            var mgr = MozliwaGodzinaRozpoczecia.Select(c => c.GodzinaOd).FirstOrDefault();

                //            var MozliwaGodzinaZakonczenia = KalendarzDniRepo.Select(c => c.IdDniaWKalendarzu == model.IdDnia);
                //            var mgz = MozliwaGodzinaZakonczenia.Select(c => c.GodzinaDo).FirstOrDefault();
                //            if (mgr == null)
                //            {
                //                Log.Error($"Nie odnaleziono mo¿liwej fodziny rozpoczêcia");
                //            }
                //            if (mgz == null)
                //            {
                //                Log.Error($"Nie odnaleziono mo¿liwej godziny zakoñczenia");
                //            }


                //            //okreslenie czasu rozpoczecia naliczenia
                //            if (model.DataRejestracji.Value.TimeOfDay < mgr)
                //            {
                //                //dzien pozostaje bez zmian - przesuniêta zostaje tylko godzina
                //                var CzasRozpoczeciaNaliczania = CzasRejestracji;
                //                var DataRozpoczeciaNaliczania = model.DataRozpoczeciaNaliczania.Date;
                //                Log.Information($"Dzieñ pozostaje bez zmian, przesuniêta zostaje godzina rozpoczêcia");
                //            }
                //            else if (model.DataRejestracji.Value.TimeOfDay > mgr)
                //            {
                //                //poszukiwany nastepny dzien roboczy
                //                if (model.kalendarzSla == 0)
                //                {
                //                    Log.Error($"Nie odnaleziono kalendarza dla kolejnego dnia pracy");
                //                }

                //                if (model.kalendarzSla == null)
                //                { 
                //                    Log.Information($"IdKalendarza nie zosta³o odnalezione");
                //                }

                //                while (true)
                //                {
                //                    //ZgloszenieRejestracjaWyliczNastepnyDzienRoboczy
                //                    //poroba odnalezienia definicji dla kolejnego dnia
                //                    var idDniaWKalendarzu = KalendarzDniRepo.Select(c => c.IdKalendarza == model.kalendarzSla && c.Data == model.DataRejestracji.Value.AddDays(1));
                //                    var idDniaWkalendarzu = idDniaWKalendarzu.Select(c => c.IdDniaWKalendarzu).FirstOrDefault();
                //                    //proba odnalezienia definicji dla danego dnia tygodnia
                //                    if (idDniaWKalendarzu == null)
                //                    {
                //                        var NastepnyDzienTygodnia = model.DataRejestracji.Value.AddDays(1);
                //                        int NastepnyDzien = (int)NastepnyDzienTygodnia.DayOfWeek;
                //                        var IdNastepnegoDniaWKalendarzu = KalendarzDniRepo.Select(c => c.IdKalendarza == model.kalendarzSla && c.NazwaDnia == NastepnyDzien.ToString());
                //                        model.IdDnia = IdNastepnegoDniaWKalendarzu.Select(c => c.IdDniaWKalendarzu).FirstOrDefault();
                //                        //dzien w kalendarzu nie zosta³ odnaleziony - wstawienie wartosci domyslnych
                //                        if (IdNastepnegoDniaWKalendarzu == null)
                //                        {
                //                            Log.Error($"IdDniaWKalendarzu nie zosta³o odnalezione w tabeli KalendarzeDni");
                //                        }
                //                    }
                //                    else
                //                    {
                //                        var RodzajDniaWKalendarzu = KalendarzDniRepo.Select(c => c.IdDniaWKalendarzu == model.IdDnia);
                //                        var rodzajDnia = RodzajDniaWKalendarzu.Select(c => c.RodzajDnia).FirstOrDefault();
                //                        //var DniWKalendzrzu = KalendarzDniRepo.Select(c => c.IdKalendarza == model.kalendarzSla);
                //                        //var rodzajDnia = DniWKalendzrzu.Select(c => c.RodzajDnia).FirstOrDefault();
                //                        if (rodzajDnia == "Roboczy")
                //                        {
                //                            //znaleziony dzien w kalendarzu
                //                            var dataWyliczona = model.DataRejestracji.Value.AddDays(1);
                //                            model.DataWyliczona = dataWyliczona;
                //                        }
                //                        if (model.DataWyliczona == null)
                //                        {
                //                            Log.Error($"DataWyliczona is NULL");
                //                        }
                //                        else
                //                        {
                //                            model.DataRozpoczeciaNaliczania = daneZgloszenia.DataZgloszenia.Value.AddDays(1);
                //                        }
                //                    }
                //                    break;
                //                }
                //            }
                //            else
                //            {
                //                var CzasRozpoczeciaNaliczania = CzasRejestracji;
                //                var DataRozpoczeciaNaliczania = model.DataRozpoczeciaNaliczania.Date;
                //            }

                //            //jesli ten sam dzien to konczymy
                //            var DataNaliczania = model.DataRejestracji.Value.TimeOfDay;
                //            if (model.DataRejestracji.Value.Date == model.DataZakonczniaNaliczenia.Date)
                //            {
                //                model.CzasPrzepracowany = (DataNaliczania - model.DataZakonczniaNaliczenia.TimeOfDay).TotalMinutes;
                //            }

                //            //petla po kolejnych dniach realizacji
                //            //while (DataNaliczania < model.DataZakonczniaNaliczenia)
                //            //{
                //            //    model.CzasPrzepracowany = model.CzasPrzepracowany + (DataNaliczania - model.DataZakonczniaNaliczenia).Minutes;
                //            //}

                //            //CronyWliczonieProcentuWykozystaniaCzasuSLA

                //            var IdSlaKomponenruUslugi = IdSLAKomponentuUslugiRepo.Select(c => c.IdSlaKomponentuUslugi == daneZgloszenia.IdSLAKomponentuUslugi).ToList();
                //            var czasNaRealizacje = IdSlaKomponenruUslugi.Select(c => c.CzasRealizacji).FirstOrDefault();

                //            //[(PozostalyCzasPracy * 100%)/LiczbaGodzinNaRealizacje]-1
                //            int WyliczonyProcetWykorzystaniaCzasu = ((Convert.ToInt32(model.CzasPrzepracowany) * 100) / czasNaRealizacje) ;
                //            Log.Information($"Wyliczono procêt wykorzystania czzasu SLA");

                //            var procentWykorzystaniaCzasu = daneZgloszenia.ProcentWykozystaniaCzasuSLA;
                //            if (WyliczonyProcetWykorzystaniaCzasu != procentWykorzystaniaCzasu || procentWykorzystaniaCzasu == 0)
                //            {
                //                var zgloszeniaPOCO = new ZgloszeniePOCO()
                //                {
                //                    //IdZgloszenia = daneZgloszenia.IdZgloszenia,
                //                    //DataZgloszenia = daneZgloszenia.DataZgloszenia,
                //                    //IdSLA = daneZgloszenia.IdSLA,
                //                    ProcentWykozystaniaCzasuSLA = WyliczonyProcetWykorzystaniaCzasu
                //                };
                //                zgloszeniaRepo.Add(zgloszeniaPOCO);
                //                zgloszeniaRepo.Save();
                //            }
                //        }
                //    }
                //}
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
