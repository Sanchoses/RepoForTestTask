namespace University.Models;
public class Address {
	public int Id { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public int HouseNumber { get; set; }

    //foreign keys
    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }

}