using System;
using System.Collections.Generic;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Providers
{
	public class RemoteClientInformationProvider : IRemoteClientInformationProvider
	{
		public void AddDetails(ExceptionLogDataContainer exceptionLogDataContainer, RemoteClientLogInfo remoteClientLogInfo, Func<IDictionary<string, object>> getClientDetails)
		{
			var group = exceptionLogDataContainer.GetGroupByTitle("Remote Client Details");
			//Need to handle possible exceptions happened during host name detection
			try
			{
				group.AddItem("Host Name", remoteClientLogInfo.Hostname);
			}
			catch (Exception)
			{
				group.AddItem("Host Name", "Cannot be detected");
			}
			group.AddItem("Port", remoteClientLogInfo.Port);
			group.AddItem("IP Address", remoteClientLogInfo.Address);
			group.AddItem("User Name", remoteClientLogInfo.UserName);

			//Populate additonal client details - usually security context coming from service
			foreach (var entry in getClientDetails())
			{
				group.AddItem(entry.Key, entry.Value);
			}
		}
	}
}
