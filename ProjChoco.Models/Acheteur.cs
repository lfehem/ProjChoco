using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjChoco.Models
{
    [Path("./Data/Acheteurs.json")]
    public class Acheteurs : ParentAttributeClass
    {
        private Guid id;
        private string nom;
        private string prenom;
        private string adresse;
        private string telephone;

        public Acheteurs()
        {
            id = Guid.NewGuid();
        }

        public Acheteurs(string nom, string prenom, string adresse, string telephone)
        {
            id = Guid.NewGuid();
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            Telephone = telephone;
        }

        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nom
        {
            get { return nom; }
            set
            {
                if (value == null || value.Trim().Equals(""))
                {
                    // Si la valeur est null ou vide
                    throw new ExceptionClass("Resaisir votre nom");
                }
                nom = value;
            }
        }

        public string Prenom
        {
            get { return prenom; }
            set
            {
                if (value == null || value.Trim().Equals(""))
                {
                    // Si la valeur est null ou vide
                    throw new ExceptionClass("Resaisir votre prenom");
                }
                prenom = value;
            }
        }

        public string Telephone
        {
            get { return telephone; }
            set
            {
                if (value == null || value.Trim().Equals(""))
                {
                    // Si le telephone est null ou vide
                    throw new ExceptionClass("Resaisir votre numéro de téléphone");
                }
                if (value.Length != 10)
                {
                    // Si le telephone ne fait pas 10 caractères
                    throw new ExceptionClass("Resaisir le numéro de téléphone (tel>10)");
                }
                telephone = value;
            }
        }
        public string Adresse
        {
            get { return adresse; }
            set
            {
                if (value == null || value.Trim().Equals(""))
                {
                    // Si la valeur est null ou vide
                    throw new ExceptionClass("Resaisir votre adresse");
                }
                adresse = value;
            }
        }

        public override string ToString()
        {
            return $"Id: {id}, Nom: {nom}, Prenom: {prenom}, Adresse: {adresse}, Telephone: {telephone}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is Acheteurs acheteurs)
            {
                return acheteurs.Nom.ToLower().Equals(Nom.ToLower()) && acheteurs.Prenom.ToLower().Equals(Prenom.ToLower()) && acheteurs.Adresse.ToLower().Equals(Adresse.ToLower()) && acheteurs.Telephone.ToLower().Equals(Telephone.ToLower());
            }
            return false;
        }
    }
}


