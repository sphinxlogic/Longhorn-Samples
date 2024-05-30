using System; 

namespace Microsoft.Samples.MessageBus.Quickstarts.RemoteObjects
{
    public class RemotableObject : MarshalByRefObject
    {
        public RemotableObject()
        {
            Console.WriteLine("Object {0} has been created.", this.GetHashCode().ToString());
        }

        public string Hello(string name)
        {
            return String.Format("Hello, {0}. This is RemotableObject instance \"{1}\"", name, this.GetHashCode().ToString());
        }
    
        ~RemotableObject()
        {
            Console.WriteLine("Object {0} is being torn down.", this.GetHashCode().ToString());
        }
    }
}