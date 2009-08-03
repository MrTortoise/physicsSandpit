using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3dplayground.Physics
{

   public  interface IHasMass 
    {
        int Mass
        { get; }

       // The logic is that mass is only importnat if movement is involved.
    }
}
