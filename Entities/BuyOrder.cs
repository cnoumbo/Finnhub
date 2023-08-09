using System.ComponentModel.DataAnnotations;

namespace Entities;

public class BuyOrder
{
    /// <summary>
    /// The unique ID od theh buy order
    /// </summary>
    [Key]
    public Guid BuyOrderID { get; set; }

    [Required]
    public string StockSymbol { get; set; }

    [Required(ErrorMessage ="Stock Name can't be null or empty")]
    public string StockName { get; set; }

    public DateTime DateAndTimeOfOrder { get; set; }

    [Range(1,100000, ErrorMessage ="You can buy maximum of 100000.")]
    [Required]
    public uint Quantity { get; set; }

    [Required]
    [Range(1, 10000, ErrorMessage ="Maximum Stock price is 10000")]
    public double Price { get; set; }
}

