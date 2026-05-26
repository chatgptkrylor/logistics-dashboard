namespace LogisticsApi.Models;

public class Shipment
{
    public string Id { get; set; } = "";
    public string Origin { get; set; } = "";
    public string Destination { get; set; } = "";
    public string Status { get; set; } = "";
    public string Carrier { get; set; } = "";
    public string Eta { get; set; } = "";
    public double Weight { get; set; }
    public decimal Cost { get; set; }
}

public class Warehouse
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string City { get; set; } = "";
    public int Capacity { get; set; }
    public int Used { get; set; }
    public string Manager { get; set; } = "";
    public string Phone { get; set; } = "";
}

public class Vehicle
{
    public string Id { get; set; } = "";
    public string Type { get; set; } = "";
    public string Plate { get; set; } = "";
    public string Driver { get; set; } = "";
    public string Route { get; set; } = "";
    public string Status { get; set; } = "";
    public int Fuel { get; set; }
}

public class DailyShipment
{
    public string Date { get; set; } = "";
    public int Count { get; set; }
    public decimal Revenue { get; set; }
}

public class Stats
{
    public int TotalShipments { get; set; }
    public int InTransit { get; set; }
    public int Delivered { get; set; }
    public int Pending { get; set; }
    public int Delayed { get; set; }
    public decimal TotalRevenue { get; set; }
    public double AvgDeliveryDays { get; set; }
    public double OnTimeRate { get; set; }
}