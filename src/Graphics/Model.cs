/* 
 * Graphics.Model
 * =====================================================================
 * FileName: Model.cs
 * Location: ./Graphics/
 * Project: Pico 3D
 * ---------------------------------------------------------------------
 * Created: 2/10/2016 3:26:21 PM
 * ---------------------------------------------------------------------
 * This document is distributed under GNU General Public License.
 * Copyright © David Kutnar 2016 - All rights reserved.
 * =====================================================================
 */

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Pico3D.Graphics
{
    public class Model
    {
        #region Fields:

        string name = string.Empty;
        string tag = string.Empty;

        Matrix transform = Matrix.Identity;

        Material material = null;
        List<Mesh> meshes = null;

        #endregion
        #region Properties:

        public string Name {
            get { return this.name; }
        }
        public string Tag {
            get { return this.tag; }
            set { this.tag = value; }
        }

        public Matrix Transform {
            get { return this.transform; }
            set { this.transform = value; }
        }

        public Material Material {
            get { return this.material; }
        }
        public Mesh[] Meshes {
            get { return this.meshes.ToArray( ); }
        }

        #endregion

        public Model( string name ) {
            this.name = name;
        }
        ~Model( ) {
            this.material = null;
            this.meshes = null;
        }

        public void SetMaterial( Material material ) {
            this.material = material;
        }

        public void SetMeshes( List<Mesh> meshes ) {
            this.meshes = meshes;
        }
    }
}