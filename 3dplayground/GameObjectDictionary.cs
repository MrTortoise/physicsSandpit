using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace _3dplayground
{
    /// <summary>
    /// Singleton class to hold the list of objects.
    /// This will need adapting in the future, but you get the gist.
    /// There will be garbage collection issues.
    /// </summary>
    class GameObjectDictionary
    {
        protected static GameObjectDictionary mInstance;

        protected Dictionary<string, IAmInSpace> mGameObjects;
        protected Dictionary<string, IGetEffectedByField> mFieldObjects;
        protected Dictionary<string, IEmitPointField> mPointFieldEmitters;





        /// <summary>
        /// Cannot be instantiated externally
        /// </summary>
        protected GameObjectDictionary()
        {
            mGameObjects = new Dictionary<string, IAmInSpace>();
            mFieldObjects = new Dictionary<string, IGetEffectedByField>();
            mPointFieldEmitters = new Dictionary<string, IEmitPointField>();
        }

        /// <summary>
        /// Returns the instance of the class.
        /// </summary>
        /// <returns></returns>
        public static  GameObjectDictionary GetInstance()
        {
            if (mInstance = null)
            {
                mInstance = new GameObjectDictionary();
            }
            return mInstance;
        }

        public Dictionary<string, IEmitPointField> PointFieldEmitters
        { get { return mPointFieldEmitters; } }


        public IAmInSpace GetGameObject(string name)
        { return mGameObjects(name); }

        /// <summary>
        /// Adds the object to he collection and any other relevant lists.
        /// </summary>
        /// <param name="theGameObject"></param>
        public void AddGameObject(IAmInSpace theGameObject)
        {
            if (!mGameObjects.ContainsKey(theGameObject.Name))
            {
                mGameObjects.Add(theGameObject.Name, theGameObject);
                // This casts the object, but sets it to null if the cast fails rather than throwing an exception
                IGetEffectedByField effect = theGameObject as IGetEffectedByField;
                if (effect != null)
                {
                    if (!mFieldObjects.ContainsKey(effect.Name))
                    {
                        mFieldObjects.Add(effect.Name, effect);
                    }
                }

                IEmitPointField emitter = theGameObject as IEmitPointField;
                if (emitter != null)
                {
                    if (!mPointFieldEmitters.ContainsKey(emitter.Name))
                    {
                        mPointFieldEmitters.Add(emitter.Name, emitter);

                    }
                }
            }
        }




    }
}
