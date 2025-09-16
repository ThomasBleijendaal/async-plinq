using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.ColorSpaces.Conversion;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Quantization;

namespace AsyncAnimator;

internal static class Draw
{
    private const int Height = 400;
    private const int Offset = 30;
    private const int Width = 80;

    public static void DrawTimings(string name, IReadOnlyCollection<Timing> inputData, IReadOnlyList<string> stageNames)
    {
        var maxTime = inputData.MaxBy(x => x.Exit)!.Exit + TimeSpan.FromSeconds(1);
        var maxStages = inputData.MaxBy(x => x.Stage)!.Stage;
        var maxItems = inputData.MaxBy(x => x.Item)!.Item;

        var items = inputData.Select(x => x.Item).Distinct().ToArray();

        var frameTime = 60;

        var image = new Image<Rgba32>(800, Height, Color.White);

        var metadata = image.Metadata.GetGifMetadata();
        metadata.RepeatCount = 0;

        var gifMetadata = image.Frames.RootFrame.Metadata.GetGifMetadata();
        gifMetadata.FrameDelay = 0;

        var maxFrames = maxTime.TotalMilliseconds / frameTime;
        for (var i = 0; i < maxFrames; i++)
        {
            var currentTime = TimeSpan.FromMilliseconds((i + 1) * frameTime);

            using var frame = new Image<Rgba32>(800, Height, Color.White);

            gifMetadata = frame.Frames.RootFrame.Metadata.GetGifMetadata();
            gifMetadata.FrameDelay = 0;

            Stages(frame, maxStages, stageNames);
            foreach (var item in items)
            {
                var filtered =
                    inputData
                        .Where(x => x.Item == item);

                var data =
                    filtered
                        .Where(x => x.Exit > currentTime)
                        .OrderBy(x => x.Entry)
                        .FirstOrDefault() ??
                    filtered
                        .OrderByDescending(x => x.Exit)
                        .First();

                Item(frame, data, currentTime);
            }

            //frame.Mutate(x => x.DrawText(currentTime.ToString(@"ss\.ff"), font, Color.Black, new PointF(0, 0)));

            image.Frames.AddFrame(frame.Frames.RootFrame);
        }

        image.Frames.RemoveFrame(0);

        var encoder = new GifEncoder()
        {
            ColorTableMode = GifColorTableMode.Local,
            Quantizer = new OctreeQuantizer(new QuantizerOptions
            {
                MaxColors = 255
            })
        };

        image.SaveAsGif(name, encoder);
    }

    private static void Stages(Image<Rgba32> image, int maxStages, IReadOnlyList<string> stageNames)
    {
        SystemFonts.TryGet("Consolas", out var fontFamily);
        var font = fontFamily.CreateFont(14, FontStyle.Bold);

        var size = new SizeF(Width, Height - (Offset * 2));

        var offset = new PointF(Offset, Offset);
        var offsetW = new PointF(Offset, 0);
        var offsetHH = new PointF(0, Offset / 2);
        var w = new SizeF(size.Width, 0);
        var h = new SizeF(0, size.Height);

        var pen = Pens.Solid(Color.Gray);

        for (var i = 0; i < maxStages; i++)
        {
            var origin = offset + (i * (offsetW + w));

            image.Mutate(x => x.DrawText(stageNames[i], font, Color.Silver, origin - offsetHH));

            image.Mutate(x => x.DrawPolygon(pen, origin, origin + w, origin + size, origin + h));
        }
    }

    private static void Item(Image<Rgba32> image, Timing item, TimeSpan progress)
    {
        var height = Offset + (item.Item * Offset);
        var leftOffset = Offset + ((item.Stage - 1) * (Width + Offset));

        var dt = item.Exit.TotalSeconds - item.Entry.TotalSeconds;
        var pt = progress.TotalSeconds - item.Entry.TotalSeconds;

        var percentage = Math.Clamp(((dt == pt) ? 0 : (pt / dt)), 0, 1);

        var pos = item switch
        {
            _ when item.Entry > progress => new PointF(leftOffset - (Offset / 2), height),

            _ when item.Exit > progress => new PointF((float)(leftOffset + (percentage * Width)), height),

            _ => new PointF(leftOffset + Width + (Offset / 2), height)
        };

        var hsv = new Hsv((item.Item - 1) * 36, 1, 1);
        var rgb = ColorSpaceConverter.ToRgb(hsv);
        var color = Color.FromRgb((byte)(rgb.R * 255), (byte)(rgb.G * 255), (byte)(rgb.B * 255));

        var width = item.Continues ? 10f : (float)(3 + (7 * (1.0 - percentage)));

        var pen = Pens.Solid(color, width);

        image.Mutate(x => x.DrawLine(pen, pos, pos));
    }
}
