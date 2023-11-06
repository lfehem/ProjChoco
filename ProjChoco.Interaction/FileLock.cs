using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjChoco.Interaction
{
    public class FileLock
    {
        private static readonly object LockObjectFile = new object();
        private static readonly object LockObject = new object();
        private static FileLock fileLockInstance;

        public static FileLock GetInstance()
        {
            lock (LockObject)
            {
                if (fileLockInstance == null)
                {
                    fileLockInstance = new FileLock();
                }
            }
            return fileLockInstance;
        }

        public object GetLockObjectFile()
        {
            return LockObjectFile;
        }
    }
}
