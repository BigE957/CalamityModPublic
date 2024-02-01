﻿using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static CalamityMod.Items.Weapons.Ranged.Scorpio;
using static CalamityMod.Projectiles.Ranged.ScorpioHoldout;
using static Terraria.ModLoader.ModContent;

namespace CalamityMod.Projectiles.Ranged
{
    public class ScorpioLargeRocket : ModProjectile, ILocalizedModType
    {
        public new string LocalizationCategory => "Projectiles.Ranged";

        public ref float RocketID => ref Projectile.ai[0];
        public ref float ProjectileSpeed => ref Projectile.ai[1];

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 8;
        }

        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.MaxUpdates = 2;
            Projectile.width = Projectile.height = 62;
            Projectile.timeLeft = 600;
            Projectile.localNPCHitCooldown = -1;

            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
        }

        public override void AI()
        {
            NPC target = Projectile.Center.ClosestNPCAt(NukeEnemyDistanceDetection);
            if (target is not null)
            {
                Vector2 targetDirection = Projectile.SafeDirectionTo(target.Center);
                float trackingSpeed = Vector2.Dot(targetDirection, Projectile.rotation.ToRotationVector2()) > NukeRequiredRotationProximity ? NukeTrackingSpeed : 0f;
                Projectile.velocity = Projectile.velocity.ToRotation().AngleTowards(targetDirection.ToRotation(), trackingSpeed).ToRotationVector2() * ProjectileSpeed;
            }

            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 4)
            {
                Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Type];
                Projectile.frameCounter = 0;
            }

            // Rotates towards its velocity.
            Projectile.rotation = Projectile.velocity.ToRotation();

            if (Main.dedServ)
                return;

            // The projectile will fade away as its time alive is ending.
            Projectile.alpha = (int)Utils.Remap(Projectile.timeLeft, 30f, 0f, 0f, 255f);

            Dust trailDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustEffectsID, Scale: Main.rand.NextFloat(0.8f, 1f));
            trailDust.noGravity = true;
            trailDust.noLight = true;
            trailDust.noLightEmittence = true;

            Particle thrustSpark = new SparkleParticle(
                Projectile.Center - Vector2.UnitX.RotatedBy(Projectile.rotation) * 8f,
                Vector2.Zero,
                EffectsColor,
                EffectsColor,
                0.8f,
                2);
            GeneralParticleHandler.SpawnParticle(thrustSpark);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) => modifiers.SourceDamage *= 1.25f;
        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers) => modifiers.SourceDamage *= 1.25f;

        public override void OnKill(int timeLeft)
        {
            var info = new CalamityUtils.RocketBehaviorInfo((int)RocketID);
            int blastRadius = Projectile.RocketBehavior(info);
            Projectile.ExpandHitboxBy((float)blastRadius);
            Projectile.Damage();

            // Inside here go all the things that dedicated servers shouldn't spend resources on.
            // Like visuals and sounds.
            if (Main.dedServ)
                return;

            Main.player[Projectile.owner].Calamity().GeneralScreenShakePower = 3f;

            int dustAmount = Main.rand.Next(30, 35 + 1);
            for (int i = 0; i < dustAmount; i++)
            {
                Dust boomDust = Dust.NewDustPerfect(Projectile.Center, 226, (MathHelper.TwoPi / dustAmount * i).ToRotationVector2() * Main.rand.NextFloat(4f, 10f), Scale: Main.rand.NextFloat(0.4f, 0.6f));
                boomDust.noGravity = true;
                boomDust.noLight = true;
                boomDust.noLightEmittence = true;
            }

            Particle explosion = new DetailedExplosion(
                Projectile.Center,
                Vector2.Zero,
                EffectsColor * 0.6f,
                Vector2.One,
                Main.rand.NextFloat(MathHelper.TwoPi),
                Projectile.width / 3600f,
                Projectile.width / 360f,
                20);
            GeneralParticleHandler.SpawnParticle(explosion);

            Particle blastRing = new DirectionalPulseRing(
                Projectile.Center,
                Vector2.Zero,
                EffectsColor,
                Vector2.One,
                0f,
                Projectile.width / 3120f,
                Projectile.width / 312f,
                15);
            GeneralParticleHandler.SpawnParticle(blastRing);
        }

        public float TrailWidthFunction(float completionRatio) => Utils.Remap(completionRatio, 0f, 0.8f, 15f, 0f);
        public Color TrailColorFunction(float completionRatio) => Color.Lerp(EffectsColor, EffectsColor * 0.3f, Utils.GetLerpValue(0f, 0.8f, completionRatio)) * Utils.GetLerpValue(255f, 0f, Projectile.alpha);

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = Request<Texture2D>(Texture).Value;
            Vector2 drawPosition = Projectile.Center - Main.screenPosition;
            Rectangle frame = texture.Frame(1, Main.projFrames[Type], 0, Projectile.frame);
            Color drawColor = Projectile.GetAlpha(lightColor);
            float drawRotation = Projectile.rotation + MathHelper.PiOver2;
            Vector2 rotationPoint = frame.Size() * 0.5f;

            PrimitiveTrail trail = new PrimitiveTrail(TrailWidthFunction, TrailColorFunction, specialShader: GameShaders.Misc["CalamityMod:TrailStreak"]);
            GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(Request<Texture2D>("CalamityMod/ExtraTextures/Trails/FabstaffStreak"));
            trail.Draw(Projectile.oldPos, Projectile.Size * 0.5f - Main.screenPosition, 25);

            Main.EntitySpriteDraw(texture, drawPosition, frame, drawColor, drawRotation, rotationPoint, Projectile.scale, SpriteEffects.None);

            return false;
        }
    }
}
