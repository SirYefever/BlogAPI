using System.Runtime.Serialization;

namespace API.Dto;

public enum PostSorting
{
    [EnumMember(Value = "CreateDesc")] CreateDesc,
    [EnumMember(Value = "CreateAsc")] CreateAsc,
    [EnumMember(Value = "LikeDesc")] LikeDesc,
    [EnumMember(Value = "LikeAsc")] LikeAsc
}