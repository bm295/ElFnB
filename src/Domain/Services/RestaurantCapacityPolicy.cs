namespace Domain.Services;

public static class RestaurantCapacityPolicy
{
    public const int MinSeats = 80;
    public const int MaxSeats = 120;

    public static void EnsureWithinRange(int totalSeats)
    {
        if (totalSeats < MinSeats || totalSeats > MaxSeats)
        {
            throw new ArgumentOutOfRangeException(nameof(totalSeats), $"El capacity must be in range {MinSeats}-{MaxSeats}.");
        }
    }
}
