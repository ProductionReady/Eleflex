﻿using System;
using System.Collections.Generic;
using VersioningModel = Eleflex;

namespace Eleflex.Messages
{
    /// <summary>
    /// The base version patch for 3.2.1.0
    /// </summary>
    public class Version_3_2_1_0 : VersioningModel.ModulePatch
    {

        /// <summary>
        /// Information regarding the patch.
        /// </summary>
        protected const string PATCH_INFO = "This patch updates the Eleflex.Messages component.";

        /// <summary>
        /// Constructor.
        /// </summary>
        public Version_3_2_1_0()
            : base(EleflexMessagesConstants.MODULE_KEY, EleflexMessagesConstants.MODULE_NAME, EleflexMessagesConstants.MODULE_DESCRIPTION, PATCH_INFO)
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
                return new VersioningModel.Version(3, 2, 1, 0);
            }
        }

        /// <summary>
        /// The versions prior to this version for which the patching process can be used.
        /// </summary>
        public override List<VersioningModel.Version> PriorVersions
        {
            get
            {
                return new List<Version>()
                {
                    new Version(3,2,0,0)
                };
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
