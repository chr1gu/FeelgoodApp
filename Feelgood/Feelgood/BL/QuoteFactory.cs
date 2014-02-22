using System;
using System.Collections.Generic;

namespace Feelgood
{
	public static class QuoteFactory
	{
		public static List<Quote> GetUsedQuotes ()
		{
			return new List<Quote> () {
				new Quote {
					Text = "Quote 0"
				},
				new Quote {
					Text = "Quote 1"
				},
				new Quote {
					Text = "Quote 2"
				}
			};
		}

		public static List<Quote> GetAvailableQuotes ()
		{
			return new List<Quote> () {
				new Quote {
					Text = "Quote 3"
				},
				new Quote {
					Text = "Quote 4"
				}
			};
		}
	}
}

