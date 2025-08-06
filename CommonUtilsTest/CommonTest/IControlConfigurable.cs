namespace CommonUtils
{
    /// <summary>
    /// Interface for controls configuration.
    /// </summary>
    public interface IControlConfigurable
    {
        /// <summary>
        /// Gets or sets the auto configure flag of the control.
        /// </summary>
        bool AutoConfigure { get; set; }
        
        /// <summary>
        /// Gets or sets the configuration name of the control.
        /// </summary>
        string ConfigurationName { get; set; }

        /// <summary>
        /// Loads the configuration data from config to the control.
        /// </summary>

        void LoadConfigData();
        
        /// <summary>
        /// Saves the configuration data from the control to config.
        /// </summary>
        void SaveConfigData();

        /// <summary>
        /// Gets the control configuration for this control.
        /// </summary>
        IControlConfig ControlConfig { get; }
    }
}