﻿using System;
using System.Collections.Generic;
using VersioningModel = Eleflex;

namespace Eleflex.Email.WebClient
{
    /// <summary>
    /// The base version patch for 3.2.0.0
    /// </summary>
    public class Version_3_2_0_0 : VersioningModel.ModulePatch
    {

        /// <summary>
        /// Information regarding the patch.
        /// </summary>
        protected const string PATCH_INFO = "This patch updates the Eleflex.Email.WebClient library. This release added the NuGet package for Email.Server.";

        /// <summary>
        /// Constructor.
        /// </summary>
        public Version_3_2_0_0()
            : base(EleflexEmailWebClientConstants.MODULE_KEY, EleflexEmailWebClientConstants.MODULE_NAME, EleflexEmailWebClientConstants.MODULE_DESCRIPTION, PATCH_INFO)
        {
        }

        /// <summary>
        /// Determine if this patch is currently available to be run.
        /// </summary>
        public override bool IsAvailable
        {
            get { return true; }
        }

        /// <summary>
        /// Dependent modules that must be executed prior to this version.
        /// </summary>
        public override List<Guid> DependentModules
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// The current version of the patch.
        /// </summary>
        public override VersioningModel.Version Version
        {
            get
            {
                return new VersioningModel.Version(3, 2, 0, 0);
            }
        }

        /// <summary>
        /// The versions prior to this version for which the patching process can be used.
        /// </summary>
        public override List<VersioningModel.Version> PriorVersions
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Custom logic to update the patch.
        /// </summary>
        /// <returns></returns>
        public override void Update()
        {
        }

    }
}
