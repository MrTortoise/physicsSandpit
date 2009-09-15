using System;
using System.Collections.Generic;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace _3dplayground.EventManagement
{ 
	 public class EventManager
	{
		#region Fields
		private static  EventManager mInstance;

		private MouseState mOldMouseState;
		private MouseState mCurrentMouseState;

		private KeyboardState mOldKeyboardState;
		private KeyboardState mCurrentKeyboardState;

		private List<Keys> mPressedKeys = new List<Keys>();
		private List<Keys> mHeldKeys = new List<Keys>();
		private List<Keys> mDepressedKeys = new List<Keys>();

		private MouseButtonState mLeftMouseButton;
		private MouseButtonState mMiddleMouseButton;
		private MouseButtonState mRightMouseButton;

		//private int mNewScrolledDistance;

		private int mOldMouseScrollWheel;
		private int mCurrentMouseScrollWheel;

		private Vector2 mCurrentMousePosition;
		private Vector2 mOldMousePosition;
		
		#endregion

		#region Constructor

		protected  EventManager()
		{


		}

		public static EventManager GetInstance()
		{
			if (mInstance == null)
			{ mInstance = new EventManager(); }

			return mInstance;
		}


		#endregion



		#region Methods

		public void ProcessInput()
		{
			mOldKeyboardState = mCurrentKeyboardState;
			mOldMouseState = mCurrentMouseState;

			mCurrentKeyboardState = Keyboard.GetState();
			mCurrentMouseState = Mouse.GetState();

			FindKeyActions();
			FindMouseActions();
			RaiseInputEvents();

		}
		protected void RaiseInputEvents()
		{
			// Need to raise the  mouse click event first as that controls control focus events
			

			if ((mMiddleMouseButton == MouseButtonState.Pressed) ||
				(mLeftMouseButton == MouseButtonState.Pressed) ||
				(mRightMouseButton == MouseButtonState.Pressed))
			{
				//RaiseEvent(AttemptedFocusEvent);
				RaiseEvent(MouseButtonPressed);
			}	 		

			//  mouse button depress  - controls exit for mouse button held events

			if ((mMiddleMouseButton == MouseButtonState.Depressed) ||
				(mLeftMouseButton == MouseButtonState.Depressed) ||
				(mRightMouseButton == MouseButtonState.Depressed))
			{
				RaiseEvent(MouseButtonReleased);
			}	

			if (mPressedKeys.Count != 0)
				RaiseEvent(KeyPressed);

			if (mDepressedKeys.Count !=	0)
				RaiseEvent(KeyReleased);   

			if (mCurrentMousePosition != mOldMousePosition)
				RaiseEvent(MousePositionChanged);

			if (mOldMouseScrollWheel !=	mCurrentMouseScrollWheel)
				RaiseEvent(MouseWheelScrolled);
		
		}
		protected void FindMouseActions()
		{
			#region	mouse
			//parse	the	mouse
			// have	to do each contorl seperatley

			if (mCurrentMouseState.LeftButton == ButtonState.Pressed)
			{
				if ((mLeftMouseButton == MouseButtonState.Depressed) || (mLeftMouseButton==MouseButtonState.Up))
				{
					mLeftMouseButton = MouseButtonState.Pressed;
				}
				else
				{
					mLeftMouseButton = MouseButtonState.Held;
				}
			}
			else
			{
				if ((mLeftMouseButton == MouseButtonState.Held) ||((mLeftMouseButton == MouseButtonState.Pressed)))
				{
					mLeftMouseButton = MouseButtonState.Depressed;
				}
				else if (mLeftMouseButton == MouseButtonState.Depressed)
				{ mLeftMouseButton = MouseButtonState.Up; }
			}

			if (mCurrentMouseState.MiddleButton == ButtonState.Pressed)
			{
				if ((mMiddleMouseButton == MouseButtonState.Depressed) || (mMiddleMouseButton==MouseButtonState.Up))
				{
					mMiddleMouseButton = MouseButtonState.Pressed;
				}
				else
				{
					mMiddleMouseButton = MouseButtonState.Held;
				}
			}
			else
			{
				if ((mMiddleMouseButton == MouseButtonState.Held) || (mMiddleMouseButton == MouseButtonState.Pressed))
				{
					mMiddleMouseButton = MouseButtonState.Depressed;
				}
				else if (mMiddleMouseButton == MouseButtonState.Depressed)
				{ mMiddleMouseButton = MouseButtonState.Up; }
			}

			if (mCurrentMouseState.RightButton == ButtonState.Pressed)
			{
				if ((mRightMouseButton == MouseButtonState.Depressed) || (mRightMouseButton==MouseButtonState.Up))
				{
					mRightMouseButton = MouseButtonState.Pressed;
				}
				else
				{
					mRightMouseButton = MouseButtonState.Held;
				}
			}
			else
			{
				if ((mRightMouseButton == MouseButtonState.Held) || (mRightMouseButton == MouseButtonState.Pressed))
				{
					mRightMouseButton = MouseButtonState.Depressed;
				}
				else if (mRightMouseButton == MouseButtonState.Depressed)
				{ mRightMouseButton = MouseButtonState.Up; }


			}

			mOldMousePosition = mCurrentMousePosition;
			mCurrentMousePosition = new Vector2(mCurrentMouseState.X, mCurrentMouseState.Y);

			mOldMouseScrollWheel = mCurrentMouseScrollWheel;
			mCurrentMouseScrollWheel = mCurrentMouseState.ScrollWheelValue;

			#endregion
		}
		protected void FindKeyActions()
		{
			#region Keys
			// new key press is one that is not in held or new key presses
			List<Keys> heldKeys = new List<Keys>();
			List<Keys> newPressedKeys = new List<Keys>();
			List<Keys> depressedKeys = new List<Keys>();


			foreach (Keys k in mCurrentKeyboardState.GetPressedKeys())
			{
				// Test if key was already being held - if so add to new held list
				// if not then test is in the last pressed list - if so add to held list
				// if not then it is a new key press
				if (mHeldKeys.Contains(k))
				{
					heldKeys.Add(k);
				}
				else if (mPressedKeys.Contains(k))
				{
					heldKeys.Add(k);
				}
				else
				{
					newPressedKeys.Add(k);
				}
			}
			//need a list of all the old jkeys that were pressed in order to 
			//compare to current pressed keys
			// this will tell us which keys are now newly depressed
			List<Keys> allOldKeyPress = new List<Keys>();

			foreach (Keys k in mHeldKeys)
			{
				allOldKeyPress.Add(k);
			}
			foreach (Keys k in mPressedKeys)
			{
				allOldKeyPress.Add(k);
			}

			// if the oldKeysPressed contains a key that is not in current keys pressed
			// then we know its a newly depressed key
			// if its not held or newly pressed, then its depressed
			// if not currently presed then dd to depressed
			foreach (Keys k in allOldKeyPress)
			{
				bool Contains = false;
				foreach (Keys cK in mCurrentKeyboardState.GetPressedKeys())
				{
					if (cK == k)
						Contains = true;
				}
				if (Contains == false)
				{
					depressedKeys.Add(k);
				} 
			}

			mPressedKeys = newPressedKeys;
			mHeldKeys = heldKeys;
			mDepressedKeys = depressedKeys;
			#endregion		

		}		
			

		
		#endregion


		#region Events

		/// <summary>
		/// It is up to the reciever of this event to manage held buttons
		/// </summary>
		public EventHandler<InputEventArgs> MouseButtonPressed;
		//public EventHandler<FocusMessageArgs> AttemptedFocusEvent;
		/// <summary>
		/// It is up to the reciever of this event to manage held buttons
		/// </summary>
		public EventHandler<InputEventArgs> MouseButtonReleased;

		public EventHandler<InputEventArgs> MouseWheelScrolled;
		public EventHandler<InputEventArgs> MousePositionChanged;

		/// <summary>
		/// It is up to the reciever of this event to manage held buttons
		/// </summary>
		public EventHandler<InputEventArgs> KeyPressed;
		/// <summary>
		/// It is up to the reciever of this event to manage held buttons
		/// </summary>
		public EventHandler<InputEventArgs> KeyReleased;

		

		#region events Methods

		/// <summary>
		/// generic threadsafe way to raise an event
		/// </summary>
		/// <param name="theEvent"></param>
		protected void RaiseEvent(EventHandler<InputEventArgs> theEvent)
		{
			//Copy to be threadsafe
			EventHandler<InputEventArgs> temp = theEvent;
			if (temp != null)
				temp(this, CreateInputEventArgs());
		}

		/*protected void RaiseEvent(EventHandler<FocusMessageArgs> theEvent)
		{
			//Copy to be threadsafe
			EventHandler<FocusMessageArgs> temp = theEvent;
			if (temp != null)
			{
				FocusMessageArgs args = new FocusMessageArgs(
					new Vector2(mCurrentMousePosition.X,
								mCurrentMousePosition.Y));
				temp(this, args);												   
			}
		}	*/

		protected virtual InputEventArgs CreateInputEventArgs()
		{
			InputEventArgs retVal = new InputEventArgs();

			retVal.CurrentMousePosition = mCurrentMousePosition;
			retVal.OldMousePosition = mOldMousePosition;

			retVal.CurrentMouseScrollWheel = mCurrentMouseScrollWheel;
			retVal.OldMouseScrollWheel = mOldMouseScrollWheel;

			retVal.LeftMouseButton = mLeftMouseButton;
			retVal.MiddleMouseButton = mMiddleMouseButton;
			retVal.RightNouseButton = mRightMouseButton;

			retVal.HeldKeys = mHeldKeys;
			retVal.NewPressedKeys = mPressedKeys;
			retVal.NewDepressedKeys = mDepressedKeys;  			

			return retVal;
		}
		#endregion

		#endregion





	}
}
