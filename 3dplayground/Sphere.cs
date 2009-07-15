using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _3dplayground
{
    class Sphere 
    {

        Vector3 mPosition = Vector3.Zero;
        Model mModel;

        public Vector3 Position
        {
            get { return mPosition; }
            set { mPosition = value; }
        }



        public void LoadContent(ContentManager contentManager, string contentName)
        {
            mModel = contentManager.Load<Model>(contentName);
        }

        public void Draw(Camera theCamera)
        {
            Matrix[] transforms = new Matrix[mModel.Bones.Count];
            mModel.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in mModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();

                    effect.View = theCamera.View;
                    effect.Projection = theCamera.Projection;
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(mPosition);
                }
                mesh.Draw();
            }

        }



    }
}
