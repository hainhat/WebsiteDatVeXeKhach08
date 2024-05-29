using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteDatVeXeKhach08.Controllers
{
    public class CustomerController : Controller
    {
        BanVeXeKhachEntities db = new BanVeXeKhachEntities();
        public ActionResult SearchBusByDate(DateTime? searchDate)
        {
            DateTime date = searchDate ?? DateTime.Today;

            // Truy vấn cơ sở dữ liệu để lấy danh sách các chuyến xe bus còn trống theo ngày
            var availableBuses = db.Buses
                .Where(b => DbFunctions.TruncateTime(b.DepartureTime) == DbFunctions.TruncateTime(date) && b.AvailableSeats > 0)
                .OrderBy(b => b.DepartureTime)
                .ToList();

            // Truyền dữ liệu tìm kiếm vào ViewBag để hiển thị lại ngày tìm kiếm
            ViewBag.SearchDate = date;
            return View(availableBuses);
        }
        public ActionResult SearchRouteByDate(DateTime? searchDate)
        {
            DateTime date = searchDate ?? DateTime.Today;

            // Truy vấn cơ sở dữ liệu để lấy danh sách các tuyến đường có xe bus còn trống theo ngày
            var availableRoutes = db.Routes
                .Where(route => db.Buses.Any(bus => bus.RouteID == route.RouteID && DbFunctions.TruncateTime(bus.DepartureTime) == DbFunctions.TruncateTime(date) && bus.AvailableSeats > 0))
                .OrderBy(route => route.RouteName)
                .ToList();
            ViewBag.SearchDate = date;
            return View(availableRoutes);
        }
    }
}