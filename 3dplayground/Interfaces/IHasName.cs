using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3dplayground
{
    /// <summary>
    /// Implement this interace if an object requires a name - usually used when stored in a dictionary.
    /// </summary>
    public interface IHasName
    {
        string Name
        { get; }    
   
    }
}
