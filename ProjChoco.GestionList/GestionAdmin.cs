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
    public class GestionAdmin
    {
        public Administrateur CreationAdministrateur()
        {
            Console.WriteLine("Saisir Login et un MotDePasse");
            Administrateur admin = new Administrateur();
            bool tmpLogin = false;
            bool tmpPassword = false;
            do
            {
                try
                {
                    if (!tmpLogin)
                    {
                        Console.WriteLine("Login : ");
                        admin.Login = Console.ReadLine();
                        tmpLogin = true;
                    }
                    if (!tmpPassword)
                    {
                        Console.WriteLine("Password : ");
                        admin.Password = Console.ReadLine();
                        tmpPassword = true;
                    }
                }
                catch (ExceptionClass e)
                {
                    Console.WriteLine(e);
                }
            } while (!(tmpLogin && tmpPassword));
            
            return admin;
        }

        public bool CreationBDD()
        {
            Ecrire<Administrateur> ecrire = new Ecrire<Administrateur>();
            if (!ecrire.FileExist())
            {
               
                Console.WriteLine("La base de données n'existe pas");
                Administrateur admin = new Administrateur("root", "root@12");
                ecrire.Ecriture(admin);
                return true;
            }
            Console.WriteLine("Le fichier Administrateur.json existe déjà");
            return false;
        }
    }
    }

