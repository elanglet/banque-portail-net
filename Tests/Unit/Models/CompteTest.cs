// On utilise NUnit
using NUnit.Framework;
// On fait référence à la classe Compte à tester
using BanqueNET.Models;

/*
 * Pour faire du filtrage sur les tests on pourrait utiliser la commande suivante : 
 *
 *   dotnet test --filter FullyQualifiedName~Tests.Unit
 *
 * Pour exécuter les tests dont le nom pleinement qualifié contient 'Tests.Unit'
 *  Le nom pleinement qualifié de ce test est 'Tests.Unit.Models.CompteTest'
 */ 

namespace Tests.Unit.Models 
{
    // L'attribut 'TestFixture' permet d'indiquer une classe de test (Test Case).
    [TestFixture]
    public class CompteTest 
    {
        private Compte? compte;

        // Méthode de fixture exécutée avant chaque test
        [SetUp]
        public void SetUp()
        {
            // 1. Créer l'objet à tester
            compte = new Compte();
            compte.Numero = 4569823;
            compte.Solde = Convert.ToDecimal(1000.00);
            compte.Idclient = 1;
        }

        // On déclare cette méthode comme test unitaire.
        [Test]
        public void TestGetNumero()
        {
            // 2. Appeler la méthode à tester -> Récupérer le numéro
            var resultat = compte.Numero;
            
            // 3. Utiliser des assertions
            // ATTENTION à l'ordre des paramètres !
            Assert.AreEqual(4569823, resultat);
        }

        [Test]
        public void TestSetNumero()
        {
            // On positionne une valeur dans Numero
            compte.Numero = 9653287;

            // Assertion...
            Assert.AreEqual(9653287, compte.Numero);
        }

        [Test]
        public void TestGetSolde()
        {
            Assert.AreEqual(1000.00, compte.Solde);
        }

        [Test]
        public void TestSetSolde()
        {
            compte.Solde = Convert.ToDecimal(500.00);
            Assert.AreEqual(500.00, compte.Solde);
        }

        [Test]
        public void TestGetIdClient()
        {
            Assert.AreEqual(1, compte.Idclient);
        }

        [Test]
        public void TestSetIdClient()
        {
            compte.Idclient = 6;
            Assert.AreEqual(6, compte.Idclient);
        }

    }

}
