using System;
using System.MessageBus.Remoting;

namespace Microsoft.Samples.MessageBus.Quickstarts.RemoteObjects
{
    [RemotableAttribute]
    public interface IRemoteInterface
    {
        string Hello(string name);

    }
}