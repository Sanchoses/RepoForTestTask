
namespace University.ViewModels;

public class SallaryBySubjectModel {
	//teacher
    public string Name { get; set; }
    public string SecondName { get; set; }
    public string MiddleName { get; set; }
    public string Position { get; set; }

    //subject
    public string Subject { get; set; }

    //sallary sallaryPerHour * CountHours
    public decimal Sallary { get; set; }
}