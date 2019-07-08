using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityMod.World;

namespace CalamityMod.Items.AstrumDeus
{
	public class AstrumDeusBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 24;
			item.height = 24;
			item.expert = true;
			item.rare = 9;
			bossBagNPC = mod.NPCType("AstrumDeusHeadSpectral");
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
			if (CalamityWorld.revenge)
			{
				if (Main.rand.Next(20) == 0)
				{
					switch (Main.rand.Next(3))
					{
						case 0:
							player.QuickSpawnItem(mod.ItemType("StressPills"));
							break;
						case 1:
							player.QuickSpawnItem(mod.ItemType("Laudanum"));
							break;
						case 2:
							player.QuickSpawnItem(mod.ItemType("HeartofDarkness"));
							break;
					}
				}
			}
			if (Main.rand.Next(40) == 0)
			{
				player.QuickSpawnItem(mod.ItemType("Quasar"));
			}
			if (Main.rand.Next(40) == 0)
			{
				player.QuickSpawnItem(mod.ItemType("HideofAstrumDeus"));
			}
			player.TryGettingDevArmor();
			player.QuickSpawnItem(mod.ItemType("Stardust"), Main.rand.Next(60, 91));
			if (Main.rand.Next(4) == 0)
			{
				player.QuickSpawnItem(mod.ItemType("Starfall"));
			}
			player.QuickSpawnItem(mod.ItemType("AstralBulwark"));
			if (Main.rand.Next(7) == 0)
			{
				player.QuickSpawnItem(mod.ItemType("AstrumDeusMask"));
			}
		}
	}
}