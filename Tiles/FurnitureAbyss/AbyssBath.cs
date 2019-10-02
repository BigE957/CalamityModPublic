using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.FurnitureAbyss
{
    public class AbyssBath : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			Main.tileWaterDeath[Type] = false;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
          	AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
			name.SetDefault("Abyss Bathtub");
			AddMapEntry(new Color(191, 142, 111), name);
			animationFrameHeight = 54;
		}

		public override bool CreateDust(int i, int j, ref int type)
		{
			Dust.NewDust(new Vector2(i, j) * 16f, 16, 16, 1, 0f, 0f, 1, new Color(100, 130, 150), 1f);
			return false;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 16, 32, mod.ItemType("AbyssBath"));
		}
	}
}
