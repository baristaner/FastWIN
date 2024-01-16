﻿using fastwin.Entities;
using MediatR;

namespace fastwin.Requests
{
    public class LoginReq 
    {
        public string Email { get; set;}
        public string Password { get; set;}
    }
}
