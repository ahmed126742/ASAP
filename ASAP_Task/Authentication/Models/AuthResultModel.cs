﻿namespace ASAP_Task.WepApi.Authentication.Models
{
    public class AuthResultModel
    {
        public string Token { get; set; }

        public bool Result { get; set; }

        public List<string> Errors { get; set; }
    }
}
