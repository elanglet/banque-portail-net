using BanqueNET.Models;

namespace BanqueNET.Service
{
    public interface IBanqueService 
    {
        Client authentifier(long identifiant, string motDePasse);

        List<Compte> mesComptes(long identifiant);

        void virementEntreComptes(long numeroCompteADebiter, long numeroCompteACrediter, decimal montant);
    }
}