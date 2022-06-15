﻿using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables;
using CalamityMod.Items.Weapons.Rogue;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Sulphurous
{
    [AutoloadEquip(EquipType.Body)]
    [LegacyName("SulfurBreastplate")]
    public class SulphurousBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            DisplayName.SetDefault("Sulphurous Breastplate");
            Tooltip.SetDefault("8% increased rogue damage and 5% increased rogue critical strike chance");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 20;
            Item.value = Item.buyPrice(0, 1, 15, 0);
            Item.defense = 6;
            Item.rare = ItemRarityID.Green;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage<ThrowingDamageClass>() += 0.08f;
            player.GetCritChance<ThrowingDamageClass>() += 5;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<UrchinStinger>(50).
                AddIngredient<Acidwood>(20).
                AddIngredient<SulphurousSand>(20).
                AddIngredient<SulphuricScale>(20).
                AddTile(TileID.Anvils).
                Register();
        }
    }
}