namespace SDKSample
{
	using Dialogs = System.Windows.Explorer.Dialogs;

	using System.Windows;
	using System.Windows.Controls;

	using System;
	using System.IO;
	using System.Windows.Explorer;

	public partial class MyPage
	{
		private Item item;

		void OpenButtonClick( Object sender, ClickEventArgs e )
		{
			Dialogs.OpenFileDialog openFileDialog = new Dialogs.OpenFileDialog();

			// Add some common Places to the dialog
			openFileDialog.AddPlace( SpecialFolder.CommonDocuments );
			openFileDialog.AddPlace( new Item( @"library:document\allcategories" ) );

			// Determine which file types to display in the dialog
			Dialogs.FileType[] fileTypes = {
				new Dialogs.FileType( "XYZ files", "*.xyz" ), new Dialogs.FileType( "HTML files", "*.htm" ), new Dialogs.FileType( "All files", "*.*" )
			};

			openFileDialog.FileTypes = fileTypes;

			// Display OpenFileDialog
			if( openFileDialog.ShowDialog( ( UIElement )sender ) == System.Windows.DialogResult.OK )
			{
				item = openFileDialog.Result;
				StreamReader streamReader = new StreamReader( item.FileSysPath );

				textBox.Text = streamReader.ReadToEnd();
			}
		}


		void ClearButtonClick( object sender, ClickEventArgs e )
		{
			textBox.Clear();
			item = null;
		}

		void SaveButtonClick( Object sender, ClickEventArgs e )
		{
			if( item != null )
			{
				SaveFile( item.FileSysPath );
			}
			else
			{
				// Create SaveFileDialog object
				Dialogs.SaveFileDialog saveFileDialog = new Dialogs.SaveFileDialog();

				// Define a default file name and extension
				saveFileDialog.SuggestedSaveName = "Untitled";
				saveFileDialog.DefaultExtension = "xyz";

				// Display SaveFileDialog and check result
				if( saveFileDialog.ShowDialog( ( UIElement )sender ) == System.Windows.DialogResult.OK )
				{
					SaveFile( saveFileDialog.Result.FileSysPath );
				}
			}

			
		}

		void SaveAsButtonClick( Object sender, ClickEventArgs e )
		{
			if( item != null )
			{
				// Create SaveFileDialog object
				Dialogs.SaveAsFileDialog saveAsFileDialog = new Dialogs.SaveAsFileDialog( item );

				// Display SaveFileDialog and check result
				if( saveAsFileDialog.ShowDialog( ( UIElement )sender ) == System.Windows.DialogResult.OK )
				{
					SaveFile( saveAsFileDialog.Result.FileSysPath );
				}
			}
			else
			{
				// Create SaveFileDialog object
				Dialogs.SaveFileDialog saveFileDialog = new Dialogs.SaveFileDialog();

				// Define a default file name and extension
				saveFileDialog.SuggestedSaveName = "Untitled";
				saveFileDialog.DefaultExtension = "xyz";

				// Display SaveFileDialog and check result
				if( saveFileDialog.ShowDialog( ( UIElement )sender ) == System.Windows.DialogResult.OK )
				{
					SaveFile( saveFileDialog.Result.FileSysPath );
				}
			}
		}

		void SaveFile( string filePath )
		{
			// Write contents of TextBox to the selected file
			FileStream fileStream = new FileStream( filePath, FileMode.OpenOrCreate, FileAccess.Write );
			StreamWriter streamWriter = new StreamWriter( fileStream );

			try
			{
				

				streamWriter.Write( textBox.Text );
			}
			finally
			{
				streamWriter.Close();
				fileStream.Close();
			}
		}
	}
}