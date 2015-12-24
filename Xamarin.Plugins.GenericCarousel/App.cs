using System;

using Xamarin.Forms;

namespace Xamarin.Plugins.GenericCarousel
{
	public class App : Application
	{
		public App ()
		{

			var gc = new Xamarin.Plugins.GenericCarousel.Controls.GenericCarousel ();

			gc.AddView (new ImageView (gc, "http://cdn.osxdaily.com/wp-content/uploads/2012/08/edit-hosts-file-mac-os-x.jpg"));

			gc.AddView (new ImageView (gc, "http://static-cdn.jtvnw.net/jtv_user_pictures/imaqtpie-profile_image-8efb10b7bed60d76-300x300.jpeg"));		


			// The root page of your application
			MainPage = new ContentPage {
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.Center,
					Children = {
						gc,
						new Label {
							XAlign = TextAlignment.Center,
							Text = "Welcome to Xamarin Forms!"
						},

					}
				}
			};
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

