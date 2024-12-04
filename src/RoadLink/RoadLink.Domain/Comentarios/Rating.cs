using RoadLink.Domain.Abstractions;

namespace RoadLink.Domain.Comentarios;

public sealed record Rating
{
    public static readonly Error InvalidRating = new Error("Rating.Invalid", "Rating is invalid");
    public int Value { get; init; }
    private Rating(int value) => Value = value;

    public static Result<Rating> Create(int value)
    {
        if (value < 1 || value > 5)
        {
            return Result.Failure<Rating>(InvalidRating);
        }

        return new Rating(value);
    }
}