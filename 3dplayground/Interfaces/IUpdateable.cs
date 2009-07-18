using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _3dplayground
{
    /// <summary>
    /// This interface exposes the functionality demanded to update an object
    /// </summary>
    public interface IUpdateable  : IHasName 
    {

        void Update(GameTime timeInterval);

        bool IsUpdateActive
        { get; }


    }
}
