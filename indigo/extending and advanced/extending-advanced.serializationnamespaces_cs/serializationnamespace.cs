using System; 
using System.IO;
using System.Serialization;

using Microsoft.Samples.MessageBus.Quickstarts.SerializableType;

namespace Microsoft.Samples.MessageBus.Quickstarts
{
    public class SerializationNamespaceExample
    {
        public static void Main()
        {

            Employee bob = new Employee();
            bob.Firstname = "Robert";
            bob.Surname = "Smith";
            bob.Age = 32;
            Employee janice = new Employee();
            janice.Firstname = "Janice";
            janice.Surname = "Peterson";
            janice.Age = 35;
            Employee manish = new Employee();
            manish.Firstname = "Manish";
            manish.Surname = "Prabhu";
            manish.Age = 26;
            Employee bulent = new Employee();
            bulent.Firstname = "Bulent";
            bulent.Surname = "Yildiz";
            bulent.Age = 56;
            Manager tomek = new Manager();
            tomek.Firstname = "Tomek";
            tomek.Surname = "Janczuk";
            tomek.Age = 25;
            tomek.DirectReports = new Employee[]{ bob, janice, manish, bulent };    

            FileStream stream = File.Create(@"Employee.xml");
            ObjectWriter writer = new ObjectWriter(stream);

            //  Write the Employee instance using the defaults    
            Console.WriteLine("Writing the object to a file...");
            writer.WriteElementObject(tomek);

            writer.Close();
            stream.Close();

            // Now deserialize the stream into an object.
            FileStream newStream = File.Open(@"Employee.xml",FileMode.Open);
            ObjectReader reader = new ObjectReader(newStream);

            //Read the element name & namespace induced from the Employee    
            Console.WriteLine("Reading an object from a file...");
            Manager newManager = (Manager) reader.ReadElementObject(typeof(Manager));
            Console.WriteLine("The {0}'s name is {1}.", newManager.GetType().Name, newManager.Firstname);
            Console.WriteLine("{0}'s direct reports are: ", newManager.Firstname);
            foreach(IEmployee direct in newManager.DirectReports)
            {
                Console.WriteLine("\t" + direct.Firstname + " " + direct.Surname);
            }
        }
    }
}

