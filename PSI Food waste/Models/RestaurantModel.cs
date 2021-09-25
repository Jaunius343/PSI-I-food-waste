// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PSI_Food_waste.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        [Required]
        public string Name {  get; set;}
        public string Adress {  get; set;}
        public int WorkerID {  get; set;}

    }
}
