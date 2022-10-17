namespace University.Models;
public class Teacher {
	public int Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string MiddleName { get; set; }

    //foreign
    public Address Address { get; set; }
    public int PositionId { get; set; }
    public Position Position { get; set; }

}