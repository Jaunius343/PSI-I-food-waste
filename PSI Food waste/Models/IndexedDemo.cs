// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSI_Food_waste.Models
{
    public class IndexedDemo<T>
    {
        private T[] array = new T[100];

        public T this[int i]
        {
            get => array[i];
            set => array[i] = value;
        }
    }
    public struct StructDemo
    {
        public string Name { get; init; }
        public string Lastname { get; init; }
        public int Age { get; init; }

        public StructDemo(string name, string lname = "-", int age = 0)
        {
            Name = name;
            Lastname = lname;
            Age = age;
        }

        public override string ToString() => $"{Name} {Lastname} {Age}";
    }
}
