using System; 

namespace Microsoft.Samples.MessageBus.Quickstarts.RemoteObjects
{
    public class RemotableObject : MarshalByRefObject
    {
        private int clientCount = 0;

        public RemotableObject()
        {
            Console.WriteLine("Object {0} has been created.", this.GetHashCode().ToString());
        }

        public string Hello(string name)
        {    
            ++clientCount;
            Console.WriteLine("{0} has said Hello. This instance has been called {1} times.", name, clientCount.ToString());
            return String.Format("Hello, {0}. This is RemotableObject instance \"{1}\"", name, this.GetHashCode().ToString());
        }
    
        ~RemotableObject()
        {
            Console.WriteLine("Object {0} is being torn down.", this.GetHashCode().ToString());
        }
    }
}
