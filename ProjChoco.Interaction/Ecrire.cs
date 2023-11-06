using Newtonsoft.Json;
using ProjChoco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjChoco.Interaction
{
    public class Ecrire<T> : Exist<T>
    {
        public bool Ecriture(T element)
        {
            lock (FileLock.GetInstance().GetLockObjectFile())
            {
                using (StreamWriter sw = new StreamWriter(new ParentAttributeClass().GetAttribute(typeof(T)), true))
                {
                    string json = JsonConvert.SerializeObject(element);

                    sw.WriteLine(json);
                }

                return true;
            }
        }
    }
}

