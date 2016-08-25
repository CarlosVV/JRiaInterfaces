﻿using System;
namespace CES.CoreApi.Payout.ViewModels
{
	public class PayoutOrderRequest
	{
		public int AgentId { get; set; }
		public int AgentLocId { get; set; }
		public int UserId { get; set; }
		public int UserLoginId { get; set; }
		public string Locale { get; set; }
		public string OrderId { get; set; }
		public string OrderPin { get; set; }
		public string CountryTo { get; set; }
		public string StateTo { get; set; }
		public DateTime LocalTime { get;  set; }

		public SampleRequest @SampleRequest { get; set; }

	}
	public class SampleRequest 
	{
		public int Id { get; set; }
	}
}