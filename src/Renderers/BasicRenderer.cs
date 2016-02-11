/* 
 * Renderers.BasicRenderer
 * =====================================================================
 * FileName: BasicRenderer.cs
 * Location: ./Renderers/
 * Project: Pico 3D
 * ---------------------------------------------------------------------
 * Created: 2/10/2016 4:04:56 PM
 * ---------------------------------------------------------------------
 * This document is distributed under GNU General Public License.
 * Copyright © David Kutnar 2016 - All rights reserved.
 * =====================================================================
 */

using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pico3D.Graphics;

using Model = Pico3D.Graphics.Model;

namespace Pico3D.Renderers
{
    public class BasicRenderer : IRenderer
    {
        #region Fields:

        GraphicsDevice g_device = null;

        List<Model> models = null;
        Effect effect = null;

        Shader default_shader = null;
        Shader texture_shader = null;

        float ambient_pwr = 0.4f;
        Color ambient_color = Color.White;

        float diffuse_pwr = 0.8f;
        Vector3 diffuse_dir = new Vector3( 0.6f, 0.8f, 0.7f );
        Color diffuse_color = Color.White;

        #endregion
        #region Properties:

        public float AmbientIntensity {
            get { return this.ambient_pwr; }
            set { this.ambient_pwr = value; }
        }
        public Color AmbientColor {
            get { return this.ambient_color; }
            set { this.ambient_color = value; }
        }

        public float DiffuseIntensity {
            get { return this.diffuse_pwr; }
            set { this.diffuse_pwr = value; }
        }
        public Vector3 DiffuseDirection {
            get { return this.diffuse_dir; }
            set { this.diffuse_dir = value; }
        }
        public Color DiffuseColor {
            get { return this.diffuse_color; }
            set { this.diffuse_color = value; }
        }

        #endregion

        public BasicRenderer( GraphicsDevice gDevice ) {
            this.g_device = gDevice;
            this.models = new List<Model>( );

            this.default_shader = new Shader( this.g_device, "default.psf" );
            this.texture_shader = new Shader( this.g_device, "texture.psf" );
        }

        public void AddModel( Model model ) {
            if ( !this.models.Contains( model ) ) {
                if ( model.Material.ContainsDiffuseMap( ) ) {
                    this.effect = this.texture_shader.Effect;
                }
                else {
                    this.effect = this.default_shader.Effect;
                }
                this.models.Add( model );
            }
        }

        public void RemoveModel( Model model ) {
            if ( this.models.Contains( model ) ) {
                this.models.Remove( model );
            }
        }

        public void Render( Matrix view, Matrix projection ) {
            this.g_device.DepthStencilState = DepthStencilState.Default;
            this.g_device.RasterizerState = RasterizerState.CullClockwise;

            foreach ( Model model in this.models ) {
                this.effect.CurrentTechnique = this.effect.Techniques[ 0 ];
                this.effect.Parameters[ "Transform" ].SetValue( model.Transform );
                this.effect.Parameters[ "CameraView" ].SetValue( view );
                this.effect.Parameters[ "CameraProjection" ].SetValue( projection );
                this.effect.Parameters[ "AmbientIntensity" ].SetValue( this.ambient_pwr );
                this.effect.Parameters[ "AmbientColor" ].SetValue( this.ambient_color.ToVector4( ) );
                this.effect.Parameters[ "DiffuseIntensity" ].SetValue( this.diffuse_pwr );
                this.effect.Parameters[ "DiffuseDirection" ].SetValue( this.diffuse_dir );
                this.effect.Parameters[ "DiffuseColor" ].SetValue( this.diffuse_color.ToVector4( ) );

                if ( model.Material.ContainsDiffuseMap( ) ) {
                    this.effect.Parameters[ "DiffuseMap" ].SetValue( model.Material.GetTexture( TextureType.DiffuseMap ) );
                }

                this.effect.CurrentTechnique.Passes[ "Default" ].Apply( );

                foreach ( Mesh mesh in model.Meshes ) {
                    this.g_device.SetVertexBuffer( mesh.VertexBuffer );
                    this.g_device.DrawPrimitives( PrimitiveType.TriangleList, mesh.StartVertex, mesh.PrimitiveCount );
                    //this.g_device.DrawUserIndexedPrimitives( PrimitiveType.TriangleList, mesh.VertexData, mesh.StartVertex, mesh.VertexData.Length, mesh.IndexData, 0, mesh.PrimitiveCount, Vertex.VertexDeclaration );
                }
            }
        }
    }
}