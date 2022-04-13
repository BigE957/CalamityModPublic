﻿using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.CeaselessVoid;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace CalamityMod.Items.TreasureBags
{
    public class CeaselessVoidBag : ModItem
    {
        public override int BossBagNPC => ModContent.NPCType<CeaselessVoid>();

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag (Ceaseless Void)");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 24;
            Item.rare = ItemRarityID.Cyan;
            Item.expert = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }

        public override bool CanRightClick() => true;

        public override void OpenBossBag(Player player)
        {
            // IEntitySource my beloathed
            var s = player.GetItemSource_OpenItem(Item.type);

            player.TryGettingDevArmor(s);

            // Materials
            DropHelper.DropItem(s, player, ModContent.ItemType<DarkPlasma>(), 4, 6);

            // Weapons
            DropHelper.DropItemChance(s, player, ModContent.ItemType<MirrorBlade>(), DropHelper.BagWeaponDropRateInt);
            DropHelper.DropItemChance(s, player, ModContent.ItemType<VoidConcentrationStaff>(), DropHelper.BagWeaponDropRateInt);

            // Equipment
            DropHelper.DropItem(s, player, ModContent.ItemType<TheEvolution>());

            // Vanity
            DropHelper.DropItemChance(s, player, ModContent.ItemType<CeaselessVoidMask>(), 7);
            if (Main.rand.NextBool(20))
            {
                DropHelper.DropItem(s, player, ModContent.ItemType<AncientGodSlayerHelm>());
                DropHelper.DropItem(s, player, ModContent.ItemType<AncientGodSlayerChestplate>());
                DropHelper.DropItem(s, player, ModContent.ItemType<AncientGodSlayerLeggings>());
            }
        }
    }
}
