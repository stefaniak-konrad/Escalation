using EO.Serwis.Portal.DataAccess.Contract.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscalationStatelessService
{
    public class LicznikProcentowegoWykorzystaniaSLA
    {
        public long? Przelicz(long liczbaMinutPomiedzyDatami, long? czasSLA)
        {
            var pozostalyCzas = czasSLA - liczbaMinutPomiedzyDatami;
            
            return pozostalyCzas;
        }
    }
}
