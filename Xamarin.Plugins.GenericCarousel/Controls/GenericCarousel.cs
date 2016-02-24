using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Xamarin.Plugins.GenericCarousel.Controls
{
	public class GenericCarousel : AbsoluteLayout
	{
		public static readonly BindableProperty ViewsProperty =
			BindableProperty.Create<GenericCarousel, ObservableCollection<GenericCarouselView>> (p => p.ContentViews, default(ObservableCollection<GenericCarouselView>));

		private ObservableCollection<GenericCarouselView> ContentViews {
			get { return (ObservableCollection<GenericCarouselView>)GetValue (ViewsProperty); }
			set { SetValue (ViewsProperty, value); }
		}

		public Color DotsColor = Color.Default;

		public float DotSize = 5;

        private IList<Button> Dots = new List<Button>();

		public GenericCarouselView CurrentView { get; private set; }

		private Timer timer;

		private double TimeOutDuration { get; set; }

		private StackLayout DotsContainer { get; set; }

		public GenericCarousel ()
		{
			ContentViews = new ObservableCollection<GenericCarouselView> ();

			ContentViews.CollectionChanged += Images_CollectionChanged;

			CreateDotsContainer ();

			this.Children.Add(DotsContainer, new Rectangle(new Point(0.5, 1), new Size(.05 , 0.1)), AbsoluteLayoutFlags.SizeProportional | AbsoluteLayoutFlags.PositionProportional);
		}

		protected override void LayoutChildren (double x, double y, double width, double height)
		{
			base.LayoutChildren (x, y, width, height);

			var point1 = ContentViews.First();

			var point = Point.Zero;

			foreach (View image in ContentViews) {
				image.Layout (new Rectangle (point, image.Bounds.Size));

				point = new Point (point.X + image.Width + this.Bounds.Width, 0);
			}

			CurrentView = (GenericCarouselView)point1;
		}

		private void SetDotIndex(GenericCarouselView currentView)
		{
			var index = ContentViews.IndexOf (currentView);

			Dots [index].Opacity = 1.0;
		}

		protected override void OnPropertyChanged (string propertyName = null)
		{
			base.OnPropertyChanged (propertyName);

			if (propertyName == ViewsProperty.PropertyName && ContentViews != null) {

				ContentViews.Clear ();

				Dots.Clear ();

				AddImagesAsChildren ();
			}
		}

		protected override void OnChildAdded (Element child)
		{
			base.OnChildAdded (child);

			if (child is GenericCarouselView) {				
				if (CurrentView == null) {
					CurrentView = child as GenericCarouselView;

					SetDotIndex (CurrentView);
				}
			}
		}

		private void Images_CollectionChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
            
			if (e.NewItems != null) {
				foreach (var item in e.NewItems) {
					var image = item as View;
					if (image != null) {

                        AddDotToList();

                        AddImageAsChild (image);
					}
				}
			}
		}

	    private void AddDotToList()
	    {
			var btn = new Button () {
				BorderRadius = Convert.ToInt32 (DotSize / 2),
				HeightRequest = DotSize,
				WidthRequest = DotSize,
				BackgroundColor = DotsColor,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.Center,
				Opacity = 0.5
			};


	        Dots.Add(btn);

			if (DotsContainer != null) {
				
				DotsContainer.Children.Add (btn);

				var point = DotsContainer.Bounds.Location;

				this.Children.Remove (DotsContainer);

				this.Children.Add(DotsContainer, new Rectangle(new Point(0.5 - (0.001 * Dots.Count), 1), new Size(.05 + (0.025 * Dots.Count), 0.1)), AbsoluteLayoutFlags.SizeProportional | AbsoluteLayoutFlags.PositionProportional);
			}
	    }

	    private void AddImagesAsChildren ()
		{
			foreach (var image in ContentViews) {
				AddImageAsChild (image);
			}
		}



		private void AddImageAsChild (View image)
		{
			var point = Point.Zero;

			this.Children.Add (image, new Rectangle (point, new Size (1, 0.9)), AbsoluteLayoutFlags.SizeProportional);			
		}


	    private void CreateDotsContainer()
	    {
            var stackLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,                
            };

			DotsContainer = stackLayout;
	    }

	    public async virtual void SwipeLeft ()
		{
			StopRotation ();

			var imageNumber = ContentViews.IndexOf (CurrentView);

			var nextNumber = imageNumber == ContentViews.Count - 1 ? 0 : imageNumber + 1;

			var nextImage = ContentViews [nextNumber];

			Dots [imageNumber].Opacity = 0.5;

			nextImage.Layout (new Rectangle (new Point (CurrentView.Width, 0), CurrentView.Bounds.Size));

			var current = CurrentView;

			await current.LayoutTo (new Rectangle (-(this.Bounds.Width + this.Width + CurrentView.Width), 0, CurrentView.Width, CurrentView.Height));

			CurrentView = (GenericCarouselView)nextImage;

			await nextImage.LayoutTo (new Rectangle (0, 0, CurrentView.Width, CurrentView.Height));

			Dots [nextNumber].Opacity = 1.0;

			BeginRotation ();

		}

		public async virtual void SwipeRight ()
		{
			StopRotation ();

			var imageNumber = ContentViews.IndexOf (CurrentView);

			var nextNumber = imageNumber == 0 ? ContentViews.Count - 1 : imageNumber - 1;

			var nextImage = ContentViews [nextNumber];

			Dots [imageNumber].Opacity = 0.5;

			nextImage.Layout (new Rectangle (new Point (-CurrentView.Width, 0), CurrentView.Bounds.Size));

			var current = CurrentView;

			await current.LayoutTo (new Rectangle ((this.Bounds.Width + this.Width + CurrentView.Width), 0, CurrentView.Width, CurrentView.Height));

            CurrentView = (GenericCarouselView)nextImage;

			await nextImage.LayoutTo (new Rectangle (0, 0, CurrentView.Width, CurrentView.Height));

			Dots [nextNumber].Opacity = 1.0;

			BeginRotation ();
		}

		public void AddView(GenericCarouselView view)
		{
			ContentViews.Add (view);
		}

		public void ClearViews()
		{
			this.ContentViews.Clear ();
			this.CurrentView = null;

		}

		#region RotationFunctions
		public void StopRotation()
		{
			if (timer != null) {
				timer.Dispose ();
				timer = null;
			}
		}

		public void StartRotation(int secondsDelay = 5)
		{
			TimeOutDuration = TimeSpan.FromSeconds(secondsDelay).TotalMilliseconds;

			BeginRotation ();
		}

		private void BeginRotation()
		{
			if (TimeOutDuration > 0) {

				if (timer != null)
				{
					timer.Dispose();
					timer = null;
				}

				if (timer == null)
				{
					timer = new Timer(OnTimerTick, null, (int)TimeOutDuration, (int)TimeOutDuration);
				}
			}
		}

		private void OnTimerTick(object state)
		{			
			if(CurrentView is Xamarin.Plugins.GenericCarousel.Controls.GenericCarouselView.ISwipeable)
				Xamarin.Forms.Device.BeginInvokeOnMainThread (() => ((Xamarin.Plugins.GenericCarousel.Controls.GenericCarouselView.ISwipeable)CurrentView).SwipeLeft ());
		}
		#endregion
	}

	public abstract class GenericCarouselView : Frame
	{
		public interface ISwipeable
		{
			void SwipeLeft();

			void SwipeRight();
		}

		public interface INavigatable
		{
			void Navigate();
		}

	    protected readonly GenericCarousel Carousel;

	    protected GenericCarouselView (GenericCarousel carousel)
		{
			this.Carousel = carousel;

			this.BackgroundColor = Color.Accent;		
		}
	}
}

