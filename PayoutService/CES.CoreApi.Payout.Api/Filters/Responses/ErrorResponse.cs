﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Payout.Api.Filters.Responses
{
	public class ErrorResponse
	{
		public List<Error> Errors { get; set; }
		public int ResultCode { get; set; }
		public string Message { get; set; }
		public Guid ResponseId { get; private set; }
		public ErrorResponse(string message, int resultCode, List<Error> errors, Guid responseId)
		{
			Errors = errors;
			Message = message;
			ResultCode = resultCode;
			ResponseId = responseId;
		}
	}
}