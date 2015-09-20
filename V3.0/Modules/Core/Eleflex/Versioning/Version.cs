using System;

namespace Eleflex
{
    /// <summary>
    /// Represents an object defining a version in the patching process.
    /// </summary>
    public partial class Version : IVersion
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Version() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <param name="build"></param>
        /// <param name="revision"></param>
        public Version(int major, int minor, int build, int revision)
        {
            Major = major;
            Minor = minor;
            Build = build;
            Revision = revision;
        }

        /// <summary>
        /// Major. This is the major release of Eleflex. Currently V2.
        /// </summary>
        public virtual int Major { get; set; }
        /// <summary>
        /// Minor. This number increases when a storage change is required.
        /// </summary>
        public virtual int Minor { get; set; }
        /// <summary>
        /// Build. This number increases when an integrated component changes.
        /// </summary>
        public virtual int Build { get; set; }
        /// <summary>
        /// Revision. This number change when only a code change is required.
        /// </summary>
        public virtual int Revision { get; set; }

        /// <summary>
        /// Change major.
        /// </summary>
        /// <param name="val"></param>
        public virtual void ChangeMajor (int val)
        {
            Major = val;
        }
        /// <summary>
        /// Change minor.
        /// </summary>
        /// <param name="val"></param>
        public virtual void ChangeMinor(int val)
        {
            Minor = val;
        }
        /// <summary>
        /// Change build.
        /// </summary>
        /// <param name="val"></param>
        public virtual void ChangeBuild (int  val)
        {
            Build = val;
        }
        /// <summary>
        /// CHange revision.
        /// </summary>
        /// <param name="val"></param>
        public virtual void ChangeRevision(int val)
        {
            Revision = val;
        }

        /// <summary>
        /// Override to string to display the version number.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Major.ToString() + "." + Minor.ToString() + "." + Build.ToString() + "." + Revision.ToString();
        }
    }
}
