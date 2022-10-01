
namespace University.ViewModels;

public class SallaryModel {
	//teacher
    public string Name { get; set; }
    public string SecondName { get; set; }
    public string MiddleName { get; set; }
    public string Position { get; set; }

    //subject
    public string Subject { get; set; }

    //sallary
    public decimal Sallary { get; set; } = 0.00m;
}