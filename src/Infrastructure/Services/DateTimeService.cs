using ProFilePOC2.Application.Common.Interfaces;

namespace ProFilePOC2.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
