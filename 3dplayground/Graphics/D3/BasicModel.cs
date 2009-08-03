using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace _3dplayground.Graphics.D3
{
    /// <summary>
    /// This class provides basic model funcitonality and can be used as a component or as a basis for inheritence.
    /// i REALLY DO NOT LIKE THIS CLASS OR THE BOUNDED SPHERE / BOX ... THESE ARE NOT MANT TO BE GAME OBJECTS
    /// THIS IS AN INTERFACE HEIRACHY PROBLEM - IT JUST SHOWS THAT I DONT HAVE IT ALL CLEAR IN MY HEAD YET NICE AND EARLY
    /// 
    /// THEN HAVING A NAME IS FREAKING CUMBERSOME.
    /// </summary>
   public  class BasicModel  : IModel 
    {

       protected  Model mModel;
       protected bool mIsDrawActive = true;
       protected string mName;



       public BasicModel(string theName)
       { mName = theName; }

        #region ILoadable Members

        public virtual  void LoadContent(ContentManager theContentManager, string ContentName)
        {
            mModel = theContentManager.Load<Model>(ContentName);
        }

        #endregion



        #region IDrawable Members

        public bool IsDrawActive
        {
            get { return mIsDrawActive; }
        }

        public void Draw(Camera theCamera, Vector3 thePosition, Quaternion theRotation)
        {
            //ToDo: havent implemented rotation ... not really plyed with quaterions before - they are vector 3's with a 4th float.
            Matrix[] transforms = new Matrix[mModel.Bones.Count];
            mModel.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in mModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.View = theCamera.View;
                    effect.Projection = theCamera.Projection;
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(thePosition);
                }
                mesh.Draw();
            }
        }


        #endregion

        #region IHasName Members
        

        public string Name
        {
            get { return mName; }
        }

        #endregion
    }
}
