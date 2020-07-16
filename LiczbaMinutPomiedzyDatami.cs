using EO.Serwis.Portal.DataAccess.Contract.Repositories;
using EO.Serwis.Portal.DataAccess.Repositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EscalationStatelessService
{
    public class LiczbaMinutPomiedzyDatami
    {

        public long WyliczLiczbeMinutPomiedzyDatamizKalendarza(DateTime dataPoczatkowa, DateTime dataKoncowa, long idKalendarza, IKalendarzeDniRepository KalendarzDniRepo)
        {
            EscalationModel model = new EscalationModel();
            model.liczbaMinutPomiedzyDatami = 0;
            //Funkcja musi pobrać kalendarz z bazy na podstawia idKalendarza. W kalendarzu powinny znjdować sie wszystkie dni z tabeli [KalendarzeDni]

            var kalendarz = KalendarzDniRepo.Select(c => c.IdKalendarza == idKalendarza).ToList();
            // Powstaje lista liczbaMinutNaDzienRoboczy (pref. typ Dictionaray<string, int>)
            Dictionary<string, int> liczbaMinutNaDzienRoboczy = new Dictionary<string, int>();
            foreach (var DzienRoboczy in kalendarz)
            {
                if(DzienRoboczy.RodzajDnia != "Roboczy")
                {
                    continue;
                }
                //Funkcja musi wyliczyć listę minut dla każdego dnia roboczego z kalendarza( RodzajDnia==Roboczy) i przechować ją w dodatkowej liście ze wskazaniem na dzien tygodnia
                liczbaMinutNaDzienRoboczy.Add(DzienRoboczy.NazwaDnia, Convert.ToInt32(DzienRoboczy.GodzinaDo.Value.TotalMinutes - DzienRoboczy.GodzinaOd.Value.TotalMinutes));
            }
            Log.Information($"Utworzono Dictionary liczbaMinutNaDzienRoboczy");

            //Funkcja musi pobrać do pamięci listę wszystkich dni wolnych z kalendarza(RodzajDnia==Wolny). Powstaje lista dniWolne.
            Dictionary<long, DateTime?> DniWolne = new Dictionary<long, DateTime?>();
            foreach (var DzienWolny in kalendarz)   
            {
                if(DzienWolny.RodzajDnia == "Roboczy")
                {
                    continue;
                }
                else
                {
                    DniWolne.Add(DzienWolny.IdDniaWKalendarzu, DzienWolny.Data);
                }
            }
            Log.Information($"Utworzono Dictionary DniWolne");

            // Powstaje listaDniPomiedzyDatami
            var listaDniPomiedzyDatami = new List<EscalationModel>();
            listaDniPomiedzyDatami.Add(new EscalationModel() { Dni = dataPoczatkowa });
            var DniPomiedzyDatami = (dataKoncowa.Date - dataPoczatkowa.Date).Days;
            //Funkcja musi wygenerować listę dni pomiędzy dataPoczatkowa i dataKoncowa i zapisać te dni w liście
            for (int i=1; i < DniPomiedzyDatami; i++)
            {
                var x = listaDniPomiedzyDatami.Select(c => c.Dni).Last();
                listaDniPomiedzyDatami.Add(new EscalationModel() { Dni = x.Date.AddDays(1) });
            }
            listaDniPomiedzyDatami.Add(new EscalationModel() { Dni = dataKoncowa });
            Log.Information($"Dni pomiędzy datą rejestracji a aktualną datą zostały dodane do listy");

            // Funkcja musi przeiterować po listaDniPomiedzyDatami i wykonać następującą operację:
            foreach (var data in listaDniPomiedzyDatami)
            {
                var dzienWolny = DniWolne.SingleOrDefault(c => c.Value == data.Dni.Date).Value;
                var dzienRoboczy = liczbaMinutNaDzienRoboczy.SingleOrDefault(c => c.Key == ((int)data.Dni.DayOfWeek).ToString());
                //sprawdzić czy data znajduje się na liście dniWolne, jeżeli się znajduje kończymy daną iterację(sprawdź "foreach continue"), sprawdzić jakim dzinem tygodnia jest jest dzień iteracji
                if (data.Dni.Date == dzienWolny)
                {
                    continue;
                }
                if (((int)data.Dni.Date.DayOfWeek).ToString() != dzienRoboczy.Key)//6 || (int)data.Dni.Date.DayOfWeek == 0) 
                {
                    continue;
                }

                //sprawdzić jakim dniem tygodnia jest dzień aktualnej iteracji(DateTime.DayOfTheWeek) i powrównać go do Nazwa dnia z dniWolne: transformacja jest następująca(1 = Monday, 2 = Tuesday, itd.)
                //if((int)data.Dni.DayOfWeek == (int)dzienWolny.Value.DayOfWeek)
                //jeżeli dzien aktualnej iteracji jest dniem wolnym kończymy daną iterację(sprawdź "foreach continue")
                //{
                //    continue;
                //}

                //system wyszukuje na liście liczbaMinutNaDzienRoboczy liczbe minut dla dnia tygodnia aktualnej iteracji
                var czasPracyNaDzien = liczbaMinutNaDzienRoboczy.Single(c => c.Key == ((int)data.Dni.DayOfWeek).ToString());
                var godzinaPracyDo = kalendarz.Single(c =>c.NazwaDnia == ((int)data.Dni.DayOfWeek).ToString());
                if(data.Dni.TimeOfDay.TotalMinutes == 0)
                {
                    model.liczbaMinutPomiedzyDatami = model.liczbaMinutPomiedzyDatami + czasPracyNaDzien.Value;
                }
                else
                {
                    if(data.Dni.TimeOfDay.TotalHours > 17)
                    {
                        continue;
                    }
                    else if(data.Dni.TimeOfDay.TotalHours < 8)
                    {
                        model.liczbaMinutPomiedzyDatami = model.liczbaMinutPomiedzyDatami + 540;
                    }
                    else if(data.Dni == dataKoncowa)
                    {
                        var czas1 = (godzinaPracyDo.GodzinaDo.Value.TotalMinutes - data.Dni.TimeOfDay.TotalMinutes);
                        int pracaWDniu = czasPracyNaDzien.Value - Convert.ToInt32(czas1);

                        if (dataKoncowa.Date == dataPoczatkowa.Date)
                        {
                            model.liczbaMinutPomiedzyDatami = Convert.ToInt32( data.Dni.TimeOfDay.TotalMinutes - dataPoczatkowa.TimeOfDay.TotalMinutes );
                        }
                        else
                        {
                            model.liczbaMinutPomiedzyDatami = model.liczbaMinutPomiedzyDatami + pracaWDniu;
                        }
                    }
                    else
                    {
                        var CzasPracyNaDzien = (godzinaPracyDo.GodzinaDo.Value.TotalMinutes - data.Dni.TimeOfDay.TotalMinutes);
                        //system dodaje wyszukaną liczbę minut do zmiennej liczbaMinutPomiedzyDatami
                        model.liczbaMinutPomiedzyDatami = model.liczbaMinutPomiedzyDatami + Convert.ToInt32(CzasPracyNaDzien);
                    }
                }
            }
            return model.liczbaMinutPomiedzyDatami;
        }
    }
}
