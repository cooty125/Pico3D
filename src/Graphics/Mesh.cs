/* 
 * Graphics.Mesh
 * =====================================================================
 * FileName: Mesh.cs
 * Location: ./Graphics/
 * Project: Pico 3D
 * ---------------------------------------------------------------------
 * Created: 2/10/2016 3:26:21 PM
 * ---------------------------------------------------------------------
 * This document is distributed under GNU General Public License.
 * Copyright © David Kutnar 2016 - All rights reserved.
 * =====================================================================
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pico3D.Graphics
{
    public class Mesh
    {
        #region Fields:

        string name = string.Empty;

        int[] index_data = null;
        Vertex[] vertex_data = null;
        VertexBuffer vertex_buffer = null;

        #endregion
        #region Properties:

        public string Name {
            get { return this.name; }
        }

        public int[] IndexData {
            get { return this.index_data; }
        }
        public Vertex[] VertexData {
            get { return this.vertex_data; }
        }
        public VertexBuffer VertexBuffer {
            get { return this.vertex_buffer; }
        }
        public int StartVertex {
            get { return 0; }
        }
        public int PrimitiveCount {
            get { return ( this.vertex_data.Length / 3 ); }
        }

        #endregion

        public Mesh( string name ) {
            this.name = name;
        }
        ~Mesh( ) {
            this.index_data = null;
            this.vertex_data = null;
            this.vertex_data = null;
        }

        public void SetVertexData( Vertex[] vertexData ) {
            this.vertex_data = vertexData;
        }

        public void SetIndexData( int[] indexData ) {
            this.index_data = indexData;
        }

        public void CreateVertexBuffer( GraphicsDevice gDevice ) {
            if ( this.vertex_data != null ) {
                this.vertex_buffer = new VertexBuffer( gDevice, Vertex.VertexDeclaration, this.vertex_data.Length, BufferUsage.WriteOnly );
                this.vertex_buffer.SetData( this.vertex_data );
            }
        }
    }
}