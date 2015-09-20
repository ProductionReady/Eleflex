using System;

namespace Eleflex
{
    /// <summary>
    /// Domain model for a module version. This will track which modules are currently installed and their respective version.
    /// This way when modules are updated the code can determine which which version to update from.
    /// </summary>
    public partial class Module : IModule, IStorageExtraData
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Module()
        {
            Version = new Version();
        }

        /// <summary>
        /// Unique module key.
        /// </summary>
        public virtual Guid ModuleKey { get; set; }
        /// <summary>
        /// Module name.
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// Module name.
        /// </summary>
        public virtual string Description { get; set; }
        /// <summary>
        /// Version number.
        /// </summary>
        public virtual Version Version { get; set; }
        /// <summary>
        /// Property providing an extension for customized information.
        /// </summary>
        public virtual string ExtraData { get; set; }


        /// <summary>
        /// Return patch name and version information quickly.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string output = Name + " " + ModuleKey.ToString();
            if (Version != null)
                output += " " + Version.ToString();
            return output;
        }
    }
}
