/* 
 * Primitives.BoundingBox
 * =====================================================================
 * FileName: BoundingBox.cs
 * Location: ./Primitives/
 * Project: Pico 3D
 * ---------------------------------------------------------------------
 * Created: 2/10/2016 10:55:00 PM
 * ---------------------------------------------------------------------
 * This document is distributed under GNU General Public License.
 * Copyright © David Kutnar 2016 - All rights reserved.
 * =====================================================================
 */

using System;
using Microsoft.Xna.Framework;

namespace Pico3D.Primitives
{
    public struct BoundingBox
    {
        #region Fields:

        public Vector3 Center;
        public Vector3 HalfExtent;
        public Quaternion Orientation;

        #endregion

        public BoundingBox( Vector3 center, Vector3 halfExtents ) {
            this.Center = center;
            this.HalfExtent = halfExtents;
            this.Orientation = Quaternion.Identity;
        }

        public void Transform( Matrix transform ) {
            this.Center = Vector3.Transform( this.Center, transform );
            this.Orientation = Quaternion.CreateFromRotationMatrix( transform );
        }

        public ContainmentType Contains( ref BoundingBox other ) {
            Quaternion invOrient;
            Quaternion.Conjugate( ref Orientation, out invOrient );
            Quaternion relOrient;
            Quaternion.Multiply( ref invOrient, ref other.Orientation, out relOrient );

            Matrix relTransform = Matrix.CreateFromQuaternion( relOrient );
            relTransform.Translation = Vector3.Transform( other.Center - Center, invOrient );

            return ContainsRelativeBox( ref HalfExtent, ref other.HalfExtent, ref relTransform );
        }

        public static ContainmentType ContainsRelativeBox( ref Vector3 hA, ref Vector3 hB, ref Matrix mB ) {
            Vector3 mB_T = mB.Translation;
            Vector3 mB_TA = new Vector3( Math.Abs( mB_T.X ), Math.Abs( mB_T.Y ), Math.Abs( mB_T.Z ) );

            Vector3 bX = mB.Right;
            Vector3 bY = mB.Up;
            Vector3 bZ = mB.Backward;
            Vector3 hx_B = bX * hB.X;
            Vector3 hy_B = bY * hB.Y;
            Vector3 hz_B = bZ * hB.Z;

            float projx_B = Math.Abs( hx_B.X ) + Math.Abs( hy_B.X ) + Math.Abs( hz_B.X );
            float projy_B = Math.Abs( hx_B.Y ) + Math.Abs( hy_B.Y ) + Math.Abs( hz_B.Y );
            float projz_B = Math.Abs( hx_B.Z ) + Math.Abs( hy_B.Z ) + Math.Abs( hz_B.Z );
            if ( mB_TA.X + projx_B <= hA.X && mB_TA.Y + projy_B <= hA.Y && mB_TA.Z + projz_B <= hA.Z )
                return ContainmentType.Contains;

            if ( mB_TA.X >= hA.X + Math.Abs( hx_B.X ) + Math.Abs( hy_B.X ) + Math.Abs( hz_B.X ) )
                return ContainmentType.Disjoint;

            if ( mB_TA.Y >= hA.Y + Math.Abs( hx_B.Y ) + Math.Abs( hy_B.Y ) + Math.Abs( hz_B.Y ) )
                return ContainmentType.Disjoint;

            if ( mB_TA.Z >= hA.Z + Math.Abs( hx_B.Z ) + Math.Abs( hy_B.Z ) + Math.Abs( hz_B.Z ) )
                return ContainmentType.Disjoint;

            if ( Math.Abs( Vector3.Dot( mB_T, bX ) ) >= Math.Abs( hA.X * bX.X ) + Math.Abs( hA.Y * bX.Y ) + Math.Abs( hA.Z * bX.Z ) + hB.X )
                return ContainmentType.Disjoint;

            if ( Math.Abs( Vector3.Dot( mB_T, bY ) ) >= Math.Abs( hA.X * bY.X ) + Math.Abs( hA.Y * bY.Y ) + Math.Abs( hA.Z * bY.Z ) + hB.Y )
                return ContainmentType.Disjoint;

            if ( Math.Abs( Vector3.Dot( mB_T, bZ ) ) >= Math.Abs( hA.X * bZ.X ) + Math.Abs( hA.Y * bZ.Y ) + Math.Abs( hA.Z * bZ.Z ) + hB.Z )
                return ContainmentType.Disjoint;

            Vector3 axis = Vector3.Zero;

            axis = new Vector3( 0, -bX.Z, bX.Y );
            if ( Math.Abs( Vector3.Dot( mB_T, axis ) ) >= Math.Abs( hA.Y * axis.Y ) + Math.Abs( hA.Z * axis.Z ) + Math.Abs( Vector3.Dot( axis, hy_B ) ) + Math.Abs( Vector3.Dot( axis, hz_B ) ) )
                return ContainmentType.Disjoint;

            axis = new Vector3( 0, -bY.Z, bY.Y );
            if ( Math.Abs( Vector3.Dot( mB_T, axis ) ) >= Math.Abs( hA.Y * axis.Y ) + Math.Abs( hA.Z * axis.Z ) + Math.Abs( Vector3.Dot( axis, hz_B ) ) + Math.Abs( Vector3.Dot( axis, hx_B ) ) )
                return ContainmentType.Disjoint;

            axis = new Vector3( 0, -bZ.Z, bZ.Y );
            if ( Math.Abs( Vector3.Dot( mB_T, axis ) ) >= Math.Abs( hA.Y * axis.Y ) + Math.Abs( hA.Z * axis.Z ) + Math.Abs( Vector3.Dot( axis, hx_B ) ) + Math.Abs( Vector3.Dot( axis, hy_B ) ) )
                return ContainmentType.Disjoint;

            axis = new Vector3( bX.Z, 0, -bX.X );
            if ( Math.Abs( Vector3.Dot( mB_T, axis ) ) >= Math.Abs( hA.Z * axis.Z ) + Math.Abs( hA.X * axis.X ) + Math.Abs( Vector3.Dot( axis, hy_B ) ) + Math.Abs( Vector3.Dot( axis, hz_B ) ) )
                return ContainmentType.Disjoint;

            axis = new Vector3( bY.Z, 0, -bY.X );
            if ( Math.Abs( Vector3.Dot( mB_T, axis ) ) >= Math.Abs( hA.Z * axis.Z ) + Math.Abs( hA.X * axis.X ) + Math.Abs( Vector3.Dot( axis, hz_B ) ) + Math.Abs( Vector3.Dot( axis, hx_B ) ) )
                return ContainmentType.Disjoint;

            axis = new Vector3( bZ.Z, 0, -bZ.X );
            if ( Math.Abs( Vector3.Dot( mB_T, axis ) ) >= Math.Abs( hA.Z * axis.Z ) + Math.Abs( hA.X * axis.X ) + Math.Abs( Vector3.Dot( axis, hx_B ) ) + Math.Abs( Vector3.Dot( axis, hy_B ) ) )
                return ContainmentType.Disjoint;

            axis = new Vector3( -bX.Y, bX.X, 0 );
            if ( Math.Abs( Vector3.Dot( mB_T, axis ) ) >= Math.Abs( hA.X * axis.X ) + Math.Abs( hA.Y * axis.Y ) + Math.Abs( Vector3.Dot( axis, hy_B ) ) + Math.Abs( Vector3.Dot( axis, hz_B ) ) )
                return ContainmentType.Disjoint;

            axis = new Vector3( -bY.Y, bY.X, 0 );
            if ( Math.Abs( Vector3.Dot( mB_T, axis ) ) >= Math.Abs( hA.X * axis.X ) + Math.Abs( hA.Y * axis.Y ) + Math.Abs( Vector3.Dot( axis, hz_B ) ) + Math.Abs( Vector3.Dot( axis, hx_B ) ) )
                return ContainmentType.Disjoint;

            axis = new Vector3( -bZ.Y, bZ.X, 0 );
            if ( Math.Abs( Vector3.Dot( mB_T, axis ) ) >= Math.Abs( hA.X * axis.X ) + Math.Abs( hA.Y * axis.Y ) + Math.Abs( Vector3.Dot( axis, hx_B ) ) + Math.Abs( Vector3.Dot( axis, hy_B ) ) )
                return ContainmentType.Disjoint;

            return ContainmentType.Intersects;
        }

        public bool Contains( ref Vector3 point ) {
            Quaternion qinv = Quaternion.Conjugate( this.Orientation );
            Vector3 plocal = Vector3.Transform( point - this.Center, qinv );

            return Math.Abs( plocal.X ) <= this.HalfExtent.X &&
                   Math.Abs( plocal.Y ) <= this.HalfExtent.Y &&
                   Math.Abs( plocal.Z ) <= this.HalfExtent.Z;
        }

        public bool Intersects( ref BoundingBox other ) {
            return this.Contains( ref other ) != ContainmentType.Disjoint;
        }

        public float? Intersects( ref Ray ray ) {
            Matrix R = Matrix.CreateFromQuaternion( this.Orientation );
            Vector3 TOrigin = ( this.Center - ray.Position );

            float t_min = -float.MaxValue;
            float t_max = float.MaxValue;

            float axisDotOrigin = Vector3.Dot( R.Right, TOrigin );
            float axisDotDir = Vector3.Dot( R.Right, ray.Direction );

            if ( axisDotDir >= -Constants.EPSILON && axisDotDir <= Constants.EPSILON ) {
                if ( ( -axisDotOrigin - HalfExtent.X ) > 0.0 || ( -axisDotOrigin + HalfExtent.X ) > 0.0f )
                    return null;
            }
            else {
                float t1 = ( axisDotOrigin - HalfExtent.X ) / axisDotDir;
                float t2 = ( axisDotOrigin + HalfExtent.X ) / axisDotDir;

                if ( t1 > t2 ) {
                    float temp = t1;
                    t1 = t2;
                    t2 = temp;
                }

                if ( t1 > t_min )
                    t_min = t1;

                if ( t2 < t_max )
                    t_max = t2;

                if ( t_max < 0.0f || t_min > t_max )
                    return null;
            }

            axisDotOrigin = Vector3.Dot( R.Up, TOrigin );
            axisDotDir = Vector3.Dot( R.Up, ray.Direction );

            if ( axisDotDir >= -Constants.EPSILON && axisDotDir <= Constants.EPSILON ) {
                if ( ( -axisDotOrigin - HalfExtent.Y ) > 0.0 || ( -axisDotOrigin + HalfExtent.Y ) > 0.0f )
                    return null;
            }
            else {
                float t1 = ( axisDotOrigin - HalfExtent.Y ) / axisDotDir;
                float t2 = ( axisDotOrigin + HalfExtent.Y ) / axisDotDir;

                if ( t1 > t2 ) {
                    float temp = t1;
                    t1 = t2;
                    t2 = temp;
                }

                if ( t1 > t_min )
                    t_min = t1;

                if ( t2 < t_max )
                    t_max = t2;

                if ( t_max < 0.0f || t_min > t_max )
                    return null;
            }

            axisDotOrigin = Vector3.Dot( R.Forward, TOrigin );
            axisDotDir = Vector3.Dot( R.Forward, ray.Direction );

            if ( axisDotDir >= -Constants.EPSILON && axisDotDir <= Constants.EPSILON ) {
                if ( ( -axisDotOrigin - HalfExtent.Z ) > 0.0 || ( -axisDotOrigin + HalfExtent.Z ) > 0.0f )
                    return null;
            }
            else {
                float t1 = ( axisDotOrigin - HalfExtent.Z ) / axisDotDir;
                float t2 = ( axisDotOrigin + HalfExtent.Z ) / axisDotDir;

                if ( t1 > t2 ) {
                    float temp = t1;
                    t1 = t2;
                    t2 = temp;
                }

                if ( t1 > t_min )
                    t_min = t1;

                if ( t2 < t_max )
                    t_max = t2;

                if ( t_max < 0.0f || t_min > t_max )
                    return null;
            }

            return t_min;
        }

        public Vector3[] GetCorners( ) {
            Vector3[] corners = new Vector3[ 8 ];
            GetCorners( corners, 0 );
            return corners;
        }
        public void GetCorners( Vector3[] corners, int startIndex ) {
            Matrix m = Matrix.CreateFromQuaternion( this.Orientation );
            Vector3 hX = ( m.Left * this.HalfExtent.X );
            Vector3 hY = ( m.Up * this.HalfExtent.Y );
            Vector3 hZ = ( m.Backward * this.HalfExtent.Z );

            int i = startIndex;
            corners[ i++ ] = ( this.Center - hX + hY + hZ );
            corners[ i++ ] = ( this.Center + hX + hY + hZ );
            corners[ i++ ] = ( this.Center + hX - hY + hZ );
            corners[ i++ ] = ( this.Center - hX - hY + hZ );
            corners[ i++ ] = ( this.Center - hX + hY - hZ );
            corners[ i++ ] = ( this.Center + hX + hY - hZ );
            corners[ i++ ] = ( this.Center + hX - hY - hZ );
            corners[ i++ ] = ( this.Center - hX - hY - hZ );
        }

        public BoundingFrustum ConvertToFrustum( ) {
            Quaternion invOrientation = Quaternion.Identity;
            Quaternion.Conjugate( ref Orientation, out invOrientation );

            float sx = ( 1.0f / this.HalfExtent.X );
            float sy = ( 1.0f / this.HalfExtent.Y );
            float sz = ( 0.5f / this.HalfExtent.Z );

            Matrix temp = Matrix.Identity;
            Matrix.CreateFromQuaternion( ref invOrientation, out temp );
            temp.M11 *= sx; temp.M21 *= sx; temp.M31 *= sx;
            temp.M12 *= sy; temp.M22 *= sy; temp.M32 *= sy;
            temp.M13 *= sz; temp.M23 *= sz; temp.M33 *= sz;
            temp.Translation = ( Vector3.UnitZ * 0.5f + Vector3.TransformNormal( -Center, temp ) );

            return new BoundingFrustum( temp );
        }

        public bool Equals( BoundingBox other ) {
            return ( ( this.Center == other.Center ) && ( this.HalfExtent == other.HalfExtent ) && ( this.Orientation == other.Orientation ) );
        }
        public override bool Equals( object obj ) {
            if ( obj != null && obj.GetType( ) == typeof( BoundingBox ) ) {
                return this.Equals( ( BoundingBox )obj );
            }

            return false;
        }

        public static bool operator ==( BoundingBox a, BoundingBox b ) {
            return Equals( a, b );
        }
        public static bool operator !=( BoundingBox a, BoundingBox b ) {
            return !Equals( a, b );
        }

        public override int GetHashCode( ) {
            return ( this.Center.GetHashCode( ) ^ this.HalfExtent.GetHashCode( ) ^ this.Orientation.GetHashCode( ) );
        }
    }
}