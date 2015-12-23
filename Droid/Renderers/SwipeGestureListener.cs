using System;
using Android.Views;

namespace Xamarin.Plugins.GenericCarousel.Droid
{
	public class SwipeGestureListener : GestureDetector.SimpleOnGestureListener
	{
		public enum SwipeType
		{
			Left,
			Right,
			Tap
		}

		const int SWIPE_THRESHOLD = 100;
		const int SWIPE_VELOCITY_THRESHOLD = 100;

		public event EventHandler<SwipeType> Swipe;


		public SwipeGestureListener ()
		{
		}

		public override bool OnSingleTapConfirmed (MotionEvent e)
		{			
			Swipe (this, SwipeType.Tap);

			return true;
		}

		public override bool OnFling (MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
		{
			bool result = false;

			try {
				float diffY = e2.GetY () - e1.GetY ();
				float diffX = e2.GetX () - e1.GetX ();

				if (Math.Abs (diffX) > Math.Abs (diffY)) {
					if (Math.Abs (diffX) > SWIPE_THRESHOLD && Math.Abs (velocityX) > SWIPE_VELOCITY_THRESHOLD) {
						if (diffX > 0) {
							if (Swipe != null) {
								Swipe (this, SwipeType.Right);
							}
						} else {
							if (Swipe != null) {
								Swipe (this, SwipeType.Left);
							}								
						}
					}

					result = true;
				}

				result = true;
			} catch (Exception ex) {
				Console.WriteLine (ex.Message);
			}

			return result;
		}
	}
}

