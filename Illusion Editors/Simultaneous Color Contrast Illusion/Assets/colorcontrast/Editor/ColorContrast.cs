using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Globalization;
using System.Collections;

public class ColorContrast : EditorWindow
{
    float windowWidth;
    float ratio = -0.5f;
    float sRatio = 1;
   
    static string[] layerStr = new string[] { "--", "Object Color", "Background Color", "Perceived Color" };
    static string[] generateLayerStr = new string[] { "1 : background, 2 : background", "1 : object, 2 : background", "1 : background, 2 : object" };
    string thirdLayerStr;

    int firstLayer;
    int secondLayer;
    int thirdLayer;
    int lastFirstLayer;
    int lastSecondLayer;
    int colorLayer;
    int customizedColorLayer;

    Color firstLayerColor = Color.white;
    Color secondLayerColor = Color.white;
    Color thirdLayerColor = Color.white;
    Color[] displayColor = new Color[4] { Color.clear, Color.clear, Color.clear, Color.clear };
    Color customizedColorOne = Color.white;
    Color customizedColorTwo = Color.white;
    Color[] customizedDisplayColor = new Color[3] { Color.white, Color.white, Color.white };

    bool showDirectionLaw = true;
    bool flag = false;
    bool[] colorBoolList = new bool[5];
    bool display;
    bool displayed = false;
    bool applyFlag;
    bool showThirdLayerInfo = true;
    bool lastShowThirdLayerInfo;
    bool showMaterialInfo = true;
    bool showCustomizedGenerate = true;
    bool customizedDisplay;
    bool customizedDisplayed;
    bool showCostomizedMaterialInfo = true;

    public Material objectMaterial;
    public Material backgroundMaterial;
    public Material colorOneMaterial;
    public Material colorTwoMaterial;

    Vector2 scrollPos = Vector2.zero;

    [MenuItem("Window/ColorContrast")]

    public static void ShowWindow()
    {
        GetWindow<ColorContrast>("Color Contrast");
    }

    void OnGUI()
    {
        windowWidth = position.width;
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false, GUILayout.Width(windowWidth + 10));
        showDirectionLaw = EditorGUILayout.Foldout(showDirectionLaw, "Generate with direction law");
        if (showDirectionLaw)
        {
            firstLayer = EditorGUILayout.Popup("First Layer", firstLayer, layerStr, GUILayout.Width(windowWidth));
            firstLayerColor = EditorGUILayout.ColorField("First Layer Color", firstLayerColor, GUILayout.Width(windowWidth));
            secondLayer = EditorGUILayout.Popup("Second Layer", secondLayer, layerStr, GUILayout.Width(windowWidth));
            secondLayerColor = EditorGUILayout.ColorField("Second Layer Color", secondLayerColor, GUILayout.Width(windowWidth));
            if (firstLayer == secondLayer)
            {
                if (firstLayer != lastFirstLayer)
                {
                    firstLayer = 0;
                }
                else
                {
                    secondLayer = 0;
                }
            }
            lastFirstLayer = firstLayer;
            lastSecondLayer = secondLayer;


            EditorGUILayout.Space();
            EditorGUILayout.Space();


            if (firstLayer != 0 && secondLayer != 0)
            {
                for (int i = 1; i <= 3; i++)
                {
                    if (i != firstLayer && i != secondLayer)
                    {
                        thirdLayerStr = layerStr[i];
                        thirdLayer = i;
                        flag = true;
                        break;
                    }
                }
            }
            else
            {
                flag = false;
                thirdLayer = 0;
                ratio = -0.5f;
                thirdLayerColor = Color.white;
                thirdLayerStr = "  ";
            }
            EditorGUILayout.LabelField("Third Layer", thirdLayerStr, GUILayout.Width(windowWidth));
            ratio = EditorGUILayout.Slider("Alpha Value", ratio, -1, -0.0001f, GUILayout.Width(windowWidth));
            thirdLayerColor = (firstLayer < secondLayer) ? GenerateColor(firstLayerColor, secondLayerColor, thirdLayer, ratio) :
                                                           GenerateColor(secondLayerColor, firstLayerColor, thirdLayer, ratio);
            EditorGUILayout.LabelField(new GUIContent("Third Layer Color"), new GUIContent(GenerateTexture(thirdLayerColor)), GUILayout.Width(windowWidth));


            EditorGUILayout.Space();
            GUI.enabled = flag;
            if (display = GUILayout.Button("Display Result", GUILayout.Width(windowWidth)))
            {
                displayColor[firstLayer] = firstLayerColor;
                displayColor[secondLayer] = secondLayerColor;
            }
            displayColor[thirdLayer] = thirdLayerColor;
            GUI.enabled = true;


            displayed = (display || displayed) && flag;
            GUILayout.BeginHorizontal();
            Texture2D displayTexture = GenerateDisplayTexture(displayColor[1], displayColor[2]);
            Texture2D perceivedTexture = GenerateDisplayTexture(displayColor[3], Color.clear);
            var middleRightStyle = new GUIStyle() { alignment = TextAnchor.MiddleRight };
            var middleLeftStyle = new GUIStyle() { alignment = TextAnchor.MiddleLeft };
            if (displayed)
            {
                EditorGUILayout.LabelField(new GUIContent(displayTexture), middleRightStyle, GUILayout.Width(windowWidth / 2), GUILayout.Height(54));
                EditorGUILayout.LabelField(new GUIContent(perceivedTexture), middleLeftStyle, GUILayout.Width(windowWidth / 2), GUILayout.Height(54));
            }
            GUILayout.EndHorizontal();

            GUI.enabled = flag;
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Swap Layer 1 & 3", GUILayout.Width(windowWidth / 2 - 3)))
            {
                firstLayer = thirdLayer;
                firstLayerColor = thirdLayerColor;
                for (int i = 0; i < 5; i++)
                {
                    colorBoolList[i] = false;
                }
            }
            if (GUILayout.Button("Swap Layer 2 & 3", GUILayout.Width(windowWidth / 2 - 3)))
            {
                secondLayer = thirdLayer;
                secondLayerColor = thirdLayerColor;
                for (int i = 0; i < 5; i++)
                {
                    colorBoolList[i] = false;
                }
            }
            GUILayout.EndHorizontal();
            EditorGUILayout.Space();
            showThirdLayerInfo = lastShowThirdLayerInfo;
            showThirdLayerInfo = EditorGUILayout.Foldout(showThirdLayerInfo, "Third Layer Color Detail");
            lastShowThirdLayerInfo = showThirdLayerInfo;
            if (!flag)
            {
                showThirdLayerInfo = false;
            }
            if (showThirdLayerInfo)
            {
                Color32 thirdLayerColor32 = thirdLayerColor;
                int thirdR = thirdLayerColor32.r;
                thirdR = (thirdR < 0) ? 0 : thirdR;
                int thirdG = thirdLayerColor32.g;
                thirdG = (thirdG < 0) ? 0 : thirdG;
                int thirdB = thirdLayerColor32.b;
                thirdB = (thirdB < 0) ? 0 : thirdB;
                EditorGUILayout.LabelField("Third Layer R", (flag) ? thirdR.ToString("G", CultureInfo.InvariantCulture) : " ", GUILayout.Width(windowWidth));
                EditorGUILayout.LabelField("Third Layer G", (flag) ? thirdG.ToString("G", CultureInfo.InvariantCulture) : " ", GUILayout.Width(windowWidth));
                EditorGUILayout.LabelField("Third Layer B", (flag) ? thirdB.ToString("G", CultureInfo.InvariantCulture) : " ", GUILayout.Width(windowWidth));
            }
            GUI.enabled = true;


            EditorGUILayout.Space();
            showMaterialInfo = EditorGUILayout.Foldout(showMaterialInfo, "Materials");
            if (showMaterialInfo)
            {
                objectMaterial = (Material)EditorGUILayout.ObjectField("Object Material", objectMaterial, typeof(Material), true, GUILayout.Width(windowWidth - 10));
                backgroundMaterial = (Material)EditorGUILayout.ObjectField("Background Material", backgroundMaterial, typeof(Material), true, GUILayout.Width(windowWidth - 10));
                if (GUILayout.Button("Apply Material Color To Editor", GUILayout.Width(windowWidth - 2)))
                {
                    ApplyMaterialToWindow();
                }
                if (thirdLayer != 0 && flag && (displayColor[firstLayer] == firstLayerColor) && (displayColor[secondLayer] == secondLayerColor))
                {
                    applyFlag = true;
                }
                else
                {
                    applyFlag = false;
                }
                GUI.enabled = applyFlag;
                if (GUILayout.Button("Apply Color To Material (Create if none)", GUILayout.Width(windowWidth - 2)))
                {
                    ApplyColorToMaterial();
                }
                GUI.enabled = true;
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        showCustomizedGenerate = EditorGUILayout.Foldout(showCustomizedGenerate, "Generate with simultaneous color contrast");
        if (showCustomizedGenerate)
        {
            customizedColorOne = EditorGUILayout.ColorField("Color One", customizedColorOne, GUILayout.Width(windowWidth));
            customizedColorTwo = EditorGUILayout.ColorField("Color Two", customizedColorTwo, GUILayout.Width(windowWidth));
            EditorGUILayout.Space();
            customizedColorLayer = EditorGUILayout.Popup("Layer", customizedColorLayer, generateLayerStr, GUILayout.Width(windowWidth));
            sRatio = EditorGUILayout.Slider("Saturation Ratio", sRatio, 1, 0.4f, GUILayout.Width(windowWidth));
            Color saturatedColorOne = GenerateSaturationColor(customizedColorOne, sRatio);
            Color saturatedColorTwo = GenerateSaturationColor(customizedColorTwo, sRatio);
            Texture2D customizedTextureOne = GenerateTexture(saturatedColorOne);
            Texture2D customizedTextureTwo = GenerateTexture(saturatedColorTwo);
            var middleCenterStyle = new GUIStyle() { alignment = TextAnchor.MiddleCenter };
            middleCenterStyle.normal.textColor = new Color(0.8f, 0.8f, 0.8f, 1);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Color 1", middleCenterStyle, GUILayout.Width(windowWidth / 2 - 1));
            EditorGUILayout.LabelField("Color 2", middleCenterStyle, GUILayout.Width(windowWidth / 2 - 1));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent(customizedTextureOne), middleCenterStyle, GUILayout.Width(windowWidth / 2 - 1));
            EditorGUILayout.LabelField(new GUIContent(customizedTextureTwo), middleCenterStyle, GUILayout.Width(windowWidth / 2 - 1));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
            if (customizedDisplay = GUILayout.Button("Display Result", GUILayout.Width(windowWidth)))
            {
                switch (customizedColorLayer)
                {
                    case (0):
                        customizedDisplayColor[0] = saturatedColorOne;
                        customizedDisplayColor[1] = saturatedColorTwo;
                        customizedDisplayColor[2] = GenerateMiddleColor(saturatedColorOne, saturatedColorTwo);
                        break;
                    case (1):
                        customizedDisplayColor[0] = saturatedColorTwo;
                        customizedDisplayColor[1] = GenerateBackground(saturatedColorTwo, saturatedColorOne);
                        customizedDisplayColor[2] = saturatedColorOne;
                        break;
                    case (2):
                        customizedDisplayColor[0] = saturatedColorOne;
                        customizedDisplayColor[1] = GenerateBackground(saturatedColorOne, saturatedColorTwo);
                        customizedDisplayColor[2] = saturatedColorTwo;
                        break;
                }
            }
            customizedDisplayed = (customizedDisplay || customizedDisplayed);
            Texture2D customizedDisplayTextureOne = GenerateDisplayTexture(customizedDisplayColor[2], customizedDisplayColor[0]);
            Texture2D customizedDisplayTextureTwo = GenerateDisplayTexture(customizedDisplayColor[2], customizedDisplayColor[1]);
            var middleRightStyle = new GUIStyle() { alignment = TextAnchor.MiddleRight };
            var middleLeftStyle = new GUIStyle() { alignment = TextAnchor.MiddleLeft };
            if (customizedDisplayed)
            {
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent(customizedDisplayTextureOne), middleRightStyle, GUILayout.Width(windowWidth / 2), GUILayout.Height(54));
                EditorGUILayout.LabelField(new GUIContent(customizedDisplayTextureTwo), middleLeftStyle, GUILayout.Width(windowWidth / 2), GUILayout.Height(54));
                GUILayout.EndHorizontal();
            }
            showCostomizedMaterialInfo = EditorGUILayout.Foldout(showCostomizedMaterialInfo, "Materials");
            if (showCostomizedMaterialInfo)
            {
                colorOneMaterial = (Material)EditorGUILayout.ObjectField("Color One Material", colorOneMaterial, typeof(Material), true, GUILayout.Width(windowWidth - 10));
                colorTwoMaterial = (Material)EditorGUILayout.ObjectField("Color Two Material", colorTwoMaterial, typeof(Material), true, GUILayout.Width(windowWidth - 10));
                if (GUILayout.Button("Apply Material Color To Editor", GUILayout.Width(windowWidth - 2)))
                {
                    ApplyCustomizedMaterialToWindow();
                }
                bool customizedApplyFlag = false;
                switch (customizedColorLayer)
                {
                    case (0):
                        if(customizedDisplayColor[0] == saturatedColorOne && customizedDisplayColor[1] == saturatedColorTwo)
                        {
                            customizedApplyFlag = true;
                        }
                        break;
                    case (1):
                        if(customizedDisplayColor[0] == saturatedColorTwo && customizedDisplayColor[2] == saturatedColorOne)
                        {
                            customizedApplyFlag = true;
                        }
                        break;
                    case (2):
                        if(customizedDisplayColor[0] == saturatedColorOne && customizedDisplayColor[2] == saturatedColorTwo)
                        {
                            customizedApplyFlag = true;
                        }
                        break;
                }
                GUI.enabled = customizedDisplayed && customizedApplyFlag;
                if (GUILayout.Button("Apply Color To Material (Create if none)", GUILayout.Width(windowWidth - 2)))
                {
                    ApplyCustomizedColorToMaterial();
                }
                GUI.enabled = true;
            }
        }
        EditorGUILayout.EndScrollView();
    }

    public static Color GenerateColor(Color first, Color second, int layer, float ratio)
    {
        switch (layer)
        {
            case 0:
                return Color.clear;
            case 1:
                return BPToL(first, second, ratio);
            case 2:
                return LPToB(first, second, ratio);
            case 3:
                return LBToP(first, second, ratio);
            default:
                return Color.clear;
        }
    }

    public static Color BPToL(Color background, Color perceived, float ratio)
    {
        float layerR, layerG, layerB;
        layerR = perceived.r / (1 - ratio) - background.r * ratio / (1 - ratio);
        layerG = perceived.g / (1 - ratio) - background.g * ratio / (1 - ratio);
        layerB = perceived.b / (1 - ratio) - background.b * ratio / (1 - ratio);
        return new Color(layerR, layerG, layerB);
    }

    public static Color LPToB(Color layer, Color perceived, float ratio)
    {
        float backgroundR, backgroundG, backgroundB;
        backgroundR = perceived.r / ratio - layer.r * (1 - ratio) / ratio;
        backgroundG = perceived.g / ratio - layer.g * (1 - ratio) / ratio;
        backgroundB = perceived.b / ratio - layer.b * (1 - ratio) / ratio;
        return new Color(backgroundR, backgroundG, backgroundB);
    }

    public static Color LBToP(Color layer, Color background, float ratio)
    {
        float perceivedR, perceivedG, perceivedB;
        perceivedR = (1 - ratio) * layer.r + ratio * background.r;
        perceivedG = (1 - ratio) * layer.g + ratio * background.g;
        perceivedB = (1 - ratio) * layer.b + ratio * background.b;

        return new Color(perceivedR, perceivedG, perceivedB);
    }

    public void ApplyMaterialToWindow()
    {
        if (objectMaterial != null)
        {
            if (firstLayer == 1)
            {
                firstLayerColor = objectMaterial.color;
            }
            else if (secondLayer == 1)
            {
                secondLayerColor = objectMaterial.color;
            }
            else if (firstLayer == 0)
            {
                firstLayer = 1;
                firstLayerColor = objectMaterial.color;
            }
            else if (secondLayer == 0)
            {
                secondLayer = 1;
                secondLayerColor = objectMaterial.color;
            }
            else
            {
                thirdLayerColor = objectMaterial.color;
            }
        }
        if (backgroundMaterial != null)
        {
            if (firstLayer == 2)
            {
                firstLayerColor = backgroundMaterial.color;
            }
            else if (secondLayer == 2)
            {
                secondLayerColor = backgroundMaterial.color;
            }
            else if (firstLayer == 0)
            {
                firstLayer = 2;
                firstLayerColor = backgroundMaterial.color;
            }
            else if (secondLayer == 0)
            {
                secondLayer = 2;
                secondLayerColor = backgroundMaterial.color;
            }
            else
            {
                thirdLayerColor = backgroundMaterial.color;
            }
        }
    }

    public void ApplyColorToMaterial()
    {
        if (objectMaterial != null)
        {
            objectMaterial.color = displayColor[1];
        }
        else
        {
            objectMaterial = new Material(Shader.Find("Standard"));
            objectMaterial.color = displayColor[1];
            var uniqueFileName = AssetDatabase.GenerateUniqueAssetPath("Assets/ObjectMaterial.mat");
            AssetDatabase.CreateAsset(objectMaterial, uniqueFileName);
        }
        if (backgroundMaterial != null)
        {
            backgroundMaterial.color = displayColor[2];
        }
        else
        {
            backgroundMaterial = new Material(Shader.Find("Standard"));
            backgroundMaterial.color = displayColor[2];
            var uniqueFileName = AssetDatabase.GenerateUniqueAssetPath("Assets/BackgroundMaterial.mat");
            AssetDatabase.CreateAsset(backgroundMaterial, uniqueFileName);
        }
    }

    public Texture2D GenerateTexture(Color color)
    {
        Texture2D texture = new Texture2D(18, 18);
        for(int i = 0; i < 18; i++)
        {
            for(int j = 0; j < 18; j++)
            {
                texture.SetPixel(i, j, color);
            }
        }
        texture.wrapMode = TextureWrapMode.Repeat;
        texture.Apply();
        return texture;
    }

    public Texture2D GenerateDisplayTexture(Color ObjectColor, Color BackgroundColor)
    {
        Texture2D texture = new Texture2D(54, 54);
        for (int i = 0; i < 54; i++)
        {
            for (int j = 0; j < 54; j++)
            {
                if(i >= 18 && i < 36 && j >= 18 && j < 36)
                {
                    texture.SetPixel(i, j, ObjectColor);
                }
                else
                {
                    texture.SetPixel(i, j, BackgroundColor);
                }
            }
        }
        texture.wrapMode = TextureWrapMode.Repeat;
        texture.Apply();
        return texture;
    }

    public static Color GenerateSaturationColor(Color color, float sRate)
    {
        float h, s1, s2, v;
        Color.RGBToHSV(color, out h, out s1, out v);
        s2 = s1 * sRate;
        return Color.HSVToRGB(h, s2, v);
    }

    public static Color GenerateMiddleColor(Color color1, Color color2)
    {
        float h1, h2, h3, s1, s2, s3, v1, v2, v3;
        Color.RGBToHSV(color1, out h1, out s1, out v1);
        Color.RGBToHSV(color2, out h2, out s2, out v2);
        if (Math.Abs(h1 - h2) <= 0.5)
        {
            h3 = (h1 + h2) / 2;
        }
        else
        {
            h3 = (h1 + h2 + 1) / 2;
        }
        s3 = (s1 + s2) / 2;
        v3 = (v1 + v2) / 2;
        return Color.HSVToRGB(h3, s3, v3);
    }

    public static Color GenerateBackground(Color backgroundColor, Color objectColor)
    {
        float h1, h2, h3, s1, s2, s3, v1, v2, v3;
        Color.RGBToHSV(backgroundColor, out h1, out s1, out v1);
        Color.RGBToHSV(objectColor, out h2, out s2, out v2);
        s1 = Math.Min(s1, 1);
        s2 = Math.Min(s2, 1);
        v1 = Math.Min(v1, 1);
        v2 = Math.Min(v2, 1);
        h3 = (h2 - (h1 - h2) + 1) % 1;
        s3 = Math.Min(Math.Max(0, 2 * s2 - s1), 1);
        v3 = Math.Min(Math.Max(0, 2 * v2 - v1), 1);
        return Color.HSVToRGB(h3, s3, v3);
    }

    public void ApplyCustomizedMaterialToWindow()
    {
        if (colorOneMaterial != null)
        {
            customizedColorOne = colorOneMaterial.color;
        }
        if (colorTwoMaterial != null)
        {
            customizedColorTwo = colorTwoMaterial.color;
        }
    }

    public void ApplyCustomizedColorToMaterial()
    {
        var uniqueFileName = AssetDatabase.GenerateUniqueAssetPath("Assets/ObjectMaterialOne.mat");
        Material backgroundMaterialOne, backgroundMaterialTwo, objectMaterialOne;
        switch (customizedColorLayer)
        {
            case (0):
                if(colorOneMaterial != null)
                {
                    colorOneMaterial.color = customizedDisplayColor[0];
                }
                else
                {
                    backgroundMaterialOne = new Material(Shader.Find("Standard"));
                    backgroundMaterialOne.color = customizedDisplayColor[0];
                    uniqueFileName = AssetDatabase.GenerateUniqueAssetPath("Assets/BackgroundMaterialOne.mat");
                    AssetDatabase.CreateAsset(backgroundMaterialOne, uniqueFileName);
                }
                if(colorTwoMaterial != null)
                {
                    colorTwoMaterial.color = customizedDisplayColor[1];
                }
                else
                {
                    backgroundMaterialTwo = new Material(Shader.Find("Standard"));
                    backgroundMaterialTwo.color = customizedDisplayColor[1];
                    uniqueFileName = AssetDatabase.GenerateUniqueAssetPath("Assets/BackgroundMaterialTwo.mat");
                    AssetDatabase.CreateAsset(backgroundMaterialTwo, uniqueFileName);
                }
                objectMaterialOne = new Material(Shader.Find("Standard"));
                objectMaterialOne.color = customizedDisplayColor[2];
                uniqueFileName = AssetDatabase.GenerateUniqueAssetPath("Assets/ObjectMaterialOne.mat");
                AssetDatabase.CreateAsset(objectMaterialOne, uniqueFileName);
                break;
            case (1):
                if (colorOneMaterial != null)
                {
                    colorOneMaterial.color = customizedDisplayColor[2];
                }
                else
                {
                    objectMaterialOne = new Material(Shader.Find("Standard"));
                    objectMaterialOne.color = customizedDisplayColor[2];
                    uniqueFileName = AssetDatabase.GenerateUniqueAssetPath("Assets/ObjectMaterialOne.mat");
                    AssetDatabase.CreateAsset(objectMaterialOne, uniqueFileName);
                }
                if (colorTwoMaterial != null)
                {
                    colorTwoMaterial.color = customizedDisplayColor[0];
                }
                else
                {
                    backgroundMaterialOne = new Material(Shader.Find("Standard"));
                    backgroundMaterialOne.color = customizedDisplayColor[0];
                    uniqueFileName = AssetDatabase.GenerateUniqueAssetPath("Assets/BackgroundMaterialOne.mat");
                    AssetDatabase.CreateAsset(backgroundMaterialOne, uniqueFileName);
                }
                backgroundMaterialTwo = new Material(Shader.Find("Standard"));
                backgroundMaterialTwo.color = customizedDisplayColor[1];
                uniqueFileName = AssetDatabase.GenerateUniqueAssetPath("Assets/BackgroundMaterialTwo.mat");
                AssetDatabase.CreateAsset(backgroundMaterialTwo, uniqueFileName);
                break;
            case (2):
                if (colorOneMaterial != null)
                {
                    colorOneMaterial.color = customizedDisplayColor[0];
                }
                else
                {
                    backgroundMaterialOne = new Material(Shader.Find("Standard"));
                    backgroundMaterialOne.color = customizedDisplayColor[0];
                    uniqueFileName = AssetDatabase.GenerateUniqueAssetPath("Assets/BackgroundMaterialOne.mat");
                    AssetDatabase.CreateAsset(backgroundMaterialOne, uniqueFileName);
                }
                if (colorTwoMaterial != null)
                {
                    colorTwoMaterial.color = customizedDisplayColor[2];
                }
                else
                {
                    objectMaterialOne = new Material(Shader.Find("Standard"));
                    objectMaterialOne.color = customizedDisplayColor[2];
                    uniqueFileName = AssetDatabase.GenerateUniqueAssetPath("Assets/ObjectMaterialOne.mat");
                    AssetDatabase.CreateAsset(objectMaterialOne, uniqueFileName);
                }
                backgroundMaterialTwo = new Material(Shader.Find("Standard"));
                backgroundMaterialTwo.color = customizedDisplayColor[1];
                uniqueFileName = AssetDatabase.GenerateUniqueAssetPath("Assets/BackgroundMaterialTwo.mat");
                AssetDatabase.CreateAsset(backgroundMaterialTwo, uniqueFileName);
                break;
        }
    }
}
