﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PSI_Food_waste.Models;

namespace PSI_Food_waste.Services
{
    public static class RegisterService         //TODO: change from static to DI
    {
        static RegisteredUser<RegisterForm> Users { get; set; }

        static RegisterService()
        {
            Users = new RegisteredUser<RegisterForm>();
            Users.Add(new RegisterForm(new List<Restaurant>(), "abc@gmail.com", "test", "test", 1));
        }

        public static RegisteredUser<RegisterForm> GetAll() => Users;

        public static RegisterForm GetUserData(string email)
        {
            for (int i = 0; i < Users.Length(); i++)
            {
                if (Users[i].Email == email)
                {
                    return Users[i];
                }
            }
            //should never happen :)
            return new RegisterForm();
        }
        public static void SetAll(RegisteredUser<RegisterForm> users)
        {
            Users = users;
        }
    }
    public static class AddingExtension
    {
        public static RegisteredUser<RegisterForm> AddToList(this RegisterForm user, RegisteredUser<RegisterForm> users)
        {
            users.Add(user);
            return users;
        }
    }

}
