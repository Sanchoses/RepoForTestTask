
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostgreTest.Data.Entities;
public class LegalAddress {
	[Key]
    public int Id { get; set; }
    [ForeignKey("LegalAddressId")]
    public int ClientId { get; set; }
    [Required]
    public string? Country { get; set; }
    [Required]
    public string? City { get; set; }
    [Required]
    public string? Street { get; set; }
    [Required]
    public int HouseNumber { get; set; }
    [Required]
    public int MailIndex { get; set; }
}