//This is a list of commonly used namespaces for a window.
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace ControlsAll
{
	
	public partial class Window1 : NavigationWindow
	{
		
		System.Windows.Controls.Button btn;

		System.Windows.Controls.CheckBox cb;

		System.Windows.Controls.ComboBox combo;

		System.Windows.Controls.ComboBoxItem[] cbi;

		System.Windows.Controls.ContextMenu contextmenu;

		System.Windows.Controls.Menu menu;

		System.Windows.Controls.MenuItem mi, mia, mib, mib1, mib1a;

		System.Windows.Controls.Primitives.HorizontalScrollBar hscrollb;

		System.Windows.Controls.HorizontalSlider hslider;

		System.Windows.Controls.HyperLink hlink;

		System.Windows.Controls.ListBox lb;

		System.Windows.Controls.ListItem[] li;

		System.Windows.Controls.RadioButton rb, rb1, rb2, rb3;

		System.Windows.Controls.RadioButtonList rblist;

		System.Windows.Controls.Primitives.RepeatButton rpbtn;

		System.Windows.Controls.ScrollViewer scrview;

		System.Windows.Controls.ToolTip ttp;

		System.Windows.Controls.Primitives.VerticalScrollBar vscrollb;

		System.Windows.Controls.VerticalSlider vslider;

		System.Windows.Controls.Primitives.Thumb thumb;

		System.Windows.Controls.TextBox txtb;

		System.Windows.Controls.Text txt, txt2;

		Int32 Num;

		private void Button(object sender, ClickEventArgs e)
		{
			cv1.Children.Clear();
			cv3.Children.Clear();
			btn = new Button();
			btn.Content = "Button";
			cv3.Children.Add(btn);
			txt = new Text();
			txt.TextWrap = System.Windows.TextWrap.Wrap;
			txt.TextContent = "Buttons are the most basic of user interface elements. They respond to the Click event. When a button's state changes its appearance changes also. Move the cursor over the button and click the button to see changes.";
			cv1.Children.Add(txt);
		}

		private void CheckBox(object sender, ClickEventArgs e)
		{
			cv1.Children.Clear();
			cv3.Children.Clear();
			cb = new CheckBox();
			cb.Content = "CheckBox";
			cv3.Children.Add(cb);
			txt = new Text();
			txt.TextWrap = System.Windows.TextWrap.Wrap;
			txt.TextContent = "Check boxes allow users to select and clear options. The user's selection is indicated by a check mark in the box. Clicking a check box switches its state and appearance. Click the box to see the changes in appearance.";
			cv1.Children.Add(txt);
		}

		private void ComboBox(object sender, ClickEventArgs e)
		{
			cv1.Children.Clear();
			cv3.Children.Clear();
			combo = new ComboBox();
			cbi = new ComboBoxItem[4];
			for (int i = 0; i < 4; i++)
			{
				cbi[i] = new ComboBoxItem(combo.Context);
				cbi[i].Content = (("Item") + (i + 1));
				combo.Items.Add(cbi[i]);
			}

			cv3.Children.Add(combo);
			txt = new Text();
			txt.TextWrap = System.Windows.TextWrap.Wrap;
			txt.TextContent = "The combo box is a composite control that presents users with a list of options. The contents can be shown and hidden as the control expands and collapses. In its default state the list is collapsed, displaying only one choice. The user clicks a button to see the complete list of options.";
			cv1.Children.Add(txt);
		}

		private void HorizontalScrollBar(object sender, ClickEventArgs e)
		{
			cv1.Children.Clear();
			cv3.Children.Clear();
			hscrollb = new HorizontalScrollBar();
			hscrollb.Width = new Length(150);
			cv3.Children.Add(hscrollb);
			txt = new Text();
			txt.TextWrap = System.Windows.TextWrap.Wrap;
			txt.TextContent = "A horizontal scroll bar is a composite control that encapsulates several buttons (thumb and two repeat buttons) to expose horizontal scrolling functionality in a user interface.";
			cv1.Children.Add(txt);
		}

		private void HorizontalSlider(object sender, ClickEventArgs e)
		{
			cv1.Children.Clear();
			cv3.Children.Clear();
			hslider = new HorizontalSlider();
			hslider.Width = new Length(150);
			cv3.Children.Add(hslider);
			txt = new Text();
			txt.TextWrap = System.Windows.TextWrap.Wrap;
			txt.TextContent = "A horizontal slider control, lets a user select from a range of values by moving a thumb. A slider is used to gradually modify a value (range selection). A slider is commonly used in volume controls, but it can be used anywhere a value has a minimum, a maximum, and an increment.";
			cv1.Children.Add(txt);
		}

		private void HyperLink(object sender, ClickEventArgs e)
		{
			cv1.Children.Clear();
			cv3.Children.Clear();
			hlink = new HyperLink();
			hlink.Content = "HyperLink";
			hlink.NavigateUri = (new Uri("data/Buttontest.xaml", false, true));
			cv3.Children.Add(hlink);
			txt = new Text();
			txt.TextWrap = System.Windows.TextWrap.Wrap;
			txt.TextContent = "A hyper link is used to indicate navigation.  Specifically, the HyperLink control is usually underlined and blue.  However, hyper links can be styled to look different and styled to respond to  user interaction.  The default HyperLink style changes the cursor to a hand. This particular hyper link navigates to a XAML file.";
			cv1.Children.Add(txt);
		}

		private void Menu(object sender, ClickEventArgs e)
		{
			cv1.Children.Clear();
			cv3.Children.Clear();
			menu = new Menu();
			menu.Background = System.Windows.Media.Brushes.LightBlue;
			mi = new MenuItem(menu.Context);
			mi.Header = "File";
			menu.Items.Add(mi);
			mia = new MenuItem(menu.Context);
			mia.Header = "New";
			mi.Items.Add(mia);
			mib = new MenuItem(menu.Context);
			mib.Header = "Open";
			mi.Items.Add(mib);
			mib1 = new MenuItem(menu.Context);
			mib1.Header = "Recently Opened";
			mib.Items.Add(mib1);
			mib1a = new MenuItem(menu.Context);
			mib1a.Header = "Text.xaml";
			mib1.Items.Add(mib1a);
			cv3.Children.Add(menu);
			txt = new Text();
			txt.TextWrap = System.Windows.TextWrap.Wrap;
			txt.TextContent = "Menus present a list of items that specify commands or options for an application. Typically, clicking a menu item opens a submenu or causes an application to carry out a command.";
			cv1.Children.Add(txt);
		}

		private void ListBox(object sender, ClickEventArgs e)
		{
			cv1.Children.Clear();
			cv3.Children.Clear();
			lb = new ListBox();
			li = new ListItem[4];
			for (int i = 0; i < 4; i++)
			{
				li[i] = new ListItem(lb.Context);
				li[i].Content = (("Item") + (i + 1));
				lb.Items.Add(li[i]);
			}

			cv3.Children.Add(lb);
			txt = new Text();
			txt.TextWrap = System.Windows.TextWrap.Wrap;
			txt.TextContent = "A list box provides a means for organizing a group of items and allowing a user to select those items. A list box provides several options for customizing how items in the control are selected.  For example, a current selection may be required. Also, list boxes are used and encapsulated by other controls.  For example, combo box uses a list box in its internal implementation.";
			cv1.Children.Add(txt);
		}

		private void RadioButton(object sender, ClickEventArgs e)
		{
			cv1.Children.Clear();
			cv3.Children.Clear();
			rb = new RadioButton();
			rb.Content = "Radio Button 1";
			cv3.Children.Add(rb);
			txt = new Text();
			txt.TextWrap = System.Windows.TextWrap.Wrap;
			txt.TextContent = "A radio button can be selected, but not cleared, by a user. The button must be cleared programmatically.";
			cv1.Children.Add(txt);
		}

		private void RadioButtonList(object sender, ClickEventArgs e)
		{
			cv1.Children.Clear();
			cv3.Children.Clear();
			rblist = new RadioButtonList();
			rb1 = new RadioButton(rblist.Context);
			rb1.Content = "Radio Button 1";
			rblist.Items.Add(rb1);
			rb2 = new RadioButton(rblist.Context);
			rb2.Content = "Radio Button 2";
			rblist.Items.Add(rb2);
			rb3 = new RadioButton(rblist.Context);
			rb3.Content = "Radio Button 3";
			rblist.Items.Add(rb3);
			cv3.Children.Add(rblist);
			txt = new Text();
			txt.TextWrap = System.Windows.TextWrap.Wrap;
			txt.TextContent = "A radio button list presents a list of items, but, unlike a check box, the items cannot be switched. Once users select an item, they cannot clear the item by clicking it. The application programmatically clears a selected item when the user selects a new item.";
			cv1.Children.Add(txt);
		}

		private void RepeatButton(object sender, ClickEventArgs e)
		{
			cv1.Children.Clear();
			cv3.Children.Clear();
			rpbtn = new RepeatButton();
			rpbtn.Width = new Length (100);
			rpbtn.Height = new Length(25);
			rpbtn.Content = "Increase";
			rpbtn.Delay = (100);
			rpbtn.Interval = (50);
			cv3.Children.Add(rpbtn);
			btn = new Button();
			btn.Content = "0";
			System.Windows.Controls.Canvas.SetLeft(btn, new System.Windows.Length(120));
			cv3.Children.Add(btn);
			txt = new Text();
			txt.TextWrap = System.Windows.TextWrap.Wrap;
			txt.TextContent = "A repeat button is a control that is similar to a button. However, repeat buttons give you control over when and how the Click event is fired. The repeat button fires its Click event repeatedly from the time it is pressed until it is released. The Delay property determines when the event begins firing. You can control the interval of the repetitions with the Interval property. If you press the example repeat button the content value of the button on the right increases until it reachs 100 and then the value is reset.";
			cv1.Children.Add(txt);
			rpbtn.Click += (Increase);
		}

		void Increase(object sender, ClickEventArgs e)
		{
			Num = Convert.ToInt32(btn.Content);
			btn.Content = ((Num + 1).ToString());
			if (Num >= 100)
			{
				btn.Content = "0";
			}
		}

		private void ScrollViewer(object sender, ClickEventArgs e)
		{
			cv1.Children.Clear();
			cv3.Children.Clear();
			scrview = new ScrollViewer();
			scrview.Width = new Length(100);
			scrview.Height = new Length(100);
			scrview.Content = "Scrolling is enabled only when it is necessary.";
			cv3.Children.Add(scrview);
			txt = new Text();
			txt.TextWrap = System.Windows.TextWrap.Wrap;
			txt.TextContent = "A scroll viewer is a scrollable area that can contain other visible elements. A scroll viewer encapsulates a content element and two scroll bars. Scrolling is enabled only when necessary. The example requires horizontal scrolling only because the text does not wrap and therefore is only one line.";
			cv1.Children.Add(txt);
		}

		private void ToolTip(object sender, ClickEventArgs e)
		{
			cv1.Children.Clear();
			cv3.Children.Clear();
			btn = new Button();
			btn.Content = "Button";
			ttp = new ToolTip();
			ttp.Content = "Some useful information.";
			btn.ToolTip = (ttp);
			cv3.Children.Add(btn);
			txt = new Text();
			txt.TextWrap = System.Windows.TextWrap.Wrap;
			txt.TextContent = "ToolTips are small popup windows that show up after particular events are fired in the system or a user hovers over a control. ToolTips can have one or more lines of text, as well as images, and embedded elements, but they do not take focus. The purpose is to present information to the user while not taking focus. Hover over the button control to see the tool tip.";
			cv1.Children.Add(txt);
		}

		private void VerticalScrollBar(object sender, ClickEventArgs e)
		{
			cv1.Children.Clear();
			cv3.Children.Clear();
			vscrollb = new VerticalScrollBar();
			vscrollb.Width = new Length(20);
			vscrollb.Height = new Length(80);
			cv3.Children.Add(vscrollb);
			txt = new Text();
			txt.TextWrap = System.Windows.TextWrap.Wrap;
			txt.TextContent = "A vertical scroll bar is a composite control that encapsulates several buttons (thumb and two repeat buttons) to expose vertical scrolling functionality in a user interface.";
			cv1.Children.Add(txt);
		}

		private void VerticalSlider(object sender, ClickEventArgs e)
		{
			cv1.Children.Clear();
			cv3.Children.Clear();
			vslider = new VerticalSlider();
			vslider.Width = new Length(20);
			vslider.Height = new Length(80);
			cv3.Children.Add(vslider);
			txt = new Text();
			txt.TextWrap = System.Windows.TextWrap.Wrap;
			txt.TextContent = "A vertical slider control, lets a user select from a range of values by moving a thumb. A slider is used to gradually modify a value (range selection). A slider is commonly used in volume controls, but it can be used anywhere a value has a minimum, a maximum, and an increment.";
			cv1.Children.Add(txt);
		}

		private void ContextMenu(object sender, ClickEventArgs e)
		{
			cv1.Children.Clear();
			cv3.Children.Clear();
			btn = new Button();
			btn.Content = "Button with ContextMenu";
			btn.Background = System.Windows.Media.Brushes.Aquamarine;
			contextmenu = new ContextMenu();
			mi = new MenuItem(contextmenu.Context);
			mi.Header = "Item 1";
			contextmenu.Items.Add(mi);
			mia = new MenuItem(contextmenu.Context);
			mia.Header = "Item 2";
			contextmenu.Items.Add(mia);
			btn.ContextMenu = (contextmenu);
			cv3.Children.Add(btn);
			txt = new Text();
			txt.TextWrap = System.Windows.TextWrap.Wrap;
			txt.TextContent = "A context menu enables you to present users with a list of items that specify commands or options associated with a particular control - for example, a button. Users right-click the control to make the menu appear. Typically, clicking a menu item opens a submenu or causes an application to carry out a command. The part of this application that presents the list of the controls for you to select is a context menu attached to a button.";
			cv1.Children.Add(txt);
		}

		private void Thumb(object sender, ClickEventArgs e)
		{
			cv1.Children.Clear();
			cv3.Children.Clear();
			thumb = new Thumb();
			cv3.Children.Add(thumb);
			vscrollb = new VerticalScrollBar();
			vscrollb.Width = new Length(20);
			vscrollb.Height = new Length(80);
			System.Windows.Controls.Canvas.SetLeft(vscrollb, new System.Windows.Length(50));
			cv3.Children.Add(vscrollb);
			txt = new Text();
			txt.TextWrap = System.Windows.TextWrap.Wrap;
			txt.TextContent = "A thumb is typically used in combination with other elements to create encapsulated components, such as scroll bars and sliders. For example, a thumb element enables basic drag functionality for a scroll bar (by creating a scroll box) or resizable window (by creating a window corner). The example shows a thumb and a thumb as it is typically used as an element of a scroll bar.";
			cv1.Children.Add(txt);
		}

		private void TextBox(object sender, ClickEventArgs e)
		{
			cv1.Children.Clear();
			cv3.Children.Clear();
			txtb = new TextBox();
			txtb.MaxHeight = new Length(50);
			txtb.Text = "This is a text box.";
			txtb.Wrap = true;
			txtb.AcceptsReturn = true;
			cv3.Children.Add(txtb);
			txt = new Text();
			txt.TextWrap = System.Windows.TextWrap.Wrap;
			txt.TextContent = "A text box provides an editable region that accepts text input. Enter some text in the sample text box and notice that the text wraps and returns are accepted.";
			cv1.Children.Add(txt);
		}
		
	}
}
