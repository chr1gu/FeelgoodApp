using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Globalization;

namespace Feelgood
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		public static MainViewController mainView;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			// date time issue for swiss fags
			// https://bugzilla.xamarin.com/show_bug.cgi?id=17690
			// if (CultureInfo.CurrentCulture.Name == "")
			//	CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
			
			// If you have defined a root view controller, set it here:
			mainView = new MainViewController ();
			window.RootViewController = mainView;

			// hide statusbar
			UIApplication.SharedApplication.SetStatusBarHidden (false, false);
			
			// make the window visible
			window.MakeKeyAndVisible ();
			
			return true;
		}

		// on app resume
		public override void OnActivated (UIApplication application)
		{
			mainView.AddQuotes ();
			//Store.GetNewQuotes ();
		}
	}
}

