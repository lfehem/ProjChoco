using ProjChoco.Core;
using ProjChoco.Models;

namespace ProjChoco;
public class Program
{


    public static void Main(string[] args)
    {
        Programe();

    }

    public static void Programe()
    {
        try
        {
            BDD bdd = BDD.GetInstance(); 
            CoreSingleton core = CoreSingleton.GetInstance(); 
            CoreModels models = core.CoreModels; 
            CoreGestion gestion = core.CoreGestion; 
            CoreInteraction interaction = core.CoreInteraction; 

            // Creation de la base de données 
            models.CreationBDD(); 
            Console.Clear();

            
            bdd.articles = models.RecupererArticle();
            bdd.administrateurs = models.RecupererAdministrateur();
            bdd.acheteurs = models.RecupererAcheteurs(); 
            bdd.articleAchetes = models.RecupererArticleAchetes();


            Console.WriteLine("Bonjour");

            // mode
            bdd.profil = gestion.ChoixProfil();

            
            // 1 pour mode Administrateur et 2 pour mode Utilisateur
            if (BDD.GetInstance().profil == '1')
            {
                gestion.ProgramAdministrateur();
            }
            else if (BDD.GetInstance().profil == '2')
            {
                gestion.ProgramAcheteur();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
