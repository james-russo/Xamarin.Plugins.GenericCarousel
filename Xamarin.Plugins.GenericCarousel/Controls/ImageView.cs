using System;
using Xamarin.Forms;

namespace Xamarin.Plugins.GenericCarousel
{
	internal class ImageView : GenericCarousel.Controls.GenericCarouselView,
		Xamarin.Plugins.GenericCarousel.Controls.GenericCarouselView.ISwipeable, 
		Xamarin.Plugins.GenericCarousel.Controls.GenericCarouselView.INavigatable
	{
		public ImageView (GenericCarousel.Controls.GenericCarousel gc, string url) :base(gc)
		{
			var image = new Image (){
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				WidthRequest  = 150,
				HeightRequest = 150
			};

			image.Source = new UriImageSource () {
				Uri = new Uri(url),
				CachingEnabled 	= true,
				CacheValidity = new TimeSpan(4, 0, 0, 0)
			
			};

			Content = image;
		}

		public void SwipeLeft ()
		{
			Carousel.SwipeLeft ();
		}

		public void SwipeRight ()
		{
			Carousel.SwipeRight ();
		}

		public void Navigate ()
		{
			//Just a demo
		}
	}
}

