using BepInEx;
using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

[BepInPlugin(ModGUID, ModName, ModVersion)]
public class Plugin : BaseUnityPlugin
{
	public const string ModGUID = "zopthemop.nostaminatools";
	public const string ModName = "No Stamina Tools";
	public const string ModVersion = "1.0.0";
	public const string ModDescription = "Remove stamina cost of Hammer, Hoe and Cultivator";

    private void Awake()
    {
        Harmony harmony = new(ModGUID);
        harmony.PatchAll();
    }

	[HarmonyPatch(typeof(ObjectDB), "Awake")]
	public static class ObjectDB_Awake_Patch
	{
		private static void Postfix(ObjectDB __instance)
		{
			RemoveWeaponStamina(__instance, "Hammer");
			RemoveWeaponStamina(__instance, "Hoe");
			RemoveWeaponStamina(__instance, "Cultivator");
		}

		private static void RemoveWeaponStamina(ObjectDB db, string prefabName)
		{
			GameObject prefab = db.GetItemPrefab(prefabName);
			if (prefab == null) return;

			ItemDrop itemDrop = prefab.GetComponent<ItemDrop>();
			if (itemDrop == null) return;

			itemDrop.m_itemData.m_shared.m_attack.m_attackStamina = 0f;
		}
	}
}
