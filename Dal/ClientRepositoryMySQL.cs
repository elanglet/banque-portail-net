using BanqueNET.Models;
using MySqlConnector;

namespace BanqueNET.Dal 
{

    public class ClientRepositoryMySQL : IClientRepository 
    {
        private MySqlConnection _cnx;

        public ClientRepositoryMySQL(MySqlConnection cnx) 
        {
            _cnx = cnx;
        }

        public Client GetClientById(long id) 
        {
            _cnx.Open();
            var cmd = new MySqlCommand("SELECT * FROM client WHERE id=" + id, _cnx);
            var reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                var client = new Client();
                client.Id = Convert.ToInt64(reader["id"]);
                client.Nom = reader["nom"].ToString();
                client.Prenom = reader["prenom"].ToString();
                client.Adresse = reader["adresse"].ToString();
                client.Codepostal = reader["codepostal"].ToString();
                client.Ville = reader["ville"].ToString();
                client.Motdepasse = reader["motdepasse"].ToString();

                return client;
            }
            else 
            {
                throw new KeyNotFoundException("Client introuvable !");
            }
        }

        public List<Client> GetAllClients() 
        {
            _cnx.Open();
            var cmd = new MySqlCommand("SELECT * FROM client", _cnx);
            var reader = cmd.ExecuteReader();
            List<Client> listeClients = new List<Client>();
            while(reader.Read())
            {
                var client = new Client();
                client.Id = Convert.ToInt64(reader["id"]);
                client.Nom = reader["nom"].ToString();
                client.Prenom = reader["prenom"].ToString();
                client.Adresse = reader["adresse"].ToString();
                client.Codepostal = reader["codepostal"].ToString();
                client.Ville = reader["ville"].ToString();
                client.Motdepasse = reader["motdepasse"].ToString();

                listeClients.Add(client);
            }
            return listeClients;
        }

        public List<Client> GetClientByFirstnameAndLastname(string firstName, string lastName)
        {
            _cnx.Open();
            var cmd = new MySqlCommand("SELECT * FROM client WHERE nom='" + lastName + "' AND prenom='" + firstName + "'", _cnx);
            var reader = cmd.ExecuteReader();
            List<Client> listeClients = new List<Client>();
            while(reader.Read())
            {
                var client = new Client();

                client.Id = Convert.ToInt64(reader["id"]);
                client.Nom = reader["nom"].ToString();
                client.Prenom = reader["prenom"].ToString();
                client.Adresse = reader["adresse"].ToString();
                client.Codepostal = reader["codepostal"].ToString();
                client.Ville = reader["ville"].ToString();
                client.Motdepasse = reader["motdepasse"].ToString();

                listeClients.Add(client);
            }
            return listeClients;        
        }
    }
}