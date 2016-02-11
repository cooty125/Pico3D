/* 
 * Embedded.GraphicsDeviceService
 * =====================================================================
 * FileName: GraphicsDeviceService.cs.cs
 * Location: ./Embedded/
 * Project: Pico 3D
 * ---------------------------------------------------------------------
 * Created: 2/10/2016 3:39:17 PM
 * ---------------------------------------------------------------------
 * This document is distributed under GNU General Public License.
 * Copyright © David Kutnar 2016 - All rights reserved.
 * =====================================================================
 */

using System;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;

namespace Pico3D.Embedded
{
    class GraphicsDeviceService : IGraphicsDeviceService
    {
        #region Fields:

        static GraphicsDeviceService singleton_instance = null;
        static int num_of_instaces = 0;

        GraphicsDevice g_device = null;
        PresentationParameters p_params = null;

        #endregion
        #region Properties:

        public GraphicsDevice GraphicsDevice {
            get { return this.g_device; }
        }

        #endregion
        #region Events:

        public event EventHandler<EventArgs> DeviceCreated = null;
        public event EventHandler<EventArgs> DeviceDisposing = null;
        public event EventHandler<EventArgs> DeviceReset = null;
        public event EventHandler<EventArgs> DeviceResetting = null;

        #endregion

        GraphicsDeviceService( IntPtr wHandle, int vWidth, int vHeight ) {
            this.p_params = new PresentationParameters( );
            this.p_params.BackBufferWidth = vWidth;
            this.p_params.BackBufferHeight = vHeight;
            this.p_params.IsFullScreen = false;
            this.p_params.MultiSampleCount = 2;
            this.p_params.BackBufferFormat = SurfaceFormat.Color;
            this.p_params.DepthStencilFormat = DepthFormat.Depth24Stencil8;
            this.p_params.DeviceWindowHandle = wHandle;
            this.p_params.PresentationInterval = PresentInterval.Immediate;

            this.g_device = new GraphicsDevice( GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, this.p_params );

            if ( this.DeviceCreated != null ) {
                this.DeviceCreated( this, EventArgs.Empty );
            }
        }

        public static GraphicsDeviceService AddInstance( IntPtr handle, int width, int height ) {
            if ( Interlocked.Increment( ref num_of_instaces ) >= 1 ) {
                singleton_instance = new GraphicsDeviceService( handle, width, height );
            }

            return singleton_instance;
        }

        public void Reset( ) {
            if ( this.DeviceResetting != null ) {
                this.DeviceResetting( this, EventArgs.Empty );
            }

            this.g_device.Reset( );

            if ( this.DeviceReset != null ) {
                this.DeviceReset( this, EventArgs.Empty );
            }
        }

        public void Resize( int width, int height ) {
            if ( this.DeviceResetting != null ) {
                this.DeviceResetting( this, EventArgs.Empty );
            }

            this.p_params.BackBufferWidth = width;
            this.p_params.BackBufferHeight = height;

            this.g_device.Reset( this.p_params );

            if ( this.DeviceReset != null ) {
                this.DeviceReset( this, EventArgs.Empty );
            }
        }

        public void Dispose( bool disposing ) {
            if ( Interlocked.Decrement( ref num_of_instaces ) == 0 ) {
                if ( disposing ) {
                    if ( this.DeviceDisposing != null ) {
                        this.DeviceDisposing( this, EventArgs.Empty );
                    }

                    this.g_device.Dispose( );
                }
                this.g_device = null;
            }
        }
    }
}