/* 
 * Graphics.Shader
 * =====================================================================
 * FileName: Shader.cs
 * Location: ./Graphics/
 * Project: Pico 3D
 * ---------------------------------------------------------------------
 * Created: 2/10/2016 3:24:50 PM
 * ---------------------------------------------------------------------
 * This document is distributed under GNU General Public License.
 * Copyright © David Kutnar 2016 - All rights reserved.
 * =====================================================================
 */

using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace Pico3D.Graphics
{
    public class Shader
    {
        #region Fields:

        Effect effect = null;

        #endregion
        #region Properties:

        public Effect Effect {
            get { return this.effect; }
        }

        #endregion

        public Shader( GraphicsDevice gDevice, string filename ) {
            if ( File.Exists( filename ) && filename.EndsWith( ".psf" ) ) {
                byte[] bytes = File.ReadAllBytes( filename );

                this.effect = new Effect( gDevice, bytes );
                bytes = null;
            }
        }
        ~Shader( ) {
            this.effect = null;
        }
    }
}