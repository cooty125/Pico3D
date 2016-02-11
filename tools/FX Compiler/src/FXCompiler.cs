/* 
 * FXCompiler
 * =====================================================================
 * FileName: FXCompiler.cs
 * Location: ./
 * Project: FX Compiler
 * ---------------------------------------------------------------------
 * Created: 2/11/2016 11:52:37 PM
 * ---------------------------------------------------------------------
 * This document is distributed under GNU General Public License.
 * Copyright © David Kutnar 2016 - All rights reserved.
 * =====================================================================
 */

using System;
using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Graphics;

namespace FX_Compiler
{
    public class FXCompiler
    {
        #region Fields:

        static EffectImporter importer = null;
        static EffectProcessor processor = null;

        #endregion

        static void Main( string[] args ) {
            Console.Title = "Pico 3D Shader Compiler";
            Console.WriteLine( Console.Title );

            importer = new EffectImporter( );
            processor = new EffectProcessor( );

            if ( args.Length > 1 ) {
                if ( args[ 0 ].EndsWith( ".fx" ) ) {
                    if ( File.Exists( args[ 0 ] ) ) {
                        string output = args[ 1 ];

                        if ( !output.EndsWith( ".psf" ) ) {
                            output = ( args[ 1 ] + ".psf" );
                        }

                        Console.WriteLine( "Compiling " + args[ 0 ] );
                        Console.WriteLine( "Output: " + output );

                        byte[] effect_data = GetEffectCodeBytes( args[ 0 ] );
                        SaveBytesToFile( output, effect_data );

                        Console.WriteLine( "[ Compiling OK ]" );
                        effect_data = null;
                    }
                    else {
                        Console.WriteLine( "Input file does not exists!" );
                    }
                }
                else {
                    Console.WriteLine( "Wrong input file format!" );
                }
            }
            else {
                Console.WriteLine( "Check your input arguments." );
            }
        }

        public static byte[] GetEffectCodeBytes( string filename ) {
            EffectContent content = importer.Import( "./" + filename, new CustomImporterContext( ) );
            CompiledEffectContent compiled = processor.Process( content, new CustomProcessorContext( ) );

            return compiled.GetEffectCode( );
        }

        public static void SaveBytesToFile( string filename, byte[] bytes ) {
            if ( bytes.Length > 0 ) {
                File.WriteAllBytes( filename, bytes );
            }
        }
    }
}