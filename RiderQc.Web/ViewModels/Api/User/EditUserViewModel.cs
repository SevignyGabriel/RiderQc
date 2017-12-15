﻿using System;

namespace RiderQc.Web.ViewModels.Api.User
{
    public class EditUserViewModel
    {
        public string Region { get; set; }
        public string Ville { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Description { get; set; }
        public string DpUrl { get; set; }
    }
}