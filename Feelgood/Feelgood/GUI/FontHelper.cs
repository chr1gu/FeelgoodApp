using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace Feelgood
{
	public static class FontHelper
	{
		public static UITextView GetQuoteLabel (RectangleF rect)
		{
			var label = new UITextView (rect);
			label.Editable = false;
			label.UserInteractionEnabled = false;
			label.BackgroundColor = UIColor.Clear;
			label.Font = UIFont.FromName ("KlinicSlab-MediumItalic", 36f);
			label.TextColor = UIColor.White;
			return label;
		}

		public static UITextView GetUnlockedAtLabel (RectangleF rect)
		{
			var label = new UITextView (rect);
			label.Editable = false;
			label.UserInteractionEnabled = false;
			label.BackgroundColor = UIColor.Clear;
			label.Font = UIFont.FromName ("KlinicSlab-MediumItalic", 18f);
			label.TextColor = UIColor.White;
			return label;
		}

		public static UITextView GetCreditsLabel (RectangleF rect)
		{
			var label = new UITextView (rect);
			label.Editable = false;
			label.UserInteractionEnabled = false;
			label.BackgroundColor = UIColor.Clear;
			label.Font = UIFont.FromName ("KlinicSlab-BookItalic", 18f);
			label.TextColor = UIColor.Black;
			return label;
		}

		public static UITextView GetShareLabel (RectangleF rect)
		{
			var label = new UITextView (rect);
			label.Editable = false;
			label.UserInteractionEnabled = false;
			label.BackgroundColor = UIColor.Clear;
			label.Font = UIFont.FromName ("KlinicSlab-BookItalic", 18f);
			label.TextColor = UIColor.White;
			return label;
		}
	}
}

