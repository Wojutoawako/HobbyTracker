using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;

namespace MobileAppProject
{
    public class GrainEffect : SKCanvasView
    {
        public SKColor BackgroundColor;
        public SKColor GrainColor;
        public double Density;

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs args)
        {
            var info = args.Info;
            var surface = args.Surface;
            var canvas = surface.Canvas;

            canvas.Clear(BackgroundColor);

            var rand = new Random();
            var paint = new SKPaint
            {
                Color = GrainColor,
                IsAntialias = true,
            };

            var density = (int)(info.Width * info.Height * Density);

            for (int i = 0; i < density; i++)
            {
                var x = rand.Next(0, info.Width);
                var y = rand.Next(0, info.Height);

                var size = (float)rand.NextDouble();

                canvas.DrawCircle(x, y, size, paint);
            }
        }
    }
}
