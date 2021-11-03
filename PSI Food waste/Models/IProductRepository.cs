// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSI_Food_waste.Models
{
    public interface IProductRepository
    {
        public List<Product> GetList(int id);
        public void SortProducts();

        public void Add(Product product);

        public void Delete(int id);

        public List<Product> GetAll();

        public void SetAll(List<Product> products);

        public Product Get(int id);

        public void NewPrice(Product product);
    }
}
