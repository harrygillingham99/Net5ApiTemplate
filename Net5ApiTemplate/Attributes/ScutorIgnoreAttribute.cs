using System;

namespace NetCore5ApiTemplate.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ScrutorIgnoreAttribute : Attribute
    {
    }
}