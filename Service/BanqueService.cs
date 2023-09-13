using BanqueNET.Models;
using BanqueNET.Dal;

namespace BanqueNET.Service
{
    public class BanqueService : IBanqueService
    {
        private IClientRepository _clientRepo; 
        private ICompteRepository _compteRepo;

        public BanqueService(IClientRepository clientRepo, ICompteRepository compteRepo) 
        {
            _clientRepo = clientRepo;
            _compteRepo = compteRepo;
        }

        public Client authentifier(long identifiant, string motDePasse)
        {
            var client = _clientRepo.GetClientById(identifiant);
            if(client != null && client.Motdepasse == motDePasse)
                return client;
            throw new Exception("Erreur d'authentification !");
        }

        public List<Compte> mesComptes(long identifiant) 
        {
            var client = _clientRepo.GetClientById(identifiant);
            return _compteRepo.GetComptesByClient(client);
        }

        public void virementEntreComptes(long numeroCompteADebiter, long numeroCompteACrediter, decimal montant)
        {
            var compteADebiter = _compteRepo.GetCompteByNumero(numeroCompteADebiter);
            var compteACrediter = _compteRepo.GetCompteByNumero(numeroCompteACrediter);

            compteADebiter.Solde = compteADebiter.Solde - montant;
            compteACrediter.Solde = compteACrediter.Solde + montant;

            _compteRepo.modifierCompte(compteADebiter);
            _compteRepo.modifierCompte(compteACrediter);

        }
    }
}