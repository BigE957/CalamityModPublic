﻿using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.NormalNPCs
{
    public class WulfrumGyrator : ModNPC
    {
        public float TimeSpentStuck
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }

        public float SuperchargeTimer
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }

        public bool Supercharged => SuperchargeTimer > 0;

        public const float PlayerTargetingThreshold = 90f;
        public const float PlayerSearchDistance = 500f;
        public const float StuckJumpPromptTime = 45f;
        public const float MaxMovementSpeedX = 6f;
        public const float JumpSpeed = -8f;
        public const float NPCGravity = 0.3f; // NPC.cs has this, but it's private, and there's no way in hell I'm using reflection.

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wulfrum Gyrator");
            Main.npcFrameCount[NPC.type] = 10;
        }

        public override void SetDefaults()
        {
            AIType = -1;
            NPC.aiStyle = -1;
            NPC.damage = 15;
            NPC.width = 40;
            NPC.height = 40;
            NPC.defense = 5;
            NPC.lifeMax = 18;
            NPC.knockBackResist = 0.15f;
            NPC.value = Item.buyPrice(0, 0, 1, 15);
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<WulfrumGyratorBanner>();
            NPC.Calamity().VulnerableToSickness = false;
            NPC.Calamity().VulnerableToElectricity = true;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            int frame = (int)(NPC.frameCounter / 5) % (Main.npcFrameCount[NPC.type] / 2);
            if (Supercharged)
                frame += Main.npcFrameCount[NPC.type] / 2;

            NPC.frame.Y = frame * frameHeight;
        }

        public override void AI()
        {
            NPC.TargetClosest(false);

            Player player = Main.player[NPC.target];

            if (Supercharged)
            {
                float chargeJumpSpeed = -12f;
                float maxHeight = chargeJumpSpeed * chargeJumpSpeed * (float)Math.Pow(Math.Sin(NPC.AngleTo(player.Center)), 2) / (4f * NPCGravity);
                bool jumpWouldHitPlayer = maxHeight > Math.Abs(player.Center.Y - NPC.Center.Y) && maxHeight < Math.Abs(player.Center.Y - NPC.Center.Y) + player.height;

                if (Main.netMode != NetmodeID.MultiplayerClient && jumpWouldHitPlayer && NPC.collideY && NPC.velocity.Y == 0f)
                {
                    NPC.velocity.Y = chargeJumpSpeed;
                    NPC.netSpam = 0;
                    NPC.netUpdate = true;
                }

                SuperchargeTimer--;
            }

            // Jump if there's an obstacle ahead.
            if (Main.netMode != NetmodeID.MultiplayerClient && HoleAtPosition(NPC.Center.X + NPC.velocity.X * 4f) && NPC.collideY && NPC.velocity.Y == 0f)
            {
                NPC.velocity.Y = JumpSpeed;
                NPC.netSpam = 0;
                NPC.netUpdate = true;
            }

            if (Collision.CanHitLine(player.position, player.width, player.height, NPC.position, NPC.width, NPC.height) &&
                Math.Abs(player.Center.X - NPC.Center.X) < PlayerSearchDistance &&
                Math.Abs(player.Center.X - NPC.Center.X) > PlayerTargetingThreshold)
            {
                int direction = Math.Sign(player.Center.X - NPC.Center.X) * (NPC.confused ? -1 : 1);
                if (NPC.direction != direction)
                {
                    NPC.direction = direction;
                    NPC.netSpam = 0;
                    NPC.netUpdate = true;
                }
            }
            else if (Main.netMode != NetmodeID.MultiplayerClient && NPC.collideX && NPC.collideY && NPC.velocity.Y == 0f)
            {
                NPC.velocity.Y = JumpSpeed;
                NPC.netSpam = 0;
                NPC.netUpdate = true;
            }

            if (NPC.oldPosition == NPC.position)
            {
                TimeSpentStuck++;
                if (Main.netMode != NetmodeID.MultiplayerClient && TimeSpentStuck > StuckJumpPromptTime)
                {
                    NPC.velocity.Y = JumpSpeed;
                    TimeSpentStuck = 0f;
                    NPC.netUpdate = true;
                }
            }
            else
                TimeSpentStuck = 0f;

            NPC.velocity.X = MathHelper.Lerp(NPC.velocity.X, MaxMovementSpeedX * NPC.direction * (Supercharged ? 1.25f : 1f), Supercharged ? 0.02f : 0.0125f);
            Vector4 adjustedVectors = Collision.WalkDownSlope(NPC.position, NPC.velocity, NPC.width, NPC.height, NPCGravity);
            NPC.position = adjustedVectors.XY();
            NPC.velocity = adjustedVectors.ZW();
        }

        private bool HoleAtPosition(float xPosition)
        {
            int tileWidth = NPC.width / 16;
            xPosition = (int)(xPosition / 16f) - tileWidth;
            if (NPC.velocity.X > 0)
                xPosition += tileWidth;

            int tileY = (int)((NPC.position.Y + NPC.height) / 16f);
            for (int y = tileY; y < tileY + 2; y++)
            {
                for (int x = (int)xPosition; x < xPosition + tileWidth; x++)
                {
                    if (Main.tile[x, y].HasTile)
                        return false;
                }
            }

            return true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float pylonMult = NPC.AnyNPCs(ModContent.NPCType<WulfrumPylon>()) ? 5.5f : 1f;
            if (spawnInfo.playerSafe || spawnInfo.Player.Calamity().ZoneSulphur)
                return 0f;
            return SpawnCondition.OverworldDaySlime.Chance * (Main.hardMode ? 0.020f : 0.115f) * pylonMult;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (!Main.dedServ)
            {
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 3, hitDirection, -1f, 0, default, 1f);
                }
                if (NPC.life <= 0)
                {
                    for (int k = 0; k < 20; k++)
                    {
                        Dust.NewDust(NPC.position, NPC.width, NPC.height, 3, hitDirection, -1f, 0, default, 1f);
                    }
                }
            }
        }

        public override void NPCLoot()
        {
            DropHelper.DropItem(NPC, ModContent.ItemType<WulfrumShard>(), 1, 2);
            DropHelper.DropItemCondition(NPC, ModContent.ItemType<EnergyCore>(), Supercharged);
            DropHelper.DropItemChance(NPC, ModContent.ItemType<WulfrumBattery>(), 0.07f);
        }
    }
}
