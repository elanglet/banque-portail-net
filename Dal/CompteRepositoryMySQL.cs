using BanqueNET.Models;
using MySqlConnector;

namespace BanqueNET.Dal
{
    public class CompteRepositoryMySQL : ICompteRepository
    {    
        private MySqlConnection _cnx;

        public CompteRepositoryMySQL(MySqlConnection cnx) 
        {
            _cnx = cnx;
        }

        public Compte GetCompteByNumero(long numero)
        {
            _cnx.Open();
            var cmd = new MySqlCommand("SELECT * FROM compte WHERE numero=" + numero, _cnx);
            var reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                var compte = new Compte();
                compte.Numero = Convert.ToInt64(reader["numero"]);
                compte.Solde = Convert.ToDecimal(reader["solde"]);
                compte.Idclient = Convert.ToInt64(reader["idclient"]);
                
                return compte;
            }
            else 
            {
                throw new KeyNotFoundException("Compte introuvable !");
            }
        }

        public List<Compte> GetComptesByClient(Client client)
        {
            _cnx.Open();
            var cmd = new MySqlCommand("SELECT * FROM compte WHERE idclient=" + client.Id, _cnx);
            var reader = cmd.ExecuteReader();
            List<Compte> listeCompte = new List<Compte>();
            while(reader.Read())
            {
                var compte = new Compte();
                compte.Numero = Convert.ToInt64(reader["numero"]);
                compte.Solde = Convert.ToDecimal(reader["solde"]);
                compte.Idclient = Convert.ToInt64(reader["idclient"]);

                listeCompte.Add(compte);
            }
            return listeCompte;
        }

        public List<Compte> GetComptesDebiteurs()
        {
            _cnx.Open();
            var cmd = new MySqlCommand("SELECT * FROM compte WHERE solde < 0", _cnx);
            var reader = cmd.ExecuteReader();
            List<Compte> listeCompte = new List<Compte>();
            while(reader.Read())
            {
                var compte = new Compte();
                compte.Numero = Convert.ToInt64(reader["numero"]);
                compte.Solde = Convert.ToDecimal(reader["solde"]);
                compte.Idclient = Convert.ToInt64(reader["idclient"]);

                listeCompte.Add(compte);
            }
            return listeCompte;
        }

        public void modifierCompte(Compte compte)
        {
            _cnx.Open();
            var cmd = new MySqlCommand("UPDATE compte SET solde=" + compte.Solde + " WHERE numero=" + compte.Numero, _cnx);
            cmd.ExecuteNonQuery();
        }
    }
}