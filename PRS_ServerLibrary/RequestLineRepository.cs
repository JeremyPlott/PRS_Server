using System;
using System.Collections.Generic;
using System.Text;
using PRS_Server;
using System.Linq;

namespace PRS_ServerLibrary {

    class RequestLineRepository {

        public static PRSDbContext context = new PRSDbContext();

        private static void RecalcReqTotal(int RequestId) {
            var request = RequestRepository.GetByPK(RequestId);
            request.Total = request.RequestLines.Sum(li => li.Product.Price * li.Quantity);            
            // save changes is in the insert / update / delete methods
        }
        public static List<RequestLines> GetAll() {
            return context.RequestLines.ToList();
        }
        public static RequestLines GetByPK(int id) {
            if(id < 0) { throw new Exception("Request Line Id must be greater than 0!"); }
            return context.RequestLines.Find(id);
        }
        public static bool Insert(RequestLines requestline) {
            if(requestline == null) { throw new Exception("Request line cannot be null!"); }
            context.RequestLines.Add(requestline);
            RecalcReqTotal(requestline.Id);
            return context.SaveChanges() == 1;
        }
        public static bool Update(RequestLines requestline) {
            if(requestline == null) { throw new Exception("Request line cannot be null!"); }
            var dbrequestline = context.RequestLines.Find(requestline.Id);
            if(dbrequestline == null) { throw new Exception("Request line cannot be null!"); }
            dbrequestline.Id = requestline.Id;
            dbrequestline.RequestId = requestline.RequestId;
            dbrequestline.ProductId = requestline.ProductId;
            dbrequestline.Quantity = requestline.Quantity;
            context.RequestLines.Update(dbrequestline);
            RecalcReqTotal(requestline.Id);
            return context.SaveChanges() == 1;
        }
        public static bool Delete(RequestLines requestline) {
            if(requestline == null) { throw new Exception("Request line cannot be null!"); }
            var dbrequestline = context.RequestLines.Find(requestline.Id);
            if(dbrequestline == null) { throw new Exception("Request line cannot be null!"); }
            context.RequestLines.Remove(dbrequestline);
            RecalcReqTotal(requestline.Id);
            return context.SaveChanges() == 1;
        }
        public static bool Delete(int id) {
            var requestline = context.RequestLines.Find(id);
            if(requestline == null) { throw new Exception("Request line cannot be null!"); }
            var del = Delete(requestline);
            return del;
        }
    }
}
