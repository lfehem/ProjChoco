using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjChoco.Models
{
    //cf cours

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class Path : Attribute
    {
        public string FilePath { get; }

        public Path(string filePath)
        {
            FilePath = filePath;
        }

    }
}
