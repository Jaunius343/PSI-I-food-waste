// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PSI_Food_waste.Models;
using Newtonsoft.Json;

namespace PSI_Food_waste.Services
{
    public class ProductService
    {
        static List<Product> Products { get; set; }
        static int nextId = 3;
        //static ProductService()
        //{
        //    Products = new List<Product>
        //    {
        //        new Product { Id = 1, Name = "Classic Italian pizza", Price=20.00M, Size=ProductSize.Large, IsGlutenFree = false },
        //        new Product { Id = 2, Name = "Veggie pizza", Price=15.00M, Size=ProductSize.Small, IsGlutenFree = true }
        //    };
        //}

        public static List<Product> GetAll() => Products;

        public static void SetAll(List<Product> products)
        {
            Products = products;
        }

        public static Product Get(int id) => Products.FirstOrDefault(p => p.Id == id);

        public static void Add(Product product)
        {
            product.Id = nextId++;
            Products.Add(product);
        }

        public static void Delete(int id)
        {
            var product = Get(id);
            if (product is null)
                return;

            Products.Remove(product);
        }

        public static void Update(Product product)
        {
            var index = Products.FindIndex(p => p.Id == product.Id);
            if (index == -1)
                return;

            Products[index] = product;

        }
    }
}
