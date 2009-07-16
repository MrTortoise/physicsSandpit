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
                                    //lol
    {
        string name;
        void set_name(string new_name) { name = new_name; }

        bool inhabited = false; //i dunno maybe some other stuff - could move in a circle etc instead of phys 

                                //John 16/7: yeah have been considering making planets simpler ... there are larger scale/precision issues as you mentioned that i have kept out of mind so far.
                                // am producing a doc, its a mess right now ... but i will try to keep it up to date with thoughts etc. 
                                // failing that some kind of database but sharing that will be a chore ... besides that sounds like hard work (well no, its easy an will be useful later on for bugs)
        public phys_planet(double new_mass, Vector3 new_speed) { mass = new_mass; speed = new_speed; } //maybe this is in the wrong place
                                                                                                       // I tend to force constructino like this  alot - add certainty. depends if you want to force setting a mass and speed.  

        //I wouldn;t make these public though. I'd write properties. You will thank me if you ever need some valiation code
        public double mass;
        public Vector3 speed;

    }


}
