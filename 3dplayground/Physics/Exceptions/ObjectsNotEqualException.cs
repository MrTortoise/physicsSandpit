using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3dplayground.Physics
{
    public class ObjectsNotEqualException : Exception  
    {

        public ObjectsNotEqualException()
        {

        }

        public ObjectsNotEqualException(string message, Exception e)
         :base( message,e)
        {


        }


    }
}
