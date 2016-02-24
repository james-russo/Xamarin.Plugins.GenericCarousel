using System;

using Xamarin.Forms;

namespace Xamarin.Plugins.GenericCarousel
{
	public class App : Application
	{
		public App ()
		{

			var gc = new Xamarin.Plugins.GenericCarousel.Controls.GenericCarousel (){
				DotsColor = Color.White
			};

			gc.AddView (new ImageView (gc, "http://cdn.osxdaily.com/wp-content/uploads/2012/08/edit-hosts-file-mac-os-x.jpg"));

			gc.AddView (new ImageView (gc, "http://static-cdn.jtvnw.net/jtv_user_pictures/imaqtpie-profile_image-8efb10b7bed60d76-300x300.jpeg"));		

			gc.AddView (new ImageView (gc, "http://cdn.osxdaily.com/wp-content/uploads/2012/08/edit-hosts-file-mac-os-x.jpg"));	


			// The root page of your application
			MainPage = new ContentPage {
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.Center,
					Children = {
						gc,
						new Label {							
							Text = "Welcome to Xamarin Forms!"
						},

					}
				}
			};

		//	gc.StartRotation (2);
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

