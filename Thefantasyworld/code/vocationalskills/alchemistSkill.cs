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
    internal class alchemistSkill
    {

        public static bool attack_alchemist3(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget.isBuilding())
            {
                return false;
            }

            pTarget.a.addStatusEffect("alchemiststate1", 30f);

            return true;
        }

        public static bool attack_alchemist4(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget.isBuilding())
            {
                return false;
            }

            pTarget.a.addStatusEffect("alchemiststate2", 30f);

            return true;
        }

        public static bool attack_alchemist5(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget.isBuilding())
            {
                return false;
            }

            pTarget.a.addStatusEffect("alchemiststate3", 30f); 

            return true;
        }

        public static bool attack_alchemist6(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget.isBuilding())
            {
                return false;
            }

            pTarget.a.addStatusEffect("alchemiststate4", 30f); 

            return true;
        } 

         public static bool attack_alchemist7(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget.isBuilding())
            {
                return false;
            }

            pTarget.a.addStatusEffect("alchemiststate5", 30f); 

            return true;
        }
    }
}