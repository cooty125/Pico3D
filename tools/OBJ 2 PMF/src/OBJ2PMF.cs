/* 
 * OBJ2PMF
 * =====================================================================
 * FileName: OBJ2PMF.cs
 * Location: ./
 * Project: OBJ 2 PMF
 * ---------------------------------------------------------------------
 * Created: 2/11/2016 12:49:45 PM
 * ---------------------------------------------------------------------
 * This document is distributed under GNU General Public License.
 * Copyright © David Kutnar 2016 - All rights reserved.
 * =====================================================================
 */

using System;
using System.IO;

namespace OBJ_2_PMF
{
    public class OBJ2PMF
    {
        static void Main( string[] args ) {
            Console.Title = "Pico 3D Model Converter";
            Console.WriteLine( Console.Title );

            if ( args.Length > 1 ) {
                if ( args[ 0 ].EndsWith( ".obj" ) ) {
                    if ( File.Exists( args[ 0 ] ) ) {
                        string output = args[ 1 ];

                        if ( !output.EndsWith( ".pmf" ) ) {
                            output = ( args[ 1 ] + ".pmf" );
                        }


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
    }
}