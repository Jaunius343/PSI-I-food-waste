﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PSI_Food_waste.Data;
using PSI_Food_waste.Models;

namespace PSI_Food_waste.Services
{
    public class RegisterService : IRegisterRepository      
    {
        public Lazy<RegisteredUser<RegisterForm>> Users { get; set; }

        public RegisterForm CurrentUser { get; set; }

        //for testing only
        //private readonly ProductContext _context;

        public RegisterService()
        {
            Users = new Lazy<RegisteredUser<RegisterForm>>();
            //FillList();
            //Users = new RegisteredUser<RegisterForm>();
            //Users.Value.Add(new RegisterForm(new List<Restaurant>(), new List<Restaurant>(), "admin", "admin", "admin", 0));
        }

        //private void FillList()
        //{
        //    for(int i = 0; i < 99; ++i)
        //    {
        //        Users.Value.Add(new RegisterForm(new List<Restaurant>(), "abc@gmail.com", "test", "test", 1));
        //    }
        //}

        public RegisteredUser<RegisterForm> GetAll() => Users.Value;

        public Lazy<RegisteredUser<RegisterForm>> GetUsersObject() => Users;

        public RegisterForm GetUserData(string email)
        {
            for (int i = 0; i < Users.Value.Length(); i++)
            {
                if (Users.Value[i].Email == email)
                {
                    return Users.Value[i];
                }
            }
            //should never happen :)
            return new RegisterForm();
        }
        public void SetAll(RegisteredUser<RegisterForm> users)
        {
            Users = new Lazy<RegisteredUser<RegisterForm>>(() => users);
        }

        public void AddToList(RegisterForm user)
        {
            Users.Value.Add(user);
        }
    }

}
