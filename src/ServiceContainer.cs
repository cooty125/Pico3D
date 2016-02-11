/* 
 * ServiceContainer
 * =====================================================================
 * FileName: ServiceContainer.cs
 * Location: ./
 * Project: Pico 3D
 * ---------------------------------------------------------------------
 * Created: 2/10/2016 3:30:41 PM
 * ---------------------------------------------------------------------
 * This document is distributed under GNU General Public License.
 * Copyright © David Kutnar 2016 - All rights reserved.
 * =====================================================================
 */

using System;
using System.Collections.Generic;

namespace Pico3D
{
    public class ServiceContainer : IServiceProvider
    {
        #region Fields:

        Dictionary<Type, object> services = null;

        #endregion

        public ServiceContainer( ) {
            this.services = new Dictionary<Type, object>( );
        }
        ~ServiceContainer( ) {
            this.services = null;
        }

        public void AddService<T>( T service ) {
            this.services.Add( typeof( T ), service );
        }

        public void RemoveService( object service ) {
            this.services.Remove( ( Type )service.GetType( ) );
        }

        public object GetService( Type serviceType ) {
            object service = null;
            this.services.TryGetValue( serviceType, out service );

            return service;
        }
    }
}