using CES.CoreApi.Compliance.Screening.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Compliance.Screening.ViewModels
{
    public class ScreeningResponse
    {
        public ScreeningResponse()
        {
        }

        public string Status { get; set; }

        public string Code
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public bool HoldFlag
        {
            get;
            set;
        }

        public string ActionTaken
        {
            get;
            set;
        }
    }
}