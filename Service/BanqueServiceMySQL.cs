using BanqueNET.Models;
using BanqueNET.Helpers;
using MySqlConnector;

namespace BanqueNET.Service
{
    public class BanqueServiceMySQL : IBanqueService
    {
        private MySqlConnection _cnx;

        public BanqueServiceMySQL(MySqlConnection cnx) 
        {
            _cnx = cnx;
        }

        public Client authentifier(long identifiant, string motDePasse)
        {
            string motDePasseCrypte = PasswordHelper.hash(Convert.ToString(identifiant), motDePasse);    

            _cnx.Open();
            var cmd = new MySqlCommand("SELECT * FROM client WHERE id=" + identifiant + " AND motdepasse='" + motDePasseCrypte + "'", _cnx);
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
                throw new KeyNotFoundException("Erreur d'authentification !");
            }
        }

        public List<Compte> mesComptes(long identifiant) 
        {
            _cnx.Open();
            var cmd = new MySqlCommand("SELECT * FROM compte WHERE idclient=" + identifiant, _cnx);
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

        public void virementEntreComptes(long numeroCompteADebiter, long numeroCompteACrediter, decimal montant)
        {
            _cnx.Open();
            string debitSql = "SELECT solde FROM compte WHERE numero="+ numeroCompteADebiter +" FOR UPDATE; UPDATE compte SET solde = solde - " +montant+ " WHERE numero="+ numeroCompteADebiter;
            string creditSql = "SELECT solde FROM compte WHERE numero="+ numeroCompteACrediter +" FOR UPDATE; UPDATE compte SET solde = solde + " +montant+ " WHERE numero="+ numeroCompteACrediter;
            
            new MySqlCommand(debitSql, _cnx).ExecuteNonQuery();
            new MySqlCommand(creditSql, _cnx).ExecuteNonQuery();
        }
    }
}