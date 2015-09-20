using AutoMapper;
using DomainModel = Eleflex;
using StorageModel = Eleflex.Versioning.Storage.EF;

namespace Eleflex.Versioning.Storage.EF.AutoMapper
{
    /// <summary>
    /// Represents a mapping registration task for mapping between the Module domain and a Module storage object.
    /// </summary>
    [MappingRegistrationTask()]
    public partial class ModuleAutoMapperRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public ModuleAutoMapperRegistrationTask()
        {
            Description = "This task registers mapping between the ModuleVersion domain and a ModuleVersion storage object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
		/// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            Mapper.CreateMap<DomainModel.Module, StorageModel.Module>()
                .AfterMap((src, dest) => dest.VersionMajor = src.Version.Major)
                .AfterMap((src, dest) => dest.VersionMinor = src.Version.Minor)
                .AfterMap((src, dest) => dest.VersionBuild = src.Version.Build)
                .AfterMap((src, dest) => dest.VersionRevision = src.Version.Revision);

            Mapper.CreateMap<StorageModel.Module, DomainModel.Module>()
                .AfterMap((src, dest) => dest.Version.Major = src.VersionMajor)
                .AfterMap((src, dest) => dest.Version.Minor = src.VersionMinor)
                .AfterMap((src, dest) => dest.Version.Build = src.VersionBuild)
                .AfterMap((src, dest) => dest.Version.Revision = src.VersionRevision);


            return base.Register(taskOptions);
        }

    }
}
