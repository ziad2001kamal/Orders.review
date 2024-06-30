public class IgnorePropertiesResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
{
    protected override Newtonsoft.Json.Serialization.JsonProperty CreateProperty(System.Reflection.MemberInfo member, Newtonsoft.Json.MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);

        // Add conditions to ignore specific properties
        if (member.DeclaringType != null && member.DeclaringType.FullName.StartsWith("System.Runtime.CompilerServices") &&
            (member.Name == "Context" || member.Name == "StateMachine"))
        {
            property.ShouldSerialize = _ => false;
        }

        return property;
    }
}
