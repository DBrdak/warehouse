namespace Warehouse.Application.Shared.Models;

public sealed record DateTimeModel
{
    public int Day { get; init; }
    public int Month { get; init; }
    public int Year { get; init; }
    public int Hour { get; init; }
    public int Minute { get; init; }
    public int Second { get; init; }

    private DateTimeModel(int day, int month, int year, int hour, int minute, int second)
    {
        Day = day;
        Month = month;
        Year = year;
        Hour = hour;
        Minute = minute;
        Second = second;
    }

    public static DateTimeModel FromDateTime(DateTime dateTime)
    {
        dateTime = dateTime.ToLocalTime();

        return new DateTimeModel(
            dateTime.Day,
            dateTime.Month,
            dateTime.Year,
            dateTime.Hour,
            dateTime.Minute,
            dateTime.Second);
    }

    public DateTime ToDateTime() => new(Year, Month, Day, Hour, Minute, Second, DateTimeKind.Local);

    public override string ToString() => $"{Day}.{Month}.{Year} {Hour}:{Minute}:{Second}";
}