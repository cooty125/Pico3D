/* 
 * Pico3DAssembly
 * =====================================================================
 * FileName: Assembly.cs
 * Location: ./Properties/
 * Project: Pico 3D
 * ---------------------------------------------------------------------
 * Created: 2/10/2016 4:38:37 PM
 * ---------------------------------------------------------------------
 * This document is distributed under GNU General Public License.
 * Copyright © David Kutnar 2016 - All rights reserved.
 * =====================================================================
 */

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Resources;

[assembly: AssemblyTitle( "Pico 3D" )]
[assembly: AssemblyDescription( "Embedded 3D graphics library for Windows" )]
[assembly: AssemblyConfiguration( "" )]
[assembly: AssemblyCompany( "David Kutnar" )]
[assembly: AssemblyProduct( "Pico 3D" )]
[assembly: AssemblyCopyright( "Copyright © David Kutnar 2016" )]
[assembly: AssemblyTrademark( "Pico 3D" )]
[assembly: AssemblyCulture( "" )]
[assembly: ComVisible( false )]
[assembly: Guid( "ccf6d285-6ecb-45b6-8d5a-1a1c633872db" )]
[assembly: AssemblyVersion( "0.0.9.7" )]
[assembly: AssemblyFileVersion( "0.0.9.7" )]
[assembly: NeutralResourcesLanguageAttribute( "en-US" )]

namespace Pico3D
{
    public static class Pico3DAssembly
    {
        public static string Name {
            get { return Assembly.GetExecutingAssembly( ).GetName( ).Name; }
        }
        public static string FullName {
            get { return Assembly.GetExecutingAssembly( ).GetName( ).FullName; }
        }
        public static string ProcessorArchitecture {
            get { return Assembly.GetExecutingAssembly( ).GetName( ).ProcessorArchitecture.ToString( ); }
        }
        public static string Version {
            get { return Assembly.GetExecutingAssembly( ).GetName( ).Version.ToString( ); }
        }

        public static Module GetManifestModule( ) {
            return Assembly.GetExecutingAssembly( ).ManifestModule;
        }
    }
}