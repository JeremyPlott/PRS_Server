using System;
using System.Collections.Generic;
using System.Text;
using PRS_Server;
using System.Linq;

namespace PRS_ServerLibrary {

    class RequestRepository {

        private static PRSDbContext context = new PRSDbContext();

        public static string RequestNew = "NEW";
        public static string RequestEdit = "EDIT";
        public static string RequestReview = "REVIEW";
        public static string RequestApproved = "APPROVED";
        public static string RequestDenied = "DENIED";

        public static List<Requests> GetAll() {
            return context.Requests.ToList();
        }
        public static Requests GetByPK(int id) {
            if(id < 0) { throw new Exception("Request Id must be greater than 0!"); }
            return context.Requests.Find(id);
        }
        public static bool Insert(Requests request) {
            if(request == null) { throw new Exception("Request must not be null!"); }
            request.Id = 0;
            context.Requests.Add(request);
            return context.SaveChanges() == 1;
        }
        public static bool Update(Requests request) {
            if(request == null) { throw new Exception("Request must not be null!"); }
            var dbrequest = context.Requests.Find(request.Id);
            if(request == null) { throw new Exception("Request must not be null!"); }
            dbrequest.Id = request.Id;
            dbrequest.Description = request.Description;
            dbrequest.Justification = request.Justification;
            dbrequest.RejectionReason = request.RejectionReason;
            dbrequest.DeliveryMode = request.DeliveryMode;
            dbrequest.Status = request.Status;
            dbrequest.Total = request.Total;
            dbrequest.UserId = request.UserId;
            context.Update(dbrequest);
            return context.SaveChanges() == 1;
        }
        public static bool Delete(Requests request) {
            if(request == null) { throw new Exception("Request must not be null!"); }
            var dbrequest = context.Requests.Find(request.Id);
            if(dbrequest == null) { throw new Exception("Request must not be null!"); }
            context.Requests.Remove(dbrequest);
            return context.SaveChanges() == 1;
        }
        public static bool Delete(int id) {
            var dbrequest = context.Requests.Find(id);
            if(dbrequest == null) { throw new Exception("Request cannot be null!"); }
            var del = Delete(dbrequest);
            return del;
        }
        public static void New(int id) {
            SetStatus(id, RequestNew);
        }
        public static void Edit(int id) {
            SetStatus(id, RequestEdit);
        }
        public static void Review(int id) {
            SetStatus(id, RequestReview);
        }
        public static void Approved(int id) {
            SetStatus(id, RequestApproved);
        }
        public static void Denied(int id) {
            SetStatus(id, RequestDenied);
        }
        public static void SetStatus(int id, string status) {
            var request = GetByPK(id);
            request.Status = status;
            if(request == null) { throw new Exception("No request with that Id!"); }
            var success = Update(request);
            if(!success) { throw new Exception("Request update failed!"); }
        }
    }
}
