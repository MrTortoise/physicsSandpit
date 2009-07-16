using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _3dplayground
{
    interface IGameObject : IAmInSpace, IDrawable, IUpdateable
    {

    }
}
