﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using StardewModdingAPI;
using MultiplayerMod;
using HarmonyLib;
using MultiplayerMod.Framework.Patch.Mobile;
using MultiplayerMod.Framework.Patch.Desktop;

namespace MultiplayerMod.Framework.Patch
{
    internal class PatchManager
    {
        public IModHelper Helper { get; set; }
        public IManifest Manifest { get; set; }
        public Config Config { get; set; }
        public List<IPatch> Patches { get; set; } = new List<IPatch>();
        public Harmony Harmony { get; }
        public PatchManager(IModHelper helper, IManifest manifest, Config config)
        {
            Helper = helper;
            Manifest = manifest;
            Config = config;
            Harmony = new Harmony(Manifest.UniqueID);

            if (Constants.TargetPlatform == GamePlatform.Android)
            {
                Patches.Add(new TitleMenuPatch());
                Patches.Add(new DebrisPatch());
                Patches.Add(new Game1Patch());
                Patches.Add(new IClickableMenuPatch());
                Patches.Add(new MobileCustomizerPatch());
                Patches.Add(new MobileFarmChooserPatch());
                Patches.Add(new SaveGamePatch());
                Patches.Add(new SMultiplayerPatch());
                Patches.Add(new GameLocationPatch());
                Patches.Add(new GameServerPatch());
                Patches.Add(new CarpenterMenuPatch());
                Patches.Add(new ShippingMenuPatch());
            }
            else
            {
                Patches.Add(new CoopMenuPatch());
                Patches.Add(new FarmerPatch());
            }

        }
        public void Apply()
        {
            foreach (var patch in Patches)
            {
                var p = patch;
                p.Apply(Harmony);
                ModUtilities.ModMonitor.Log($"Patch {p.GetType().Name} done...", LogLevel.Alert);
            }
        }
    }
}
