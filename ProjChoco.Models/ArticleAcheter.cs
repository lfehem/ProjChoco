using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjChoco.Models
{
    [Path("./Data/ArticleAchetes.json")]
    public class ArticleAchetes : ParentAttributeClass
    {
        // Guid idAcheteur, Guid IdChocolat, Quantité (int), DateAchat (datetime)
        private Guid idAcheteur;
        private Guid idArticle;
        private int quantite;
        private DateTime dateAchat;

        public ArticleAchetes()
        {

            DateAchat = DateTime.Now;
        }

        public ArticleAchetes(Guid idAcheteur, Guid idArticle, int quantite)
        {
            IdAcheteur = idAcheteur;
            IdArticle = idArticle;
            Quantite = quantite;
            DateAchat = DateTime.Now;
        }

        public Guid IdAcheteur
        {
            get { return idAcheteur; }
            set { idAcheteur = value; }
        }

        public Guid IdArticle
        {
            get { return idArticle; }
            set { idArticle = value; }
        }

        public int Quantite
        {
            get { return quantite; }
            set
            {
                if (value < 0)
                {
                    
                    throw new ExceptionClass("Resaisir une quantité");
                }
                quantite = value;
            }
        }


        public DateTime DateAchat
        {
            get { return dateAchat; }
            set
            {
                if (value == null || typeof(DateTime) != value.GetType())
                {
                   
                    throw new ExceptionClass("Resaisir une date");
                }
                dateAchat = value;
            }
        }

        public override string ToString()
        {
            return "IdAcheteur : " + IdAcheteur + " ; IdArticle : " + IdArticle + " ; Quantite : " + Quantite + " ; DateAchat : " + DateAchat;
        }
    }
}
