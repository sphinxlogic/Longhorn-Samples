namespace ExpenseItApp
{
    using System;
    using System.Windows;
    using System.Windows.Navigation;
    using System.Windows.Controls;
    using System.Collections;

    public partial class Page2
    {
        SubmitDialog dlgSubmit; // Dialog verification for Submitting an expense

        private void Navigate(object sender, ClickEventArgs args)
        {
            Application.Navigate(sender);
        }
        
        private void AddExpense(object sender, ClickEventArgs args)
        {
            Expense expense = (Expense) Application.FindResource("ExpenseData");

            expense.LineItems.Add(new LineItem());
        }

        private void ViewChart(object sender, ClickEventArgs args)
        {
			NavigationApplication myApp;
			NavigationWindow mainWindow;

			myApp = (NavigationApplication)System.Windows.Application.Current;
			mainWindow = (NavigationWindow)myApp.MainWindow;
			mainWindow.Navigate(new Uri("page3.xaml", false, true));
        }

        #region Submit 
        private void SubmitExpense(object sender, ClickEventArgs args)
        {
            NavigationWindow window = (NavigationWindow)Application.Windows[0];

            dlgSubmit = new SubmitDialog();
            dlgSubmit.Owner = window;
            dlgSubmit.Show();
            dlgSubmit.Closed += new System.EventHandler(SubmitDlgOnClosed);
        }

        void SubmitDlgOnClosed(object sender, System.EventArgs e)
        {
        /* process yes or no from the SubmitDialog, and perform appropriate action
            if (dlg.DialogResult == DialogResult.OK)
                Text1.TextRange.Text = "Clicked Yes";

            if (dlg.DialogResult == DialogResult.No)
                Text1.TextRange.Text = "Clicked No";
        */
        }
        # endregion Submit
        
    }
}

