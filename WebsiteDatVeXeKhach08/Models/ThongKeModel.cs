using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteDatVeXeKhach08.Models
{

    public class RouteTicketSalesStatistic
    {
        public string RouteName { get; set; }
        public int TicketsSold { get; set; }
    }
    public class TicketSalesStatistic
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int TotalTicketsSold { get; set; }
        public List<RouteTicketSalesStatistic> RouteTicketSalesStatistics { get; set; }
    }

}