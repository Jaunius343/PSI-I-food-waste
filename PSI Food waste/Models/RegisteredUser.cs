// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSI_Food_waste.Models
{
    public class RegisteredUser<T>
    {
        private T[] array = new T[100];
        int nextIndex = 0;

        public T this[int i]
        {
            get => array[i];
            set => array[i] = value;
        }

        public void Add(T value)
        {
            if (nextIndex >= array.Length)
                throw new IndexOutOfRangeException($"The collection can hold only {array.Length} elements.");
            array[nextIndex++] = value;
        }

        public int Length ()
        {
            return nextIndex;
        }
    }

    public struct RegisterForm
    {
        public string Email { get; set; }
        public string Username { get; init; }
        public string Password { get; init; }
        public int FavNum { get; init; }
        public List<Restaurant> SubscribedRestaurants {  get; set; } 

        public RegisterForm(List<Restaurant> restaurants, string email, string name, string pass, int favNum = 0)
        {
            SubscribedRestaurants = restaurants;
            Email = email;
            Username = name;
            Password = pass;
            FavNum = favNum;
        }
    }
}
