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

            private Vector2 pos;
            private Vector2 anchor;

            public Vector2 Position => pos;
            public Vector2 AnchorPoint => anchor;

            #endregion

            #region # - Constructor

            public Location(Vector2 position, Vector2 anchor)
            {
                pos = position;
                anchor = anchor;
            }

            public Location(float x, float y, Vector2 anchor) : base(new Vector2(x, y), anchor) {}

            public Location(float x, float y) : base(x, y, AnchorPoints.Center) {}

            #endregion
        }
    }
}
