using BanqueNET.Models;
using BanqueNET.Helpers;

namespace BanqueNET.Dal 
{

    public class ClientRepositoryEF : IClientRepository 
    {
        private DataContext _context;

        public ClientRepositoryEF(DataContext context) 
        {
            _context = context;
        }

        public Client GetClientById(long id) 
        {
            var client = _context.Clients.Find(id);
            if (client == null) throw new KeyNotFoundException("Client introuvable !");
            return client;
        }

        public List<Client> GetAllClients() 
        {
            return _context.Clients.ToList();
        }

        public List<Client> GetClientByFirstnameAndLastname(string firstName, string lastName)
        {
            return _context.Clients.Where(c => c.Prenom == firstName && c.Nom == lastName).ToList();
        }
    }

}