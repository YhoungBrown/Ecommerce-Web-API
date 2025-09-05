namespace StackBuldTechnicalAssessment.Exceptions;

public class OutOfStockException : Exception
{
    public OutOfStockException(string productName, int requested, int available)
        : base($"Product '{productName}' is out of stock. Requested {requested}, but only {available} available.")
    {
    }
}
