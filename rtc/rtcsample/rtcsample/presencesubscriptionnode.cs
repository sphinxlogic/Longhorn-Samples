using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collaboration;

namespace Microsoft.Samples.Collaboration.RtcSample
{
    /// <summary>
    /// Defines a tree node associated with a presence subscription.
    /// </summary>
    sealed public class PresenceSubscriptionNode : TreeNode
    {
        public PresenceSubscriptionNode(PresenceSubscription presenceSubscription) : 

            base(presenceSubscription.RealTimeAddress)
        {
            this.presenceSubscription = presenceSubscription;
            base.Tag = presenceSubscription;

            this.SetPresenceColor();
            this.presenceSubscription.PresenceChanged += 
                new EventHandler(this.presenceSubscription_PresenceChanged);

            if(!presenceSubscription.IsStarted)
            {
                presenceSubscription.Start();
            }
        }

        public void SetPresenceColor()
        {
            base.Text = this.presenceSubscription.RealTimeAddress;

            if(this.presenceSubscription.IsStarted)
            {
                base.ForeColor = Color.Red;
                
                base.Nodes.Clear();

                foreach(
                    EndpointPresence endpointPresence in 
                    presenceSubscription.Presence.EndpointPresenceList)
                {
                    string name = endpointPresence.DisplayName;

                    if ( name == null || name.Length == 0)
                    {
                        name = endpointPresence.EndpointID;
                    }

                    TreeNode endpoint = new TreeNode(name);
                    endpoint.Tag = endpointPresence;

                    if(endpointPresence.PresenceState != PresenceState.Offline)
                    {
                        endpoint.ForeColor = Color.Green;
                    }
                    else
                    {
                        endpoint.ForeColor = Color.Red;
                    }

                    base.Nodes.Add(endpoint);
                }

                foreach(
                    EndpointPresence endpointPresence in 
                    presenceSubscription.Presence.EndpointPresenceList)
                {
                    if(endpointPresence.PresenceState != PresenceState.Offline)
                    {
                        base.ForeColor = Color.Green;
                        break;
                    }
                }
            }
            else
            {
                base.ForeColor = Color.Gray;
            }
        }

        private void presenceSubscription_PresenceChanged(object sender, EventArgs e)
        {
            PresenceSubscription    presenceSubscription;

            presenceSubscription = (PresenceSubscription) sender;
            
            this.SetPresenceColor();
        }

        public PresenceSubscription PresenceSubscription
        {
            get
            {
                return this.presenceSubscription;
            }
        }

        private PresenceSubscription presenceSubscription;
    }
}
