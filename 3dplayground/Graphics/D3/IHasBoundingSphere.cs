using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

namespace _3dplayground.Graphics.D3
{
    interface IHasBoundingSphere
    {
        BoundingSphere  boundingSphere
        { get; }

        float Radius
        { get; }

        Vector3 Center
        { get; }
    }
}
