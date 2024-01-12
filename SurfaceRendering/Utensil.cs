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
                Center = (new Vector2(Width, Height) / 2f) + (Offset * 2);
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
                    Position = (at + new Location(0, size.Y / 2)).ToSpritePosition(size) + Offset,
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
                    Position = at.ToSpritePosition(size) + Offset,
                    Size = size,
                    RotationOrScale = rotation
                });
            }

            public Utensil DrawSprite(Location at, Vector2 size, string sprite, float rotation = 0f) => DrawSprite(at, size, sprite, ForegroundColor, rotation);

            public Utensil DrawRectangle(Location at, Vector2 size, Color color, float rotation = 0f) =>
                DrawSprite(new MySprite
                {
                    Type = SpriteType.TEXTURE,
                    Data = "SquareSimple",
                    Color = color,
                    Position = at.ToSpritePosition(size) + Offset,
                    Size = size,
                    RotationOrScale = rotation
                });

            public Utensil DrawRectangle(Location at, Vector2 size, float rotation = 0f) => DrawRectangle(at, size, ForegroundColor, rotation);

            public Utensil DrawRectangleBorder(Location at, Vector2 size, Color color, float thickness = 2f, float rotation = 0f)
            {
                StartClip(at + new Location(thickness, thickness), size - new Vector2(thickness * 2, thickness * 2));
                    DrawRectangle(at, size, color, rotation);
                return StopClip();
            }

            public Utensil DrawRectangleBorder(Location at, Vector2 size, float thickness = 2f, float rotation = 0f) => DrawRectangleBorder(at, size, ForegroundColor, thickness, rotation);

            public Utensil DrawScaledText(Location at, string text, float scale, Color color, string font = "White", TextAlignment alignment = TextAlignment.CENTER) =>
                DrawSprite(new MySprite()
                {
                    Type = SpriteType.TEXT,
                    Data = text,
                    Color = color,
                    Position = at.ToSpritePosition(surface.MeasureStringInPixels(new StringBuilder(text), font, scale))
                });

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
