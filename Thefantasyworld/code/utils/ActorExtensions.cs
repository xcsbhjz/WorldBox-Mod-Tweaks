using UnityEngine;

namespace AttributeExpansion.code.utils
{
    public static class ActorExtensions
    {
        private const string careerexperience_key = "wushu.careerexperienceNum";
        private const string DodgeEvade_key = "wushu.DodgeEvadeNum";
        private const string hitthetarget_key = "wushu.hitthetargetNum";
        private const string MagicApplication_key = "wushu.MagicApplicationNum";
        private const string MagicShield_key = "wushu.MagicShield";
        private const string Fixedwound_key = "wushu.Fixedwound";
        private const string Restorehealth_key = "wushu.Restorehealth";
        public static float GetMagicShield(this Actor actor)
        {
            actor.data.get(MagicShield_key, out float val, 0);
            return val;
        }

        public static void SetMagicShield(this Actor actor, float val)
        {
            actor.data.set(MagicShield_key, val);
        }

        public static void ChangeMagicShield(this Actor actor, float delta)
        {
            actor.data.get(MagicShield_key, out float val, 0);
            val += delta;
            actor.data.set(MagicShield_key, Mathf.Max(0, val));
        }

        public static float GetMagicApplication(this Actor actor)
        {
            actor.data.get(MagicApplication_key, out float val, 1);
            return val;
        }
        public static void SetMagicApplication(this Actor actor, float val)
        {
            actor.data.set(MagicApplication_key, val);
        }
        public static void ChangeMagicApplication(this Actor actor, float delta)
        {
            actor.data.get(MagicApplication_key, out float val, 0);
            val += delta;
            actor.data.set(MagicApplication_key, Mathf.Max(1, val));
        }

        public static float Gethitthetarget(this Actor actor)
        {
            actor.data.get(hitthetarget_key, out float val, 0);
            return val;
        }
        public static void Sethitthetarget(this Actor actor, float val)
        {
            actor.data.set(hitthetarget_key, val);
        }
        public static void Changehitthetarget(this Actor actor, float delta)
        {
            actor.data.get(hitthetarget_key, out float val, 0);
            val += delta;
            actor.data.set(hitthetarget_key, Mathf.Max(0, val));
        }

        public static float GetDodgeEvade(this Actor actor)
        {
            actor.data.get(DodgeEvade_key, out float val, 0);
            return val;
        }
        public static void SetDodgeEvade(this Actor actor, float val)
        {
            actor.data.set(DodgeEvade_key, val);
        }
        public static void ChangeDodgeEvade(this Actor actor, float delta)
        {
            actor.data.get(DodgeEvade_key, out float val, 0);
            val += delta;
            actor.data.set(DodgeEvade_key, val);
        }

        public static float Getcareerexperience(this Actor actor)
        {
            actor.data.get(careerexperience_key, out float val, 0);

            return val;
        }

        public static void Setcareerexperience(this Actor actor, float val)
        {
            actor.data.set(careerexperience_key, val);
        }

        public static void Changecareerexperience(this Actor actor, float delta)
        {
            actor.data.get(careerexperience_key, out float val, 0);
            val += delta;
            actor.data.set(careerexperience_key, Mathf.Max(0, val));
        }
        public static float GetFixedwound(this Actor actor)
        {
            actor.data.get(Fixedwound_key, out float val, 0);
            return val;
        }
        public static void SetFixedwound(this Actor actor, float val)
        {
            actor.data.set(Fixedwound_key, val);
        }
        public static void ChangeFixedwound(this Actor actor, float delta)
        {
            actor.data.get(Fixedwound_key, out float val, 0);
            val += delta;
            actor.data.set(Fixedwound_key, Mathf.Max(0, val));
        }
        public static float GetRestorehealth(this Actor actor)
        {
            actor.data.get(Restorehealth_key, out float val, 0);
            return val;
        }
        public static void SetRestorehealth(this Actor actor, float val)
        {
            actor.data.set(Restorehealth_key, val);
        }
        public static void ChangeRestorehealth(this Actor actor, float delta)
        {
            actor.data.get(Restorehealth_key, out float val, 0);
            val += delta;
            actor.data.set(Restorehealth_key, Mathf.Max(0, val));
        }
    }
}