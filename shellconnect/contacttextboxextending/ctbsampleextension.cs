//---------------------------------------------------------------------
//  This file is part of the Microsoft .NET Framework SDK Code Samples.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
// 
//This source code is intended only as a supplement to Microsoft
//Development Tools and/or on-line documentation.  See these other
//materials for detailed information regarding Microsoft code samples.
// 
//THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
//KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//PARTICULAR PURPOSE.
//---------------------------------------------------------------------
using System;
using System.IO;
using System.Collections;
using System.Threading;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;

//Mark the assembly as being CLS compliant
[assembly: CLSCompliant(true)]

//Mark the assembly as not visible through COM interop
[assembly: System.Runtime.InteropServices.ComVisible(false)]

namespace Microsoft.Samples.Communications
{
    //This class is extending the ResolvingTextBox to use our own implemented resolver and redefine the way visuals are displayed
    internal class ExtendedCTB : ResolvingTextBox
    {
        ResolverCollection myResolvers = null;

        //Overriding this method allows our control to change the collection of resolvers it will be using
        protected override ResolverCollection GetDefaultResolvers()
        {
            if (myResolvers == null)
            {
                myResolvers = new ResolverCollection(1);
                myResolvers.Add(new MyResolver("MyData.txt"));
            }

            return myResolvers;
        }

        //Overriding this method allows our control to change the visuals that will be used to represent resolved items
        protected override FrameworkElement CreateResolvedItemVisual(ResolvedItem resolvedItem)
        {
            DockPanel container = new DockPanel();

            Text text = new Text();
            text.TextContent = resolvedItem.ResolvedText;
            text.TextWrap = TextWrap.NoWrap;
            TextDecorations textDecorations = new TextDecorations();
            textDecorations.Add(TextDecoration.Underline);
            text.TextDecorations = textDecorations;
            text.FontStyle = FontStyle.Italic;
            text.Foreground = new SolidColorBrush(Colors.Blue);
            container.Children.Add(text);

            return container;
        }
    }

    //This class is extending the Resolver, and will resolve items using the provided txt file as a data source
    internal class MyResolver : Resolver
    {
        private string fileName = null;
        internal ArrayList myFullList = null;

        internal MyResolver(string fileName)
        {
            this.fileName = fileName;
        }

        //Overriding this method allows our resolver to provide its initialization mechanism
        public override void PrimeResolver()
        {
            if (this.myFullList == null)
            {
                lock (this)
                {
                    if (this.myFullList == null)
                    {
                        this.myFullList = new ArrayList();
                        new Thread(this.PopulateData).Start();
                    }
                }
            }
        }

        //Overriding this method allows our resolver to spawn a resolution worker that will
        //eventually be called in a background thread to try to resolve the given string
        public override ResolutionWorker CreateWorker(string unresolvedText)
        {
            return new MyResolutionWorker(this, unresolvedText);
        }

        private void PopulateData()
        {
            StreamReader file = null;
            String element;

            try
            {
                file = new StreamReader(this.fileName);
                while ((element = file.ReadLine()) != null)
                {
                    lock (this)
                    {
                        this.myFullList.Add(element);
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.StackTrace, e.Message);
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                }
            }
        }

        //This class is extending the ResolutionWorker, which will actually perform the job of resolving each item through our Resolver
        private class MyResolutionWorker : ResolutionWorker
        {
            protected internal MyResolutionWorker(Resolver resolver, String unresolvedText) : base(resolver, unresolvedText) { }

            //Overriding this method allows our resolution worker to try to go ahead and try to resolve its UnresolvedText member
            //It is always called in a background thread by the owning ResolvingTextbox
            protected override void TryToResolve()
            {
                ArrayList myResolvedList = new ArrayList();

                lock (this.Resolver)
                {
                    foreach (String element in ((MyResolver)this.Resolver).myFullList)
                    {
                        if (element.Contains(UnresolvedText))
                        {
                            myResolvedList.Add(new MyResolvedItem(element));
                        }
                    }
                }

                ResolvedItem[] myFinalList = new ResolvedItem[myResolvedList.Count];
                myResolvedList.CopyTo(myFinalList);

                //This should be called to notify the owning ResolvingTextBox that resolution is finished
                FinishResolving(myFinalList);
            }
        }
    }

    //This class is extending the ResolvedItem to store the resolved information in a ResolutionWorker
    //It is also used to display the items within our ResolvingTextBox
    internal class MyResolvedItem : ResolvedItem
    {
        private String myResolvedText;

        internal MyResolvedItem(String resolvedText)
        {
            myResolvedText = resolvedText;
        }

        //Overriding this property allows our class to define the text that will be displayed for resolved items
        public override string ResolvedText
        {
            get
            {
                return myResolvedText;
            }
        }
    }

    internal class MyWindow : Window
    {
        private ExtendedCTB contactTB1, contactTB2;

        internal MyWindow(): base()
        {
            //Sets location, size and back color of the window
            this.Text = "MyCTBWindow";
            this.Width = new Length(600);
            this.Height = new Length(300);
            this.WindowAutoLocation = WindowAutoLocation.CenterScreen;

            //Creates a DockPanel
            DockPanel dockPanel = new DockPanel();

            //Creates a ExtendedCTB and adds it to the DockPanel
            contactTB1 = new ExtendedCTB();
            contactTB1.Height = new Length(50);
            DockPanel.SetDock(contactTB1, Dock.Top);
            dockPanel.Children.Add(contactTB1);

            //Creates another ExtendedCTB and adds it to the DockPanel
            contactTB2 = new ExtendedCTB();
            contactTB2.Height = new Length(50);
            contactTB2.IsAutoSuggestEnabled = false;
            contactTB2.IsBackgroundResolutionEnabled = false;
            contactTB2.IsReadOnly = true;
            DockPanel.SetDock(contactTB2, Dock.Top);
            dockPanel.Children.Add(contactTB2);

            //Creates a resolve button and adds it to the DockPanel
            Button resolve = new Button();
            resolve.Content = "Resolve";
            resolve.AddHandler(Button.ClickEventID, new ClickEventHandler(OnResolveClicked));
            DockPanel.SetDock(resolve, Dock.Top);
            dockPanel.Children.Add(resolve);

            //Creates a transfer button and adds it to the DockPanel
            Button transfer = new Button();
            transfer.Content = "Transfer contents";
            transfer.AddHandler(Button.ClickEventID, new ClickEventHandler(OnTransferClicked));
            DockPanel.SetDock(transfer, Dock.Top);
            dockPanel.Children.Add(transfer);

            //Adds the DockPanel
            Content = dockPanel;
        }

        //When the resolve is clicked, resolve contacts
        private void OnResolveClicked(object e, ClickEventArgs args)
        {
            contactTB1.Resolve();
        }

        //When the transfer is clicked, it will copy all the contacts in the first ResolvingTextBox to the second one
        private void OnTransferClicked(object e, ClickEventArgs args)
        {
            ResolvedItem[] list = contactTB1.ResolvedItems;
            foreach (ResolvedItem element in list)
            {
                contactTB2.Add(element);
            }
        }
    }

    public class MyApp : Application
    {
        [STAThread]
        public static void Main()
        {
            MyApp sampleApp = new MyApp();
            sampleApp.Run();
        }

        protected override void OnStartingUp(StartingUpCancelEventArgs e)
        {
            base.OnStartingUp(e);

            MyWindow sampleWindow = new MyWindow();
            sampleWindow.Show();
        }
    }
}
