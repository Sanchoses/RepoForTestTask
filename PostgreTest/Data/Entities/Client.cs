using System.ComponentModel.DataAnnotations;

namespace PostgreTest.Data.Entities;
public class Client {
    [Key]
	public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(100)]
    public string ManagerFirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string ManagerSecondName { get; set; }

    [Required]
    [MaxLength(100)]
    public string ManagerMiddleName { get; set; }

    [Required]
    public int LegalAddressId { get; set; }

    [Required]
    [MaxLength(16)]
    public string BankAccount { get; set; }
    
    
}