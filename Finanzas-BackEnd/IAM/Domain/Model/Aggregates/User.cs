using System.Text.Json.Serialization;
using Finanzas_BackEnd.Bills.Domain.Model.Aggregates;

namespace Finanzas_BackEnd.IAM.Domain.Model.Aggregates;

public class User(string email, string username, string passwordHash)
{
    public User() : this(string.Empty, string.Empty, string.Empty) { }
    public int Id { get;  }
    public string Username { get; private set; } = username;
    public string Email { get; private set; } = email;
    [JsonIgnore] public string PasswordHash { get; private set; } = passwordHash;
    
    public ICollection<Bill> Bills { get; }
    
    public User UpdateUsername(string username)
    {
        Username = username;
        return this;
    }
    
    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }
}