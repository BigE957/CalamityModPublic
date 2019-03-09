﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.NPCs.AstralBiomeNPCs
{
    public class AstralachneaWall : ModNPC
    {
        private static Texture2D glowmask;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astralachnea");

            Main.npcFrameCount[npc.type] = 4;

            if (!Main.dedServ)
                glowmask = mod.GetTexture("NPCs/AstralBiomeNPCs/AstralachneaWallGlow");

            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            npc.width = 60;
            npc.height = 60;
            npc.aiStyle = -1;
            npc.damage = 90;
            npc.defense = 30;
            npc.lifeMax = 750;
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/AstralEnemyDeath");
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.value = Item.buyPrice(0, 0, 20, 0);
            npc.buffImmune[20] = true;
            npc.buffImmune[31] = false;
            npc.timeLeft = NPC.activeTime * 2;
            animationType = NPCID.BlackRecluseWall;
			banner = mod.NPCType("AstralachneaGround");
			bannerItem = mod.ItemType("AstralachneaBanner");
		}

        public override void AI()
        {
            CalamityGlobalNPC.DoSpiderWallAI(npc, mod.NPCType("AstralachneaGround"), 2.4f, 0.1f);
        }

        public override void FindFrame(int frameHeight)
        {
            //DO DUST
            int frame = npc.frame.Y / frameHeight;
            Rectangle rect = new Rectangle(12, 24, 18, 10);
            Rectangle rect2 = new Rectangle(12, 44, 18, 10);
            switch (frame)
            {
                case 1:
                    rect = new Rectangle(6, 26, 28, 8);
                    rect2 = new Rectangle(6, 44, 28, 8);
                    break;
                case 2:
                    rect = new Rectangle(12, 26, 18, 8);
                    rect2 = new Rectangle(12, 44, 18, 8);
                    break;
                case 3:
                    rect = new Rectangle(16, 24, 16, 10);
                    rect2 = new Rectangle(16, 44, 16, 10);
                    break;
            }
            Dust d = CalamityGlobalNPC.SpawnDustOnNPC(npc, 80, frameHeight, mod.DustType("AstralOrange"), rect, Vector2.Zero, 0.225f, true);
            Dust d2 = CalamityGlobalNPC.SpawnDustOnNPC(npc, 80, frameHeight, mod.DustType("AstralOrange"), rect2, Vector2.Zero, 0.225f, true);
            if (d != null)
            {
                d.customData = 0.04f;
            }
            if (d2 != null)
            {
                d2.customData = 0.04f;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.soundDelay == 0)
            {
                npc.soundDelay = 15;
                switch (Main.rand.Next(3))
                {
                    case 0:
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/AstralEnemyHit"), npc.Center);
                        break;
                    case 1:
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/AstralEnemyHit2"), npc.Center);
                        break;
                    case 2:
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/AstralEnemyHit3"), npc.Center);
                        break;
                }
            }

            CalamityGlobalNPC.DoHitDust(npc, hitDirection, (Main.rand.Next(0, Math.Max(0, npc.life)) == 0) ? 5 : mod.DustType("AstralEnemy"), 1f, 4, 22);

            //if dead do gores
            if (npc.life <= 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    Gore.NewGore(npc.Center, npc.velocity * 0.3f, mod.GetGoreSlot("Gores/Astralachnea/AstralachneaGore" + i));
                }
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Vector2 origin = new Vector2(40f, 40f);
            spriteBatch.Draw(glowmask, npc.Center - Main.screenPosition - new Vector2(0, 8f), npc.frame, Color.White * 0.6f, npc.rotation, origin, 1f, SpriteEffects.None, 0);
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Stardust"), Main.rand.Next(2, 4));
            if (Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Stardust"));
            }
            if (CalamityWorld.downedStarGod && Main.rand.Next(7) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AstralachneaStaff"));
            }
        }
    }
}
