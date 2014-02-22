using System;
using MonoTouch.UIKit;
using System.Drawing;

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
				}
			}
		}

		protected UILabel Label;

		public QuoteView (RectangleF rect) : base (rect)
		{
			Label = FontHelper.GetQuoteLabel ();
			Add (Label);
		}
	}
}

