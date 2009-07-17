using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3dplayground
{
    interface IModel : ILoadable
    {

        void draw(Vector3 position, Quaternion  rotation, Camera theCamera);


    }
}
