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
    public class ProductService : IProductRepository
    {
        List<Product> Products { get; set; }
        List<Product> IdProducts = new List<Product>();
        int nextId = 7;

        //static ProductService()
        //{
        //    Products = new List<Product>
        //    {
        //        new Product { Id = 1, Name = "Classic Italian pizza", Price=20.00M, Size=ProductSize.Large, IsGlutenFree = false },
        //        new Product { Id = 2, Name = "Veggie pizza", Price=15.00M, Size=ProductSize.Small, IsGlutenFree = true }
        //    };
        //}

        


        public IRestaurantRepository _restaurantRepository;

        public ProductService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
            string initialData = (Directory.GetCurrentDirectory() + "\\text.json");
            string json = File.ReadAllText(@initialData);
            List<Product> myObj = JsonConvert.DeserializeObject<List<Product>>(json);
            Products = myObj;
        }

        public List<Product> GetAll() => Products;

        public void SetAll(List<Product> products)
        {
            Products = products;
        }

        public Product Get(int id) => Products.FirstOrDefault(p => p.PrId == id);

        public List<Product> GetList(int id)
        {

            IdProducts.Clear();
           foreach(Product prod in Products ?? new List<Product>())
            {
                if(prod.RestId == id)
                    IdProducts.Add(prod);
            }
            return IdProducts;
        }
        public void AddToList(Product product)
        {
            IdProducts.Add(product);
        }
        public void Add(Product product)
        {
            if (Products == null)
            {
                Products = new List<Product>();
            }
            product.RestId = _restaurantRepository.GetID();
            product.PrId = nextId++;
            Products.Add(product);
            
        }

        public void Delete(int id)
        {
            var product = Get(id);
            if (product is null)
                return;

            Products.Remove(product);
        }
        public void Update(Product product)
        {
            var index = Products.FindIndex(p => p.PrId == product.PrId);
            if (index == -1)
                return;

            Products[index] = product;

        }
        public void NewPrice(Product product)
        {
            product.DiscountedPrice = product.Price * (1 - (double)product.Discount/ 100);
        }
        public void SortProducts()
        {
            Products?.Sort();
        }
    }
}
