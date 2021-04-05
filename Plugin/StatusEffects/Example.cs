using ValheimLib;
using ValheimLib.ODB;

namespace ExampleMod.StatusEffect
{
    internal static class Example
    {
        internal static void Init()
        {
            ObjectDBHelper.OnAfterInit += AddMegingjordEffectToBronzeLegs;
        }

        private static void AddMegingjordEffectToBronzeLegs()
        {
            var armorBronzeLegsItemDrop = Prefab.Cache.GetPrefab<ItemDrop>("ArmorBronzeLegs");

            // Get the Megingjord status effect
            var megingjordStatusEffect = Prefab.Cache.GetPrefab<SE_Stats>("BeltStrength");

            armorBronzeLegsItemDrop.m_itemData.m_shared.m_equipStatusEffect = megingjordStatusEffect;
        }
    }
}
