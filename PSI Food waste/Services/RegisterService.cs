﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PSI_Food_waste.Models;

namespace PSI_Food_waste.Services
{
    public static class RegisterService
    {
        static RegisteredUser<RegisterForm> Users { get; set; }

        static RegisterService()
        {
            Users = new RegisteredUser<RegisterForm>();
            Users.Add(new RegisterForm("test", "test", 1));
        }

        public static RegisteredUser<RegisterForm> GetAll() => Users;

        public static void AddToList(RegisterForm user)
        {
            Users.Add(user);
        }
    }
}