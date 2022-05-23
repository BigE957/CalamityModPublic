﻿using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityMod.Items.Accessories
{
    public class MutatedTruffle : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            DisplayName.SetDefault("Mutated Truffle");
            Tooltip.SetDefault("Summons a small Old Duke to fight for you\n" +
                               "When below 50% life, it moves much faster");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 26;
            Item.value = CalamityGlobalItem.Rarity13BuyPrice;
            Item.Calamity().customRarity = CalamityRarity.PureGreen;
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.Calamity();
            modPlayer.miniOldDuke = true;
            if (player.whoAmI == Main.myPlayer)
            {
                var source = player.GetSource_Accessory(Item);
                if (player.FindBuffIndex(ModContent.BuffType<MutatedTruffleBuff>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<MutatedTruffleBuff>(), 3600, true);
                }
                if (player.ownedProjectileCounts[ModContent.ProjectileType<YoungDuke>()] < 1)
                {
                    const int baseDamage = 1200;
                    int damage = (int)player.GetDamage<SummonDamageClass>().ApplyTo(baseDamage);
                    var duke = Projectile.NewProjectileDirect(source, player.Center, Vector2.Zero,
                        ModContent.ProjectileType<YoungDuke>(),
                        damage,
                        6.5f, Main.myPlayer, 0f, 0f);

                    duke.originalDamage = baseDamage;
                }
            }
        }
    }
}
