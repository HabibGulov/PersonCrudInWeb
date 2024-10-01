public class Person
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = null!;
    public DateTime Birthdate { get; set; }
    public string Email { get; set; } = null!;
}