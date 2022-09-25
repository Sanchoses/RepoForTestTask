using System.Collections;

namespace PostgreTest.ViewModels;
public class TransactionsViewModel{
	public string? ClientName { get; set; }
    public string? ProductName { get; set; }
    public DateTime DateTransaction { get; set; }
    public decimal Price { get; set; }

    
}