namespace ProFilePOC2.Domain.Entities.Physical;

public class Acquisition : BaseEntity
{
    public User Borrower { get; set; }
    public Document Document { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime DueDate { get; set; }
    public string Justification { get; set; }
}