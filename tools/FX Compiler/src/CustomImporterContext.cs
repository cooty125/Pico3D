/* 
 * CustomImporterContext
 * =====================================================================
 * FileName: CustomImporterContext.cs
 * Location: ./
 * Project: FX Compiler
 * ---------------------------------------------------------------------
 * Created: 2/11/2016 12:05:04 PM
 * ---------------------------------------------------------------------
 * This document is distributed under GNU General Public License.
 * Copyright © David Kutnar 2016 - All rights reserved.
 * =====================================================================
 */

using Microsoft.Xna.Framework.Content.Pipeline;

namespace FX_Compiler
{
    internal class CustomImporterContext : ContentImporterContext
    {
        #region Properties:

        public override ContentBuildLogger Logger {
            get { 
                return null; 
            }
        }

        public override string IntermediateDirectory {
            get {
                return string.Empty;
            }
        }
        public override string OutputDirectory {
            get {
                return string.Empty;
            }
        }

        #endregion

        public CustomImporterContext( ) {
        }

        public override void AddDependency( string filename ) {
        }
    }
}