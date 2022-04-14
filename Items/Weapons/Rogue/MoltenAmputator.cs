﻿using Terraria.DataStructures;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace CalamityMod.Items.Weapons.Rogue
{
    public class MoltenAmputator : RogueWeapon
    {
        public const float Speed = 21f;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Molten Amputator");
            Tooltip.SetDefault("Throws a scythe that emits molten globs on enemy hits\n" +
                "Stealth strikes spawn molten globs periodically in flight and more on-hit");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SafeSetDefaults()
        {
            Item.width = 60;
            Item.damage = 166;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 18;
            Item.knockBack = 9f;
            Item.UseSound = SoundID.Item1;
            Item.height = 60;
            Item.value = CalamityGlobalItem.Rarity12BuyPrice;
            Item.rare = ItemRarityID.Purple;
            Item.Calamity().customRarity = CalamityRarity.Turquoise;
            Item.shoot = ModContent.ProjectileType<MoltenAmputatorProj>();
            Item.shootSpeed = Speed;
            Item.Calamity().rogue = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.Calamity().StealthStrikeAvailable())
            {
                damage = (int)(damage * 1.07);
                int ss = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
                if (ss.WithinBounds(Main.maxProjectiles))
                    Main.projectile[ss].Calamity().stealthStrike = true;
                return false;
            }
            return true;
        }
    }
}
