/* 
 * Mesh
 * =====================================================================
 * FileName: Mesh.cs
 * Location: ./
 * Project: OBJ 2 PMF
 * ---------------------------------------------------------------------
 * Created: 2/17/2016 3:48:14 PM
 * ---------------------------------------------------------------------
 * This document is distributed under GNU General Public License.
 * Copyright © David Kutnar 2016 - All rights reserved.
 * =====================================================================
 */

using System.Collections.Generic;

namespace OBJ_2_PMF
{
    public struct Mesh
    {
        #region Fields:

        public string Name;
        public List<string> Positions;
        public List<string> Coordinates;
        public List<string> Faces;

        #endregion
    }
}