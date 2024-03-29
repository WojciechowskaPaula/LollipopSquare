﻿using LolipopSquare.Models;
using LolipopSquare.Models.ViewModels;

namespace LolipopSquare.Interface
{
    public interface IProductService
    {
        AllProductsVM GetAllProducts(int pageSize, int actualPage, string search, string category);
        ProductImageVM GetProductById(int id);
        void UpdateProduct(ProductImageVM productImageVM);
        Product AddNewProduct(AddNewProductVM productVM);
        AddNewProductVM GetNewProductVM();
        void DeleteProduct(int id);
        DeleteProductVM GetProductByIdForDelete(int id);
        Task<ProductDetailsVM> GetProductDetails(int id);
        void DeleteSingleImg(int id);
    }
}
