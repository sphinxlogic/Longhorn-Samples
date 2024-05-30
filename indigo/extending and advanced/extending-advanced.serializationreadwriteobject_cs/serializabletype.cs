using System;
using System.Serialization;

namespace Microsoft.Samples.MessageBus.Quickstarts
{

    [SerializableTypeAttribute]
    public class Person
    {

        private string nameValue;
    
        public int Age;

        public string Name
        {
            get { return nameValue; }
            set { nameValue = value; }
        }

        public void SetName(string nameString)
        {
            this.Name = nameString;
        }
 
    }
}
