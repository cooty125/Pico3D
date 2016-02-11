/* 
 * Graphics.Material
 * =====================================================================
 * FileName: Material.cs
 * Location: ./Graphics/
 * Project: Pico 3D
 * ---------------------------------------------------------------------
 * Created: 2/10/2016 3:17:13 PM
 * ---------------------------------------------------------------------
 * This document is distributed under GNU General Public License.
 * Copyright © David Kutnar 2016 - All rights reserved.
 * =====================================================================
 */

using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Pico3D.Graphics
{
    public enum TextureType
    {
        DiffuseMap,
        NormalMap,
        SpecularMap,
        EmissiveMap
    };

    public class Material
    {
        #region Fields:

        string name = "sgl_material";

        Dictionary<TextureType, Texture2D> textures = null;

        #endregion
        #region Properties:

        public string Name {
            get { return this.name; }
        }

        public Dictionary<TextureType, Texture2D> Textures {
            get { return this.textures; }
        }

        #endregion

        public Material( string name ) {
            this.name = name;
            this.textures = new Dictionary<TextureType, Texture2D>( );
        }

        public void AddTexture( TextureType type, Texture2D texture ) {
            if ( !this.textures.ContainsKey( type ) ) {
                this.textures.Add( type, texture );
            }
        }

        public void RemoveTexture( TextureType type ) {
            if ( this.textures.ContainsKey( type ) ) {
                this.textures.Remove( type );
            }
        }

        public Texture2D GetTexture( TextureType type ) {
            if ( this.textures.ContainsKey( type ) ) {
                return this.textures[ type ];
            }

            return null;
        }

        public bool ContainsDiffuseMap( ) {
            return this.textures.ContainsKey( TextureType.DiffuseMap );
        }
    }
}