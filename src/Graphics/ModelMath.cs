/* 
 * Graphics.ModelMath
 * =====================================================================
 * FileName: ModelMath.cs
 * Location: ./Graphics/
 * Project: Pico 3D
 * ---------------------------------------------------------------------
 * Created: 2/10/2016 10:52:47 PM
 * ---------------------------------------------------------------------
 * This document is distributed under GNU General Public License.
 * Copyright © David Kutnar 2016 - All rights reserved.
 * =====================================================================
 */

using System;
using Microsoft.Xna.Framework;
using Pico3D.Primitives;

using BoundingBox = Pico3D.Primitives.BoundingBox;

namespace Pico3D.Graphics
{
    public static class ModelMath
    {
        public static BoundingBox ComputeBoundingBox( Model model ) {
            if ( model != null ) 
            {
                Vector3 center = model.Transform.Translation;
                Vector3 model_min = Vector3.Zero;
                Vector3 model_max = Vector3.Zero;

                foreach ( Mesh mesh in model.Meshes ) {
                    Vector3 mesh_min = Vector3.Zero;
                    Vector3 mesh_max = Vector3.Zero;

                    for ( int i = 0; i < mesh.VertexData.Length; i++ ) {
                        Vector3 vert_pos = mesh.VertexData[ i ].Position;

                        mesh_min = Vector3.Min( mesh_min, vert_pos );
                        mesh_max = Vector3.Max( mesh_max, vert_pos );
                    }

                    model_min = Vector3.Min( model_min, mesh_min );
                    model_max = Vector3.Max( model_max, mesh_max );
                }

                Vector3 extents = Vector3.One;
                extents.X = ( Math.Abs( model_max.X - model_min.X ) / 2.0f );
                extents.Y = ( Math.Abs( model_max.Y - model_min.Y ) / 2.0f );
                extents.Z = ( Math.Abs( model_max.Z - model_min.Z ) / 2.0f );

                return new BoundingBox( center, extents );
            }

            return new Pico3D.Primitives.BoundingBox( );
        }
    }
}