namespace FlashCardSidebar
{
	using System;
	using System.Collections;
	using System.IO;
	using System.Windows.Desktop;
	using System.Windows.Media;
	using System.Windows.Input;
	using System.Windows;
	using System.Threading;
	using System.Windows.Controls;

	public sealed class FrenchFlashcardTile : BaseTile
	{
		/*********************************************/
		/*           Class variables                 */
		/*********************************************/
		// Holds a corresponding pair of words
		struct WordPair
		{
			public string english;

			public string french;
		}

		// An ArrayList of WordPair structures
		ArrayList pairs = new ArrayList();

		// Array counter
		int word = 0;

		// Timer for the automatic option
		UITimer timer;

		// Flag used for the lower text element
		bool hideTranslation = true;

		// Language constants
		enum language
		{
			English,
			French
		};

		// Initial language of the upper text element
		language primaryLang = language.English;

		// Random number generator to jump around in the ArrayList
		Random randomIndex = new Random();

		// The text elements containing the words
		Text textPrimary;

		Text textSecondary;

		// Initial strings used in the context menu
		string contextMenuTranslationString = "French to English";

		string contextMenuProgressionString = "Automatic Progression";

		/******************************************/
		/*          Override methods              */
		/******************************************/
		public override void Initialize()
		{
			// Load up the words from the text file into the ArrayList
			StreamReader reader = new StreamReader("c:\\WordPairs.txt");

			// Get the first English word
			string newWord = reader.ReadLine();

			while (newWord != "")
			{
				WordPair wordPair;

				wordPair.english = newWord;

				// Get the French translation
				newWord = reader.ReadLine();
				wordPair.french = newWord;
				pairs.Add(wordPair);

				// Get the next English word
				newWord = reader.ReadLine();
			}

			reader.Close();

			// Get the first random array index
			word = randomIndex.Next(pairs.Count);

			// Create the tile body and display it.
			// This is set before the timer initialization to avoid
			// delays in display that could result from that
			// initialization. Always set the visual elements
			// as soon as possible.
			this.Foreground = GetTile();
			UpdateForeground(this.Foreground);

			// Timer initialization
			timer = new UITimer();
			timer.Interval = new TimeSpan(0, 0, 3);  // 3 seconds
			timer.Tick += new EventHandler(timer_Tick);

			// Set up the handler for custom context menu additions
			this.Sidebar.ContextMenuEvent += new ContextMenuEventHandler(ContextMenuHandler);
		}

		/*******************************************/
		/*            Class methods                */
		/*******************************************/
		private FrameworkElement GetTile()
		{
			// Base element of the Foreground
			DockPanel myTile = new DockPanel();

			myTile.Height = new Length(100);
			myTile.MinHeight = new Length(100);
			myTile.ID = "myTile";

			// The IsEnabled and Background properties must be set in order 
			// to receive the MouseLeftButtonDown event.
			myTile.IsEnabled = true;
			myTile.Background = new SolidColorBrush(Colors.Transparent);
			myTile.MouseLeftButtonDown += new MouseButtonEventHandler(OnMouseButtonDown);

			// Top text box initialization
			Text myTopText = new Text();

			DockPanel.SetDock(myTopText, Dock.Top);
			myTopText.Height = new Length(50, UnitType.Percent);
			myTopText.FontSize = new FontSize(12, FontSizeType.Point);
			myTopText.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
			myTopText.VerticalAlignment = System.Windows.Media.VerticalAlignment.Center;
			myTopText.Foreground = new SolidColorBrush(Color.FromRGB(0xcf, 0xd2, 0xff));
			myTopText.ID = "myTopText";
			myTopText.TextWrap = TextWrap.Wrap;
			myTile.Children.Add(myTopText);
			textPrimary = myTopText;

			// Bottom text box initialization
			Text myBottomText = new Text();

			myBottomText.FontSize = new FontSize(12, FontSizeType.Point);
			myBottomText.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
			myBottomText.VerticalAlignment = System.Windows.Media.VerticalAlignment.Center;
			myBottomText.Foreground = new SolidColorBrush(Colors.White);

			//myBottomText.Fill = new SolidColorBrush(Colors.Blue);
			myBottomText.ID = "myBottomText";
			myBottomText.TextWrap = TextWrap.Wrap;
			myBottomText.TextContent = "";
			textSecondary = myBottomText;

			// Border to display the dividing line
			Border border = new Border();

			DockPanel.SetDock(border, Dock.Bottom);
			border.Height = new Length(50, UnitType.Percent);
			border.BorderBrush = Brushes.SlateGray;
			border.BorderThickness = new Thickness(new Length(0), new Length(1), new Length(0), new Length(0));
			border.Child = myBottomText;
			myTile.Children.Add(border);
			return (FrameworkElement)myTile;
		}

		// This tile can be thought of as a state machine with two 
		// continually repeating states.
		// 1. New word in the top box, no translation in the bottom box.
		// 2. That same word and its translation both showing.
		private void NextState()
		{
			if (hideTranslation == true)
			{
				hideTranslation = false;
			}
			else
			{
				hideTranslation = true;
				word = randomIndex.Next(0, pairs.Count);
			}
		}

		// UpdateForeground refreshes both text elements, even when
		// the top word stays the same.
		private void UpdateForeground(FrameworkElement tile)
		{
			// Set the word to translate in the upper box. This may
			// just redisplay the same word.
			textPrimary.TextContent = GetPrimaryLang(word);

			// Clear the bottom box, even though it may already be clear.
			textSecondary.TextContent = "";

			// If appropriate for this update, display the translation
			// in the lower box.
			if (!hideTranslation)
			{
				textSecondary.TextContent = GetSecondaryLang(word);
			}
		}

		private string GetPrimaryLang(int i)
		{
			if (primaryLang == language.English)
				return (((WordPair)pairs[i]).english);
			else
				return (((WordPair)pairs[i]).french);
		}

		private string GetSecondaryLang(int i)
		{
			if (primaryLang == language.English)
				return (((WordPair)pairs[i]).french);
			else
				return (((WordPair)pairs[i]).english);
		}

		private void timer_Tick(object o, EventArgs ea)
		{
			NextState();
			UpdateForeground(this.Foreground);
		}

		/*******************************************/
		/*               Handlers                  */
		/*******************************************/
		private void ContextMenuHandler(object sender, ContextMenuEventArgs args)
		{
			MenuItem mi1 = new MenuItem();

			mi1.ID = "TransDir";
			mi1.Header = contextMenuTranslationString;
			mi1.Click += new ClickEventHandler(ReverseTranslationDirection);
			args.ContextMenu.Items.Add(mi1);

			MenuItem mi2 = new MenuItem();

			mi2.ID = "Progression";
			mi2.Header = contextMenuProgressionString;
			mi2.Click += new ClickEventHandler(SetProgression);
			args.ContextMenu.Items.Add(mi2);
		}

		private void ReverseTranslationDirection(object sender, ClickEventArgs args)
		{
			if (primaryLang == language.English)
			{
				primaryLang = language.French;
				contextMenuTranslationString = "English to French";
			}
			else
			{
				primaryLang = language.English;
				contextMenuTranslationString = "French to English";
			}

			hideTranslation = true;
			UpdateForeground(this.Foreground);
		}

		private void SetProgression(object sender, ClickEventArgs args)
		{
			if ((string)(((MenuItem)sender).Header) == "Automatic Progression")
			{
				timer.Start();
				contextMenuProgressionString = "Manual Progression";
			}
			else
			{
				timer.Stop();
				contextMenuProgressionString = "Automatic Progression";
				this.ForegroundAutoHide = true;
			}
		}

		internal void OnMouseButtonDown(object sender, MouseButtonEventArgs args)
		{
			NextState();
			UpdateForeground(this.Foreground);
		}
	}
}