using Microsoft.Azure.Amqp;
using ProjChoco.GestionList;
using ProjChoco.Interaction;
using ProjChoco.Models;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjChoco.Core
{

    public class CoreGestion
    {
        
        public char ChoixProfil()
        {
            Console.WriteLine("Saisr : \t1 pour Administrateur  \t2 pour Utilisateur");

            bool choixOk = false;
            char choix = '0';
            Console.WriteLine("Votre Choix : ");
            do
            {
                try
                {
                    // Verifier si l'utilisateur a bien rentré un nombre
                    choix = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                    if (choix == '1' || choix == '2')
                    {
                        choixOk = true;
                    }
                    else
                    {
                        Console.WriteLine("Erreur");

                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine(e);
                }
            } while (!choixOk);


            return choix;
        }


        public bool ConnectionAdmin()
        {
            Console.WriteLine("Saisir le Login et  MotDePasse");

            BDD bdd = BDD.GetInstance();
            string login = "";
            bool tmpLogin = false;
            bool tmpPassword = false;
            int nbEssai = 0;
            do
            {
                try
                {
                    if (!tmpLogin)
                    {
                        
                        Console.WriteLine(nbEssai > 0 ? "Login incorrect" : "Login : ");
                        login = Console.ReadLine();

                        tmpLogin = bdd.administrateurs.Exists(x => x.Login == login);
                    }

                    if (!tmpPassword)
                    {
                        
                        Console.WriteLine(nbEssai > 0 ? "Motdepasse incorrect " : "Mot de passe : ");
                        string password = Console.ReadLine();

                        tmpPassword = bdd.administrateurs.Exists(x => x.Password == password);
                    }
                }
                catch (ExceptionClass e)
                {
                    Console.WriteLine(e);
                }

                nbEssai++;
            } while (!(tmpLogin && tmpPassword));


            Console.WriteLine("Connecté " + login + " !");
            return true;
        }

       
        public char ChoixAdmin()
        {
            Console.WriteLine("----------------------------------------------------------------------------------------------");
            Console.WriteLine("1 : Saisir des articles.");
            Console.WriteLine("2 : Créer un fichier txt (format facture) donnant la somme des articles vendus.");
            Console.WriteLine(
                "3 : Créer un fichier txt (format facture) donnant la somme des articles vendus par acheteurs.");
            Console.WriteLine(
                "4 : Créer un fichier txt (format facture) donnant la somme des articles vendus par date d'achat.");
            Console.WriteLine("5 : EXIT");
            Console.WriteLine("----------------------------------------------------------------------------------------------");
            bool choixOk = false;
            char choix = '0';

            do
            {
                Console.WriteLine("Votre choix : ");
                choix = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (choix == '1' || choix == '2' || choix == '3' || choix == '4' || choix == '5')
                {
                    choixOk = true;
                }
                else
                {
                    Console.WriteLine("Erreur");

                }
            } while (!choixOk);


            return choix;
        }

       
        public bool ProgramAdministrateur()
        {
            CoreInteraction interaction = CoreSingleton.GetInstance().CoreInteraction; 

            
            Console.WriteLine("Bienvenue dans le profil administrateur");
            this.ConnectionAdmin();
            bool continuer = true;
            do
            {
                switch (this.ChoixAdmin())
                {
                    case '1':
                        interaction.AjouterArticle();
                        break;
                    case '2':
                       
                        interaction.CreerFichierFactureArticle();
                        break;
                    case '3':
                       
                        interaction.CreerFichierFactureAcheteur();
                        break;
                    case '4':
                       
                        interaction.CreerFichierFactureDate();
                        break;
                    case '5':
                        // Exit
                        
                        continuer = false;
                        return false;
                        break;
                    default:
                        
                        continuer = false;
                        break;
                }
                Console.WriteLine();
            } while (continuer);

            return true;
        }

      //Utilisateur 
       
        private string GetUserInput(string prompt)
        {
            string input = null;
            bool isValid = false;

            do
            {
                if (isValid)
                {
                    Console.WriteLine($"{prompt} incorrect veuillez réessayer");
                }
                else
                {
                    Console.WriteLine(prompt);
                }

                input = Console.ReadLine();
                isValid = !string.IsNullOrWhiteSpace(input);
            } while (!isValid);

            return input;
        }

        
        public Acheteurs ConnectionAcheteur()
        {
            // On demande à l'utilisateur de rentrer son nom, prénom, adresse et téléphone
            Console.WriteLine("Saisir nom, prénom, adresse et téléphone");


            // On crée l'acheteur
            Acheteurs acheteur = new Acheteurs();

            acheteur.Nom = GetUserInput("Nom : ");
            acheteur.Prenom = GetUserInput("Prenom : ");
            acheteur.Adresse = GetUserInput("Adresse : ");
            acheteur.Telephone = GetUserInput("Telephone : ");

            Console.WriteLine();

            
            return InscriptionAcheteur(acheteur);
        }


      
        public Acheteurs InscriptionAcheteur(Acheteurs acheteur)
        {
           
            foreach (var a in BDD.GetInstance().acheteurs)
            {
                if (acheteur.Equals(a))
                {

                    acheteur.Id = a.Id;
                    return acheteur;
                }
            }

            // Sinon on ajoute l'acheteur dans la base de données
            Ecrire<Acheteurs> ecrire = new Ecrire<Acheteurs>();
            ecrire.Ecriture(acheteur);

            return acheteur;
        }

      
        public bool ChoisirArticle(Acheteurs acheteur, List<ArticleAchetes> articlesAchetes)
        {
            char choix = LireCaractereDepuisConsole();
            Console.WriteLine();

            if (choix == 'F')
            {
                
                return false;
            }

            if (choix == 'P')
            {
                AfficherPrixCommande(articlesAchetes);
                return true;
            }

            if (EstChoixArticleValide(choix))
            {
                int quantite = SaisirQuantite();
                AjouterArticleAchete(acheteur, articlesAchetes, choix, quantite);
                return true;
            }

            return true;
        }

        private char LireCaractereDepuisConsole()
        {
            return Console.ReadKey().KeyChar;
        }

        private void AfficherPrixCommande(List<ArticleAchetes> articlesAchetes)
        {
            double prix = CoreSingleton.GetInstance().CoreModels.PrixCommande(articlesAchetes);
            Console.WriteLine("Total de la commande : " + prix);

        }

        private bool EstChoixArticleValide(char choix)
        {
            if (!int.TryParse(choix.ToString(), out int choixInt))
            {
                throw new ExceptionClass("Erreur");
            }

            BDD bdd = BDD.GetInstance();

            if (choixInt <= 0 || choixInt > bdd.articles.Count)
            {
                throw new ExceptionClass("Erreur");
            }

            return true;
        }

        private int SaisirQuantite()
        {
            Console.WriteLine("Quantité : ");
            if (!int.TryParse(Console.ReadLine(), out int quantite) || quantite <= 0)
            {
                throw new ExceptionClass("Erreur");
            }

            return quantite;
        }

        private void AjouterArticleAchete(Acheteurs acheteur, List<ArticleAchetes> articlesAchetes, char choix,
            int quantite)
        {
            int choixInt = int.Parse(choix.ToString());
            Article article = BDD.GetInstance().articles[choixInt - 1];

            ArticleAchetes articleAchete = new ArticleAchetes
            {
                IdArticle = article.Id,
                IdAcheteur = acheteur.Id,
                Quantite = quantite
            };

            BDD.GetInstance().articleAchetes.Add(articleAchete);
            articlesAchetes.Add(articleAchete);

            Ecrire<ArticleAchetes> ecrire = new Ecrire<ArticleAchetes>();
            ecrire.Ecriture(articleAchete);

           
        }


    
        public bool Commander(Acheteurs acheteur)
        {
            Console.WriteLine(" Choisir des articles.");
            List<ArticleAchetes> articlesAchetes = new List<ArticleAchetes>();
            bool continuer = true;
            do
            {
                CoreSingleton.GetInstance().CoreModels.AfficherArticle(BDD.GetInstance().articles);
                Console.WriteLine("F : pour Finir ");
                Console.WriteLine("P : total de la commande");
                
                try
                {
                    continuer = this.ChoisirArticle(acheteur, articlesAchetes);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                Console.WriteLine();
            } while (continuer);

            // On crée le fichier de la facture
            string nomFichier = acheteur.Nom + "-" + acheteur.Prenom + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm") +
                                ".txt";

            
            CoreSingleton.GetInstance().CoreInteraction.RecapCommande(nomFichier, acheteur, articlesAchetes);
           
            return true;
        }

       
        public bool ProgramAcheteur()
        {
           
            Acheteurs profileAcheteur = this.ConnectionAcheteur();
            
            Console.WriteLine("Bonjour " + profileAcheteur.Prenom + " " + profileAcheteur.Nom);
            
            this.Commander(profileAcheteur);
            return true;
        }
    }
}
