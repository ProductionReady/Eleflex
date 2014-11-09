#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2014 Production Ready, LLC. All Rights Reserved.
//Copyright © 2014 Production Ready, LLC. All Rights Reserved.
//For more information, visit http://www.ProductionReady.com
//This file is part of PRODUCTION READY® ELEFLEX®.
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU Affero General Public License as
//published by the Free Software Foundation, either version 3 of the
//License, or (at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Affero General Public License for more details.
//
//You should have received a copy of the GNU Affero General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion
using System;
using System.Collections.Generic;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines events triggered when manipulating EPOs.
    /// </summary>
    public partial interface IEPOTrigger
    {

        /// <summary>
        /// Pre get trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        void EPOTriggerPreGet(IPersistenceRequest request);

        /// <summary>
        /// Post get trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        void EPOTriggerPostGet(IPersistenceRequest request, List<IEleflexPersistenceObject> result);

        /// <summary>
        /// Pre get aggregate trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        void EPOTriggerPreGetAggregate(IPersistenceRequest request);

        /// <summary>
        /// Post get trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        void EPOTriggerPostGetAggregate(IPersistenceRequest request, double result);

        /// <summary>
        /// Pre delete trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        void EPOTriggerPreDelete(IPersistenceRequest request);

        /// <summary>
        /// Post delete trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        void EPOTriggerPostDelete(IPersistenceRequest request, int result);

        /// <summary>
        /// Pre update trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        void EPOTriggerPreUpdate(IPersistenceRequest request);

        /// <summary>
        /// Post update trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        void EPOTriggerPostUpdate(IPersistenceRequest request);

        /// <summary>
        /// Pre insert trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        void EPOTriggerPreInsert(IPersistenceRequest request);

        /// <summary>
        /// Post insert trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        void EPOTriggerPostInsert(IPersistenceRequest request);

        /// <summary>
        /// Pre bulk update trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        void EPOTriggerPreBulkUpdate(IPersistenceRequest request);

        /// <summary>
        /// Post bulk update trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        void EPOTriggerPostBulkUpdate(IPersistenceRequest request);

        /// <summary>
        /// Pre bulk delete trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        void EPOTriggerPreBulkDelete(IPersistenceRequest request);

        /// <summary>
        /// Post bulk delete trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        void EPOTriggerPostBulkDelete(IPersistenceRequest request);

    }

}
