/* 
 * IRenderer
 * =====================================================================
 * FileName: IRenderer.cs
 * Location: ./
 * Project: Pico 3D
 * ---------------------------------------------------------------------
 * Created: 2/10/2016 3:33:45 PM
 * ---------------------------------------------------------------------
 * This document is distributed under GNU General Public License.
 * Copyright © David Kutnar 2016 - All rights reserved.
 * =====================================================================
 */

using Microsoft.Xna.Framework;

namespace Pico3D
{
    public interface IRenderer
    {
        void Render( Matrix view, Matrix projection );
    }
}