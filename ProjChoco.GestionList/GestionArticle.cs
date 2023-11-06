using ProjChoco.Interaction;
using ProjChoco.Models;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjChoco.GestionList
{
    public class GestionArticle
    {
        public Article CreerArticle()
        {
            Article article = new Article();

            Console.WriteLine("Création d'article");

            bool tmpReference = false;
            bool tmpPrix = false;
            do
            {
                try
                {
                    if (!tmpReference)
                    {
                        Console.WriteLine("Reference : ");
                        article.Reference = Console.ReadLine();
                        tmpReference = true;
                    }
                    if (!tmpPrix)
                    {
                        Console.WriteLine("Prix : ");
                        float prix = float.Parse(Console.ReadLine());
                        article.Prix = prix;
                        tmpPrix = true;
                    }
                }
                catch (ExceptionClass e)
                {
                    Console.WriteLine(e);
                }
                catch (FormatException e)
                {
                    Console.WriteLine("erreur format");
                }
            } while (!(tmpReference && tmpPrix));
            return article;
        }

        public bool CreationBDD()
        {
            Ecrire<Article> ecrire = new Ecrire<Article>();
            if (!ecrire.FileExist())
            {
                
                Console.WriteLine("création des articles");
                List<Article> articles = new List<Article>();
                articles.Add(new Article("Coffret Dégustation Grand cru de 54 tablettes & Fruit Déguisés", 35));
                articles.Add(new Article("Côte d’Or ", 4));
                articles.Add(new Article("Milka au lait", 5.30f));
                articles.Add(new Article("Ferrero Rocher",3));
                articles.Add(new Article("Galaxie mini 20g", 3.75f));

                foreach (var article in articles)
                {
                    ecrire.Ecriture(article);
                    Console.WriteLine("Article ajouté" + article);
                }

                return true;
            }
            Console.WriteLine("Le fichier Article.json existe déjà");
            
            return false;
        }
    }
}
