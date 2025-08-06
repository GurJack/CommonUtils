using System;

namespace CommonUtils.Attributes
{
    /// <summary>
    /// Ignored properties to be copied when the object is cloned.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CopyOnCloneAttribute : Attribute
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public CopyOnCloneAttribute()
        {
        }
    }
}