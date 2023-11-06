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
    public class CoreInteraction
    {
        public CoreInteraction()
        {
            if (!Directory.Exists("./Data/Facture"))
            {
                Directory.CreateDirectory("./Data/Facture");
            }
            if (!Directory.Exists("./Data/RecapCommande"))
            {
                Directory.CreateDirectory("./Data/RecapCommande");
            }
        }

      
        public bool RecapCommandeAcheteur(string filepath, Acheteurs acheteur, List<ArticleAchetes> articlesAchetes)
        {
            File.AppendAllText(filepath, acheteur.Nom + " " + acheteur.Prenom + "\nAdresse : " + acheteur.Adresse + "\nTelephone : " + acheteur.Telephone + "\n");
            File.AppendAllText(filepath, "\n\nArticles achetés : \n");
            File.AppendAllText(filepath, "$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$\n");
            // On parcours les articles achetés
            foreach (ArticleAchetes articleAchete in articlesAchetes)
            {
                // On parcours les articles
                foreach (Article article in BDD.GetInstance().articles)
                {
                    // Si l'article acheté est le même que l'article
                    if (articleAchete.IdArticle == article.Id)
                    {
                        // On ajoute le prix de l'article acheté à la somme
                        File.AppendAllText(filepath, "\t* " + article.Reference + " : " + article.Prix + "€ x " + articleAchete.Quantite + " = " + article.Prix * articleAchete.Quantite + "€\n");
                    }
                }
            }
            File.AppendAllText(filepath, "$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$\n");
            File.AppendAllText(filepath, "total : " + CoreSingleton.GetInstance().CoreModels.PrixCommande(articlesAchetes) + "€\n");
            File.AppendAllText(filepath, "\nDate d'achat : " + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"));
            return true;
        }


      
        public bool AjouterArticle()
        {
            Console.WriteLine("Ajout d'un article à la base de donnée");
           
            Ecrire<Article> ecrire = new Ecrire<Article>();
            ecrire.Ecriture(CoreSingleton.GetInstance().CoreModels.CreationArticle());
            Console.WriteLine("Article ajouté");
            return true;
        }

      
        public bool CreerFichierFactureArticle()
        {
            Console.WriteLine("Création du fichier facture donnant la somme des articles vendus");
            // On créer un fichier avec pour nom la date, l'heure et "_factureArticle.txt"
            string nomFichier = "./Data/Facture/" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + "_factureArticle.txt";
            // On créer le fichier
            File.Create(nomFichier).Close();
            // On récupère les articles achetés
            List<ArticleAchetes> articlesAchetes = BDD.GetInstance().articleAchetes;
            // On récupère les articles
            List<Article> articles = BDD.GetInstance().articles;
            // On parcours les articles achetés
            if (articlesAchetes.Count == 0)
            {
                
                return false;
            }
            foreach (ArticleAchetes articleAchete in articlesAchetes)
            {
                int nbAchete = 0;
                Article article = articles.Find(x => x.Id == articleAchete.IdArticle);
                if (article == null)
                {
                    
                    return false;
                }
                nbAchete += articleAchete.Quantite;
                // On ajoute le prix de l'article acheté à la somme
                File.AppendAllText(nomFichier, article.Reference + " : " + article.Prix + "€ x " + nbAchete + " = " + article.Prix * nbAchete + "€\n");
            }
            return true;
        }

        
        public bool CreerFichierFactureAcheteur()
        {
            Console.WriteLine("Création du fichier facture donnant la somme des articles vendus par acheteurs");
            // On créer un fichier avec pour nom la date, l'heure et "_factureAcheteur.txt"
            string nomFichier = "./Data/Facture/" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + "_factureAcheteur.txt";
            // On créer le fichier
            File.Create(nomFichier).Close();
            // On récupère les articles achetés
            List<ArticleAchetes> articlesAchetes = BDD.GetInstance().articleAchetes;
            // On récupère les acheteurs
            List<Acheteurs> acheteurs = BDD.GetInstance().acheteurs;
            // On trie les articles achetés par acheteurs dans un dictionnaire
            Dictionary<Acheteurs, List<ArticleAchetes>> dico = new Dictionary<Acheteurs, List<ArticleAchetes>>();
            foreach (ArticleAchetes articleAchete in articlesAchetes)
            {
                Acheteurs tmp = acheteurs.Find(x => x.Id == articleAchete.IdAcheteur);
                if (dico.ContainsKey(tmp))
                {
                    dico[tmp].Add(articleAchete);
                }
                else
                {
                    dico.Add(tmp, new List<ArticleAchetes>());
                    dico[tmp].Add(articleAchete);
                }
            }
            // On parcours les acheteurs
            foreach (KeyValuePair<Acheteurs, List<ArticleAchetes>> acheteur in dico)
            {
                this.RecapCommandeAcheteur(nomFichier, acheteur.Key, acheteur.Value);
                File.AppendAllText(nomFichier, "\n\n");
            }

            return true;
        }

        public bool CreerFichierFactureDate()
        {
            Console.WriteLine("Création du fichier facture donnant la somme des articles vendus par date d'achat");
            // On créer un fichier avec pour nom la date, l'heure et "_factureDate.txt"
            string nomFichier = "./Data/Facture/" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + "_factureDate.txt";
            // On créer le fichier
            File.Create(nomFichier).Close();
            // On récupère les articles achetés
            List<ArticleAchetes> articlesAchetes = BDD.GetInstance().articleAchetes;
            // On trie les articles achetés par date d'achat dans un dictionnaire
            Dictionary<string, List<ArticleAchetes>> dico = new Dictionary<string, List<ArticleAchetes>>();
            foreach (ArticleAchetes articleAchete in articlesAchetes)
            {
                string tmp = articleAchete.DateAchat.ToString("dd-MM-yyyy");
                if (dico.ContainsKey(tmp))
                {
                    dico[tmp].Add(articleAchete);
                }
                else
                {
                    dico.Add(tmp, new List<ArticleAchetes>());
                    dico[tmp].Add(articleAchete);
                }
            }
            // On parcours les dates
            foreach (KeyValuePair<string, List<ArticleAchetes>> date in dico)
            {
                File.AppendAllText(nomFichier, "Date d'achat : " + date.Key + "\n");
                File.AppendAllText(nomFichier, "----------------------------------------\n");
                // On parcours les articles achetés
                foreach (ArticleAchetes articleAchete in date.Value)
                {
                    // On parcours les articles
                    foreach (Article article in BDD.GetInstance().articles)
                    {
                        // Si l'article acheté est le même que l'article
                        if (articleAchete.IdArticle == article.Id)
                        {
                            // On ajoute le prix de l'article acheté à la somme
                            File.AppendAllText(nomFichier, "\t* " + article.Reference + " : " + article.Prix + "€ x " + articleAchete.Quantite + " = " + article.Prix * articleAchete.Quantite + "€\n");
                        }
                    }
                }
                File.AppendAllText(nomFichier, "----------------------------------------\n");
                File.AppendAllText(nomFichier, "Prix total : " + CoreSingleton.GetInstance().CoreModels.PrixCommande(date.Value) + "€\n");
                File.AppendAllText(nomFichier, "\n\n");
            }
            return true;
        }


       
       
        public bool RecapCommande(string filename, Acheteurs acheteur, List<ArticleAchetes> articlesAchetes)
        {
            // On vérifie si le dossier de l'acheteur existe
            string filepath = "./Data/RecapCommande/" + acheteur.Nom + "-" + acheteur.Prenom;
            if (!Directory.Exists(filepath))
            {
                // Si le dossier n'existe pas, on le créer
                Directory.CreateDirectory(filepath);
            }
            // On créer le fichier
            filepath = filepath + "/" + filename + ".txt";
            this.RecapCommandeAcheteur(filepath, acheteur, articlesAchetes);
            return true;
        }
    }
}
