﻿using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjChoco.GestionList
{
    public class SingletonLog 
    {
        public static SingletonLog instance;

        // On crée un objet pour le lock cad que si un thread est en train d'utiliser l'objet, les autres threads doivent attendre
        private static readonly object LockObject = new object();

        public static SingletonLog GetInstance()
        {
            // On le premier appel, on va créer l'instance
            lock (LockObject)
            {
                // On vérifie si l'instance est null si oui on la crée
                if (instance == null)
                {
                    instance = new SingletonLog();
                }
            }
            return instance;
        }
    }
}
