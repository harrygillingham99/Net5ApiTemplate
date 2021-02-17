using System;

namespace NetCore31ApiTemplate.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ScrutorIgnoreAttribute : Attribute
    {
    }
}