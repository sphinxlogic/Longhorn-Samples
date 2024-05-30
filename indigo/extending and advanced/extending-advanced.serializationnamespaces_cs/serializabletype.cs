using System;
using System.Serialization;

[assembly:XsdNamespaceAttribute(ClrNamespace="Microsoft.Samples.MessageBus.Quickstarts.SerializableType", TargetNamespace="http://employees/namespace")]

namespace Microsoft.Samples.MessageBus.Quickstarts.SerializableType
{

	[SerializableTypeAttribute]
	public class Employee : IEmployee
	{
	   protected IManager managerValue = null;
	   protected string surnameValue = string.Empty;
	   protected string firstnameValue = string.Empty;
	   protected string middlenameValue = string.Empty;
	   protected int ageValue = 0;
	   
	   public Employee()
	   {
	      
	   }

	   public IManager Manager
	   {
	      get { return managerValue; }
	      set { managerValue = value; }
	   }

	   public string Surname
	   {
	      get { return surnameValue; }
	      set { surnameValue = value; }
	   }

	   public string Firstname   {
	      get { return firstnameValue; }
	      set { firstnameValue = value; }
	   }

	   public string Middlename{
	      get { return middlenameValue; }
	      set { middlenameValue = value; }
	   }

	   public int Age
	   {
	      get { return ageValue; }
	      set { ageValue = value; }
	   }

	}

	[SerializableTypeAttribute]
	public class Manager : Employee, IManager
	{
	   protected Employee[] directs = null;

	   public Employee[] DirectReports
	   {
	      get { return directs; }
	      set { directs = value; }
	   }
	}

	public interface IManager : IEmployee
	{
	   Employee[] DirectReports { get; set;}
	}

	public interface IEmployee
	{
	   IManager Manager { get; set; }
	   string Surname { get; set; }
	   string Firstname { get; set; }
	   string Middlename { get; set; }
	   int Age { get; set; }
	}
}
