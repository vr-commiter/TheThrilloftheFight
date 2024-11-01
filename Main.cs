using MelonLoader;
using HarmonyLib;
using TotF;
using UnityEngine;
using System.Collections.Generic;
using MyTrueGear;

namespace TTOTF_TrueGear
{
    public static class BuildInfo
    {
        public const string Name = "TTOTF_TrueGear"; // Name of the Mod.  (MUST BE SET)
        public const string Description = "TrueGear Mod for TTOTF"; // Description for the Mod.  (Set as null if none)
        public const string Author = "HuangLY"; // Author of the Mod.  (MUST BE SET)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }

    public class TTOTF_TrueGear : MelonMod
    {
        private static TrueGearMod _TrueGear = null;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("OnApplicationStart");
            HarmonyLib.Harmony.CreateAndPatchAll(typeof(TTOTF_TrueGear));
            _TrueGear = new TrueGearMod();
        }

        public static KeyValuePair<float, float> GetAngle(Transform transform, Vector3 hitPoint)
        {
            Vector3 hitPos = hitPoint - transform.position;
            float hitAngle = Mathf.Atan2(hitPos.x, hitPos.z) * Mathf.Rad2Deg;
            if (hitAngle < 0f)
            {
                hitAngle += 360f;
            }
            if (hitAngle > 90f && hitAngle < 180f) 
            {
                hitAngle -= 90f;
            }
            else if (hitAngle > 180f && hitAngle < 270f ) 
            {
                hitAngle += 90f;
            }
            float verticalDifference = hitPoint.y - transform.position.y;
            return new KeyValuePair<float, float>(hitAngle, verticalDifference);
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PlayerController), "Start")]
        private static void PlayerController_Start_Postfix()
        {
            MelonLogger.Msg("--------------------------------------------------");
            MelonLogger.Msg("PlayerControllerStart");
            _TrueGear.Play("PlayerControllerStart");
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PlayerController), "HurtboxCollisionVisualResult")]
        private static void PlayerController_HurtboxCollisionVisualResult_Postfix(PlayerController __instance, float damage, bool wasKnockedDown, Hurtbox hurtbox, Hitbox hitbox, Collision collision)
        {
            var angle = GetAngle(hurtbox.transform,hitbox.transform.position);
            if (wasKnockedDown)
            {
                MelonLogger.Msg("--------------------------------------------------");
                MelonLogger.Msg("PlayerKnockdown");
                _TrueGear.Play("PlayerKnockdown");
            }
            else
            {
                if (hurtbox.type == Hurtbox.HurtboxType.Head)
                {
                    float time = damage / 4680f;
                    MelonLogger.Msg("--------------------------------------------------");
                    MelonLogger.Msg($"HeadDamage,{angle.Key},{angle.Value}");
                    _TrueGear.PlayAngle("HeadDamage",angle.Key,angle.Value);
                }
                else
                {
                    MelonLogger.Msg("--------------------------------------------------");
                    MelonLogger.Msg($"BodyDamage,{angle.Key},{angle.Value}");
                    _TrueGear.PlayAngle("BodyDamage",angle.Key,angle.Value);
                }
            }
            if (damage > 3200f)
            {
                _TrueGear.Play("PlayerDizz");
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PlayerFist), "ReceiveForce")]
        private static void PlayerFist_ReceiveForce_Postfix(PlayerFist __instance, float force, bool isMajorHit)
        {
            MelonLogger.Msg("--------------------------------------------------");
            MelonLogger.Msg("force :" + force);
            if (force < 100f)
            {
                return;
            }
            if (isMajorHit)
            {
                if (__instance.leftHand)
                {
                    MelonLogger.Msg("LeftMajorHit");
                    _TrueGear.Play("LeftMajorHit");
                }
                else
                {
                    MelonLogger.Msg("RightMajorHit");
                    _TrueGear.Play("RightMajorHit");
                }
            }
            else
            {
                if (__instance.leftHand)
                {
                    MelonLogger.Msg("LeftHit");
                    _TrueGear.Play("LeftHit");
                }
                else
                {
                    MelonLogger.Msg("RightHit");
                    _TrueGear.Play("RightHit");
                }                
            }
        }
    }
}