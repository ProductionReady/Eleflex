#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2015 Production Ready, LLC. All Rights Reserved.
//Copyright © 2015 Production Ready, LLC. All Rights Reserved.
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
    /// This interface defines the foundation for the reflection-less design.
    /// Properties are not used to reduce naming collisions with implementing classes.
    /// </summary>
    public partial interface IEleflexObject
    {

        /// <summary>
        /// Provides a means of serializing/deserializing dynamic data (non-contract-typed).
        /// </summary>
        List<EleflexKeyValue> EleflexKeyValues { get; set; }

        /// <summary>
        /// Create an instance of the implementing object.
        /// </summary>
        /// <returns></returns>
        IEleflexObject EleflexCreate();

        /// <summary>
        /// Initialize an object with defaults.
        /// </summary>
        void EleflexInitialize();

        /// <summary>
        /// Get the key used to distinguish this object from other managed objects.
        /// </summary>
        /// <returns></returns>
        string EleflexGetObjectKey();

        /// <summary>
        /// Get the underlying storage mechanism.
        /// </summary>
        /// <returns></returns>
        Dictionary<string, Tuple<IEleflexDataType, IEleflexProperty>> EleflexGetStorage();

        /// <summary>
        /// Set the underlying dymamic storage mechanism.
        /// </summary>
        /// <returns></returns>
        void EleflexSetStorage(Dictionary<string, Tuple<IEleflexDataType, IEleflexProperty>> storage);

        /// <summary>
        /// Get a list of properties for the managed object.
        /// </summary>
        /// <returns></returns>
        List<string> EleflexGetPropertyNames();

        /// <summary>
        /// Get a list of properties for the managed object.
        /// </summary>
        /// <returns></returns>
        List<IEleflexProperty> EleflexGetProperties();

        /// <summary>
        /// Get a value from the managed object.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool EleflexGetPropertyValue(string propertyName, out IEleflexDataType value);

        /// <summary>
        /// Set a value to the managed object.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool EleflexSetPropertyValue(string propertyName, IEleflexDataType value);

        /// <summary>
        /// Set a value to the managed object.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        bool EleflexSetPropertyValue(string propertyName, IEleflexDataType value, IEleflexProperty property);

        /// <summary>
        /// Set a value to the managed object.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool EleflexSetPropertyValue(string propertyName, string value);

    }
}
