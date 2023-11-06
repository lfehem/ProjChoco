using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjChoco.Models
{
    [Path(@"./Data/Administrateur.json")]
    public class Administrateur : ParentAttributeClass
    {
        private Guid id;
        private string login;
        private string password;

        public Administrateur()
        {

            Id = Guid.NewGuid();
        }

        public Administrateur(string login, string password)
        {
            Id = Guid.NewGuid();
            Login = login;
            Password = password;
        }

        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Login
        {
            get { return login; }
            set
            {

                if (value == null || value.Trim().Equals(""))
                {
                    throw new ExceptionClass("Resaisir le login");
                }
                login = value;
            }
        }

        public string Password
        {
            get { return password; }
            set
            {

                if (value == null || value.Trim().Equals(""))
                {
                    // Si le mot de passe est null ou vide
                    throw new ExceptionClass("Resaisir le mot de passe");
                }

                if (value.Length < 6)
                {
                    // Si le mot de passe est inférieur à 6 caractères
                    throw new ExceptionClass("Erreur , Motdepasse");
                }

                
                password = value;
            }
        }
        // to string 
        public override string ToString()
        {
            return "Id : " + id + " / Login : " + login + " / Password : " + password;
        }

        public override bool Equals(object obj)
        {

            if (obj == null)
            {
                return false;
            }
            if (obj is Administrateur administrateur)
            {
                return administrateur.Login.ToLower().Equals(Login.ToLower()) && administrateur.Password.ToLower().Equals(Password.ToLower());
            }
            return false;
        }

    }
}

