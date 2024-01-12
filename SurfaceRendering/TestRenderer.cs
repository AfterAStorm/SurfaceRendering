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
        public class TestRenderer : SurfaceRenderer
        {
            public TestRenderer(IMyTextSurface surface) : base(surface)
            {
            }

            protected override void Render(Utensil utensil)
            {
                utensil.DrawSprite(new Location(0, 0), utensil.Size, "UVChecker", Color.White);

                Vector2 size = new Vector2(20, 20);

                utensil.DrawRectangle(new Location(0, 0, AnchorPoints.TopLeft), size);
                utensil.DrawRectangleBorder(new Location(0, 0, AnchorPoints.TopLeft), size, Color.Tomato);
                utensil.DrawRectangle(new Location(0, 0, AnchorPoints.TopMiddle), size);
                utensil.DrawRectangleBorder(new Location(0, 0, AnchorPoints.TopMiddle), size, Color.Tomato);
                utensil.DrawRectangle(new Location(0, 0, AnchorPoints.TopRight), size);
                utensil.DrawRectangleBorder(new Location(0, 0, AnchorPoints.TopRight), size, Color.Tomato);

                utensil.DrawRectangle(new Location(0, 0, AnchorPoints.MiddleLeft), size);
                utensil.DrawRectangleBorder(new Location(0, 0, AnchorPoints.MiddleLeft), size, Color.Tomato);
                utensil.DrawRectangle(new Location(0, 0, AnchorPoints.Center), size);
                utensil.DrawRectangleBorder(new Location(0, 0, AnchorPoints.Center), size, Color.Tomato);
                utensil.DrawRectangle(new Location(0, 0, AnchorPoints.MiddleRight), size);
                utensil.DrawRectangleBorder(new Location(0, 0, AnchorPoints.MiddleRight), size, Color.Tomato);

                utensil.DrawRectangle(new Location(0, 0, AnchorPoints.BottomLeft), size);
                utensil.DrawRectangleBorder(new Location(0, 0, AnchorPoints.BottomLeft), size, Color.Tomato);
                utensil.DrawRectangle(new Location(0, 0, AnchorPoints.BottomMiddle), size);
                utensil.DrawRectangleBorder(new Location(0, 0, AnchorPoints.BottomMiddle), size, Color.Tomato);
                utensil.DrawRectangle(new Location(0, 0, AnchorPoints.BottomRight), size);
                utensil.DrawRectangleBorder(new Location(0, 0, AnchorPoints.BottomRight), size, Color.Tomato);

                utensil.DrawScaledText(new Location(utensil.Center) - new Location(0, 50), $"Surface Size: {surface.SurfaceSize.X}x{surface.SurfaceSize.Y}", 1f, Color.White);
                utensil.DrawScaledText(new Location(utensil.Center) - new Location(0, 30), $"Texture Size: {surface.TextureSize.X}x{surface.TextureSize.Y}", 1f, Color.White);
            }
        }
    }
}
