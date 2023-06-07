﻿using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.NPCs.Yharon;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace CalamityMod.Projectiles.Boss
{
    public class Infernado : ModProjectile, ILocalizedModType
    {
        public new string LocalizationCategory => "Projectiles.Boss";
        public override string Texture => "CalamityMod/Projectiles/Boss/Flarenado";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 12;
        }

        public override void SetDefaults()
        {
            Projectile.Calamity().DealsDefenseDamage = true;
            Projectile.width = 320;
            Projectile.height = 88;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 720;
            CooldownSlot = ImmunityCooldownID.Bosses;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(Projectile.localAI[0]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Projectile.localAI[0] = reader.ReadSingle();
        }

        public override void AI()
        {
            float scaleBase = 36f;
            float scaleMult = 1.5f;
            float baseWidth = 320f;
            float baseHeight = 88f;

            if (Projectile.velocity.X != 0f)
            {
                Projectile.direction = Projectile.spriteDirection = -Math.Sign(Projectile.velocity.X);
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame >= Main.projFrames[Projectile.type])
            {
                Projectile.frame = 0;
            }
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 1f;
                Projectile.scale = (scaleBase - Projectile.ai[1]) * scaleMult / scaleBase;
                Projectile.ExpandHitboxBy((int)(baseWidth * Projectile.scale), (int)(baseHeight * Projectile.scale));
                Projectile.netUpdate = true;
            }
            if (Projectile.ai[1] != -1f)
            {
                Projectile.scale = (scaleBase - Projectile.ai[1]) * scaleMult / scaleBase;
                Projectile.width = (int)(baseWidth * Projectile.scale);
                Projectile.height = (int)(baseHeight * Projectile.scale);
            }

            if (!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
            {
                Projectile.alpha -= 30;
                if (Projectile.alpha < 60)
                {
                    Projectile.alpha = 60;
                }
            }
            else
            {
                Projectile.alpha += 30;
                if (Projectile.alpha > 150)
                {
                    Projectile.alpha = 150;
                }
            }
            if (Projectile.ai[0] > 0f)
            {
                Projectile.ai[0] -= 1f;
            }

            if (Projectile.ai[0] == 1f && Projectile.ai[1] > 0f && Projectile.owner == Main.myPlayer)
            {
                Projectile.netUpdate = true;
                Vector2 center = Projectile.Center;
                center.Y -= baseHeight * Projectile.scale / 2f;
                float num618 = (scaleBase - Projectile.ai[1] + 1f) * scaleMult / scaleBase;
                center.Y -= baseHeight * num618 / 2f;
                center.Y += 2f;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), center, Projectile.velocity, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner, 11f, Projectile.ai[1] - 1f);
            }
            int tornadoSpeed = 10;
            int breakThreshold = -300;
            bool breakapart = CalamityWorld.getFixedBoi && Projectile.ai[0] <= breakThreshold;
            if (Projectile.ai[0] <= 0f && !breakapart)
            {
                float num622 = 0.104719758f;
                float num623 = (float)Projectile.width / 5f;
                num623 *= 2f;
                float num624 = (float)(Math.Cos((double)(num622 * -(double)Projectile.ai[0])) - 0.5) * num623;
                Projectile.position.X -= num624 * -Projectile.direction;
                Projectile.ai[0] -= 1f;
                num624 = (float)(Math.Cos((double)(num622 * -(double)Projectile.ai[0])) - 0.5) * num623;
                Projectile.position.X += num624 * -Projectile.direction;
            }
            if (Projectile.ai[0] == breakThreshold && CalamityWorld.getFixedBoi)
            {
                Projectile.velocity.X = Main.rand.NextBool(2) ? -tornadoSpeed : tornadoSpeed;
            }

            if (Projectile.timeLeft == 600)
                Projectile.damage = Projectile.GetProjectileDamage(ModContent.NPCType<Yharon>());
        }

        public override bool CanHitPlayer(Player target) => Projectile.timeLeft <= 600;

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 53, Projectile.alpha);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = ModContent.Request<Texture2D>(Texture).Value;
            int num214 = ModContent.Request<Texture2D>(Texture).Value.Height / Main.projFrames[Projectile.type];
            int y6 = num214 * Projectile.frame;
            Main.spriteBatch.Draw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, y6, texture2D13.Width, num214)), Projectile.GetAlpha(lightColor), Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)num214 / 2f), Projectile.scale, SpriteEffects.None, 0);
            return false;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (info.Damage <= 0)
                return;

            if (Projectile.timeLeft <= 600)
                target.AddBuff(ModContent.BuffType<Dragonfire>(), 300);
        }
    }
}
