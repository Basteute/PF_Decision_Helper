using UnityEngine;
using System.Collections;

public class RaiseRangeClass
{
    // IP VS raise
    private static string IPVSEPCall = "QQ-22, AJs+, KQs, AQo+";
    private static string IPVSEP3BetF = "ATs-A2s, KJs";
    private static string IPVSEP3BetA = "AA, KK";

    private static string BUVSCOCall = "JJ-22, AQs-ATs, KJs+, QJs, JTs, AQo-AJo, KQo";
    private static string BUVSCO3BetF = "A9s-A2s, KTs, QTs, T9s, 98s, 87s, 76s, 65s, 54s, ATo, KJo";
    private static string BUVSCO3BetA = "AA, AKs, AKo, KK, QQ";

    private static string BBVSSBCall = "TT-22, AJs-A2s, K8s+, Q8s+, J8s+, T7s+, 97s+, 87s, 76s, 65s, 54s, AJo-A6o, K9o+, Q9o+, J9o+, T9o, 98o";
    private static string BBVSSB3BetF = "AQs, K7s-K2s, 86s, 75s, 64s, AQo, A5o-A2o";
    private static string BBVSSB3BetA = "AA, KK, QQ, JJ, AKo, AKs";

    // SB VS raise
    private static string SBVSEPCall = "AKs, AQs, AKo, AQo, QQ, JJ, TT, 99, 88, 77";
    private static string SBVSEP3BetA = "KK+";

    private static string SBVSCOCall = "JJ-77, AQs, AJs, ATs, A9s, KJs+, AQo, AJo, KQo";
    private static string SBVSCO3BetF = "QJs, JTs, QTs, KTs, A5s-A2s";
    private static string SBVSCO3BetA = "KK+, AKo, AKs";

    private static string SBVSBUCall = "AJo, KJo, QJs, KJs+, QTs+, A9s, A8s, TT-55";
    private static string SBVSBU3BetF = "AQo, KQo, KQs, AQs, JTs, T9s, J9s, Q9s, K9s, A7s-A2s";
    private static string SBVSBU3BetA = "AKo, AKs, JJ+";

    // BB VS raise
    private static string BBVSEPCall = "AQo+, AKs, AQs, QQ-22";
    private static string BBVSEP3BetA = "KK+";

    private static string BBVSCOCall = "JJ-22, AJo, AQo, KQo, KQs, AQs, QJs, KJs+, ATs, A9s, A8s, A7s, A6s, JJ-22";
    private static string BBVSCO3BetF = "T9o, JTs, QTs, KTs, A5o-A2o";
    private static string BBVSCO3BetA = "QQ+,AKo,AKs";

    private static string BBVSBUCall = "A9o, ATo, AJo, KJo, KQo, QJo, QJs, KJs+, JTs, QTs+, T9s, J9s+, T8s, J8s, K8s, AJs-A2s, TT-22";
    private static string BBVSBU3BetF = "AQo, KQs, AQs, 98s, 87s, 97s, 76s, 86s, 75s, 65s";

    // Squeeze
    private static string IPSqueezeCall = "QJs, JTs, T9s, TT-22";
    private static string IPSqueeze3Bet = "JJ+, AQo, AKo, KQs, AQs, AJs";

    private static string OOPSqueezeCall = "JJ-77, KQs, AJs, AJo, KQo";
    private static string OOPSqueezeCallBB = "66-22, JTs, QJs, KJs, ATs";
    private static string OOPSqueeze3Bet = "QQ+, AQo, AKo, AKs, AQs";

    // Getters
    public static string GetIPVSEPCall
    {
        get
        {
            return IPVSEPCall;
        }
    }

    public static string GetIPVSEP3BetF
    {
        get
        {
            return IPVSEP3BetF;
        }
    }

    public static string GetIPVSEP3BetA
    {
        get
        {
            return IPVSEP3BetA;
        }
    }

    public static string GetBUVSCOCall
    {
        get
        {
            return BUVSCOCall;
        }
    }

    public static string GetBUVSCO3BetF
    {
        get
        {
            return BUVSCO3BetF;
        }
    }

    public static string GetBUVSCO3BetA
    {
        get
        {
            return BUVSCO3BetA;
        }
    }

    public static string GetBBVSSBCall
    {
        get
        {
            return BBVSSBCall;
        }
    }

    public static string GetBBVSSB3BetF
    {
        get
        {
            return BBVSSB3BetF;
        }
    }

    public static string GetBBVSSB3BetA
    {
        get
        {
            return BBVSSB3BetA;
        }
    }

    public static string GetSBVSEPCall
    {
        get
        {
            return SBVSEPCall;
        }
    }

    public static string GetSBVSEP3BetA
    {
        get
        {
            return SBVSEP3BetA;
        }
    }

    public static string GetSBVSCOCall
    {
        get
        {
            return SBVSCOCall;
        }
    }

    public static string GetSBVSCO3BetF
    {
        get
        {
            return SBVSCO3BetF;
        }
    }

    public static string GetSBVSCO3BetA
    {
        get
        {
            return SBVSCO3BetA;
        }
    }

    public static string GetSBVSBUCall
    {
        get
        {
            return SBVSBUCall;
        }
    }

    public static string GetSBVSBU3BetF
    {
        get
        {
            return SBVSBU3BetF;
        }
    }

    public static string GetSBVSBU3BetA
    {
        get
        {
            return SBVSBU3BetA;
        }
    }

    public static string GetBBVSEPCall
    {
        get
        {
            return BBVSEPCall;
        }
    }

    public static string GetBBVSEP3BetA
    {
        get
        {
            return BBVSEP3BetA;
        }
    }

    public static string GetBBVSCOCall
    {
        get
        {
            return BBVSCOCall;
        }
    }

    public static string GetBBVSCO3BetF
    {
        get
        {
            return BBVSCO3BetF;
        }
    }

    public static string GetBBVSCO3BetA
    {
        get
        {
            return BBVSCO3BetA;
        }
    }

    public static string GetBBVSBUCall
    {
        get
        {
            return BBVSBUCall;
        }
    }

    public static string GetBBVSBU3BetF
    {
        get
        {
            return BBVSBU3BetF;
        }
    }

    public static string GetIPSqueezeCall
    {
        get
        {
            return IPSqueezeCall;
        }
    }

    public static string GetIPSqueeze3Bet
    {
        get
        {
            return IPSqueeze3Bet;
        }
    }

    public static string GetOOPSqueezeCall
    {
        get
        {
            return OOPSqueezeCall;
        }
    }

    public static string GetOOPSqueezeCallBB
    {
        get
        {
            return OOPSqueezeCallBB;
        }
    }

    public static string GetOOPSqueeze3Bet
    {
        get
        {
            return OOPSqueeze3Bet;
        }
    }
}
