using System;
using System.MessageBus;
using System.MessageBus.Services;
using System.MessageBus.Remoting;
using System.Collections;

namespace System.Remoting 
{

	public class RemotingApplication 
	{

		RemotingManager _remotingManager;
		Uri _uri;
		Port _port;
		RemotingInstanceCollection _instanceCollection;

		public RemotingApplication() 
		{
			Initialize();
		}

		public RemotingApplication(string path) 
		{
		
			_uri = new Uri(path);
			Initialize();
		
		}

		public RemotingApplication(Port port) 
		{
		
			_port = port;
			Initialize();

		}

		private void Initialize() 
		{

			if(_uri != null)
			{
			
				_port = new Port(_uri);
			
			}
			else 
			{
			
				_port = new Port();
			
			}
			
			_remotingManager = new RemotingManager();
			_remotingManager.DialogManager = new DialogManager(_port);
			_instanceCollection = new RemotingInstanceCollection(_remotingManager);

		}

		public Port Port { get { return _port; } set { _port = value; } }

		public RemoteTypeCollection ProxyTypes { get { return _remotingManager.RemoteTypes; } }

		public RemotingInstanceCollection PublishedInstances { get { return _instanceCollection; } }

		public System.MessageBus.Remoting.TypeCollection Services { get { return _remotingManager.PublishedServerTypes; } }

		public string Url { get { return _uri.ToString(); }  }

		public object CreateProxy(Type targetType) 
		{
		
			if(targetType.IsInterface) 
			{

				return GetPublishedObjectProxy(targetType);

			}
			else 
			{
				
				return _remotingManager.CreateInstance(targetType,null);
			
			}
		
		}

		public object CreateProxy(Type targetType, string location) 
		{ 
			
			return _remotingManager.CreateInstance(targetType, null, new Uri(location));
	
		}

		public object CreateProxy(Type targetType, object[] args) 
		{ 
	
			return _remotingManager.CreateInstance(targetType,args);
	
		}

		public object CreateProxy(Type targetType, object[] args, string location) 
		{ 
			
			return _remotingManager.CreateInstance(targetType, args, new Uri(location));
	
		}

		public ObjectToken Marshal(object remotingInstance) { return _remotingManager.Marshal(remotingInstance); }

		public void Open() { _port.Open(); }

		public object Unmarshal(ObjectToken reference) { return _remotingManager.Unmarshal(reference); }

		public object GetPublishedObjectProxy(Type targetType) 
		{

			RemoteType remoteType = _remotingManager.RemoteTypes.GetByTargetType(targetType);
			
			Uri location = new Uri (remoteType.Location.GetLeftPart(UriPartial.Authority));
	
			string token = remoteType.Location.Segments[remoteType.Location.Segments.Length - 1];
			Uri tokenUri = new UriBuilder("urn:", token).Uri;
		
			return _remotingManager.GetObject(location, tokenUri);

		}
			
	}
	
	public class RemotingInstanceCollection : DictionaryBase
	{
		RemotingManager _remotingManager;

		internal RemotingInstanceCollection(RemotingManager remotingManager ) 
		{
			_remotingManager = remotingManager;
		}
		
		public void Add(string relativePath, object instance) 
		{
		
			Uri uri = new UriBuilder("urn:", relativePath).Uri;
			
			_remotingManager.PublishedServerObjects.Add(new PublishedServerObject(instance, uri));
		
		}
	}

}
