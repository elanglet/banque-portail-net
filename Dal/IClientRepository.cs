using BanqueNET.Models;

namespace BanqueNET.Dal {

    public interface IClientRepository 
    {
        Client GetClientById(long id);

        List<Client> GetAllClients();

        List<Client> GetClientByFirstnameAndLastname(string firstName, string lastName);
     }

}