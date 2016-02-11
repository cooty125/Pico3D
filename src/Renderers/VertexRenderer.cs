/* 
 * Renderers.VertexRenderer
 * =====================================================================
 * FileName: VertexRenderer.cs
 * Location: ./Renderers/
 * Project: Pico 3D
 * ---------------------------------------------------------------------
 * Created: 2/10/2016 4:00:35 PM
 * ---------------------------------------------------------------------
 * This document is distributed under GNU General Public License.
 * Copyright © David Kutnar 2016 - All rights reserved.
 * =====================================================================
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pico3D.Renderers
{
    public class VertexRenderer : IRenderer
    {
        #region Fields:

        GraphicsDevice g_device = null;

        BasicEffect effect = null;

        VertexPositionColor[] vertices = null;
        int primitive_count = 0;
        Matrix transform = Matrix.Identity;

        #endregion

        public VertexRenderer( GraphicsDevice gDevice ) {
            this.g_device = gDevice;

            this.effect = new BasicEffect( this.g_device );
            this.effect.World = this.transform;
            this.effect.VertexColorEnabled = true;
        }

        public void SetVertices( VertexPositionColor[] vertices, int primitiveCount, Matrix transform ) {
            this.vertices = vertices;
            this.primitive_count = primitiveCount;
            this.transform = transform;
        }

        public void Render( Matrix view, Matrix projection ) {
            this.g_device.DepthStencilState = DepthStencilState.Default;
            this.g_device.RasterizerState = RasterizerState.CullNone;

            if ( this.vertices.Length > 0 ) {
                this.effect.World = this.transform;
                this.effect.View = view;
                this.effect.Projection = projection;
                this.effect.CurrentTechnique.Passes[ 0 ].Apply( );

                this.g_device.DrawUserPrimitives( PrimitiveType.TriangleList, this.vertices, 0, this.primitive_count );
            }
        }
    }
}