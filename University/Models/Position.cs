namespace University.Models;
public class Position {
	public int Id { get; set; }
    public string Name { get; set; }
    public decimal SallaryPerHour { get; set; }

    //Key for teachers
    public ICollection<Teacher> Teachers { get; set; }
}