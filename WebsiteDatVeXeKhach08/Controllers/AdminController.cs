using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteDatVeXeKhach08.Models;

namespace WebsiteDatVeXeKhach08.Controllers
{
    public class AdminController : Controller
    {
        BanVeXeKhachEntities db = new BanVeXeKhachEntities();
        public ActionResult ListCustomers()
        {
            var ds = db.Users.OrderBy(s => s.UserID).Where(s=>s.RoleID==2).ToList();
            return View(ds);
        }
        public ActionResult DetailsCustomer(int id)
        {
            ViewBag.RoleID = new SelectList(db.UserRoles, "RoleID", "RoleName");
            User cus = db.Users.FirstOrDefault(s => s.UserID == id);
            return View(cus);
        }
        public ActionResult CreateCustomer()
        {
            ViewBag.RoleID = new SelectList(db.UserRoles, "RoleID", "RoleName");
            return View();
        }
        [HttpPost]
        public ActionResult CreateCustomer(FormCollection collection, User c)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.Users.Add(c);
                        db.SaveChanges();
                        transaction.Commit();
                        return RedirectToAction("ListCustomers");
                    }
                    return View(c);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Content(ex.Message);
                }
            }
        }
        public ActionResult EditCustomer(int id)
        {
            ViewBag.RoleID = new SelectList(db.UserRoles, "RoleID", "RoleName");
            var cus = db.Users.Find(id);
            return View(cus);
        }
        [HttpPost]
        public ActionResult EditCustomer(int id, FormCollection collection)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    User customer = db.Users.Find(id);
                    if (customer != null)
                    {
                        customer.Username = collection["Username"];
                        customer.Password = collection["Password"];
                        customer.Email = collection["Email"];
                        customer.FullName = collection["FullName"];
                        customer.Phone = collection["Phone"];
                        customer.RoleID = int.Parse(collection["RoleID"]);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    return RedirectToAction("ListCustomers");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Content(ex.Message);
                }
            }
        }
        public ActionResult DeleteCustomer(int id)
        {
            ViewBag.RoleID = new SelectList(db.UserRoles, "RoleID", "RoleName");
            var c = db.Users.Find(id);
            return View(c);
        }
        [HttpPost]
        public ActionResult DeleteCustomer(int id, FormCollection collection)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    User customer = db.Users.Find(id);
                    if (customer != null)
                    {
                        db.Users.Remove(customer);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    return RedirectToAction("ListCustomers");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Content(ex.Message);
                }
            }
        }

        public ActionResult ListRoutes()
        {
            var ds = db.Routes.OrderBy(s => s.RouteID).ToList();
            return View(ds);
        }
        public ActionResult DetailsRoute(int id)
        {
            Route r = db.Routes.FirstOrDefault(s => s.RouteID == id);
            return View(r);
        }
        public ActionResult CreateRoute()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRoute(Route r)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Routes.Add(r);
                    db.SaveChanges();
                    transaction.Commit();
                    return RedirectToAction("ListRoutes");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Content(ex.Message);
                }
            }
        }

        public ActionResult EditRoute(int id)
        {
            var r = db.Routes.FirstOrDefault(s => s.RouteID == id);
            return View(r);
        }

        [HttpPost]
        public ActionResult EditRoute(int id, FormCollection collection)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Route route = db.Routes.FirstOrDefault(r => r.RouteID == id);
                    if (route != null)
                    {
                        route.RouteName = collection["RouteName"];
                        route.StartLocation = collection["StartLocation"];
                        route.EndLocation = collection["EndLocation"];
                        route.Amount = decimal.Parse(collection["Amount"]);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    return RedirectToAction("ListRoutes");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Content(ex.Message);
                }
            }
        }

        public ActionResult DeleteRoute(int id)
        {
            var r = db.Routes.FirstOrDefault(s => s.RouteID == id);
            return View(r);
        }

        [HttpPost]
        public ActionResult DeleteRoute(int id, FormCollection collection)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Route route = db.Routes.FirstOrDefault(r => r.RouteID == id);
                    if (route != null)
                    {
                        db.Routes.Remove(route);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    return RedirectToAction("ListRoutes");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Content(ex.Message);
                }
            }
        }
        public ActionResult ListBuses()
        {
            var ds = db.Buses.OrderBy(s => s.BusID).ToList();
            return View(ds);
        }

        public ActionResult DetailsBus(int id)
        {
            ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName");
            Bus b = db.Buses.FirstOrDefault(s => s.BusID == id);
            return View(b);
        }

        public ActionResult CreateBus()
        {
            ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName");
            return View();
        }

        [HttpPost]
        public ActionResult CreateBus(Bus b)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Buses.Add(b);
                    db.SaveChanges();
                    transaction.Commit();
                    return RedirectToAction("ListBuses");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Content(ex.Message);
                }
            }
        }

        public ActionResult EditBus(int id)
        {
            var b = db.Buses.FirstOrDefault(s => s.BusID == id);
            ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName");
            return View(b);
        }

        [HttpPost]
        public ActionResult EditBus(int id, FormCollection collection)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Bus bus = db.Buses.FirstOrDefault(r => r.BusID == id);
                    if (bus != null)
                    {
                        bus.RouteID = int.Parse(collection["RouteID"]);
                        bus.BusNumber = collection["BusNumber"];
                        bus.DepartureTime = DateTime.Parse(collection["DepartureTime"]);
                        bus.ArrivalTime = DateTime.Parse(collection["ArrivalTime"]);
                        bus.TotalSeats = int.Parse(collection["TotalSeats"]);
                        bus.AvailableSeats = int.Parse(collection["AvailableSeats"]);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    return RedirectToAction("ListBuses");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Content(ex.Message);
                }
            }
        }

        public ActionResult DeleteBus(int id)
        {
            var b = db.Buses.FirstOrDefault(s => s.BusID == id);
            ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName");
            return View(b);
        }

        [HttpPost]
        public ActionResult DeleteBus(int id, FormCollection collection)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Bus bus = db.Buses.FirstOrDefault(r => r.BusID == id);
                    if (bus != null)
                    {
                        db.Buses.Remove(bus);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    return RedirectToAction("ListBuses");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Content(ex.Message);
                }
            }
        }
        public ActionResult ListTickets()
        {
            var ds = db.Tickets.OrderBy(s => s.TicketID).ToList();
            return View(ds);
        }

        public ActionResult DetailsTicket(int id)
        {
            Ticket t = db.Tickets.FirstOrDefault(s => s.TicketID == id);
            return View(t);
        }

        public ActionResult CreateTicket()
        {
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName");
            ViewBag.BusID = new SelectList(db.Buses, "BusID", "BusNumber");
            ViewBag.StatusID = new SelectList(db.TicketStatuses, "StatusID", "StatusName");
            return View();
        }

        [HttpPost]
        public ActionResult CreateTicket(FormCollection collection, Ticket t)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var bus = db.Buses.SingleOrDefault(b => b.BusID == t.BusID);
                        if (bus == null)
                        {
                            ModelState.AddModelError("", "Bus not found.");
                        }
                        else
                        {
                            bool seatExists = db.Tickets.Any(ticket => t.BusID == ticket.BusID && t.SeatNumber == ticket.SeatNumber);
                            if (seatExists)
                            {
                                ModelState.AddModelError("", "Seat number already taken on this bus.");
                            }
                            else
                            {
                                bus.AvailableSeats -= 1;
                                if (bus.AvailableSeats < 0)
                                {
                                    ModelState.AddModelError("", "No available seats.");
                                }
                                else
                                {
                                    db.Tickets.Add(t);
                                    t.StatusID = 2;
                                    t.BookingDate = DateTime.Now;
                                    db.SaveChanges();
                                    transaction.Commit();
                                    return RedirectToAction("ListTickets");
                                }
                            }
                        }
                    }
                    ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", t.UserID);
                    ViewBag.BusID = new SelectList(db.Buses, "BusID", "BusNumber", t.BusID);
                    ViewBag.StatusID = new SelectList(db.TicketStatuses, "StatusID", "StatusName", t.StatusID);
                    return View(t);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Content(ex.Message);
                }
            }
        }

        public ActionResult EditTicket(int id)
        {
            var ticket = db.Tickets.FirstOrDefault(t => t.TicketID == id);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName");
            ViewBag.BusID = new SelectList(db.Buses, "BusID", "BusNumber");
            ViewBag.StatusID = new SelectList(db.TicketStatuses, "StatusID", "StatusName");
            return View(ticket);
        }

        [HttpPost]
        public ActionResult EditTicket(Ticket ticket)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var existingTicket = db.Tickets.FirstOrDefault(t => t.TicketID == ticket.TicketID);
                        var bus = db.Buses.SingleOrDefault(b => b.BusID == existingTicket.BusID);
                        if (bus == null)
                        {
                            ModelState.AddModelError("", "Bus not found.");
                        }
                        else if (ticket.SeatNumber != existingTicket.SeatNumber && db.Tickets.Any(t => t.BusID == ticket.BusID && t.SeatNumber == ticket.SeatNumber))
                        {
                            ModelState.AddModelError("", "Seat number already taken on this bus.");
                        }
                        else
                        {
                            var previousStatus = existingTicket.StatusID;
                            existingTicket.SeatNumber = ticket.SeatNumber;
                            existingTicket.StatusID = ticket.StatusID;

                            if (previousStatus != ticket.StatusID && ticket.StatusID == 2)
                            {
                                bus.AvailableSeats -= 1;
                            }
                            else if (previousStatus == 2 && ticket.StatusID != 2)
                            {
                                bus.AvailableSeats += 1;
                            }

                            db.SaveChanges();
                            transaction.Commit();
                            return RedirectToAction("ListTickets");
                        }
                    }

                    ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", ticket.UserID);
                    ViewBag.BusID = new SelectList(db.Buses, "BusID", "BusNumber", ticket.BusID);
                    ViewBag.StatusID = new SelectList(db.TicketStatuses, "StatusID", "StatusName", ticket.StatusID);
                    return View(ticket);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Content(ex.Message);
                }
            }
        }

        public ActionResult DeleteTicket(int id)
        {
            var ticket = db.Tickets.FirstOrDefault(t => t.TicketID == id);
            return View(ticket);
        }

        [HttpPost]
        public ActionResult DeleteTicket(int id, FormCollection collection)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var ticket = db.Tickets.FirstOrDefault(t => t.TicketID == id);
                    if (ticket.StatusID == 2)
                    {
                        var bus = db.Buses.FirstOrDefault(b => b.BusID == ticket.BusID);
                        if (bus != null)
                        {
                            bus.AvailableSeats += 1;
                        }
                    }
                    db.Tickets.Remove(ticket);
                    db.SaveChanges();
                    transaction.Commit();
                    return RedirectToAction("ListTickets");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Content(ex.Message);
                }
            }
        }
        public ActionResult ListPayment()
        {
            var payment = db.Payments.OrderBy(p => p.PaymentID).ToList();
            return View(payment);
        }
        public ActionResult DetailsPayment(int id)
        {
            var payment = db.Payments.FirstOrDefault(p => p.PaymentID == id);
            var ticket = db.Tickets.FirstOrDefault(t => t.TicketID == payment.TicketID);
            var bus = db.Buses.FirstOrDefault(b => b.BusID == ticket.BusID);
            var route = db.Routes.FirstOrDefault(r => r.RouteID == bus.RouteID);
            ViewBag.Amount = route.Amount;
            return View(payment);
        }
        public ActionResult CreatePayment()
        {
            var ticketsWithoutPayment = db.Tickets.Where(t => !db.Payments.Any(p => p.TicketID == t.TicketID)).ToList();
            ViewBag.TicketID = new SelectList(ticketsWithoutPayment, "TicketID", "TicketID");
            ViewBag.MethodID = new SelectList(db.PaymentMethods, "MethodID", "MethodName");
            return View();
        }
        [HttpPost]
        public ActionResult CreatePayment(Payment payment)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Payments.Add(payment);
                        payment.PaymentDate = DateTime.Now;
                        db.SaveChanges();
                        var ticket = db.Tickets.FirstOrDefault(t => t.TicketID == payment.TicketID);
                        if (ticket != null)
                        {
                            ticket.StatusID = 2; 
                            db.SaveChanges();
                        }
                        transaction.Commit();
                        return RedirectToAction("ListPayment");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return Content(ex.Message);
                    }
                }
            }

            var ticketsWithoutPayment = db.Tickets.Where(t => t.Payments == null).ToList();
            SelectList ticketList = new SelectList(ticketsWithoutPayment, "TicketID", "TicketID");
            ViewBag.TicketList = ticketList;
            ViewBag.MethodID = new SelectList(db.PaymentMethods, "MethodID", "MethodName", payment.MethodID);
            return View(payment);
        }
        public ActionResult EditPayment(int id)
        {
            var tickets = db.Tickets.ToList();
            var methods = db.PaymentMethods.ToList();

            SelectList ticketList = new SelectList(tickets, "TicketID", "TicketID");
            SelectList methodList = new SelectList(methods, "MethodID", "MethodName");

            ViewBag.TicketList = ticketList;
            ViewBag.MethodList = methodList;
            var payment = db.Payments.FirstOrDefault(p => p.PaymentID == id);
            return View(payment);
        }
        [HttpPost]
        public ActionResult EditPayment(Payment payment)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var existingPayment = db.Payments.FirstOrDefault(p => p.PaymentID == payment.PaymentID);
                        if (existingPayment != null)
                        {
                            existingPayment.MethodID = payment.MethodID;
                            db.SaveChanges();
                            transaction.Commit();
                            return RedirectToAction("ListPayment");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Payment not found.");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return Content(ex.Message);
                    }
                }
            }
            return View(payment);
        }
        public ActionResult DeletePayment(int id)
        {
            var payment = db.Payments.FirstOrDefault(p => p.PaymentID == id);
            return View(payment);
        }
        [HttpPost]
        public ActionResult DeletePayment(int id, FormCollection collection)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var payment = db.Payments.FirstOrDefault(p => p.PaymentID == id);
                    db.Payments.Remove(payment);
                    db.SaveChanges();
                    var ticket = db.Tickets.FirstOrDefault(t => t.TicketID == payment.TicketID);
                    if (ticket != null)
                    {
                        ticket.StatusID = null;
                        db.SaveChanges();
                    }
                    transaction.Commit();   
                    return RedirectToAction("ListPayment");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Content(ex.Message);
                }
            }
        }

        public ActionResult ThongKe(int month = 1)
        {
            int year = DateTime.Now.Year;
            // Get the start and end date of the selected month
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            // Query the database to get the tickets sold in the selected month
            var query = db.Tickets
           .Where(t => t.BookingDate >= startDate && t.BookingDate <= endDate && t.StatusID == 2)
           .GroupBy(t => t.Bus.Route.RouteName)
           .Select(g => new RouteTicketSalesStatistic
           {
               RouteName = g.Key,
               TicketsSold = g.Count()
           })
           .ToList();
            int totalTicketsSold = query.Sum(r => r.TicketsSold);
            TicketSalesStatistic model = new TicketSalesStatistic
            {
                Month = month,
                Year = year,
                TotalTicketsSold = totalTicketsSold,
                RouteTicketSalesStatistics = query
            };

            return View(model);
        }
    }
}