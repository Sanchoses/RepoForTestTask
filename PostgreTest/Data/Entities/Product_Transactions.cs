using Microsoft.EntityFrameworkCore;

namespace PostgreTest.Data.Entities;
[Keyless]
public class Product_Transactions {
	public int Products_Id { get; set; }
    public int Transactions_ProductId { get; set; }
}