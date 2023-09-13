using BanqueNET.Models;
using BanqueNET.Helpers;

namespace BanqueNET.Service
{
    public class BanqueServiceEF : IBanqueService
    {
        private DataContext _context;

        public BanqueServiceEF(DataContext context) 
        {
            _context = context;
        }

        public Client authentifier(long identifiant, string motDePasse)
        {
            var client = _context.Clients.Find(identifiant);
            if (client == null) 
                throw new KeyNotFoundException("Client introuvable !");

            if(client != null && client.Motdepasse == motDePasse)
                return client;
            
            throw new Exception("Erreur d'authentification !");
        }

        public List<Compte> mesComptes(long identifiant) 
        {
            return _context.Comptes.Where(c => c.Idclient == identifiant).ToList();
        }

        public void virementEntreComptes(long numeroCompteADebiter, long numeroCompteACrediter, decimal montant)
        {
            var compteADebiter = _context.Comptes.Find(numeroCompteADebiter);
            if (compteADebiter == null) 
                throw new KeyNotFoundException("Compte introuvable !");
            var compteACrediter = _context.Comptes.Find(numeroCompteACrediter);
            if (compteACrediter == null) 
                throw new KeyNotFoundException("Compte introuvable !");

            compteADebiter.Solde = compteADebiter.Solde - montant;
            compteACrediter.Solde = compteACrediter.Solde + montant;

            _context.Comptes.Update(compteADebiter);
            _context.Comptes.Update(compteACrediter);
            _context.SaveChanges();

        }
    }
}