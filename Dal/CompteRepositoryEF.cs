using BanqueNET.Models;
using BanqueNET.Helpers;

namespace BanqueNET.Dal
{
    public class CompteRepositoryEF : ICompteRepository
    {    
        private DataContext _context;

        public CompteRepositoryEF(DataContext context) 
        {
            _context = context;
        }

        public Compte GetCompteByNumero(long numero)
        {
            var compte = _context.Comptes.Find(numero);
            if (compte == null) throw new KeyNotFoundException("Compte introuvable !");
            return compte;
        }

        public List<Compte> GetComptesByClient(Client client)
        {
            return _context.Comptes.Where(c => c.IdclientNavigation == client).ToList();
        }

        public List<Compte> GetComptesDebiteurs()
        {
            return _context.Comptes.Where(c => c.Solde < 0).ToList();
        }

        public void modifierCompte(Compte compte)
        {
            _context.Comptes.Update(compte);
            _context.SaveChanges();
        }
    }
}