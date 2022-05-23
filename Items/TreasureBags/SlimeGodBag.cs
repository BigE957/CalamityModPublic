﻿using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.Materials;
using CalamityMod.Items.PermanentBoosters;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.SlimeGod;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.TreasureBags
{
    public class SlimeGodBag : ModItem
    {
        public override int BossBagNPC => ModContent.NPCType<SlimeGodRun>();

        public override void SetStaticDefaults()
        {
            SacrificeTotal = 3;
            DisplayName.SetDefault("Treasure Bag (The Slime God)");
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
        }

        public override bool CanRightClick() => true;

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            return CalamityUtils.DrawTreasureBagInWorld(Item, spriteBatch, ref rotation, ref scale, whoAmI);
        }

        public override void OpenBossBag(Player player)
        {
            // IEntitySource my beloathed
            var s = player.GetSource_OpenItem(Item.type);

            // Materials
            // No Gel is dropped here because the boss drops Gel directly
            DropHelper.DropItem(s, player, ModContent.ItemType<PurifiedGel>(), 40, 52);

            // Weapons
            float w = DropHelper.BagWeaponDropRateFloat;
            DropHelper.DropEntireWeightedSet(s, player,
                DropHelper.WeightStack<OverloadedBlaster>(w),
                DropHelper.WeightStack<AbyssalTome>(w),
                DropHelper.WeightStack<EldritchTome>(w),
                DropHelper.WeightStack<CorroslimeStaff>(w),
                DropHelper.WeightStack<CrimslimeStaff>(w),
                DropHelper.WeightStack<SlimePuppetStaff>(w)
            );

            // Equipment
            DropHelper.DropItem(s, player, ModContent.ItemType<ManaOverloader>());
            DropHelper.DropItemCondition(s, player, ModContent.ItemType<ElectrolyteGelPack>(), CalamityWorld.revenge && !player.Calamity().adrenalineBoostOne);

            // Vanity
            DropHelper.DropItemFromSetChance(s, player, 0.142857f, ModContent.ItemType<SlimeGodMask>(), ModContent.ItemType<SlimeGodMask2>());
        }
    }
}
