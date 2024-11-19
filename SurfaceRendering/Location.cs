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
        public struct Location
        {
            #region # - Properties

            private Vector2 position;
            private Vector2 anchor;

            public Vector2 Position => position;
            public Vector2 AnchorPoint => anchor;

            #endregion

            #region # - Constructor

            public Location(Vector2 position, Vector2 anchor)
            {
                this.position = position;
                this.anchor = anchor;
            }

            public Location(float x, float y, Vector2 anchor) : this(new Vector2(x, y), anchor) { }

            public Location(Vector2 position) : this(position.X, position.Y) { }

            public Location(float x, float y) : this(x, y, AnchorPoints.TopLeft) {}

            #endregion

            #region # - Methods

            public Vector2 ToSpritePosition(Vector2 size, Vector2 anchor)
            {
                return new Vector2(
                    position.X - (size.X * anchor.X),
                    position.Y - (size.Y * anchor.Y)
                );
            }

            public Vector2 ToSpritePosition(Vector2 size)
            {
                return ToSpritePosition(size, anchor);
            }

            #endregion

            #region # - Operators

            public static Location operator +(Location a) => a;
            public static Location operator -(Location a) => new Location(-a.Position.X, -a.Position.Y, a.AnchorPoint);

            public static Location operator +(Location a, Location b) => new Location(a.Position.X + b.Position.X, a.Position.Y + b.position.Y, a.AnchorPoint);
            public static Location operator -(Location a, Location b) => a + (-b);
            public static Location operator *(Location a, Location b) => new Location(a.Position.X * b.Position.X, a.Position.Y * b.position.Y, a.AnchorPoint);
            public static Location operator /(Location a, Location b) => new Location(a.Position.X / b.Position.X, a.Position.Y / b.position.Y, a.AnchorPoint);

            public static Location operator *(Location a, float b) => new Location(a.Position.X * b, a.Position.Y * b, a.AnchorPoint);
            public static Location operator /(Location a, float b) => new Location(a.Position.X / b, a.Position.Y / b, a.AnchorPoint);

            #endregion
        }
    }
}
