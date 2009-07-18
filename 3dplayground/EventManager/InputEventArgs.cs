using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _3dplayground.EventManagement
{
	/// <summary>
	/// generic input argument class. Covers all bases.
	/// </summary>
	public class  InputEventArgs : EventArgs 
	{
	//	public KeyboardState OldKeyboardState;
	//	public KeyboardState CurrentKeyboardState;

		public EventManager EventManager;

		public List<Keys> NewPressedKeys;
		public List<Keys> HeldKeys;
		public List<Keys> NewDepressedKeys;

		public MouseButtonState LeftMouseButton;
		public MouseButtonState MiddleMouseButton;
		public MouseButtonState RightNouseButton;

		public int NewScrolledDistance;

		public int OldMouseScrollWheel;
		public int CurrentMouseScrollWheel;

		public Vector2 CurrentMousePosition;
		public Vector2 OldMousePosition;


		public InputEventArgs Clone()
		{
			InputEventArgs temp = new InputEventArgs();
			//temp.OldKeyboardState = OldKeyboardState;
			//temp.CurrentKeyboardState = CurrentKeyboardState;
			temp.EventManager = EventManager;
			temp.NewPressedKeys = NewPressedKeys;
			temp.HeldKeys = HeldKeys;
			temp.NewDepressedKeys = NewDepressedKeys;
			temp.LeftMouseButton = LeftMouseButton;
			temp.MiddleMouseButton = MiddleMouseButton;
			temp.RightNouseButton = RightNouseButton;
			temp.NewScrolledDistance = NewScrolledDistance;
			temp.OldMouseScrollWheel = OldMouseScrollWheel;
			temp.CurrentMouseScrollWheel = CurrentMouseScrollWheel;
			temp.CurrentMousePosition = CurrentMousePosition;
			temp.OldMousePosition = OldMousePosition;

			return temp;
		}
	}
}
