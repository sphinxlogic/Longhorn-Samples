using System;
using System.Windows;
using System.Windows.Controls;


namespace DependencyPropertyDemo
{
	public class MyButton: Button
	{
		public static readonly DependencyProperty MagicStringProperty = DependencyProperty.Register("MagicString", typeof(string), typeof(MyButton));

		static MyButton()
		{
			//the following four lines can be merged into one
			PropertyMetadata myMeta = new PropertyMetadata();
			myMeta.DefaultValue = "MyDefaultString";
			myMeta.PropertyInvalidatedCallback = new PropertyInvalidatedCallback(OnMagicStringPropertyInvalidated);
			myMeta.GetValueOverride = new GetValueOverride(MagicStringGetValueCallback);

			MagicStringProperty.OverrideMetadata(typeof(MyButton), myMeta);
		}

		private static void OnMagicStringPropertyInvalidated(DependencyObject d)
		{
			//Mark cache as invalid
			((MyButton)d)._magicStringValid = false;
		}
		private static object MagicStringGetValueCallback(DependencyObject d)
		{
			//call caching get accessor
			return ((MyButton)d).MagicString;
		}

		//Provide CLR Accessors and local caching
		public string MagicString
		{
			get
			{
				//Cache pattern to cache computed results from GetValue
				if (!_magicStringValid)
				{
					_magicString = (string)GetValueBase(MagicStringProperty);
					_magicStringValid = true;
				}

				return _magicString;
			}
			set
			{
				SetValue(MagicStringProperty, value);
			}
		}
		//Type-safe, non-boxed (requires additional dirty bit)
		private string _magicString;
		private bool _magicStringValid;
	}

	public class ClassDeriveFromObjectDirectly
	{
		public static readonly DependencyProperty NumberProperty = DependencyProperty.RegisterAttached("Number", typeof(int), typeof(ClassDeriveFromObjectDirectly), new PropertyMetadata(0));

		public static void SetNumber(DependencyObject d, int Number)
		{
			d.SetValue(NumberProperty, Number); //Boxing
		}
		public static int GetNumber(DependencyObject d)
		{
			return (int)d.GetValue(NumberProperty);  //Unboxing
		}
	}

	public class MyClass: DependencyObject
	{
		static MyClass()
		{
			ClassDeriveFromObjectDirectly.NumberProperty.AddOwner(typeof(MyClass));

			PropertyMetadata myMeta = new PropertyMetadata();

			myMeta.DefaultValue = 100;
			myMeta.PropertyInvalidatedCallback = new PropertyInvalidatedCallback(OnNumberPropertyInvalidated);
			myMeta.GetValueOverride = new GetValueOverride(NumberGetValueCallback);
			ClassDeriveFromObjectDirectly.NumberProperty.OverrideMetadata(typeof(MyClass), myMeta);
		}
		private static void OnNumberPropertyInvalidated(DependencyObject d)
		{
			//Mark cache as invalid
			((MyClass)d)._NumberValid = false;
		}
		private static object NumberGetValueCallback(DependencyObject d)
		{
			//call cachong get accessor
			return ((MyClass)d).Number;
		}
		//Provide CLR Accessors and local caching
		public int Number
		{
			get
			{
				//Cache pattern to cache computed results from GetValue
				if (!_NumberValid)
				{
					_Number = (int)GetValueBase(ClassDeriveFromObjectDirectly.NumberProperty);
					_NumberValid = true;
				}

				return _Number;
			}
			set
			{
				SetValue(ClassDeriveFromObjectDirectly.NumberProperty, value);
			}
		}
		//Type-safe, non-boxed (requires additional dirty bit)
		private int _Number;
		private bool _NumberValid;
	}
}
