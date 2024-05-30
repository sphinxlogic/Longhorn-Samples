using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WCSample
{
  public partial class YellowTask : ObjectPageFunction
  {
    bool lastTask = false;

    public YellowTask(bool l) :this()
    {
      lastTask = l;

      if (lastTask == true)
      {
        nextTask.Content = "Done";
      }
    }
    
    private void NextTask(object sender, ClickEventArgs e)
    {
      pfData returnData;
      
      ObjectReturnEventArgs ra = new ObjectReturnEventArgs();
     
      if (sender == nextTask && lastTask == false)
      {
        returnData = new pfData(pfResponse.NextTask, "String from Yellow Task");
        ra.NonGenericResult = returnData;
        OnFinish(ra);
      }
      else if (sender == nextTask && lastTask == true)
      {
        returnData = new pfData(pfResponse.Done, "String from Yellow Task");
        ra.NonGenericResult = returnData;
        OnFinish(ra);
      }
      else if (sender == previousTask)
      {
        returnData = new pfData(pfResponse.PreviousTask, "String from Yellow Task");
        ra.NonGenericResult = returnData;
        OnFinish(ra);
      }
      else
      {
        returnData = new pfData(pfResponse.Cancel, "String from Yellow Task");
        ra.NonGenericResult = returnData;
        OnFinish(ra);
      }
    }
  }
}