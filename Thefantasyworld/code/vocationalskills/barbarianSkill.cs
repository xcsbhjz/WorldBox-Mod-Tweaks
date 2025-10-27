using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ai;
using UnityEngine;
using AttributeExpansion.code.utils;
using ReflectionUtility;
using System.IO;

namespace PeerlessThedayofGodswrath.code
{
    internal class barbarianSkill
    {
        public static bool barbarian3_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth() * 0.5f)
            {
                pTarget.a.addStatusEffect("barbarianstate1", 60f);
            }

            return true;
        }

        public static bool barbarian4_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth() * 0.5f)
            {
                pTarget.a.addStatusEffect("barbarianstate2", 60f);
            }

            return true;
        }

        public static bool barbarian5_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth() * 0.5f)
            {
                pTarget.a.addStatusEffect("barbarianstate3", 60f);
            }

            return true;
        }

        public static bool barbarian6_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth() * 0.5f)
            {
                pTarget.a.addStatusEffect("barbarianstate4", 60f);
            }

            return true;
        }

        public static bool barbarian7_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth() * 0.5f)
            {
                pTarget.a.addStatusEffect("barbarianstate5", 60f);
            }

            return true;
        }
    }
}