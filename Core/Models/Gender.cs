using System.Runtime.Serialization;

namespace Core.Models;

[DataContract]
public enum Gender
{
    [EnumMember( Value = "Male" )]
    Male,
    [EnumMember( Value = "Female" )]
    Female
}