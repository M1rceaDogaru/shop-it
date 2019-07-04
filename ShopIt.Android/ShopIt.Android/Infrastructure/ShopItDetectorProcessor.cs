using System;
using Android.Widget;
using Android.Gms.Vision;
using Android.Gms.Vision.Barcodes;
using Android.Util;

namespace ShopIt.Android.Infrastructure
{
    class ShopItDetectorProcessor : Detector.IProcessor
    {
        public IntPtr Handle { get; }

        private readonly TextView _barcodeText;

        public ShopItDetectorProcessor(TextView barcodeText)
        {
            _barcodeText = barcodeText;
        }

        public void Dispose()
        {
        }

        public void ReceiveDetections(Detector.Detections detections)
        {
            var detectedItems = detections.DetectedItems as SparseArray<Barcode>;
            if (detectedItems.Size() > 0)
            {
                _barcodeText.Post(() => _barcodeText.SetText(detectedItems.ValueAt(0).DisplayValue, TextView.BufferType.Normal));                
            }
        }

        public void Release()
        {
            //
        }
    }
}