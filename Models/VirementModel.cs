namespace BanqueNET.Models
{
    public class VirementModel
    {
        public long NumeroCompteADebiter { get; set; }
        public long NumeroCompteACrediter { get; set; }
        public decimal Montant { get; set; }

        public IEnumerable<Compte> Comptes { get; }

        public VirementModel(IEnumerable<Compte> comptes)
        {
            Comptes = comptes;
        }

        public VirementModel()
        {
            Comptes = new List<Compte>();
        }
           
    }
}