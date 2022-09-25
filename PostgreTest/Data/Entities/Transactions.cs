using System.ComponentModel.DataAnnotations;

namespace PostgreTest.Data.Entities;
public class Transactions {
	[Key]
    public int Id { get; set; }
    [Required]
    public int ClientId { get; set; }
    [Required]
    public int ProductId { get; set; }
    public uint CountOfProduct { get; set; }
    public string Unit { get; set; }
    public DateTime Date { get; set; }
}