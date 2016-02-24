using NUnit.Framework;
using System;
using Xamarin.Plugins.GenericCarousel.Controls;

namespace Xamarin.Plugins.GenericCarousel.Tests
{
	[TestFixture ()]
	public class CarouselTest
	{
		internal class DemoCarouselView : Xamarin.Plugins.GenericCarousel.Controls.GenericCarouselView
		{
			public DemoCarouselView (Xamarin.Plugins.GenericCarousel.Controls.GenericCarousel gc)
				:base(gc)
			{
				
			}
		}

		[Test ()]
		public void CofirmDotsCountTest ()
		{
			var gc = new Xamarin.Plugins.GenericCarousel.Controls.GenericCarousel();

			gc.AddView (new DemoCarouselView (gc));

			Assert.AreEqual (1, gc.ContentViews.Count);

			Assert.AreEqual (1, gc.Dots.Count);

			gc.AddView (new DemoCarouselView (gc));

			Assert.AreEqual (2, gc.Dots.Count);

			Assert.AreEqual (2, gc.ContentViews.Count);
		}
	}
}

