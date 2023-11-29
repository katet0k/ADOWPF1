using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static ClassLibrary1.Class1;

public class ViewModel
{
    public ObservableCollection<Product> Products { get; set; }
    public IEnumerable ProductTypes { get; internal set; }
    public IEnumerable Suppliers { get; internal set; }

    public ViewModel()
    {
        Products = new ObservableCollection<Product>
        {
            new Product { ProductName = "Продукт1", ProductType = "Тип1", Supplier = "Постачальник1", Quantity = 10, CostPrice = 100, SupplyDate = DateTime.Now },
            new Product { ProductName = "Продукт2", ProductType = "Тип2", Supplier = "Постачальник2", Quantity = 20, CostPrice = 200, SupplyDate = DateTime.Now.AddDays(-5) },
           
        };
    }

    public List<Product> GetAllProducts()
    {
        return Products.ToList();
    }

    public List<string> GetAllProductTypes()
    {
        return Products.Select(p => p.ProductType).Distinct().ToList();
    }

    public List<string> GetAllSuppliers()
    {
        return Products.Select(p => p.Supplier).Distinct().ToList();
    }

    public Product GetProductWithMaxQuantity()
    {
        return Products.OrderByDescending(p => p.Quantity).FirstOrDefault();
    }

    public Product GetProductWithMinQuantity()
    {
        return Products.OrderBy(p => p.Quantity).FirstOrDefault();
    }

    public Product GetProductWithMinCostPrice()
    {
        return Products.OrderBy(p => p.CostPrice).FirstOrDefault();
    }

    public Product GetProductWithMaxCostPrice()
    {
        return Products.OrderByDescending(p => p.CostPrice).FirstOrDefault();
    }
    public List<Product> GetProductsByCategory(string category)
    {
        return Products.Where(p => p.ProductType == category).ToList();
    }

    public List<Product> GetProductsBySupplier(string supplier)
    {
        return Products.Where(p => p.Supplier == supplier).ToList();
    }

    public Product GetOldestProductInStock()
    {
        return Products.OrderBy(p => p.SupplyDate).FirstOrDefault();
    }

    public Dictionary<string, double> GetAverageQuantityByProductType()
    {
        var averageQuantityByType = Products
            .GroupBy(p => p.ProductType)
            .ToDictionary(g => g.Key, g => g.Average(p => p.Quantity));

        return averageQuantityByType;
    }
}
