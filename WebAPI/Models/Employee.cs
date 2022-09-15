namespace WebAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DepartmentName { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string PhotoFileName { get; set; }

    }
}
