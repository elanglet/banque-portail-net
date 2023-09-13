using System;
using System.Collections.Generic;

namespace BanqueNET.Models
{
    public partial class Compte
    {
        public long Numero { get; set; }
        public decimal? Solde { get; set; }
        public long? Idclient { get; set; }

        public virtual Client? IdclientNavigation { get; set; }
    }
}
