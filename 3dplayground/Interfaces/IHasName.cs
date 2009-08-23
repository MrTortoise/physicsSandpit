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
        /// <summary>
        /// The globally unique name of this object - used for connecting objects at design and load time
        /// </summary>
        string Name
        { get; }

        /// <summary>
        /// This id should be unique globally and also assigned by the GlobalIDgenerator in the objects constructor.
        /// It is used as it will be faster and more reliable than hasing a string - only constant per runtime instance of the game
        /// </summary>
        int ID
        { get; }
   
    }
}
