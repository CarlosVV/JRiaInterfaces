using CES.CoreApi.Data.Attributes;
using System.Runtime.Serialization;
//using CES.CoreApi.Common.Attributes;

namespace CES.CoreApi.Data.Enumerations
{
	//[DataContract]
	public enum DatabaseType
	{
		[ConnectionName("")]
		Undefined,
		[ConnectionName("Main")]
		Main,
		[ConnectionName("ReadOnly")]
		ReadOnly,
		[ConnectionName("FrontEnd")]
		FrontEnd,
		[ConnectionName("Image")]
		Image,
	}
}