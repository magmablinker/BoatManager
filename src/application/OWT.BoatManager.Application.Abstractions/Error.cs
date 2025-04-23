namespace OWT.BoatManager.Application.Abstractions;

public sealed class Error : IEquatable<Error>
{
    public string Identifier { get; }
    public IList<string> Messages { get; } = new List<string>();

    private readonly int _hashCode;

    public Error(string identifier)
    {
        Identifier = identifier;
        _hashCode = HashCode.Combine(identifier);
    }

    public override bool Equals(object? obj) => Equals(obj as Error);

    public bool Equals(Error? other) =>
        ReferenceEquals(this, other) || (other != null && Identifier == other.Identifier);

    public override int GetHashCode() => _hashCode;

    public Error AddMessage(string message)
    {
        Messages.Add(message);
        return this;
    }

    public Error AddMessages(IEnumerable<string> messages)
    {
        foreach (var message in messages) AddMessage(message);
        return this;
    }

    public Dictionary<string, string[]> ToDictionary() =>
        new() { { Identifier, Messages.ToArray() } };

    public override string ToString() => $"{Identifier} : {string.Join(",", Messages)}";
}