using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _3dplayground.Physics
{
    /// <summary>
    /// Implement this interface to be added to the list of objects that can be effected by fields.
    /// </summary>
    public interface IGetEffectedByGravity : IAmInSpace, IHasMass, ICanMove   
    {
        //I am not sure you even want this. 
        //I guess you need to ask space what the force is on you.

        // we want this object to be an object and be updateable

        //I dont think this interface needs any methods. 
        // I am kinda cheating a bit here ... but look in the game objectDictionary AddGameObject Method.

        //Note, when you add an interface the namespace will be namespace _3dplayground.DIRECTORYNAME 
        //... remember that it will place objects into a different namespace (not a bad thing, but just an organisational thing)

        DisplacementStructure GetGravityDisplacement(GameTime theTime);


    }
}
