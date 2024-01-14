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
        public abstract class InvalidatableSurfaceRenderer : SurfaceRenderer
        {

            protected bool invalidated = false;

            public bool Invalidated => invalidated;

            public InvalidatableSurfaceRenderer(IMyTextSurface surface) : base(surface)
            {
            }

            public override void Render()
            {
                if (invalidated)
                    base.Render();
                invalidated = false;
            }

            public void Invalidate()
            {
                invalidated = true;
            }
        }
    }
}
