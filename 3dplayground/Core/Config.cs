using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace _3dplayground
{
    /// <summary>
    /// This class will take a xml document / string and parse it into its properties.
    /// The ides is that multiple config files simply get concatonated before being passed in.
    /// </summary>
    static class Config
    {
        #region Constants
        private static double mG =  6.673E-5;
        /// <summary>
        /// This represents the gravitational constant G ... well some value anyway that is used like it.
        /// </summary>
        public static double G
        { get { return mG; } }

        private static float  mTimeScale = 10000000;
        /// <summary>
        /// This is the multiplies by which to divide the number of ticks passed each update.
        /// There are 10,000 ticks in a ms ... maybe we shoudl be using int?
        /// </summary       
        public static float TimeScale 
        {get{return mTimeScale;} }

        private static float mHorizMouseSens =0.1f;
        /// <summary>
        /// This is the in game horizontal mouse sensitivity.
        /// </summary>
        public static float MouseSensHoriz
        { get { return mHorizMouseSens; } }

        private static float mVertMouseSense = 0.1f;         
        /// <summary>
        /// This is the in game Vertical Mouse Sensitivity
        /// </summary>
        public static float MouseSensVert
        { get { return mVertMouseSense; } }


        #endregion

        #region InputMappings

        private static Keys mAccelerate = Keys.W;
        /// <summary>
        /// The key that will cause a ship to accelerate / move forward
        /// </summary>
        public static Keys Accelerate
        { get { return mAccelerate; } }

        private static Keys mDecelerate = Keys.S;
        /// <summary>
        /// The key that will decelerate or move a ship backwards.
        /// </summary>
        public static Keys Decelerate
        { get { return mDecelerate; } }   


        private static Keys mThrustLeft = Keys.A;
        /// <summary>
        /// The key that will accelerate left in direction, not rotation.
        /// </summary>
        public static Keys ThrustLeft
        { get { return mThrustLeft; } }

        private static Keys mThrustRight = Keys.D;
        /// <summary>
        /// The key that will accelerate right in direction, not rotation.
        /// </summary>
        public static Keys ThrustRight
        { get { return mThrustRight; } }

        /// <summary>
        /// This function takes the concatonated config file string and parses it into an xdocument.
        /// It then iterates through the elements and writes to the appropiate value.        /// 
        /// </summary>
        /// <param name="theConfigFile"></param>
        public static void LoadFromString(string theConfigFile)
        {
            //ToDo: Implement loading of the config file and defining of the format 


        }

        #endregion

    }
}
