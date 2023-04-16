﻿using CalamityMod.Items.Placeables.PlaceableTurrets;
using CalamityMod.TileEntities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.PlayerTurrets
{
    public class PlayerWaterTurret : ModTile
    {
        public const int Width = 3;
        public const int Height = 2;
        public const int OriginOffsetX = 1;
        public const int OriginOffsetY = 1;
        public const int SheetSquare = 18;

        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileWaterDeath[Type] = false;

            // No need to set width, height, origin, etc. here, Style3x2 is exactly what we want.
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.LavaDeath = false;

            // When this tile is placed, it places the Draedon Lab Turret tile entity.
            ModTileEntity te = ModContent.GetInstance<TEPlayerWaterTurret>();
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(te.Hook_AfterPlacement, -1, 0, true);

            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Water Turret");
            AddMapEntry(new Color(67, 72, 81), name);
            HitSound = SoundID.Item14;

            // Has 500% durability.
            MineResist = 5f;
        }

        public override bool CanExplode(int i, int j) => false;

        public override bool CreateDust(int i, int j, ref int type)
        {
            Dust.NewDust(new Vector2(i, j) * 16f, 16, 16, 226);
            return false;
        }

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<WaterTurret>(), 1);

            Tile t = Main.tile[i, j];
            int left = i - t.TileFrameX % (Width * SheetSquare) / SheetSquare;
            int top = j - t.TileFrameY % (Height * SheetSquare) / SheetSquare;

            TEPlayerWaterTurret te = CalamityUtils.FindTileEntity<TEPlayerWaterTurret>(i, j, Width, Height, SheetSquare);
            te?.Kill(left, top);
        }

        // The turret tile draws a pulse turret on top of itself.
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile t = Main.tile[i, j];
            if (t.TileFrameX != 36 || t.TileFrameY != 0)
                return;

            TEPlayerWaterTurret te = CalamityUtils.FindTileEntity<TEPlayerWaterTurret>(i, j, Width, Height, SheetSquare);
            if (te is null)
                return;
            int drawDirection = te.Direction;
            Color drawColor = Lighting.GetColor(i, j);

            Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Tiles/PlayerTurrets/WaterTurretHead").Value;
            Vector2 screenOffset = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
            Vector2 drawOffset = new Vector2(i * 16 - Main.screenPosition.X, j * 16 - Main.screenPosition.Y) + screenOffset;
            drawOffset.Y -= 2f;
            drawOffset.X += (drawDirection == -1 ? -10f : 2f) -2f;

            SpriteEffects sfx = drawDirection == -1 ? SpriteEffects.FlipVertically : SpriteEffects.None;
            spriteBatch.Draw(tex, drawOffset, null, drawColor, te.Angle, tex.Size() * 0.5f, 1f, sfx, 0.0f);
        }
    }
}