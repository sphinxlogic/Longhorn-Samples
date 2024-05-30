using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WCSample
{
  public class NavHub : ObjectPageFunction
  {
    NavigationApplication myApp;
    NavigationWindow navWindow;
    int currentTask;
    object[] tasks ;
    string[] navHubReturnStrings;

    protected override void Start()
    {
      navHubReturnStrings = new string[4];
      this.KeepAlive = true;
      myApp = (NavigationApplication) System.Windows.Application.Current;
      navWindow = (NavigationWindow) myApp.MainWindow;

      //pick a random starting task, to vary the order of the pages
      DateTime currentTime = DateTime.Now;
      int startTask = currentTime.Millisecond % 3;
      tasks = new object[3];
      
      switch (startTask)
      {
        case 1 :
          tasks[0] = new YellowTask();
          tasks[1] = new BlueTask();
          tasks[2] = new GreenTask(true);
          break;
        case 2:
          tasks[0] = new GreenTask();
          tasks[1] = new YellowTask();
          tasks[2] = new BlueTask(true);
          break;

        default :
          tasks[0] = new BlueTask();
          tasks[1] = new GreenTask();
          tasks[2] = new YellowTask(true);
          break;
      }
     currentTask = 1;

      ((ObjectPageFunction) tasks[0]).NonGenericReturn += new ObjectReturnEventHandler(task_Return);
      ((ObjectPageFunction)tasks[1]).NonGenericReturn += new ObjectReturnEventHandler(task_Return);
      ((ObjectPageFunction)tasks[2]).NonGenericReturn += new ObjectReturnEventHandler(task_Return);
      navWindow.Navigate((ObjectPageFunction)tasks[0]);
    }

    public void task_Return(object sender, ObjectReturnEventArgs e)
    {
      pfData returnedData = (pfData)e.NonGenericResult;

      ObjectReturnEventArgs ra = new ObjectReturnEventArgs();
      
      //assign returned string to the list of strings to be returned to the start page
      //currentTask==0 is reserved for the response value
      navHubReturnStrings[currentTask] = returnedData.responseString;

      switch (returnedData.responseType)
      {
        case pfResponse.NextTask:
          ++currentTask;
          break;

        case pfResponse.PreviousTask:
          if (currentTask == 1)
          {
            navHubReturnStrings[0] = "Cancelled";
            ra.NonGenericResult = navHubReturnStrings;
            OnFinish(ra);
            return;
          }
          else
            --currentTask;
          break;

        case pfResponse.Cancel:
          navHubReturnStrings[0] = "Cancelled";
          ra.NonGenericResult = navHubReturnStrings;
          OnFinish(ra);
          return;

        case pfResponse.Done :
          navHubReturnStrings[0] = "Done";
          ra.NonGenericResult = navHubReturnStrings;
          OnFinish(ra);
          return;
      }

      navWindow.Navigate((ObjectPageFunction)tasks[currentTask - 1]);
    }
  }

  //Holds data returned from a task page
  public class pfData
  {
    pfResponse type;
    string details;

    public pfData(pfResponse r, string s)
    {
      type = r;
      details = s;
    }

    public pfResponse responseType
    {
      get {return type;}

      set{type = value;}
    }

    public string responseString
    {
      get { return details; }
      set { details = value; }
    }   
  }

  public enum pfResponse
  {
    NextTask,
    PreviousTask,
    Cancel,
    Done
  }
}