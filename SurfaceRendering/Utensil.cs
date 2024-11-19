using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    partial class Program
    {
        public class Utensil : IDisposable
        {
            #region # - Properties

            private bool disposed;

            protected IMyTextSurface surface;
            protected MySpriteDrawFrame frame;

            public IMyTextSurface Surface => surface;

            public readonly RectangleF Viewport;
            public readonly Vector2 Offset;

            public readonly float Width;
            public readonly float Height;
            public readonly Vector2 Center;
            public readonly Vector2 Size;

            public Color ForegroundColor;
            public Color BackgroundColor;

            #endregion

            #region # - Constructor

            public Utensil(IMyTextSurface surface)
            {
                Offset = (surface.TextureSize - surface.SurfaceSize) / 2f;
                Viewport = new RectangleF(
                    (surface.TextureSize - surface.SurfaceSize) / 2f,
                    surface.SurfaceSize
                );
                Width = surface.SurfaceSize.X;
                Height = surface.SurfaceSize.Y;
                Center = (new Vector2(Width, Height) / 2f);
                Size = new Vector2(Width, Height);

                ForegroundColor = surface.ScriptForegroundColor;
                BackgroundColor = surface.ScriptBackgroundColor;

                this.surface = surface;
                frame = surface.DrawFrame();
            }

            #endregion

            #region # - Surface Methods

            public Utensil SetColors(Color? foreground = null, Color? background = null)
            {
                if (foreground.HasValue)
                    surface.ScriptForegroundColor = foreground.Value;
                if (background.HasValue)
                    surface.ScriptBackgroundColor = background.Value;
                ForegroundColor = surface.ScriptForegroundColor;
                BackgroundColor = surface.ScriptBackgroundColor;
                return this;
            }

            public Utensil SetScript(string script = "None")
            {
                surface.Script = script;
                return this;
            }

            #endregion

            #region # - Clipping Methods

            public Utensil StartClip(Location at, Vector2 size)
            {
                return DrawSprite(new MySprite()
                {
                    Type = SpriteType.CLIP_RECT,
                    Position = (at).ToSpritePosition(size) + Offset + new Vector2(0, size.Y / 2),
                    Size = size
                });
            }

            public Utensil StopClip()
            {
                return DrawSprite(MySprite.CreateClearClipRect());
            }

            #endregion

            #region # - Draw Methods

            public Utensil DrawSprite(MySprite sprite)
            {
                frame.Add(sprite);
                return this;
            }

            public Utensil DrawSprite(Location at, Vector2 size, string sprite, Color color, float rotation = 0f)
            {
                return DrawSprite(new MySprite()
                {
                    Type = SpriteType.TEXTURE,
                    Data = sprite,
                    Color = color,
                    Position = at.ToSpritePosition(size) + Offset + new Vector2(0, size.Y / 2),
                    Size = size,
                    RotationOrScale = rotation
                });
            }

            public Utensil DrawSprite(Location at, Vector2 size, string sprite, float rotation = 0f) => DrawSprite(at, size, sprite, ForegroundColor, rotation);

            public Utensil DrawMonoImage(Location at, Vector2 size, Color color, string[] image, bool reversed=false)
            {
                Vector4 offset = GetMonoImageOffsetForSize(size, image);

                for (int ln = 0; ln < image.Length; ln++)
                {
                    string line = !reversed ? image[ln] : new string(image[ln].ToCharArray().Reverse().ToArray());
                    DrawScaledText(at + new Location(offset.X, offset.Y + offset.W * ln), line, offset.Z, color, "Monospace", TextAlignment.LEFT);
                }
                //DrawScaledText(at - new Location(0, 50), offset.ToString(), 1f, Color.Red, "White", TextAlignment.CENTER);
                //DrawScaledText(at - new Location(0, 30), $"{Surface.MeasureStringInPixels(new StringBuilder(image[0]), "Monospace", 1f).Y}", 1f, Color.Red, "White", TextAlignment.CENTER);
                return this;
            }


            public Utensil DrawMonoImage(Location at, Vector2 size, Color color, string image, bool reversed = false) => DrawMonoImage(at, size, color, image.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries), reversed);

            public Vector4 GetMonoImageOffsetForSize(Vector2 size, string[] image)
            {
                Vector2 lineSize = Surface.MeasureStringInPixels(new StringBuilder(image[0]), "Monospace", 1f);
                float characterHeight = lineSize.Y; // pixels
                float spacePerLineY = size.Y / image.Length; // rows
                float fontSizeY = (spacePerLineY / characterHeight);//characterHeight / size.Y;
                float characterWidth = lineSize.X; // pixels
                float spacePerLineX = size.X / (image[0].Length); // columns
                float fontSizeX = (spacePerLineX / spacePerLineX);

                float fontSize = Math.Min(fontSizeX, fontSizeY);
                lineSize = Surface.MeasureStringInPixels(new StringBuilder(image[0]), "Monospace", fontSize);
                return new Vector4(
                    -lineSize.X / 2,
                    -lineSize.Y * image.Length / 2 + lineSize.Y * 2,
                    fontSize,
                    lineSize.Y
                );
            }

            public Utensil DrawRectangle(Location at, Vector2 size, Color color, float rotation = 0f) =>
                DrawSprite(new MySprite
                {
                    Type = SpriteType.TEXTURE,
                    Data = "SquareSimple",
                    Color = color,
                    Position = at.ToSpritePosition(size) + Offset + new Vector2(0, size.Y / 2),
                    Size = size,
                    RotationOrScale = rotation
                });

            public Utensil DrawRectangle(Location at, Vector2 size, float rotation = 0f) => DrawRectangle(at, size, ForegroundColor, rotation);

            public Utensil DrawRectangleBorder(Location at, Vector2 size, Color color, float thickness = 2f, float rotation = 0f)
            {
                        DrawRectangle(at - ((size.ToLocation() - new Location(0, thickness)) * new Location(0, at.AnchorPoint.Y))                                          , new Vector2(size.X, thickness), color, rotation); // top
                        DrawRectangle(at - ((size.ToLocation() - new Location(thickness, 0)) * new Location(at.AnchorPoint.X, 0))                                          , new Vector2(thickness, size.Y), color, rotation); // left
                        DrawRectangle(at + new Location(0, size.Y - thickness) - ((size.ToLocation() - new Location(0, thickness)) * new Location(0, at.AnchorPoint.Y))    , new Vector2(size.X, thickness), color, rotation); // bottom
                return  DrawRectangle(at + new Location(size.X - thickness, 0) - ((size.ToLocation() - new Location(thickness, 0)) * new Location(at.AnchorPoint.X, 0))    , new Vector2(thickness, size.Y), color, rotation); // right
            }

            public Utensil DrawRectangleBorder(Location at, Vector2 size, float thickness = 2f, float rotation = 0f) => DrawRectangleBorder(at, size, ForegroundColor, thickness, rotation);

            public Utensil DrawScaledText(Location at, string text, float scale, Color color, string font = "White", TextAlignment alignment = TextAlignment.CENTER)
            {
                Vector2 pixelSize = surface.MeasureStringInPixels(new StringBuilder(text), font, scale);
                return DrawSprite(new MySprite()
                {
                    Type = SpriteType.TEXT,
                    Data = text,
                    Color = color,
                    Position =
                        at.ToSpritePosition(pixelSize)
                        + (pixelSize * new Vector2(at.AnchorPoint.X, (at.AnchorPoint.Y == 0 ? 1 : (at.AnchorPoint.Y == .5f ? 2 : 3)) / scale * 1.5f))
                        + (new Vector2(0, pixelSize.Y / 2 + 1)),
                    FontId = font,
                    Alignment = alignment,
                    RotationOrScale = scale
                });
            }

            #endregion

            #region # - IDisposable Methods

            public void Dispose()
            {
                if (disposed)
                    return;
                disposed = true;
                frame.Dispose();
            }

            #endregion
        }
    }
}
