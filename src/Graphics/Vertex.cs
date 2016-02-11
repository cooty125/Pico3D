/* 
 * Graphics.Vertex
 * =====================================================================
 * FileName: Vertex.cs
 * Location: ./Graphics/
 * Project: Pico 3D
 * ---------------------------------------------------------------------
 * Created: 2/10/2016 3:20:34 PM
 * ---------------------------------------------------------------------
 * This document is distributed under GNU General Public License.
 * Copyright © David Kutnar 2016 - All rights reserved.
 * =====================================================================
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pico3D.Graphics
{
    public struct Vertex : IVertexType
    {
        #region Fields:

        public Vector3 Position;
        public Vector2 TextureCoordinate;
        public Vector3 Normal;

        const int V3_SIZE = ( sizeof( float ) * 3 );
        const int V2_SIZE = ( sizeof( float ) * 2 );

        #endregion

        public Vertex( Vector3 position, Vector2 texCoordinate, Vector3 normal ) {
            this.Position = position;
            this.TextureCoordinate = texCoordinate;
            this.Normal = normal;
        }

        public readonly static VertexDeclaration VertexDeclaration = new VertexDeclaration
        (
            new VertexElement( 0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0 ),
            new VertexElement( V3_SIZE, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0 ),
            new VertexElement( V3_SIZE + V2_SIZE, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0 )
        );

        VertexDeclaration IVertexType.VertexDeclaration {
            get { return VertexDeclaration; }
        }
    }
}