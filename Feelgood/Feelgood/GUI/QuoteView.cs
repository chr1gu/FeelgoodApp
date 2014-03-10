using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;

namespace Feelgood
{
	public class QuoteView : UIView
	{
		public string Text {
			get { 
				if (Label != null)
					return Label.Text;
				else
					return string.Empty;
			}
			set {
				if (Label != null) {
					Label.Text = value;
					Label.SizeToFit ();
					// font rendering issue iOS7
					// forces redrawing of subviews
					Label.Text = "";
					ReArrangeUnlockedAtLabel ();
					Label.Frame = Rect;
					Label.Text = value;
				}
			}
		}

		public UITextView UnlockedAt;
		public UITextView Label;
		protected RectangleF Rect;

		public QuoteView (RectangleF rect) : base (rect)
		{
			BackgroundColor = ColorHelper.GetBackgroundColor ();
			rect.Y = 20;
			rect.X = 20;
			rect.Width = rect.Width - 40;
			Rect = rect; 
			Label = FontHelper.GetQuoteLabel (Rect);
			UnlockedAt = FontHelper.GetUnlockedAtLabel (RectangleF.Empty);
			UnlockedAt.Alpha = 0;
			Add (Label);
			Add (UnlockedAt);
			UserInteractionEnabled = true;
		}

		public void ShowUnlockedAt (bool delay)
		{
			UIView.BeginAnimations (null, System.IntPtr.Zero);
			UIView.SetAnimationBeginsFromCurrentState (true);
			//UIView.BeginAnimations ("fadeUnlockedAtAnimation");
			if (delay)
				UIView.SetAnimationDelay (1);
			UIView.SetAnimationDuration (0.5);
			UIView.SetAnimationCurve (UIViewAnimationCurve.EaseInOut);
			UnlockedAt.Alpha = 0.5f;
			UIView.SetAnimationDelegate (this);
			UIView.SetAnimationDidStopSelector (
				new Selector ("fadeAnimationFinished:"));
			UIView.CommitAnimations ();
		}

		[Export("fadeAnimationFinished:")]
		void FadeStopped () {
			FadeStopped (true);
		}

		void FadeStopped (bool withDelay)
		{
			UIView.BeginAnimations ("fadeOutUnlockedAtAnimation");
			UIView.SetAnimationDuration (0.5);
			if (withDelay)
				UIView.SetAnimationDelay (1);
			UIView.SetAnimationCurve (UIViewAnimationCurve.EaseInOut);
			UnlockedAt.Alpha = 0;
			UIView.SetAnimationDelegate (this);
			UIView.CommitAnimations ();
		}

		protected void ReArrangeUnlockedAtLabel ()
		{
			UnlockedAt.Frame = new RectangleF (Label.Frame.X, Label.Frame.Y + Label.Frame.Height + 20, Rect.Width, 80);
		}

		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);
			//if (UnlockedAt.Alpha == 0)
			ShowUnlockedAt (false);
			//else
			//	FadeStopped (false);
		}
	}
}

