using System;
using System.Collections.Generic;
using System.Text;
using PRS_Server;
using System.Linq;

namespace PRS_ServerLibrary {

    class ProductRepository {

        private static PRSDbContext context = new PRSDbContext();

        public static List<Products> GetAll() {
            return context.Products.ToList();
        }
        public static Products GetByPK(int id) {
            if(id < 1) { throw new Exception("Product Id must be greater than 0!"); }
            return context.Products.Find(id);
        }
        public static bool Insert(Products product) {
            if(product == null) { throw new Exception("Product cannot be null!"); }
            product.Id = 0;
            context.Products.Add(product);
            return context.SaveChanges() == 1;
        }
        public static bool Update(Products product) {
            if(product == null) { throw new Exception("Product cannot be null!"); }
            var dbproduct = context.Products.Find(product.Id);
            if(dbproduct == null) { throw new Exception("Product cannot be null!"); }
            dbproduct.Id = product.Id;
            dbproduct.PartNbr = product.PartNbr;
            dbproduct.Name = product.Name;
            dbproduct.Price = product.Price;
            dbproduct.Unit = product.Unit;
            dbproduct.PhotoPath = product.PhotoPath;
            dbproduct.VendorId = product.VendorId;
            context.Products.Update(dbproduct);
            return context.SaveChanges() == 1;
        }
        public static bool Delete(Products product) {
            if(product == null) { throw new Exception("Product cannot be null!"); }
            var dbproduct = context.Products.Find(product.Id);
            if (dbproduct == null) { throw new Exception("Product cannot be null!"); }
            context.Products.Remove(dbproduct);
            return context.SaveChanges() == 1;
        }
        public static bool Delete(int id) {
            var product = context.Products.Find(id);
            if(product == null) { return false; }
            var del = Delete(id);
            return del;
        }
    }
}
