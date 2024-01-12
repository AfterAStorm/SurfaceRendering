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
        public static class AnchorPoints
        {
            public static readonly Vector2 TopLeft        = new Vector2(0, 0);
            public static readonly Vector2 TopMiddle      = new Vector2(0.5f, 0);
            public static readonly Vector2 TopRight       = new Vector2(1, 0);

            public static readonly Vector2 MiddleLeft     = new Vector2(0, 0.5f);
            public static readonly Vector2 Center         = new Vector2(0.5f, 0.5f);
            public static readonly Vector2 MiddleRight    = new Vector2(1, 0.5f);

            public static readonly Vector2 BottomLeft     = new Vector2(0, 1);
            public static readonly Vector2 BottomMiddle   = new Vector2(0.5f, 1);
            public static readonly Vector2 BottomRight    = new Vector2(1, 1);
        }
    }
}
