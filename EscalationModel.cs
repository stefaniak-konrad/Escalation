using EO.Serwis.Portal.DataAccess.Contract.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscalationStatelessService
{
    public class EscalationModel
    {
        //public double CzasPrzepracowany { get; set; }
        //public DateTime DataWyliczona { get; set; }
        //public DateTime DataZakonczniaNaliczenia { get; set; }
        //public DateTime CzasRejestracji { get; set; }
        //public long? kalendarzSla { get; set; }
        //public DateTime? DataRejestracji { get; set; }
        //public long IdDnia { get; set; }
        //public DateTime DataRozpoczeciaNaliczania { get; set; }
        public DateTime Dni { get; set; }
        public int liczbaMinutPomiedzyDatami { get; set; }
    }
}
