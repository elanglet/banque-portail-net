namespace BanqueNET.Models
{
    public class AuthentificationModel
    {
        public long Identifiant { get; set; }

        public string MotDePasse { get; set; }

        public bool Error { get; set;}

        public AuthentificationModel()
        {
            Error = false;
            MotDePasse = "";
        }

        public AuthentificationModel(bool error)
        {
            Error = error;
            MotDePasse = "";
        }
    }
}