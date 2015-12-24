using System;
using Xamarin.Plugins.GenericCarousel.Droid;
using Xamarin.Plugins.GenericCarousel;
using Xamarin.Plugins.GenericCarousel.Controls;
using Xamarin.Forms.Platform.Android;
using Xamarin.Plugins.GenericCarousel.Droid.Renderers;
using Xamarin.Forms;
using Android.Widget;
using Android.Views;

[assembly: ExportRenderer (typeof(GenericCarouselView), typeof(SwipeableFrameRenderer))]
namespace Xamarin.Plugins.GenericCarousel.Droid.Renderers
{
	public class SwipeableFrameRenderer : ViewRenderer<GenericCarouselView, LinearLayout>
	{	

		private readonly SwipeGestureListener _listener;
		private readonly GestureDetector _detector;

		public SwipeableFrameRenderer ()
		{
			_listener = new SwipeGestureListener ();
			_detector = new GestureDetector (_listener);

			_listener.Swipe += Listener_Swipe;
		}

		void Listener_Swipe (object sender, SwipeGestureListener.SwipeType e)
		{
			switch (e) {
			case SwipeGestureListener.SwipeType.Left:
				((GenericCarouselView)Element).SwipeLeft ();
				break;
			case SwipeGestureListener.SwipeType.Right:
				((GenericCarouselView)Element).SwipeRight ();
				break;
			case SwipeGestureListener.SwipeType.Tap:
				((GenericCarouselView)Element).NavigateTo ();
				break;
			}
		}

		protected override void OnElementChanged (ElementChangedEventArgs<GenericCarouselView> e)
		{
			base.OnElementChanged (e);

			if (e.NewElement == null) {
				this.GenericMotion -= HandleGenericMotion;
				this.Touch -= HandleTouch;
			}

			if (e.OldElement == null) {
				this.GenericMotion += HandleGenericMotion;
				this.Touch += HandleTouch;
			}
		}

		void HandleTouch (object sender, TouchEventArgs e)
		{
			_detector.OnTouchEvent (e.Event);
		}

		void HandleGenericMotion (object sender, GenericMotionEventArgs e)
		{
			_detector.OnTouchEvent (e.Event);
		}


	}
}

