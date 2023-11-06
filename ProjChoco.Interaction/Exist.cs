using ProjChoco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjChoco.Interaction
{
    public class Exist<T>
    {

        public bool FileExist()
        {
            lock (FileLock.GetInstance().GetLockObjectFile())
            {
                return File.Exists(new ParentAttributeClass().GetAttribute(typeof(T)));
            }
        }

        public bool CreateFile()
        {
            lock (FileLock.GetInstance().GetLockObjectFile())
            {
                File.Create(new ParentAttributeClass().GetAttribute(typeof(T)));
            }
            return true;
        }
    }
}
