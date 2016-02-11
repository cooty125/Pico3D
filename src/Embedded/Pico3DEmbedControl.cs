/* 
 * Embedded.Pico3DEmbedControl
 * =====================================================================
 * FileName: Pico3DEmbedControl.cs
 * Location: ./Embedded/
 * Project: Pico 3D
 * ---------------------------------------------------------------------
 * Created: 2/10/2016 3:42:12 PM
 * ---------------------------------------------------------------------
 * This document is distributed under GNU General Public License.
 * Copyright © David Kutnar 2016 - All rights reserved.
 * =====================================================================
 */

using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = Microsoft.Xna.Framework.Color;

namespace Pico3D.Embedded
{
    public abstract class Pico3DEmbedControl : Control
    {
        #region Fields:

        GraphicsDeviceService g_device_service = null;
        ServiceContainer services = null;

        bool active = true;
        Stopwatch timer = null;

        Color r_color = Color.Black;

        #endregion
        #region Properties:

        public GraphicsDevice GraphicsDevice {
            get { return this.g_device_service.GraphicsDevice; }
        }
        public ServiceContainer Services {
            get { return this.services; }
        }

        public bool IsActive {
            get { return this.active; }
            set { this.active = value; }
        }

        public Color ResetingColor {
            get { return this.r_color; }
            set {
                if ( value != null ) {
                    this.r_color = value;
                }
                else {
                    this.r_color = Color.Black;
                }
            }
        }

        #endregion

        #region Abstract Methods:

        protected abstract void Initialize( );
        protected abstract void Update( float eTime );
        protected abstract void Draw( );

        #endregion

        ~Pico3DEmbedControl( ) {
            this.Dispose( );
        }

        protected override void OnCreateControl( ) {
            if ( !this.DesignMode ) {
                this.g_device_service = GraphicsDeviceService.AddInstance( this.Handle, this.ClientSize.Width, this.ClientSize.Height );

                this.services = new ServiceContainer( );
                this.services.AddService<IGraphicsDeviceService>( this.g_device_service );

                this.timer = Stopwatch.StartNew( );
                this.Initialize( );

                Application.Idle += delegate { this.Invalidate( ); };
            }

            base.OnCreateControl( );
        }

        protected override void OnPaint( PaintEventArgs e ) {
            if ( this.g_device_service != null ) {
                if ( this.active ) {
                    this.Update( ( float )this.timer.Elapsed.TotalSeconds );
                    this.timer.Restart( );
                }

                bool need_to_reset = false;

                switch ( this.GraphicsDevice.GraphicsDeviceStatus ) {
                    case GraphicsDeviceStatus.Lost:
                        throw new InvalidOperationException( "Can't create graphics device instance!" );
                    case GraphicsDeviceStatus.NotReset:
                        need_to_reset = true;
                        break;
                    default:
                        PresentationParameters pp = GraphicsDevice.PresentationParameters;
                        need_to_reset = ( ClientSize.Width > pp.BackBufferWidth ) || ( ClientSize.Height > pp.BackBufferHeight );
                        break;
                }

                if ( need_to_reset && this.active ) {
                    this.g_device_service.Reset( );
                }

                this.GraphicsDevice.Viewport = this.GetViewport( );
                this.GraphicsDevice.Clear( this.r_color );
                this.GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;
                this.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                this.Draw( );
                this.GraphicsDevice.Present( this.GetRectangle( ), null, this.Handle );
            }
            else {
                StringFormat s_format = new StringFormat( );
                s_format.Alignment = StringAlignment.Center;
                s_format.LineAlignment = StringAlignment.Center;

                System.Drawing.Graphics g = this.CreateGraphics( );
                g.Clear( System.Drawing.Color.DarkGray );
                g.DrawString( "Embed SGL Control", this.Font, new SolidBrush( System.Drawing.Color.Black ), new PointF( this.ClientRectangle.Width / 2.0f, this.ClientRectangle.Height / 2.0f ), s_format );
            }

            base.OnPaint( e );
        }

        protected override void OnPaintBackground( PaintEventArgs pevent ) {
        }

        protected override void OnResize( EventArgs e ) {
            if ( !this.DesignMode && this.g_device_service != null ) {
                if ( this.ClientRectangle.Width > 0 && this.ClientRectangle.Height > 0 ) {
                    this.g_device_service.Resize( this.ClientSize.Width, this.ClientSize.Height );
                }
            }
            
            base.OnResize( e );
        }

        Viewport GetViewport( ) {
            Viewport vp = new Viewport( this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientSize.Width, this.ClientSize.Height );
            vp.MinDepth = 0.0f;
            vp.MaxDepth = 1.0f;

            return vp;
        }
        Rectangle GetRectangle( ) {
            return new Rectangle( 0, 0, this.ClientSize.Width, this.ClientSize.Height );
        }

        protected override void Dispose( bool disposing ) {
            if ( this.g_device_service != null ) {
                this.timer.Stop( );
                this.timer = null;
                this.services.RemoveService( this.g_device_service );
                this.g_device_service.Dispose( disposing );
                this.g_device_service = null;
            }

            base.Dispose( disposing );
        }
    }
}