﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IsoRangeClass
{
  // test 
    private static string isoBBVSSB40 = "22+, A2s+, K2s+, Q7s+, J7s+, T7s+, 96s+, 85s+, 75s+, 64s+, 54s, A2o+, K8o+, Q9o+, J9o+, T9o, 98o";
    private static string isoBBVS1Limp40 = "22+, A2s+, K8s+, Q9s+, J9s+, T8s+, 98s, 87s, A9o+, KTo+, QTo+, JTo";
    private static string isoOOPVS1Limp = "88+, ATs+, KTs+, QTs+, JTs, ATo+, KJo+, QJo";
    private static string isoIPVS2Limp = isoOOPVS1Limp;
    private static string alwaysIso = "TT+, AJs+, KQs, AJo+, KQo";

    public static string IsoBBVSSB40
    {
        get
        {
            return isoBBVSSB40;
        }
    }

    public static string IsoBBVS1Limp40
    {
        get
        {
            return isoBBVS1Limp40;
        }
    }

    public static string IsoOOPVS1Limp
    {
        get
        {
            return isoOOPVS1Limp;
        }
    }
}
