using System;
using Xamarin.Forms;

namespace Xamarin.Plugins.GenericCarousel
{
	public class ImageView : GenericCarousel.Controls.GenericCarouselView
	{
		public ImageView (GenericCarousel.Controls.GenericCarousel gc, string url) :base(gc)
		{
			this.Orientation = StackOrientation.Vertical;

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

			Children.Add (image);

			Children.Add (new Label () {
				Text = url
			});
		}

		#region implemented abstract members of GenericCarouselView

		public override void NavigateTo ()
		{
		}

		#endregion
	}
}

