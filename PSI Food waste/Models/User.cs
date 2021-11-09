// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PSI_Food_waste.Models
{
    public class User : IEquatable<User>
    {
        [Required]
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        [Required]
        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        [Required]
        private string password;
        public string Password
        {
            get => password;
            set => password = value;
        }

        public bool Equals (User other)
        {
            if (other == null)
                return false;
            if (this.Email == other.Email && this.Password == other.Password)
                return true;
            else
                return false;
        }
    }
}
