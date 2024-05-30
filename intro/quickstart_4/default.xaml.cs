namespace WCSample     // Namespace must be the same as what you set in project file
{
  using System;
  using System.Windows.Controls;

  public partial class MyPage
  {
    void HandleClick(object sender, ClickEventArgs e)
    {
      Button1.Content ="Hello World";
    }
  }
}