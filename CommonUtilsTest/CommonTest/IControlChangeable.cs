using System;

namespace CommonUtils
{
    /// <summary>
    /// Interface for controls changing
    /// </summary>
    public interface IControlChangeable
    {
        /// <summary>
        /// Occurs when the value of this control or any child has changed.
        /// </summary>
        event EventHandler ValueChanged;
        
        /// <summary>
        /// Gets the validation flag.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Gets the changed flag.
        /// </summary>
        bool IsChanged { get; }
        
        /// <summary>
        /// Gets or sets the control value.
        /// </summary>
        object Value { get; set; }

        /// <summary>
        /// Accepts all changes on the control and children.
        /// </summary>
        void AcceptChanges();

        /// <summary>
        /// Rejects all changes on the control and children.
        /// </summary>
        void RejectChanges();

        /// <summary>
        /// Notifies the control about child changing.
        /// </summary>
        void NotifyChanges();
    }
}