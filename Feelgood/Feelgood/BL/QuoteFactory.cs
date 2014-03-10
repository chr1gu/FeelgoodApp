using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using MonoTouch.Foundation;
using System.Text.RegularExpressions;

namespace Feelgood
{
	public static class QuoteFactory
	{
		/// <summary>
		/// Gets the already used quotes.
		/// </summary>
		/// <returns>The used quotes.</returns>
		public static List<Quote> GetUsedQuotes ()
		{
			//return GetAll ();
			var storedQuotes = GetQuotesForKey ("UsedQuotes");
			if (storedQuotes == null || storedQuotes.Count == 0) {
				var newQuote = GetNewRandomQuote ();
				newQuote.DateUsed = GetUpdatedAtString ();
				storedQuotes = new List<Quote> () { newQuote };
				UpdateUsedQuotes (storedQuotes);
			}
			return storedQuotes;
		}

		public static string GetUpdatedAtString ()
		{
			var dateString = "Unlocked on " + DateTime.Now.ToString ("dddd,\ndd. MMMM yy");
			//var dateString = "Unlocked on " + DateTime.Now.ToString ("dddd,\ndd. MMMM yy (hh:mm:ss)");
			return dateString;
		}

		/// <summary>
		/// Checks wether there are new quotes and returns em
		/// </summary>
		/// <returns>The new quotes.</returns>
		public static List<Quote> GetNewQuotes ()
		{
			if (!NewQuoteAvailable ())
				return null;

			// 1. Get new quote
			var newQuote = GetNewRandomQuote ();

			// if the last quote is the unlocked-all text.. stop here
			var usedQuotes = GetUsedQuotes ();
			var allUnlockedString = "I can’t believe you unlocked them all!\nNow go out, do good and have fun.";
			if (usedQuotes.Last ().Text == allUnlockedString)
				return null;

			// no more quotes available
			if (newQuote == null) {
				newQuote = new Quote () {
					Text = allUnlockedString
				};
			}

			// 2. Update used quotes
			newQuote.DateUsed = GetUpdatedAtString ();
			usedQuotes.Add (newQuote);
			UpdateUsedQuotes (usedQuotes);

			// 3. return
			return new List<Quote> () {
				newQuote
			};
		}

		public static Quote GetNewRandomQuote ()
		{
			// 1. get available quotes
			var source = GetAvailableQuotes ();
			if (source == null || source.Count == 0) {
				source = GetAll ();
			}

			// 2. pick one out
			//Console.WriteLine ("Quotes available: " + source.Count);
			var quote = source [GetRandom (0, source.Count)];
			source.Remove (quote);
			//Console.WriteLine ("Quotes available: " + source.Count);

			// 3. update available quotes
			UpdateAvailableQuotes (source);
			UpdateQuoteTimestamp ();

			// 4. return
			return quote;
		}

		public static List<Quote> GetAll ()
		{
			return new List<Quote> () {
				new Quote {
					Text = "You are so different from anyone else. I really like that."
				},
				new Quote {
					Text = "I really hope I make you happy because that makes me happy too."
				},
				new Quote {
					Text = "You are awesome. And when you laugh you are superawesome."
				},
				new Quote {
					Text = "You are very attractive."
				},
				new Quote {
					Text = "Since I know you, you never had a bad-hair-day. How do you do that?"
				},
				new Quote {
					Text = "You’re perfect just the way you are."
				},
				new Quote {
					Text = "I have the feeling that you will rock today."
				},
				new Quote {
					Text = "You are fun to hang out with."
				},
				new Quote {
					Text = "You have a great smile."
				},
				new Quote {
					Text = "Damn, you smell great!"
				},
				new Quote {
					Text = "Is that your smell or did you get a hug by the old spice guy? I’m impressed!"
				},
				new Quote {
					Text = "I love your taste in music."
				},
				new Quote {
					Text = "You look breathtaking today."
				},
				new Quote {
					Text = "Your sense of humor is fantastic."
				},
				new Quote {
					Text = "I admire your creativity and ideas."
				},
				new Quote {
					Text = "You are full of good ideas and I enjoy it when you share them with me."
				},
				new Quote {
					Text = "You are funnier than a clown fish… They are not really funny. But you are."
				},
				new Quote {
					Text = "You are more wonderful than the smell of coffee beans."
				},
				new Quote {
					Text = "You are like my favourite food. But you make me happy without gaining weight."
				},
				new Quote {
					Text = "I am very glad we are friends."
				},
				new Quote {
					Text = "Everything gets better with you around."
				},
				new Quote {
					Text = "If more people would be like you, everything would be better."
				},
				new Quote {
					Text = "You are so funny and cute, you could be made out of rainbow."
				},
				new Quote {
					Text = "I would love to hang out and do stupid things together."
				},
				new Quote {
					Text = "I like you even more than my comfy pants. And I really, really like them."
				},
				new Quote {
					Text = "You are like Mentos and Coke together: surprisingly awesome!"
				},
				new Quote {
					Text = "If anyone is as cool as MacGyver is, it has to be you. I mean it."
				},
				new Quote {
					Text = "To hug you is like hugging a coala bear. Except that you smell good as well."
				},
				new Quote {
					Text = "You pretty much killed it with your look today."
				},
				new Quote {
					Text = "You are like my childhood hero except that I am not embarrassed to admit it."
				},
				new Quote {
					Text = "You make me happy and everybody else with the privilege to know you."
				},
				new Quote {
					Text = "Life is awesome. Enjoy every step."
				},
				new Quote {
					Text = "You just made my day."
				},
				new Quote {
					Text = "You are awesome."
				},
				new Quote {
					Text = "You look really good today."
				},
				new Quote {
					Text = "Any day spent with you is my favorite day."
				},
				new Quote {
					Text = "You are cooler than Ninjapirates. Which don’t exist. But I’m glad you do."
				},
				new Quote {
					Text = "You are just so weird sometimes. It’s great."
				},
				new Quote {
					Text = "Chocolate is good but your friendship is better."
				},
				new Quote {
					Text = "You’re a whole lot of lovely."
				},
				new Quote {
					Text = "You are the friend, everyone wants to have."
				},
				new Quote {
					Text = "How do you get your hair to look that great?"
				},
				new Quote {
					Text = "Can you teach me how to be as awesome as you?"
				},
				new Quote {
					Text = "I’m glad you exist."
				},
				new Quote {
					Text = "You are pretty much my favorite of all time in history of ever."
				},
				new Quote {
					Text = "You are somebody’s reason to smile."
				},
				new Quote {
					Text = "You are pretty darn wonderful."
				},
				new Quote {
					Text = "You make me smile. Even when I’m trying not to."
				},
				new Quote {
					Text = "You’re more fun than bubble wrap."
				},
				new Quote {
					Text = "You could survive a zombie apocalypse."
				},
				new Quote {
					Text = "You would be my first choice to survive a zombie apocalypse together."
				},
				new Quote {
					Text = "You are anything but ordinary."
				},
				new Quote {
					Text = "You are important."
				},
				new Quote {
					Text = "You are amazing… don’t you forget that!"
				},
				new Quote {
					Text = "You’re so cool, that on a scale from 1 to 10, you’re elevendyseven."
				},
				new Quote {
					Text = "I like your face."
				},
				new Quote {
					Text = "You’re pretty special. Just saying."
				},
				new Quote {
					Text = "With you around, I feel like Ninja Turtles. Which is badass."
				},
				new Quote {
					Text = "I was just thinking about you."
				},
				new Quote {
					Text = "When I grow up, I want to be you."
				},
				new Quote {
					Text = "Looking good, as usual."
				},
				new Quote {
					Text = "Thank you for being who you are."
				},
				new Quote {
					Text = "I really admire your dressing sense."
				},
				new Quote {
					Text = "Being with you is as awesome as Morgan Freeman’s voice."
				},
				new Quote {
					Text = "You, Leonardo, Paul, you, Theresa, you, … did I already mention you?"
				},
				new Quote {
					Text = "I don’t like it when you are not around. Because everything is better when you are."
				},
				new Quote {
					Text = "You are very pretty and very smart."
				}
			};
		}


		public static int GetRandom (int min, int max)
		{
			var range = min + max;
			var b = new byte[12];
			var rngCrypto = new RNGCryptoServiceProvider();
			rngCrypto.GetBytes(b);
			var stringBuilder = new StringBuilder("0.");
			var numbers = b.Select(i => Convert.ToInt32((i * 100 / 255)/10)).ToArray();
			foreach (var number in numbers)
				stringBuilder.Append(number);

			var random = float.Parse (stringBuilder.ToString ());
			var randomOutput = (int)Math.Round (range * random);
			randomOutput = Math.Min (max - 1, randomOutput);
			randomOutput = Math.Max (min, randomOutput);
			return randomOutput;
		}

		public static void SetQuotesForKey (string key, List<Quote> quotes)
		{
			var regex = new Regex (@"\|\=");
			var serializedQuotes = quotes.Select (
				q => regex.Replace (q.Text, "") + "="
				+ (!String.IsNullOrEmpty(q.DateUsed) ? regex.Replace (q.DateUsed, "") : "")
			).ToList ();
			var defaults = NSUserDefaults.StandardUserDefaults;
			defaults.SetString (string.Join ("|", serializedQuotes), key);
			defaults.Synchronize ();
		}

		public static List<Quote> GetQuotesForKey (string key)
		{
			var defaults = NSUserDefaults.StandardUserDefaults;
			var dict = defaults.StringForKey (key);
			if (dict != null) {
				// transform string into list of quotes
				var serializedItems = dict.Split (new char[] { '|' });
				var list = serializedItems.Select ((t) => {
					if (string.IsNullOrEmpty (t))
						return null;
					var quote = new Quote ();
					var data = t.Split (new char[]  { '=' });
					quote.Text = data [0];
					quote.DateUsed = data [1];
					return quote;
				}).ToList ();
				return list;
			}
			return new List<Quote> ();
		}

		public static List<Quote> GetAvailableQuotes ()
		{
			return GetQuotesForKey ("AvailableQuotes");
		}

		public static void UpdateAvailableQuotes (List<Quote> quotes)
		{
			SetQuotesForKey ("AvailableQuotes", quotes);
		}

		public static void UpdateUsedQuotes (List<Quote> quotes)
		{
			SetQuotesForKey ("UsedQuotes", quotes);
		}

		public static void UpdateQuoteTimestamp ()
		{
			var defaults = NSUserDefaults.StandardUserDefaults;
			var date = DateTime.Now.Date.ToString ();
			defaults.SetString (date, "LastQuote");
			defaults.Synchronize ();
		}

		public static bool NewQuoteAvailable ()
		{
			var defaults = NSUserDefaults.StandardUserDefaults;
			var dateString = defaults.StringForKey ("LastQuote");
			if (!string.IsNullOrEmpty (dateString)) {
				var date = DateTime.Parse (dateString);
				var newerDate = DateTime.Now.Date;//.AddSeconds (-1);
				if (DateTime.Compare (date.Date, newerDate.Date) < 0) {
					return true;
				}
			}
			return false;
		}
	}
}

