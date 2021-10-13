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
}
