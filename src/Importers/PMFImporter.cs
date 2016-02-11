/* 
 * Importers.PMFImporter
 * =====================================================================
 * FileName: PMFImporter.cs
 * Location: ./Importers/
 * Project: Pico 3D
 * ---------------------------------------------------------------------
 * Created: 2/10/2016 4:07:01 PM
 * ---------------------------------------------------------------------
 * This document is distributed under GNU General Public License.
 * Copyright © David Kutnar 2016 - All rights reserved.
 * =====================================================================
 */

using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pico3D;
using Pico3D.Graphics;

using Model = Pico3D.Graphics.Model;

namespace Pico3D.Importers
{
    public class PMFImporter : IImporter
    {
        #region Fields:

        GraphicsDevice g_device = null;

        string source_dir = "./";

        string model_name = null;
        string model_path = "./";

        Model model = null;
        Material material = null;
        Mesh current_mesh = null;

        List<Vector3> positions = null;
        List<Vector2> texcoords = null;

        List<Vertex> vertex_data = null;
        Vertex[] vertices = null;
        int[] indices = null;

        List<Mesh> meshes = null;

        #endregion
        #region Properties:

        public string SourceDirectory {
            get { return this.source_dir; }
            set { this.source_dir = value; }
        }

        #endregion

        public PMFImporter( GraphicsDevice gDevice, string sourceDir ) {
            this.g_device = gDevice;
            this.source_dir = sourceDir;

            this.Initialize( );
        }

        public Model Import( string filename ) {
            this.Initialize( );

            this.model = new Model( this.model_name );
            string path = Path.Combine( this.source_dir, filename );
            this.model_path = path.Replace( Path.GetFileName( path ), string.Empty );

            if ( File.Exists( path ) && path.EndsWith( ".pmf" ) ) {
                foreach ( string[] lineTokens in this.GetLineTokens( path ) ) {
                    this.Parse( lineTokens );
                }

                if ( this.current_mesh != null ) {
                    this.CalculateNormals( );

                    this.current_mesh.SetVertexData( this.vertices );
                    this.vertices = null;
                    this.current_mesh.SetIndexData( this.indices );
                    this.indices = null;
                    this.current_mesh.CreateVertexBuffer( this.g_device );

                    this.meshes.Add( this.current_mesh );
                    this.current_mesh = null;
                }

                this.model.SetMeshes( this.meshes );
                //this.model.SetBoundingBox( this.ComputeBoundingBox( ) );
            }

            return model;
        }

        void Parse( string[] lineTokens ) {
            switch ( lineTokens[ 0 ].ToLower( ) ) {
                // Model.
                case "model":
                    this.model_name = lineTokens[ 1 ];
                    this.model = new Model( this.model_name );
                    break;
                // Mesh.
                case "mesh":
                    if ( this.current_mesh != null ) {
                        this.CalculateNormals( );

                        this.current_mesh.SetVertexData( this.vertices );
                        this.vertices = null;
                        this.current_mesh.SetIndexData( this.indices );
                        this.indices = null;
                        this.current_mesh.CreateVertexBuffer( this.g_device );

                        this.meshes.Add( this.current_mesh );
                        this.current_mesh = null;
                    }
                    else {
                        this.current_mesh = new Mesh( lineTokens[ 1 ] );
                    }
                    break;
                // BeginMaterial.
                case "bmat":
                    this.material = new Material( lineTokens[ 1 ] );
                    break;
                // DiffuseMap.
                case "dmap":
                    if ( this.material != null ) {
                        Texture2D tex = this.LoadTexture( Path.Combine( this.model_path, lineTokens[ 1 ] ) );
                        this.material.AddTexture( TextureType.DiffuseMap, tex );
                        tex = null;
                    }
                    break;
                // NormalMap.
                case "nmap":
                    if ( this.material != null ) {
                        Texture2D tex = this.LoadTexture( Path.Combine( this.model_path, lineTokens[ 1 ] ) );
                        this.material.AddTexture( TextureType.NormalMap, tex );
                        tex = null;
                    }
                    break;
                // SpecularMap.
                case "smap":
                    if ( this.material != null ) {
                        Texture2D tex = this.LoadTexture( Path.Combine( this.model_path, lineTokens[ 1 ] ) );
                        this.material.AddTexture( TextureType.SpecularMap, tex );
                        tex = null;
                    }
                    break;
                // EmissiveMap.
                case "emap":
                    if ( this.material != null ) {
                        Texture2D tex = this.LoadTexture( Path.Combine( this.model_path, lineTokens[ 1 ] ) );
                        this.material.AddTexture( TextureType.EmissiveMap, tex );
                        tex = null;
                    }
                    break;
                // EndMaterial.
                case "emat":
                    this.model.SetMaterial( this.material );
                    this.material = null;
                    break;
                // Position.
                case "v":
                    this.positions.Add( this.ParseVector3( lineTokens ) );
                    break;
                // TextureCoordinate.
                case "vt":
                    Vector2 vt = this.ParseVector2( lineTokens );
                    vt.Y = ( 1.0f - vt.Y );
                    this.texcoords.Add( vt );
                    break;
                // Face.
                case "f":
                    for ( int vi = 1; vi <= 3; vi++ ) {
                        string[] indices = lineTokens[ vi ].Split( '/' );
                        int pos_index = ( int.Parse( indices[ 0 ], CultureInfo.InvariantCulture ) - 1 );

                        int tc_index = 0;
                        Vector2 tex_coord = Vector2.Zero;

                        if ( int.TryParse( indices[ 1 ], out tc_index ) ) {
                            tex_coord = this.texcoords[ tc_index - 1 ];
                        }

                        this.vertex_data.Add( new Vertex( this.positions[ pos_index ], tex_coord, Vector3.Zero ) );
                    }
                    break;
                // Unknown command.
                default:
                    break;
            }
        }

        void Initialize( ) {
            this.model = null;

            this.positions = new List<Vector3>( );
            this.texcoords = new List<Vector2>( );

            this.vertex_data = new List<Vertex>( );

            this.meshes = new List<Mesh>( );
        }

        IEnumerable<string[]> GetLineTokens( string filename ) {
            StreamReader reader = new StreamReader( filename );

            while ( !reader.EndOfStream ) {
                string[] tokens = Regex.Split( reader.ReadLine( ).Trim( ), @"\s+" );

                if ( tokens.Length > 0 && tokens[ 0 ] != string.Empty && !tokens[ 0 ].StartsWith( "#" ) ) {
                    yield return tokens;
                }
            }

            reader.Close( );
        }

        void CalculateNormals( ) {
            this.vertices = this.vertex_data.ToArray( );
            this.vertex_data = null;

            this.indices = new int[ this.vertices.Length ];
            for ( int i = 0; i < this.vertices.Length; i++ ) {
                this.indices[ i ] = i;
            }

            for ( int i = 0; i < this.indices.Length / 3; i++ ) {
                int ind1 = this.indices[ i * 3 ];
                int ind2 = this.indices[ i * 3 + 1 ];
                int ind3 = this.indices[ i * 3 + 2 ];

                Vector3 sa = ( this.vertices[ ind1 ].Position - this.vertices[ ind3 ].Position );
                Vector3 sb = ( this.vertices[ ind1 ].Position - this.vertices[ ind2 ].Position );
                Vector3 normal = Vector3.Cross( sa, sb );

                this.vertices[ ind1 ].Normal += normal;
                this.vertices[ ind2 ].Normal += normal;
                this.vertices[ ind3 ].Normal += normal;
            }

            for ( int i = 0; i < this.vertices.Length; i++ ) {
                this.vertices[ i ].Normal.Normalize( );
            }
        }

        Texture2D LoadTexture( string filename ) {
            if ( File.Exists( filename ) ) {
                return Texture2D.FromStream( this.g_device, File.Open( filename, FileMode.Open ) );
            }
            return null;
        }

        Vector2 ParseVector2( string[] lineTokens ) {
            return new Vector2(
                float.Parse( lineTokens[ 1 ], CultureInfo.InvariantCulture ),
                float.Parse( lineTokens[ 2 ], CultureInfo.InvariantCulture ) );
        }
        Vector3 ParseVector3( string[] lineTokens ) {
            return new Vector3(
                float.Parse( lineTokens[ 1 ], CultureInfo.InvariantCulture ),
                float.Parse( lineTokens[ 2 ], CultureInfo.InvariantCulture ),
                float.Parse( lineTokens[ 3 ], CultureInfo.InvariantCulture ) );
        }
    }
}