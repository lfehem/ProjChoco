using Newtonsoft.Json;
using ProjChoco.Models;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjChoco.Interaction
{
    public class Lecture<T> : Exist<T>
    {
        public List<T> LectureFichier()
        {
            if (FileExist())
            {
                lock (FileLock.GetInstance().GetLockObjectFile())
                {
                    List<T> elements = new List<T>();
                    using (StreamReader srial = new StreamReader(new ParentAttributeClass().GetAttribute(typeof(T))))
                    {
                        string line;
                       
                        while ((line = srial.ReadLine()) != null)
                        {
                            T element = JsonConvert.DeserializeObject<T>(line);
                            
                            elements.Add(element);
                        }
                       
                    }

                    return elements;
                }
            }
           
            return new List<T>();
        }
    }
}
