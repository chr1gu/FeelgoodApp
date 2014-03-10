using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;

namespace Feelgood
{
	public class CreditsView : UIView
	{
		protected UIScrollView ScrollContainer;
		protected UIImageView Logo;

		public CreditsView (RectangleF rect) : base (rect)
		{
			BackgroundColor = UIColor.White;
			ScrollContainer = new CreditsScrollView (Bounds, this);
			Add (ScrollContainer);
			rect.X = 20;
			rect.Y = 20;
			rect.Height = 9999;
			rect.Width -= 40;
			var label = FontHelper.GetCreditsLabel (rect);
			label.Text = "Hey, congratulations! \n\nSeriously, we mean it so and we’re flattered you are here. \n\nThere is a jungle of apps out there and the fact, that you are reading this, means you did not only find and download this app but you also took the time to figure out how it works.\n\nThe little joys and nice things in life need some effort and proactivity to be discovered. \nAnd of course the courage to spread and pass them on. \n\nBasically this is the main concept:\nWe’ve built this app for people like you, who are not tired to discover and enjoy a little kindness.\n\nThere is no sponsoring, marketing, advertising nor any possibility to upgrade or purchase something within this app.\n\nFurthermore there is no functionality to directly share the app or its content on social media platforms. It is up to you, if you want to tell your friends about it.\n\nYou know, like good old times.\n\nWe hope you like what you are reading and it brings you a bit of joy.\nAnd when you’re ready, take it to the streets.\n\nGo out, do good and have fun.\n\n—\nSome compliments are inspired by Daily Odd Compliment.\nBecause they are simply too good to be ignored.\n\n\nCode\nChristoph Ebert\n\nConcept & Design \nDani Rolli\n\nv.1.0";
			label.ScrollEnabled = false;
			label.SizeToFit ();
			ScrollContainer.Add (label);
			var contentSize = label.Frame.Size;
			Logo = new LogoView (new RectangleF (60, contentSize.Height+20, 200, 115));
			Logo.Image = UIImage.FromFile ("logo_appetite.png");
			ScrollContainer.Add (Logo);
			contentSize.Height += 155;
			ScrollContainer.ContentSize = contentSize;
		}
	}

	public class CreditsScrollView : UIScrollView
	{
		protected UIView CreditsView;

		public CreditsScrollView (RectangleF rect, UIView view) : base (rect) {
			CreditsView = view;
			ShowsVerticalScrollIndicator = false;
		}

		public override bool GestureRecognizerShouldBegin (UIGestureRecognizer gestureRecognizer)
		{
			var panGesture = ((UIPanGestureRecognizer)gestureRecognizer);
			float translation = panGesture.TranslationInView (CreditsView).Y;
			if (ContentOffset.Y == 0 && translation > 0)
				return false;

			return base.GestureRecognizerShouldBegin (gestureRecognizer);
		}
	}
}

