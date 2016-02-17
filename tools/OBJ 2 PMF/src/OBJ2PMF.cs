/* 
 * OBJ2PMF
 * =====================================================================
 * FileName: OBJ2PMF.cs
 * Location: ./
 * Project: OBJ 2 PMF
 * ---------------------------------------------------------------------
 * Created: 2/17/2016 12:49:45 PM
 * ---------------------------------------------------------------------
 * This document is distributed under GNU General Public License.
 * Copyright © David Kutnar 2016 - All rights reserved.
 * =====================================================================
 */

// WARNING! Source is not complete! This version is just for one mesh based models! (without materials yet)

using System;
using System.Collections.Generic;
using System.IO;

namespace OBJ_2_PMF
{
    public class OBJ2PMF
    {
        static void Main( string[] args ) {
            Console.Title = "Pico 3D Model Converter";
            Console.WriteLine( Console.Title );

            if ( args.Length > 0 ) {
                if ( args[ 0 ].EndsWith( ".obj" ) ) {
                    if ( File.Exists( args[ 0 ] ) ) {
                        string output = args[ 0 ].Replace( ".obj", ".pmf" );

                        if ( args.Length == 2 ) {
                            output = args[ 1 ];
                        }

                        if ( !output.EndsWith( ".pmf" ) ) {
                            output = ( args[ 1 ] + ".pmf" );
                        }

                        Console.WriteLine( "Converting " + args[ 0 ] );
                        Console.WriteLine( "Output: " + output );

                        string[] lines = GetLines( args[ 0 ] );
                        uint index = 0;

                        string mesh = string.Empty;
                        List<string> positions = new List<string>( );
                        List<string> coordinates = new List<string>( );
                        List<string> faces = new List<string>( );
                        List<Mesh> meshes = new List<Mesh>( );

                        foreach ( string line in lines ) {
                            if ( line.StartsWith( "g" ) ) {
                                if ( mesh == string.Empty ) {
                                    mesh = line.Remove( 0, 2 ).Trim( );

                                    positions.Clear( );
                                    coordinates.Clear( );
                                    faces.Clear( );
                                }
                            }
                            else if ( line.StartsWith( "v" ) && !line.StartsWith("vt") && mesh != string.Empty ) {
                                positions.Add( line );
                            }
                            else if ( line.StartsWith( "vt" ) && mesh != string.Empty ) {
                                coordinates.Add( line );
                            }
                            else if ( line.StartsWith( "f" ) && mesh != string.Empty ) {
                                faces.Add( line );
                            }
                            else {
                                // Unknown command.
                            }

                            if ( index == ( lines.Length - 1 ) ) {
                                meshes.Clear( );

                                Mesh msh = new Mesh( );
                                msh.Name = mesh;
                                msh.Positions = positions;
                                msh.Coordinates = coordinates;
                                msh.Faces = faces;

                                meshes.Add( msh );

                                CreatePMF( output, meshes );
                            }
                            else {
                                index++;
                            }
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

            Console.ReadKey( );
        }

        static string[] GetLines( string filename ) {
            StreamReader reader = new StreamReader( filename );
            int nol = File.ReadAllLines( filename ).Length;
            string[] lines = new string[ nol ];

            for ( int i = 0; i < nol; i++ ) {
                lines[ i ] = reader.ReadLine( );
            }

            reader.Close( );

            return lines;
        }

        static void CreatePMF( string filename, List<Mesh> meshes ) {
            StreamWriter writer = new StreamWriter( filename );
            writer.WriteLine( "# Pico 3D Model File (generated with OBJ 2 PMF utility)." );
            writer.WriteLine( string.Empty );

            foreach ( Mesh mesh in meshes ) {
                foreach ( string position in mesh.Positions ) {
                    writer.WriteLine( position );
                }
                writer.WriteLine( string.Empty );
                foreach ( string coordinate in mesh.Coordinates ) {
                    writer.WriteLine( coordinate );
                }
                writer.WriteLine( string.Empty );
                writer.WriteLine( "mesh " + mesh.Name );
                writer.WriteLine( string.Empty );
                foreach ( string face in mesh.Faces ) {
                    writer.WriteLine( face );
                }
            }

            writer.Close( );

            Console.WriteLine( "[ Converting OK ]" );
        }
    }
}
