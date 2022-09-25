
namespace PostgreTest.ViewModels;
public class ClientCreateModel {
    //personal data
    public string? Name { get; set; }
    public string? ManagerFirstName { get; set; }
    public string? ManagerSecondName { get; set; }
    public string? ManagerMiddleName { get; set; }
    public string? BankAccount { get; set; }

    //address
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public int HouseNumber { get; set; }
    public int MailIndex { get; set; }
}