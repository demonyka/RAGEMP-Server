using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

public class CefCreatePersonRemoteEvents : Script
{
    [RemoteEvent("CLIENT:SERVER::PERSON_CREATE_SEX_SWITCH_BUTTON_CLICKED")]
    public void OnCefPersonCreateSexSwitchButtonClicked(Player player, int sex)
    {
        HeadBlend headBlend = new HeadBlend()
        {
            ShapeFirst = 21,
            ShapeSecond = 0,
            ShapeThird = 0,
            SkinFirst = 21,
            SkinSecond = 0,
            SkinThird = 0,
            ShapeMix = 0.5f,
            SkinMix = 0.5f,
            ThirdMix = 0,
        };

        float[] faceFeatures = new float[20]
        {
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0
        };

        Dictionary<int, HeadOverlay> headOverlay = new Dictionary<int, HeadOverlay>();

        player.SetCustomization(sex == 1, headBlend, byte.MinValue, byte.MinValue, byte.MinValue, faceFeatures, headOverlay, new Decoration[] { });
    }
}