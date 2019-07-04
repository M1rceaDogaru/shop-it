using System;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Android.Gms.Vision;

namespace ShopIt.Android.Infrastructure
{
    class ShopItSurfaceHolderCallback : ISurfaceHolderCallback
    {

        private readonly CameraSource _source;

        public ShopItSurfaceHolderCallback(CameraSource source)
        {
            _source = source;
        }

        public IntPtr Handle { get; set; }

        public void Dispose()
        {
            //
        }

        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
        {
            //
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            try
            {
                _source.Start(holder);
            }
            catch (Exception)
            {
            }
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            _source.Stop();
        }
    }
}