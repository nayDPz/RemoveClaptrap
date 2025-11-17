using BepInEx;
using RoR2;
using System;
using System.Collections;
using UnityEngine;

namespace RemoveClaptrap
{    
    [BepInPlugin("com.claptraphater.RemoveClaptrap", "REMOVE CLAPTRAP", "1.2.0")]
    public class RemoveClaptrap : BaseUnityPlugin
	{
        public static string[] thingsThatMightBeClaptrap = new string[]
        {
            "claptrap",
            "cl4p-tp",
            "clap-trap",
            "cl4p-tr4p",
            "cl4ptp",
            "claptp",
            "clap-tp",
            "clapbot",
            "trapbot",
            "clapchef"
        };

        public static int claptrapsFound = 0;

        public void Awake()
        {
            Debug.LogWarning("LETS SEE YOU GET PAST THIS, FUCKER");
            On.RoR2.SurvivorCatalog.SetSurvivorDefs += SurvivorCatalog_SetSurvivorDefs;
            On.RoR2.BodyCatalog.SetBodyPrefabs += BodyCatalog_SetBodyPrefabs;
            On.RoR2.MasterCatalog.SetEntries += MasterCatalog_SetEntries;
            On.RoR2.ArtifactCatalog.SetArtifactDefs += ArtifactCatalog_SetArtifactDefs;
            On.RoR2.BuffCatalog.SetBuffDefs += BuffCatalog_SetBuffDefs;
            On.RoR2.EffectCatalog.SetEntries += EffectCatalog_SetEntries;
            On.RoR2.EliteCatalog.SetEliteDefs += EliteCatalog_SetEliteDefs;
            On.RoR2.EntityStateCatalog.SetElements += EntityStateCatalog_SetElements;
            On.RoR2.EquipmentCatalog.SetEquipmentDefs += EquipmentCatalog_SetEquipmentDefs;
            On.RoR2.GameEndingCatalog.SetGameEndingDefs += GameEndingCatalog_SetGameEndingDefs;
            On.RoR2.GameModeCatalog.SetGameModes += GameModeCatalog_SetGameModes;
            On.RoR2.ItemCatalog.SetItemDefs += ItemCatalog_SetItemDefs;
            On.RoR2.MusicTrackCatalog.SetEntries += MusicTrackCatalog_SetEntries;
            On.RoR2.PickupCatalog.SetEntries += PickupCatalog_SetEntries;
            On.RoR2.ProjectileCatalog.SetProjectilePrefabs += ProjectileCatalog_SetProjectilePrefabs;
            On.RoR2.SceneCatalog.SetSceneDefs += SceneCatalog_SetSceneDefs;
            On.RoR2.SurfaceDefCatalog.SetSurfaceDefs += SurfaceDefCatalog_SetSurfaceDefs;
            On.RoR2.UnlockableCatalog.SetUnlockableDefs += UnlockableCatalog_SetUnlockableDefs;
            On.RoR2.SkinCatalog.FindSkinsForBody += SkinCatalog_FindSkinsForBody;

            RoR2Application.onLoad += () =>
            {
                if (claptrapsFound == 0)
                    Debug.LogWarning("Hunt completed. No Claptraps found.");
                else
                    Debug.LogError($"Hunt completed. {claptrapsFound} Claptraps eliminated.");
            };
        }

        private SkinDef[] SkinCatalog_FindSkinsForBody(On.RoR2.SkinCatalog.orig_FindSkinsForBody orig, BodyIndex bodyIndex)
        {
            var newEntries = orig(bodyIndex);

            int c = newEntries.Length;
            for (int i = 0; i < c; i++)
            {
                foreach (string fucker in thingsThatMightBeClaptrap)
                    if (newEntries[i].nameToken.ToLower().Contains(fucker))
                    {
                        Debug.LogError("CLAPTRAP IDENTIFIED. PURGING.");
                        HG.ArrayUtils.ArrayRemoveAtAndResize(ref newEntries, i);
                        HG.ArrayUtils.ArrayRemoveAtAndResize(ref BodyCatalog.bodyPrefabs[(int)bodyIndex].GetComponent<ModelLocator>().modelTransform.GetComponent<ModelSkinController>().skins, i);
                        HG.ArrayUtils.ArrayRemoveAtAndResize(ref BodyCatalog.skins[(int)bodyIndex], i);
                        claptrapsFound++;
                    }

            }
            return newEntries;
        }

        private void UnlockableCatalog_SetUnlockableDefs(On.RoR2.UnlockableCatalog.orig_SetUnlockableDefs orig, UnlockableDef[] newEntries)
        {
            int c = newEntries.Length;
            for (int i = 0; i < c; i++)
            {
                foreach (string fucker in thingsThatMightBeClaptrap)
                    if (newEntries[i].nameToken.ToLower().Contains(fucker))
                    {
                        Debug.LogError("CLAPTRAP IDENTIFIED. PURGING.");
                        HG.ArrayUtils.ArrayRemoveAtAndResize(ref newEntries, i);                       
                        claptrapsFound++;
                    }
                        
            }
            orig(newEntries);
        }
        private void SurfaceDefCatalog_SetSurfaceDefs(On.RoR2.SurfaceDefCatalog.orig_SetSurfaceDefs orig, SurfaceDef[] newEntries)
        {
            int c = newEntries.Length;
            for (int i = 0; i < c; i++)
            {
                foreach(string fucker in thingsThatMightBeClaptrap)
                    if (newEntries[i].name.ToLower().Contains(fucker))
                    {
                        Debug.LogError("CLAPTRAP IDENTIFIED. PURGING.");
                        HG.ArrayUtils.ArrayRemoveAtAndResize(ref newEntries, i);
                        claptrapsFound++;
                    }
            }
            orig(newEntries);
        }
        private IEnumerator SceneCatalog_SetSceneDefs(On.RoR2.SceneCatalog.orig_SetSceneDefs orig, SceneDef[] newEntries)
        {
            int c = newEntries.Length;
            for (int i = 0; i < c; i++)
            {
                foreach (string fucker in thingsThatMightBeClaptrap)
                    if (newEntries[i].nameToken.ToLower().Contains(fucker))
                    {
                        Debug.LogError("CLAPTRAP IDENTIFIED. PURGING.");
                        HG.ArrayUtils.ArrayRemoveAtAndResize(ref newEntries, i);
                        claptrapsFound++;
                    }
            }
            yield return orig(newEntries);
        }
        private void ProjectileCatalog_SetProjectilePrefabs(On.RoR2.ProjectileCatalog.orig_SetProjectilePrefabs orig, GameObject[] newEntries)
        {
            int c = newEntries.Length;
            for (int i = 0; i < c; i++)
            {
                foreach (string fucker in thingsThatMightBeClaptrap)
                    if (newEntries[i].name.ToLower().Contains(fucker))
                    {
                        Debug.LogError("CLAPTRAP IDENTIFIED. PURGING.");
                        HG.ArrayUtils.ArrayRemoveAtAndResize(ref newEntries, i);
                        claptrapsFound++;
                    }
            }
            orig(newEntries);
        }
        private void PickupCatalog_SetEntries(On.RoR2.PickupCatalog.orig_SetEntries orig, PickupDef[] newEntries)
        {
            int c = newEntries.Length;
            for (int i = 0; i < c; i++)
            {
                foreach (string fucker in thingsThatMightBeClaptrap)
                    if (newEntries[i].nameToken.ToLower().Contains(fucker))
                    {
                        Debug.LogError("CLAPTRAP IDENTIFIED. PURGING.");
                        HG.ArrayUtils.ArrayRemoveAtAndResize(ref newEntries, i);
                        claptrapsFound++;
                    }
            }
            orig(newEntries);
        }
        private void MusicTrackCatalog_SetEntries(On.RoR2.MusicTrackCatalog.orig_SetEntries orig, MusicTrackDef[] newEntries)
        {
            int c = newEntries.Length;
            for (int i = 0; i < c; i++)
            {
                foreach (string fucker in thingsThatMightBeClaptrap)
                    if (newEntries[i].cachedName.ToLower().Contains(fucker))
                    {
                        Debug.LogError("CLAPTRAP IDENTIFIED. PURGING.");
                        HG.ArrayUtils.ArrayRemoveAtAndResize(ref newEntries, i);
                        claptrapsFound++;
                    }
            }
            orig(newEntries);
        }
        private void ItemCatalog_SetItemDefs(On.RoR2.ItemCatalog.orig_SetItemDefs orig, ItemDef[] newEntries)
        {
            int c = newEntries.Length;
            for (int i = 0; i < c; i++)
            {
                foreach (string fucker in thingsThatMightBeClaptrap)
                    if (newEntries[i].nameToken.ToLower().Contains(fucker))
                    {
                        Debug.LogError("CLAPTRAP IDENTIFIED. PURGING.");
                        HG.ArrayUtils.ArrayRemoveAtAndResize(ref newEntries, i);
                        claptrapsFound++;
                    }
            }
            orig(newEntries);
        }
        private void GameModeCatalog_SetGameModes(On.RoR2.GameModeCatalog.orig_SetGameModes orig, Run[] newEntries)
        {
            int c = newEntries.Length;
            for (int i = 0; i < c; i++)
            {
                foreach (string fucker in thingsThatMightBeClaptrap)
                    if (newEntries[i].nameToken.ToLower().Contains(fucker))
                    {
                        Debug.LogError("CLAPTRAP IDENTIFIED. PURGING.");
                        HG.ArrayUtils.ArrayRemoveAtAndResize(ref newEntries, i);
                        claptrapsFound++;
                    }
            }
            orig(newEntries);
        }
        private void GameEndingCatalog_SetGameEndingDefs(On.RoR2.GameEndingCatalog.orig_SetGameEndingDefs orig, GameEndingDef[] newEntries)
        {
            int c = newEntries.Length;
            for (int i = 0; i < c; i++)
            {
                foreach (string fucker in thingsThatMightBeClaptrap)
                    if (newEntries[i].cachedName.ToLower().Contains(fucker))
                    {
                        Debug.LogError("CLAPTRAP IDENTIFIED. PURGING.");
                        HG.ArrayUtils.ArrayRemoveAtAndResize(ref newEntries, i);
                        claptrapsFound++;
                    }
            }
            orig(newEntries);
        }
        private void EquipmentCatalog_SetEquipmentDefs(On.RoR2.EquipmentCatalog.orig_SetEquipmentDefs orig, EquipmentDef[] newEntries)
        {
            int c = newEntries.Length;
            for (int i = 0; i < c; i++)
            {
                foreach (string fucker in thingsThatMightBeClaptrap)
                    if (newEntries[i].name.ToLower().Contains(fucker))
                    {
                        Debug.LogError("CLAPTRAP IDENTIFIED. PURGING.");
                        HG.ArrayUtils.ArrayRemoveAtAndResize(ref newEntries, i);
                        claptrapsFound++;
                    }
            }
            orig(newEntries);
        }
        private IEnumerator EntityStateCatalog_SetElements(On.RoR2.EntityStateCatalog.orig_SetElements orig, Type[] newEntries1, EntityStateConfiguration[] newEntries2)
        {
            int c1 = newEntries2.Length;
            for (int i = 0; i < c1; i++)
            {
                foreach (string fucker in thingsThatMightBeClaptrap)
                    if (newEntries1[i].Name.ToLower().Contains(fucker))
                    {
                        Debug.LogError("CLAPTRAP IDENTIFIED. PURGING.");
                        HG.ArrayUtils.ArrayRemoveAtAndResize(ref newEntries1, i);
                        claptrapsFound++;
                    }
            }

            int c2 = newEntries2.Length;
            for (int i = 0; i < c2; i++)
            {
                foreach (string fucker in thingsThatMightBeClaptrap)
                    if (newEntries2[i].name.ToLower().Contains(fucker))
                    {
                        Debug.LogError("CLAPTRAP IDENTIFIED. PURGING.");
                        HG.ArrayUtils.ArrayRemoveAtAndResize(ref newEntries2, i);
                        claptrapsFound++;
                    }
            }
            yield return orig(newEntries1, newEntries2);
        }
        private void EliteCatalog_SetEliteDefs(On.RoR2.EliteCatalog.orig_SetEliteDefs orig, EliteDef[] newEntries)
        {
            int c = newEntries.Length;
            for (int i = 0; i < c; i++)
            {
                foreach (string fucker in thingsThatMightBeClaptrap)
                    if (newEntries[i].name.ToLower().Contains(fucker))
                    {
                        Debug.LogError("CLAPTRAP IDENTIFIED. PURGING.");
                        HG.ArrayUtils.ArrayRemoveAtAndResize(ref newEntries, i);
                        claptrapsFound++;
                    }
            }
            orig(newEntries);
        }
        private void EffectCatalog_SetEntries(On.RoR2.EffectCatalog.orig_SetEntries orig, EffectDef[] newEntries)
        {
            int c = newEntries.Length;
            for (int i = 0; i < c; i++)
            {
                foreach (string fucker in thingsThatMightBeClaptrap)
                    if (newEntries[i].prefabName.ToLower().Contains(fucker))
                    {
                        Debug.LogError("CLAPTRAP IDENTIFIED. PURGING.");
                        HG.ArrayUtils.ArrayRemoveAtAndResize(ref newEntries, i);
                        claptrapsFound++;
                    }
            }
            orig(newEntries);
        }
        private void BuffCatalog_SetBuffDefs(On.RoR2.BuffCatalog.orig_SetBuffDefs orig, BuffDef[] newEntries)
        {
            int c = newEntries.Length;
            for (int i = 0; i < c; i++)
            {
                foreach (string fucker in thingsThatMightBeClaptrap)
                    if (newEntries[i].name.ToLower().Contains(fucker))
                    {
                        Debug.LogError("CLAPTRAP IDENTIFIED. PURGING.");
                        HG.ArrayUtils.ArrayRemoveAtAndResize(ref newEntries, i);
                        claptrapsFound++;
                    }
            }
            orig(newEntries);
        }
        private void ArtifactCatalog_SetArtifactDefs(On.RoR2.ArtifactCatalog.orig_SetArtifactDefs orig, ArtifactDef[] newEntries)
        {
            int c = newEntries.Length;
            for (int i = 0; i < c; i++)
            {
                foreach (string fucker in thingsThatMightBeClaptrap)
                    if (newEntries[i].nameToken.ToLower().Contains(fucker))
                    {
                        Debug.LogError("CLAPTRAP IDENTIFIED. PURGING.");
                        HG.ArrayUtils.ArrayRemoveAtAndResize(ref newEntries, i);
                        claptrapsFound++;
                    }
            }
            orig(newEntries);
        }
        private void MasterCatalog_SetEntries(On.RoR2.MasterCatalog.orig_SetEntries orig, UnityEngine.GameObject[] newEntries)
        {
            int c = newEntries.Length;
            for (int i = 0; i < c; i++)
            {
                foreach (string fucker in thingsThatMightBeClaptrap)
                    if (newEntries[i].name.ToLower().Contains(fucker))
                    {
                        Debug.LogError("CLAPTRAP IDENTIFIED. PURGING.");
                        HG.ArrayUtils.ArrayRemoveAtAndResize(ref newEntries, i);
                        claptrapsFound++;
                    }
            }
            orig(newEntries);
        }
        private void BodyCatalog_SetBodyPrefabs(On.RoR2.BodyCatalog.orig_SetBodyPrefabs orig, UnityEngine.GameObject[] newEntries)
        {
            int c = newEntries.Length;
            for (int i = 0; i < c; i++)
            {
                foreach (string fucker in thingsThatMightBeClaptrap)
                    if (newEntries[i].name.ToLower().Contains(fucker))
                    {
                        Debug.LogError("CLAPTRAP IDENTIFIED. PURGING.");
                        HG.ArrayUtils.ArrayRemoveAtAndResize(ref newEntries, i);
                        claptrapsFound++;
                    }
            }
            orig(newEntries);
        }
        private void SurvivorCatalog_SetSurvivorDefs(On.RoR2.SurvivorCatalog.orig_SetSurvivorDefs orig, SurvivorDef[] newEntries)
        {
            int c = newEntries.Length;
            for (int i = 0; i < c; i++)
            {
                foreach (string fucker in thingsThatMightBeClaptrap)
                    if (newEntries[i].cachedName.ToLower().Contains(fucker) || newEntries[i].displayNameToken.ToLower().Contains(fucker))
                    {
                        Debug.LogError("CLAPTRAP IDENTIFIED. PURGING.");
                        HG.ArrayUtils.ArrayRemoveAtAndResize(ref newEntries, i);
                        claptrapsFound++;
                    }
            }
            orig(newEntries);
        }
    }
}
