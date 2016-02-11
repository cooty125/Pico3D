/* 
 * IImporter
 * =====================================================================
 * FileName: IImporter.cs
 * Location: ./
 * Project: Pico 3D
 * ---------------------------------------------------------------------
 * Created: 2/10/2016 3:34:39 PM
 * ---------------------------------------------------------------------
 * This document is distributed under GNU General Public License.
 * Copyright © David Kutnar 2016 - All rights reserved.
 * =====================================================================
 */

using Pico3D.Graphics;

namespace Pico3D
{
    public interface IImporter
    {
        string SourceDirectory { get; set; }

        Model Import( string filename );
    }
}