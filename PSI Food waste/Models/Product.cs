// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PSI_Food_waste.Models
{
    public class Product : IComparable
    {
        public int RestId {  get; set; }

        public int PrId { get; set; }
        

        [Required]
        public string Name { get; set; }
        public ProductSize Size { get; set; }
        public bool IsGlutenFree { get; set; }

        [Range(0.01, 9999.99)]
        public double Price { get; set; }

        [Range(0, 100)]
        public int Discount { get; set; }

        //public int CompareTo(object obj) => Price.CompareTo(obj);
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Product otherProduct = obj as Product;
            if (otherProduct != null)
                return this.Price.CompareTo(otherProduct.Price);
            else
                throw new ArgumentException("Object is not a Product");
        }

    }

    public enum ProductSize { Small, Medium, Large }
}
