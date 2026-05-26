using LogisticsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsApi.Controllers;

[ApiController]
[Route("api")]
public class LogisticsController : ControllerBase
{
    private static readonly List<Shipment> Shipments = new()
    {
        new() { Id = "SHP-1001", Origin = "Mumbai", Destination = "Delhi", Status = "In Transit", Carrier = "BlueDart", Eta = "2026-05-28", Weight = 12.5, Cost = 2450 },
        new() { Id = "SHP-1002", Origin = "Chennai", Destination = "Bangalore", Status = "Delivered", Carrier = "DTDC", Eta = "2026-05-25", Weight = 8.2, Cost = 1680 },
        new() { Id = "SHP-1003", Origin = "Pune", Destination = "Hyderabad", Status = "Pending", Carrier = "Delhivery", Eta = "2026-05-30", Weight = 25.0, Cost = 3200 },
        new() { Id = "SHP-1004", Origin = "Kolkata", Destination = "Mumbai", Status = "In Transit", Carrier = "India Post", Eta = "2026-05-29", Weight = 5.7, Cost = 980 },
        new() { Id = "SHP-1005", Origin = "Ahmedabad", Destination = "Jaipur", Status = "Delayed", Carrier = "Gati", Eta = "2026-05-27", Weight = 18.3, Cost = 4100 },
        new() { Id = "SHP-1006", Origin = "Delhi", Destination = "Chennai", Status = "Delivered", Carrier = "BlueDart", Eta = "2026-05-24", Weight = 3.1, Cost = 720 },
        new() { Id = "SHP-1007", Origin = "Bangalore", Destination = "Pune", Status = "In Transit", Carrier = "Delhivery", Eta = "2026-05-28", Weight = 42.0, Cost = 5600 },
        new() { Id = "SHP-1008", Origin = "Hyderabad", Destination = "Kolkata", Status = "Pending", Carrier = "DTDC", Eta = "2026-05-31", Weight = 15.6, Cost = 2890 },
        new() { Id = "SHP-1009", Origin = "Jaipur", Destination = "Mumbai", Status = "Delivered", Carrier = "Ecom Express", Eta = "2026-05-23", Weight = 6.8, Cost = 1350 },
        new() { Id = "SHP-1010", Origin = "Mumbai", Destination = "Ahmedabad", Status = "In Transit", Carrier = "Gati", Eta = "2026-05-27", Weight = 30.2, Cost = 4800 },
    };

    private static readonly List<Warehouse> Warehouses = new()
    {
        new() { Id = "WH-01", Name = "Mumbai Hub", City = "Mumbai", Capacity = 5000, Used = 3750, Manager = "Rajesh Patil", Phone = "+91982001001" },
        new() { Id = "WH-02", Name = "Delhi North", City = "Delhi", Capacity = 4000, Used = 2100, Manager = "Amit Sharma", Phone = "+91982001002" },
        new() { Id = "WH-03", Name = "Chennai South", City = "Chennai", Capacity = 3500, Used = 3200, Manager = "Priya Venkat", Phone = "+91982001003" },
        new() { Id = "WH-04", Name = "Bangalore Tech", City = "Bangalore", Capacity = 4500, Used = 1800, Manager = "Karthik Rao", Phone = "+91982001004" },
        new() { Id = "WH-05", Name = "Kolkata East", City = "Kolkata", Capacity = 3000, Used = 2700, Manager = "Suman Das", Phone = "+91982001005" },
    };

    private static readonly List<Vehicle> Vehicles = new()
    {
        new() { Id = "VH-01", Type = "Truck", Plate = "MH-01-AB-1234", Driver = "Suresh Kumar", Route = "Mumbai → Delhi", Status = "Active", Fuel = 72 },
        new() { Id = "VH-02", Type = "Van", Plate = "MH-02-CD-5678", Driver = "Ravi Singh", Route = "Mumbai → Pune", Status = "Active", Fuel = 45 },
        new() { Id = "VH-03", Type = "Truck", Plate = "DL-03-EF-9012", Driver = "Manoj Tiwari", Route = "Delhi → Jaipur", Status = "Maintenance", Fuel = 88 },
        new() { Id = "VH-04", Type = "Tempo", Plate = "KA-04-GH-3456", Driver = "Nagesh Reddy", Route = "Bangalore → Chennai", Status = "Active", Fuel = 60 },
        new() { Id = "VH-05", Type = "Truck", Plate = "TN-05-IJ-7890", Driver = "Bala Subramaniam", Route = "Chennai → Hyderabad", Status = "Active", Fuel = 33 },
        new() { Id = "VH-06", Type = "Van", Plate = "MH-06-KL-1122", Driver = "Anil Deshmukh", Route = "Pune → Mumbai", Status = "Idle", Fuel = 95 },
    };

    private static readonly List<DailyShipment> Daily = new()
    {
        new() { Date = "2026-05-20", Count = 145, Revenue = 398000 },
        new() { Date = "2026-05-21", Count = 132, Revenue = 365000 },
        new() { Date = "2026-05-22", Count = 158, Revenue = 425000 },
        new() { Date = "2026-05-23", Count = 141, Revenue = 388000 },
        new() { Date = "2026-05-24", Count = 167, Revenue = 456000 },
        new() { Date = "2026-05-25", Count = 155, Revenue = 420000 },
        new() { Date = "2026-05-26", Count = 144, Revenue = 395500 },
    };

    private static readonly Stats Stats = new()
    {
        TotalShipments = 1042, InTransit = 187, Delivered = 789,
        Pending = 48, Delayed = 18, TotalRevenue = 2847500,
        AvgDeliveryDays = 3.2, OnTimeRate = 94.6
    };

    [HttpGet("stats")]
    public ActionResult<Stats> GetStats() => Stats;

    [HttpGet("shipments")]
    public IEnumerable<Shipment> GetShipments([FromQuery] string? status)
        => string.IsNullOrEmpty(status) ? Shipments : Shipments.Where(s => s.Status == status);

    [HttpGet("warehouses")]
    public IEnumerable<Warehouse> GetWarehouses() => Warehouses;

    [HttpGet("vehicles")]
    public IEnumerable<Vehicle> GetVehicles() => Vehicles;

    [HttpGet("daily")]
    public IEnumerable<DailyShipment> GetDaily() => Daily;

    [HttpGet("health")]
    public object Health() => new { status = "ok", timestamp = DateTime.UtcNow };
}