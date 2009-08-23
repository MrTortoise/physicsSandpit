using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3dplayground.Physics
{
    /// <summary>
    /// It is somewhat arbitrary, but this is th einterface to implement if collisions are to be considered.
    /// Basically not all objects that implement ICanMove will want collisions, they require some element of physical presence.
    /// Later this may just turn into a bool flag on this object.
    /// </summary>
    public interface IPhysicsObject : IGameObject, IHasMass, ICanMove 
    {

        // 

    }
}
