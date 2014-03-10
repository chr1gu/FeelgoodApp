using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.MessageUI;

namespace Feelgood
{
	public class ShareView : UIView
	{
		public UIViewController ViewController;
		protected MFMailComposeViewController MailController;
		protected MFMessageComposeViewController MessageController;
		protected UIImageView MessageIcon;
		protected UIImageView MailIcon;
		protected UIImageView MessageShadow;
		protected UIImageView MailShadow;

		protected float Transparency = 0.75f;
		protected float Duration = 0.3f;

		public void OpenShareDialog (Quote currentQuote)
		{
			if (MessageIcon.Alpha == 1.0f) {
				MessageController = new MFMessageComposeViewController ();
				MessageController.Body = currentQuote.Text;
				MessageController.Finished += ( object s, MFMessageComposeResultEventArgs args) => {
					args.Controller.DismissViewController (true, null);
				};
				ViewController.PresentViewController (MessageController, true, null);
				return;
			}

			if (MailIcon.Alpha == 1.0f) {
				MailController = new MFMailComposeViewController ();
				//MailController.SetToRecipients (new string[]{"john@doe.com"});
				MailController.SetSubject ("Got something for you");
				MailController.SetMessageBody (currentQuote.Text, false);
				MailController.Finished += ( object s, MFComposeResultEventArgs args) => {
					args.Controller.DismissViewController (true, null);
				};
				ViewController.PresentViewController (MailController, true, null);
				return;
			}
		}

		public void DeActivateShares ()
		{
			if (MessageIcon.Alpha < 1.0f && MailIcon.Alpha < 1.0f)
				return;

			UIView.BeginAnimations ("fadeAnimation");
			UIView.SetAnimationDuration (Duration);
			UIView.SetAnimationCurve (UIViewAnimationCurve.EaseInOut);
			DeActivateMessageShare ();
			DeActivateMailShare ();
			UIView.CommitAnimations ();
		}

		public void ActivateMessageShare ()
		{
			if (MessageIcon.Alpha > Transparency)
				return;

			UIView.BeginAnimations ("fadeAnimation");
			UIView.SetAnimationDuration (Duration);
			UIView.SetAnimationCurve (UIViewAnimationCurve.EaseInOut);
			MessageIcon.Alpha = 1.0f;
			MessageShadow.Alpha = 1.0f;
			MessageIcon.Frame = new RectangleF (20, Frame.Height - 38 - 25, 36, 38);
			DeActivateMailShare ();
			UIView.CommitAnimations ();
		}

		public void DeActivateMessageShare ()
		{
			if (MessageIcon.Alpha < 1.0f)
				return;

			MessageIcon.Frame = new RectangleF (20, Frame.Height - 38 - 20, 36, 38);
			MessageIcon.Alpha = Transparency;
			MessageShadow.Alpha = 0;
		}

		public void ActivateMailShare ()
		{
			if (MailIcon.Alpha > Transparency)
				return;

			UIView.BeginAnimations ("fadeAnimation");
			UIView.SetAnimationDuration (Duration);
			UIView.SetAnimationCurve (UIViewAnimationCurve.EaseInOut);
			MailIcon.Alpha = 1.0f;
			MailShadow.Alpha = 1.0f;
			MailIcon.Frame = new RectangleF (89, Frame.Height - 38 - 25, 39, 38);
			DeActivateMessageShare ();
			UIView.CommitAnimations ();
		}

		public void DeActivateMailShare ()
		{
			if (MailIcon.Alpha < 1.0f)
				return;

			MailIcon.Frame = new RectangleF (89, Frame.Height - 38 - 20, 39, 38);
			MailIcon.Alpha = Transparency;
			MailShadow.Alpha = 0;
		}

		public ShareView (RectangleF rect) : base (rect)
		{
			BackgroundColor = UIColor.FromRGB (237, 24, 72);
			var label = FontHelper.GetShareLabel (new RectangleF (20, 20, Frame.Width-40, 60));
			label.Text = "Made you think of someone?\nLet them know!";
			label.SizeToFit ();
			Add (label);

			MessageIcon = new UIImageView (new RectangleF (20, Frame.Height - 38 - 20, 36, 38));
			MessageIcon.Image = UIImage.FromFile ("ico_msg.png");
			MessageIcon.Alpha = 0.75f;
			Add (MessageIcon);

			MessageShadow = new UIImageView (new RectangleF (20, Frame.Height - 20, 36, 10));
			MessageShadow.Image = UIImage.FromFile ("ico_shadow.png");
			MessageShadow.Alpha = 0;
			Add (MessageShadow);

			MailIcon = new UIImageView (new RectangleF (89, Frame.Height - 38 - 20, 39, 38));
			MailIcon.Image = UIImage.FromFile ("ico_mail.png");
			MailIcon.Alpha = 0.75f;
			Add (MailIcon);

			MailShadow = new UIImageView (new RectangleF (89, Frame.Height - 20, 36, 10));
			MailShadow.Image = UIImage.FromFile ("ico_shadow.png");
			MailShadow.Alpha = 0;
			Add (MailShadow);
		}
	}
}

