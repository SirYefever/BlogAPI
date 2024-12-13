using System.Runtime.Serialization;

namespace Core.Models;

public enum CommunityRole
{
    [EnumMember(Value = "Subscriber")] Subscriber = 1,
    [EnumMember(Value = "Administrator")] Administrator = 2
}