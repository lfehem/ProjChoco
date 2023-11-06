using ProjChoco.Models;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjChoco.GestionList
{
    public class GestionAcheteur
    {
       
        public Acheteurs CreationAcheteurs()
        {
            Acheteurs acheteur = new Acheteurs();
            bool tmpNom = false;
            bool tmpPrenom = false;
            bool tmpAdresse = false;
            bool tmpTelephone = false;

            Console.WriteLine("Saisir votre Nom, Prénom, Adresse et un Numéro ");
            do
            {
                try
                {
                    if (!tmpNom)
                    {
                        Console.WriteLine("Nom : ");
                        acheteur.Nom = Console.ReadLine();
                        tmpNom = true;

                    }
                    if (!tmpPrenom)
                    {
                        Console.WriteLine("Prenom : ");
                        acheteur.Prenom = Console.ReadLine();
                        tmpPrenom = true;
                    }
                    if (!tmpAdresse)
                    {
                        Console.WriteLine("Adresse : ");
                        acheteur.Adresse = Console.ReadLine();
                        tmpAdresse = true;
                    }
                    if (!tmpTelephone)
                    {
                        Console.WriteLine("Telephone : ");
                        acheteur.Telephone = Console.ReadLine();
                        tmpTelephone = true;
                    }
                }
                catch (ExceptionClass e)
                {
                    Console.WriteLine(e);
                }
            } while (!(tmpNom && tmpPrenom && tmpAdresse && tmpTelephone));
            
            return acheteur;
        }

    }
}
