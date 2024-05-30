using System;
using System.Collaboration;
using System.Xml.Serialization;

namespace Microsoft.Samples.Collaboration.RtcSample
{
    [Serializable()]
    public class TestPresence : ApplicationPresence
    {
        public const string TestPresencePublisher = "TestProvider.TestPresence";

        public TestPresence() : base(TestPresence.TestPresencePublisher)
        {
        }

        public string Home
        {
            get
            {
                return this.home;
            }
            set
            {
                this.home = value;
            }
        }

        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
            }
        }

        public string Information
        {
            get
            {
                return this.information;
            }
            set
            {
                this.information = value;
            }
        }

        public string TestString
        {
            get
            {
                return this.testString;
            }

            set
            {
                this.testString = value;
            }
        }

        public int TestValue
        {
            get
            {
                return this.testValue;
            }

            set
            {
                this.testValue = value;
            }
        }

        private string home;
        private string email;
        private string information;
        private string testString;
        private int    testValue;
    }
}
