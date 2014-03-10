using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;
using MonoTouch.ObjCRuntime;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Feelgood
{
	public class MainViewController : UIViewController
	{
		protected ShareView Share;
		protected CreditsView Credits;
		protected float CreditsToggleOffset = 50;
		protected List<Quote> Quotes = new List<Quote> ();

		public UIScrollView ScrollContainer;

		public MainViewController () : base (null, null)
		{
			View.BackgroundColor = ColorHelper.GetBackgroundColor ();

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			if (ScrollContainer == null) {
				ScrollContainer = new UIScrollView (View.Bounds);
				ScrollContainer.ShowsHorizontalScrollIndicator = false;
				ScrollContainer.PagingEnabled = true;
				View.Add (ScrollContainer);
				AddShareView ();
				AddCreditsView ();
				AddGestureRecognizers ();
				AddQuotes ();
			}
		}

		protected void AddShareView ()
		{
			var frame = View.Bounds;
			frame.Height = 140;
			Share = new ShareView (frame);
			Share.ViewController = this;
			Share.Hidden = true;
			Add (Share);
			View.SendSubviewToBack (Share);
		}

		protected void AddCreditsView ()
		{
			var frame = View.Bounds;
			frame.Height -= 50;
			frame.Y = View.Bounds.Height;
			Credits = new CreditsView (frame);
			Add (Credits);
		}

		public void TapContentView(UITapGestureRecognizer tapGesture)
		{	
			ResetFrame ();
		}

		float startY;
		public void DragContentView(UIPanGestureRecognizer panGesture)
		{
			var frame = ScrollContainer.Frame;
			float translation = panGesture.TranslationInView(View).Y;
			if (panGesture.State == UIGestureRecognizerState.Began)
			{
				startY = frame.Y;
			}
			else if (panGesture.State == UIGestureRecognizerState.Changed)
			{
				if (frame.Y > 0) {
					Share.Hidden = false;
					frame.Y = Math.Min (translation + startY, Share.Frame.Height);
				} else {
					Share.Hidden = true;
					//var percentage = Math.Min (100 / Credits.Frame.Height * ((translation + startY) * -1), 100);
					//Credits.Alpha = percentage / 100;
					frame.Y = Math.Max (translation + startY, -Credits.Frame.Height);
				}

				var creditsFrame = Credits.Frame;
				creditsFrame.Y = View.Frame.Height + frame.Y;
				Credits.Frame = creditsFrame;
				ScrollContainer.Frame = frame;

				// activate share buttons
				//Console.WriteLine ("frame:" + translation);
				if (translation + startY > 190) {
					Share.ActivateMailShare ();
				} else if (translation + startY > 140) {
					Share.ActivateMessageShare ();
				} else {
					Share.DeActivateShares ();
				}
			}
			else if (panGesture.State == UIGestureRecognizerState.Ended)
			{
				if (frame.Y < 0 && startY - frame.Y > CreditsToggleOffset) {
					// show credits
					var newFrame = ScrollContainer.Frame;
					newFrame.Y = -Credits.Frame.Height;
					ResetFrame (newFrame);
				} else {
					// show quotes view
					int page = (int)(ScrollContainer.ContentOffset.X / ScrollContainer.Frame.Size.Width);
					var currentQuote = Quotes [page];
					Share.OpenShareDialog (currentQuote);
					ResetFrame ();
				}
			}
		}

		[Export("slideAnimationFinished:")]
		void SlideStopped ()
		{
			if (ScrollContainer.Frame.Y > 0)
				Share.Hidden = false;
			else 
				Share.Hidden = true;
		}

		protected void ResetFrame ()
		{
			var frame = ScrollContainer.Frame;
			frame.Y = 0;
			ResetFrame (frame);
		}

		protected void ResetFrame (RectangleF frame)
		{
			UIView.BeginAnimations ("slideAnimation");
			UIView.SetAnimationDuration (0.3);
			UIView.SetAnimationCurve (UIViewAnimationCurve.EaseInOut);
			ScrollContainer.Frame = frame;
			var creditsFrame = Credits.Frame;
			creditsFrame.Y = frame.Height + frame.Y;
			Credits.Frame = creditsFrame;
			//var percentage = Math.Min (100 / Credits.Frame.Height * (frame.Y * -1), 100);
			//Credits.Alpha = percentage / 100;
			UIView.SetAnimationDelegate (this);
			UIView.SetAnimationDidStopSelector (
				new Selector ("slideAnimationFinished:"));
			UIView.CommitAnimations ();
		}

		protected void AddGestureRecognizers ()
		{
			var panGesture = new UIPanGestureRecognizer (DragContentView);
			panGesture.CancelsTouchesInView = false;
			View.AddGestureRecognizer (panGesture);

			var tapGesture = new UITapGestureRecognizer (TapContentView);
			tapGesture.CancelsTouchesInView = false;
			View.AddGestureRecognizer (tapGesture);
		}

		public void AddQuotes ()
		{
			// TODO: check if there are already quotes... and if so attach new ones with animation
			var startIndex = Quotes != null ? Quotes.Count : 0;
			var newQuotes = startIndex == 0 ? QuoteFactory.GetUsedQuotes () : QuoteFactory.GetNewQuotes ();
			if (newQuotes == null || newQuotes.Count == 0)
				return;
			var rect = View.Bounds;
			var count = newQuotes.Count + startIndex;
			for (var i = startIndex; i < count; i++) {
				rect.X = View.Bounds.Width * i;
				var view = new QuoteView (rect);
				var quote = newQuotes [i - startIndex];
				ScrollContainer.Add (view);
				Quotes.Add (quote);
				view.UnlockedAt.Text = quote.DateUsed;
				view.Text = quote.Text;
				if (i == count - 1)
					view.ShowUnlockedAt (true);
			}
			var contentOffset = new PointF (View.Bounds.Width * (Quotes.Count - 1), 0);
			var frameSize = new SizeF (View.Bounds.Width * Quotes.Count, View.Bounds.Height);
			ScrollContainer.ContentSize = frameSize;
			ResetFrame ();

			// scroll to last item
			if (startIndex == 0) {
				ScrollContainer.SetContentOffset (contentOffset, false);
			} else {
				UIView.BeginAnimations ("slideAnimation");
				UIView.SetAnimationDelay (0.5);
				UIView.SetAnimationDuration (0.3);
				UIView.SetAnimationCurve (UIViewAnimationCurve.EaseInOut);
				ScrollContainer.SetContentOffset (contentOffset, false);
				UIView.CommitAnimations ();
			}
		}

		public override bool PrefersStatusBarHidden ()
		{
			return true;
		}
	}
}
