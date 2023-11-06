using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjChoco.Models
{
    //cf cours
    public class ParentAttributeClass
    {
        public string GetAttribute(Type t)
        {
            Path[] MyAttributes = (Path[])Attribute.GetCustomAttributes(t, typeof(Path));

            if (MyAttributes.Length == 0)
            {
                throw new ExceptionClass("Erreur does Not exist  ");
            }
            return MyAttributes[0].FilePath;
        }

    }
}
