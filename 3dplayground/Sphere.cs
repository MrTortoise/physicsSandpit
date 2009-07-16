using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _3dplayground
{
    public class Sphere 
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


    public class phys_planet : Sphere// damn multiple inhertiance being locked out! c++ is ftw c# for the suq
    {
        string name;
        void set_name(string new_name) { name = new_name; }

        bool inhabited = false; //i dunno maybe some other stuff - could move in a circle etc instead of phys 
        public phys_planet(double new_mass, Vector3 new_speed) { mass = new_mass; speed = new_speed; } //maybe this is in the wrong place

        public double mass;
        public Vector3 speed;
        public Vector3 old_position = Vector3.Zero;

        public Vector3 K1pos = Vector3.Zero;
        public Vector3 K2pos = Vector3.Zero;
        public Vector3 K3pos = Vector3.Zero;
        public Vector3 k4pos = Vector3.Zero;

        public Vector3 K1vel = Vector3.Zero;
        public Vector3 K2vel = Vector3.Zero;
        public Vector3 K3vel = Vector3.Zero;
        public Vector3 k4vel = Vector3.Zero;
    }


}
