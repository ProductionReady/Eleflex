//#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2015 Production Ready, LLC. All Rights Reserved.
////Copyright © 2015 Production Ready, LLC. All Rights Reserved.
////For more information, visit http://www.ProductionReady.com
////This file is part of PRODUCTION READY® ELEFLEX®.
////
////This program is free software: you can redistribute it and/or modify
////it under the terms of the GNU Affero General Public License as
////published by the Free Software Foundation, either version 3 of the
////License, or (at your option) any later version.
////
////This program is distributed in the hope that it will be useful,
////but WITHOUT ANY WARRANTY; without even the implied warranty of
////MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
////GNU Affero General Public License for more details.
////
////You should have received a copy of the GNU Affero General Public License
////along with this program.  If not, see <http://www.gnu.org/licenses/>.
//#endregion
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using Bootstrap.Extensions.StartupTasks;

//namespace Eleflex
//{
//    /// <summary>
//    /// Loads all business rules.
//    /// </summary>
//    public class BusinessRuleConfig : IStartupTask
//    {
//        /// <summary>
//        /// Run.
//        /// </summary>
//        public void Run()
//        {
//            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
//            foreach (Assembly assembly in assemblies)
//            {
//            }                            
//        }


//        protected virtual List<IModulePatch> GetRuleSets(Assembly assembly)
//        {
//            List<IModulePatch> list = new List<IModulePatch>();
//            Type patchType = typeof(IModulePatch);
//            List<Type> types = assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract && patchType.IsAssignableFrom(x)).ToList();
//            if (types == null || types.Count == 0)
//                return null;

//            foreach (Type type in types)
//            {
//                IModulePatch patch = Activator.CreateInstance(type) as IModulePatch;
//                if (patch != null)
//                    list.Add(patch);
//            }

//            if (list.Count == 0)
//                return null;
//            return list;
//        }

//        /// <summary>
//        /// Reset.
//        /// </summary>
//        public void Reset()
//        {
//        }
//    }
//}
