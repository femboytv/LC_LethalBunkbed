﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;
using HarmonyLib;

namespace LethalBunkbed
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            var folders = Directory.GetDirectories(Paths.PluginPath, PluginInfo.PLUGIN_NAME, SearchOption.AllDirectories).ToList();
            
            foreach (var folder in folders)
            {
                foreach (var file in Directory.GetFiles(folder, "posters"))
                {
                    if (Path.GetExtension(file) != ".old")
                    {
                        TextureFiles.Add(file);
                    }
                }
            }
            
            Patches.Init(Logger);

            var harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll(typeof(Patches));
            
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
        
        public static readonly List<string> TextureFiles = new();
        public static Random Rand = new();
    }
}
