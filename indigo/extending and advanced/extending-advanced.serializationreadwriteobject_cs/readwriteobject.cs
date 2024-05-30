using System; 
using System.IO;
using System.Serialization;

namespace Microsoft.Samples.MessageBus.Quickstarts
{

    public class ReadWriteObjectExample
    {
        public static void Main()
        {

            Person person = new Person();
            person.SetName("Robert");
            person.Age = 34;

            FileStream stream = File.Create(@"Person.xml");
            ObjectWriter writer = new ObjectWriter(stream);

            //  Write the Person instance using the defaults    
            Console.WriteLine("Writing the object to a file...");
            writer.WriteStartElement("Ser", "PersonElement", "http://this.Person");
            writer.WriteObject(person, typeof(Person));
            writer.WriteEndElement();
            writer.Close();
            stream.Close();

            // Now deserialize the stream into an object.
            FileStream newStream = File.Open(@"Person.xml",FileMode.Open);
            ObjectReader reader = new ObjectReader(newStream);

            //Read the element name & namespace induced from the Person    
            Console.WriteLine("Reading an object from a file...");
            // Note that IsStartElement must be used here to correctly 
            // position the current element for ReadObject
            if (reader.IsStartElement("PersonElement", "http://this.Person"))
            {
                Person newPerson = (Person)reader.ReadObject(typeof(Person));
                Console.WriteLine("The name in the object is {0}.", newPerson.Name);
                reader.ReadEndElement();
            }
        }
    }
}
