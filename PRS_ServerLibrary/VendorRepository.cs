using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PRS_Server;

namespace PRS_ServerLibrary {

    class VendorRepository {

        private static PRSDbContext context = new PRSDbContext();

        public static List<Vendors> GetAll() {
            return context.Vendors.ToList();
        }
        public static Vendors GetByPK(int id) {
            if (id < 0) { throw new Exception("Vendor Id must be greater than 0!"); }
            return context.Vendors.Find(id);
        }
        public static bool Insert(Vendors vendor) {
            if (vendor == null) { throw new Exception("Vendor cannot be null!"); }
            vendor.Id = 0;
            context.Vendors.Add(vendor);
            return context.SaveChanges() == 1;
        }
        public static bool Update(Vendors vendor) {
            if (vendor == null) { throw new Exception("Vendor cannot be null!"); }
            var dbvendor = context.Vendors.Find(vendor.Id);
            if (dbvendor == null) { throw new Exception("Vendor cannot be null!"); }
            dbvendor.Id = vendor.Id;
            dbvendor.Code = vendor.Code;
            dbvendor.Name = vendor.Name;
            dbvendor.Address = vendor.Address;
            dbvendor.City = vendor.City;
            dbvendor.State = vendor.State;
            dbvendor.Zip = vendor.Zip;
            dbvendor.Phone = vendor.Phone;
            dbvendor.Email = vendor.Email;
            return context.SaveChanges() == 1;
        }
        public static bool Delete(Vendors vendor) {
            if (vendor == null) { throw new Exception("Vendor cannot be null!"); }
            var dbvendor = context.Vendors.Find(vendor.Id);
            if (dbvendor == null) { throw new Exception("Vendor cannot be null!"); }
            context.Vendors.Remove(dbvendor);
            return context.SaveChanges() == 1;
        }
        public static bool Delete(int id) {
            var vendor = context.Vendors.Find(id);
            if (vendor == null) { return false; }
            var del = Delete(vendor);
            return del;
        }
    }
}
