﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using CalamityMod.Particles;

namespace CalamityMod.Projectiles.Ranged
{
    public class IcicleArrowProj : ModProjectile, ILocalizedModType
    {
        public new string LocalizationCategory => "Projectiles.Ranged";
        public override string Texture => "CalamityMod/Items/Ammo/IcicleArrow";
        public bool falling = false;
        public Vector2 startVelocity;

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.arrow = true;
            Projectile.coldDamage = true;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 8;
            Projectile.timeLeft = 1200;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10 * Projectile.extraUpdates;
            Projectile.Calamity().pointBlankShotDuration = CalamityGlobalProjectile.DefaultPointBlankDuration;
        }

        public override void AI()
        {
            Player Owner = Main.player[Projectile.owner];
            float targetDist = Vector2.Distance(Owner.Center, Projectile.Center);

            if (Projectile.localAI[0] == 0f)
            {
                startVelocity = Projectile.velocity;
            }
            Projectile.localAI[0] += 1f;
            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;

            if (!falling)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center - Projectile.velocity * 2, Main.rand.NextBool(3) ? 135 : 279, -Projectile.velocity.RotatedByRandom(0.5f) * Main.rand.NextFloat(0.05f, 0.4f) - new Vector2(0, 1));
                dust.scale = Main.rand.NextFloat(0.35f, 0.6f);
                dust.noGravity = false;

                Projectile.velocity *= 0.99f;
                Projectile.alpha += 1;
                if (Projectile.localAI[0] == 250)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        Dust dust2 = Dust.NewDustPerfect(Projectile.Center, Main.rand.NextBool(3) ? 135 : 279, new Vector2(3, 3).RotatedByRandom(100) * Main.rand.NextFloat(0.05f, 0.8f));
                        dust2.scale = Main.rand.NextFloat(1.2f, 2.2f);
                        dust2.noGravity = true;
                    }
                    Projectile.alpha = 255;
                    Projectile.velocity = new Vector2(0, 0.3f);
                    Projectile.penetrate = 2;
                    Projectile.ExpandHitboxBy(50);

                    falling = true;
                }
            }
            else
            {
                if (targetDist < 1400f)
                {
                    SparkParticle Visual = new SparkParticle(Projectile.Center, Projectile.velocity * 0.1f, false, 2, 1.2f, Color.SkyBlue);
                    GeneralParticleHandler.SpawnParticle(Visual);
                    if (Projectile.localAI[0] % 3 == 0)
                    {
                        LineParticle subTrail = new LineParticle(Projectile.Center, Projectile.velocity * 0.01f, false, 4, 1.1f, Color.SkyBlue);
                        GeneralParticleHandler.SpawnParticle(subTrail);
                    }
                }

                if (Projectile.localAI[0] < 350)
                    Projectile.velocity *= 1.033f;
                else
                    Projectile.tileCollide = true;
            }


        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn2, 180);
            if (!falling)
            {
                SoundEngine.PlaySound(SoundID.Item50 with { Volume = 0.35f, Pitch = -0.2f, PitchVariance = 0.2f }, Projectile.Center);
                Projectile.velocity = new Vector2(0, -5.5f).RotatedByRandom(0.25f) * Main.rand.NextFloat(0.75f, 1.1f);
                for (int k = 0; k < 3; k++)
                {
                    Vector2 velocity = startVelocity.RotatedByRandom(0.4f) * Main.rand.NextFloat(0.3f, 0.85f);
                    WaterFlavoredParticle burst = new WaterFlavoredParticle(Projectile.Center + velocity * 3f, velocity, false, 5, 0.65f, Color.SkyBlue);
                    GeneralParticleHandler.SpawnParticle(burst);
                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (falling)
                Projectile.damage = (int)(Projectile.damage * 1.35f);
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item27 with { Volume = 0.3f, Pitch = 0.8f }, Projectile.Center);
            for (int k = 0; k < 12; k++)
            {
                Dust dust2 = Dust.NewDustPerfect(Projectile.Center, Main.rand.NextBool(3) ? 135 : 279, new Vector2(3, 3).RotatedByRandom(100) * Main.rand.NextFloat(0.05f, 0.8f));
                dust2.scale = Main.rand.NextFloat(0.65f, 0.85f);
                dust2.noGravity = false;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (!falling)
            {
                Projectile.tileCollide = false;
                SoundEngine.PlaySound(SoundID.Item50 with { Volume = 0.35f, Pitch = -0.2f, PitchVariance = 0.2f }, Projectile.Center);
                for (int k = 0; k < 7; k++)
                {
                    Dust dust2 = Dust.NewDustPerfect(Projectile.Center, Main.rand.NextBool(3) ? 135 : 279, new Vector2(3, 3).RotatedByRandom(100) * Main.rand.NextFloat(0.05f, 0.8f));
                    dust2.scale = Main.rand.NextFloat(0.75f, 0.95f);
                    dust2.noGravity = false;
                }
                Projectile.velocity = new Vector2(0, -7.5f).RotatedByRandom(0.25f) * Main.rand.NextFloat(0.75f, 1.1f);
                return false;
            }
            else
                return true;
        }
        public override bool? CanDamage() => !falling && Projectile.numHits >= 1 ? false : null;
    }
}
