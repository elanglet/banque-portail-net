using BanqueNET.Models;

namespace BanqueNET.Dal 
{
    public interface ICompteRepository
    {
        Compte GetCompteByNumero(long numero);

        List<Compte> GetComptesByClient(Client client);

        List<Compte> GetComptesDebiteurs();

        void modifierCompte(Compte compte);
    }
}