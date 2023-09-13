using System;
using System.Collections.Generic;

namespace BanqueNET.Models
{
    public partial class Client
    {
        public Client()
        {
            Comptes = new HashSet<Compte>();
        }

        public long Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Adresse { get; set; }
        public string? Codepostal { get; set; }
        public string? Ville { get; set; }
        public string? Motdepasse { get; set; }

        public virtual ICollection<Compte> Comptes { get; set; }
    }
}
