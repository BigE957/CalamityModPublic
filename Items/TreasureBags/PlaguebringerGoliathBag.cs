﻿using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.PlaguebringerGoliath;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace CalamityMod.Items.TreasureBags
{
    public class PlaguebringerGoliathBag : ModItem
    {
        public override int BossBagNPC => ModContent.NPCType<PlaguebringerGoliath>();

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag (The Plaguebringer Goliath)");
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
            DropHelper.DropItem(s, player, ModContent.ItemType<PlagueCellCluster>(), 13, 17);
            DropHelper.DropItem(s, player, ModContent.ItemType<InfectedArmorPlating>(), 16, 20);
            DropHelper.DropItem(s, player, ItemID.Stinger, 4, 8);

            // Weapons
            float w = DropHelper.BagWeaponDropRateFloat;
            DropHelper.DropEntireWeightedSet(s, player,
                DropHelper.WeightStack<VirulentKatana>(w), // Virulence
                DropHelper.WeightStack<DiseasedPike>(w),
                DropHelper.WeightStack<ThePlaguebringer>(w), // Pandemic
                DropHelper.WeightStack<Malevolence>(w),
                DropHelper.WeightStack<PestilentDefiler>(w),
                DropHelper.WeightStack<TheHive>(w),
                DropHelper.WeightStack<MepheticSprayer>(w), // Blight Spewer
                DropHelper.WeightStack<PlagueStaff>(w),
                DropHelper.WeightStack<FuelCellBundle>(w),
                DropHelper.WeightStack<InfectedRemote>(w),
                DropHelper.WeightStack<TheSyringe>(w)
            );

            // Equipment
            DropHelper.DropItem(s, player, ModContent.ItemType<ToxicHeart>());
            DropHelper.DropItemChance(s, player, ModContent.ItemType<Malachite>(), 0.1f);

            // Vanity
            DropHelper.DropItemChance(s, player, ModContent.ItemType<PlaguebringerGoliathMask>(), 7);
            DropHelper.DropItemChance(s, player, ModContent.ItemType<PlagueCaller>(), 10);
        }
    }
}
