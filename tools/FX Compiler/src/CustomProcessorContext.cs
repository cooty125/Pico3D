/* 
 * CustomProcessorContext
 * =====================================================================
 * FileName: CustomProcessorContext.cs
 * Location: ./
 * Project: FX Compiler
 * ---------------------------------------------------------------------
 * Created: 2/11/2016 12:09:25 PM
 * ---------------------------------------------------------------------
 * This document is distributed under GNU General Public License.
 * Copyright © David Kutnar 2016 - All rights reserved.
 * =====================================================================
 */

using System;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Graphics;

namespace FX_Compiler
{
    internal class CustomProcessorContext : ContentProcessorContext
    {
        #region Properties:

        public override ContentBuildLogger Logger {
            get { 
                return null; 
            }
        }

        public override TargetPlatform TargetPlatform {
            get {
                return TargetPlatform.Windows;
            }
        }
        public override GraphicsProfile TargetProfile {
            get {
                return GraphicsProfile.HiDef;
            }
        }

        public override string BuildConfiguration {
            get {
                return string.Empty;
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
        public override string OutputFilename {
            get {
                return string.Empty;
            }
        }

        public override OpaqueDataDictionary Parameters {
            get {
                return new OpaqueDataDictionary( );
            }
        }

        #endregion

        public CustomProcessorContext( ) {
        }

        public override void AddDependency( string filename ) {
        }

        public override void AddOutputFile( string filename ) {
        }
        
        public override TOutput Convert<TInput, TOutput>( TInput input, string processorName, OpaqueDataDictionary processorParameters ) {
            throw new InvalidCastException( );
        }

        public override TOutput BuildAndLoadAsset<TInput, TOutput>( ExternalReference<TInput> sourceAsset, string processorName, OpaqueDataDictionary processorParameters, string importerName ) {
            throw new InvalidCastException( );
        }

        public override ExternalReference<TOutput> BuildAsset<TInput, TOutput>( ExternalReference<TInput> sourceAsset, string processorName, OpaqueDataDictionary processorParameters, string importerName, string assetName ) {
            throw new InvalidCastException( );
        }
    }
}