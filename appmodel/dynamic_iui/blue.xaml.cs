using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WCSample
{
  public partial class BlueTask: ObjectPageFunction
  {
    bool lastTask = false;

    public BlueTask(bool l) : this()
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
        returnData = new pfData(pfResponse.NextTask, "String from Blue Task");
        ra.NonGenericResult = returnData;
        OnFinish(ra);
      }
      else if (sender == nextTask && lastTask == true)
      {
        returnData = new pfData(pfResponse.Done, "String from Blue Task");
        ra.NonGenericResult = returnData;
        OnFinish(ra);
      }
      else if (sender == previousTask)
      {
        returnData = new pfData(pfResponse.PreviousTask, "String from Blue Task");
        ra.NonGenericResult = returnData;
        OnFinish(ra);
      }
      else
      {
        returnData = new pfData(pfResponse.Cancel, "String from Blue Task");
        ra.NonGenericResult = returnData;
        OnFinish(ra);
      }
    }
  }
}