public class AuthorViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string FirstName { get; internal set; }
    public string LastName { get; internal set; }
}
