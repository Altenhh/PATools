﻿using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace StreamToolUI.Main.Screens
{
    public abstract class StreamScreen : Screen, IHasDescription
    {
        public string Description => "Test Screen";

        protected BackgroundScreen Background => backgroundStack?.CurrentScreen as BackgroundScreen;

        protected new StreamGameBase Game => base.Game as StreamGameBase;

        private BackgroundScreen localBackground;

        [Resolved(canBeNull: true)]
        private BackgroundScreenStack backgroundStack { get; set; }

        protected StreamScreen()
        {
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
        }

        public override void OnEntering(IScreen last)
        {
            backgroundStack?.Push(localBackground = CreateBackground());

            base.OnEntering(last);
        }

        public override bool OnExiting(IScreen next)
        {
            if (base.OnExiting(next))
                return true;

            if (localBackground != null && backgroundStack?.CurrentScreen == localBackground)
                backgroundStack?.Exit();

            return false;
        }

        /// <summary> Override to create a BackgroundMode for the current screen.
        /// Note that the instance created may not be the used instance if it matches the BackgroundMode equality clause.</summary>
        protected virtual BackgroundScreen CreateBackground() => null;
    }
}
