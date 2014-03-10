using System;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Feelgood
{
	public class LogoView : UIImageView
	{
		public UIView Parent;

		public LogoView (RectangleF rect) : base (rect)
		{
			UserInteractionEnabled = true;
		}

		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);

			// show website
			var url = new NSUrl ("http://app.etite.ch/?ref=FeelgoodApp");
			UIApplication.SharedApplication.OpenUrl(url);
			//var view = AppDelegate.mainView.View;
			//var webView = new UIWebView (view.Bounds);
			//webView.LoadRequest(new NSUrlRequest(url));
			//view.AddSubview(webView);
		}
	}
}

