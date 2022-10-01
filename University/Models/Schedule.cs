namespace University.Models;
public class Schedule {
	public int Id { get; set; }
    public int TeacherId { get; set; }
    public int SubjectId { get; set; }
    public int CountHours { get; set; }
}