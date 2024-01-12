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
        public abstract class SurfaceRenderer
        {
            #region # - Properties

            protected readonly IMyTextSurface surface;

            public IMyTextSurface Surface => surface;

            #endregion

            #region # - Constructor

            public SurfaceRenderer(IMyTextSurface surface)
            {
                this.surface = surface;
            }

            #endregion

            #region # - Methods

            public void Render()
            {
                using (Utensil utensil = new Utensil(surface))
                    Render(utensil);
            }

            protected abstract void Render(Utensil utensil);

            #endregion
        }
    }
}
