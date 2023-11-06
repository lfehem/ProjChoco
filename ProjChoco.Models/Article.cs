using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjChoco.Models
{

    [Path("./Data/Article.json")]
    public class Article : ParentAttributeClass
    {
        private Guid id;
        private string reference;
        private float prix;

        public Article()
        {

            Id = Guid.NewGuid();
        }

        public Article(string reference, float prix)
        {
            Id = Guid.NewGuid();
            Reference = reference;
            Prix = prix;
        }

        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Reference
        {
            get { return reference; }
            set
            {
                //si vide
                if (value == null || value.Trim().Equals(""))
                {
                    
                    throw new ExceptionClass("Resaisir une référence");
                }
                reference = value;
            }
        }

        public float Prix
        {
            get { return prix; }
            set
            {

                if (value < 0 || value == null)
                {
                   
                    throw new ExceptionClass("Resaisir un prix");
                }
                prix = value;
            }
        }

        public override string ToString()
        {
            return "Id : " + Id + " Reference : " + reference + " value : " + prix;
        }

        public override bool Equals(object obj)
        {

            if (obj == null)
            {
                return false;
            }
            if (obj is Article article)
            {
                return article.Id == Id;
            }
            return false;
        }
    }
}
