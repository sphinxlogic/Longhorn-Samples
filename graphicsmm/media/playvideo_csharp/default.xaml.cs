//------------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Microsoft.Samples.Communications
{
  public partial class PlayVideo{
    public Video v1;

    public void OnLoad(object sender, EventArgs e)
    {
      VideoData vd1 = new VideoData("bee.wmv");
      v1 = new Video();
      v1.Source = vd1;
      dp1.Children.Add(v1);
    }

    void OnStop(object sender, System.Windows.Controls.ClickEventArgs e)
      {
        v1.EndIn(0);
      }
      void OnPause(object sender, System.Windows.Controls.ClickEventArgs e)
      {
	if(v1.MediaState == System.Windows.Media.MediaState.MediaStatePaused)
        {   v1.Resume(); }
        else if (v1.MediaState == System.Windows.Media.MediaState.MediaStatePlaying)
        {   v1.Pause(); }
        else
        {
            System.Windows.MessageBox.Show("The media cannot be paused or resumed.");
        } 
	
      }
      void OnPlay(object sender, System.Windows.Controls.ClickEventArgs e)
      {
        v1.BeginIn(0);
      }

      void OnMute(object sender, System.Windows.Controls.ClickEventArgs e)
      {
        if(v1.Mute == true)
          v1.Mute = false;
        else
          v1.Mute = true;
      }


  }
}
