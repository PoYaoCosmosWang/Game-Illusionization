using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StripeCreator))]
public class StripeCreatorEditor : Editor
{
    SerializedProperty stripeElementsProperty;
    SerializedProperty AreaHeightPropertyProperty;
    SerializedProperty AreaWidthProperty;
    SerializedProperty AdvanceSettingProperty;
    SerializedProperty IntensityProperty;
    SerializedProperty StripeIntervalProperty;
    SerializedProperty StripeWidthProperty;
    SerializedProperty RatioProperty;
    private void OnEnable()
    {
        stripeElementsProperty = serializedObject.FindProperty("StripeElements");
        AreaHeightPropertyProperty = serializedObject.FindProperty("AreaHeight");
        AreaWidthProperty = serializedObject.FindProperty("AreaWidth");
        IntensityProperty = serializedObject.FindProperty("Intensity");
        AdvanceSettingProperty = serializedObject.FindProperty("AdvanceSetting");
        StripeIntervalProperty = serializedObject.FindProperty("StripeInterval");
        StripeWidthProperty = serializedObject.FindProperty("StripeWidth");
        RatioProperty = serializedObject.FindProperty("Ratio");


    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        StripeCreator stripeCreator = (StripeCreator)target;
        EditorGUILayout.PropertyField(AreaHeightPropertyProperty, new GUIContent("Stripe Area Height"));
        EditorGUILayout.PropertyField(AreaWidthProperty, new GUIContent("Stripe Area Width"));
        EditorGUILayout.PropertyField(AdvanceSettingProperty, new GUIContent("Advanced Setting"));
        if (AdvanceSettingProperty.boolValue)
        {
            EditorGUILayout.PropertyField(StripeIntervalProperty, new GUIContent("Stripe Interval"));
            EditorGUILayout.PropertyField(StripeWidthProperty, new GUIContent("Stripe Width"));
            EditorGUILayout.PropertyField(RatioProperty, new GUIContent("Stripe Area Ratio"));
        }
        else
        {
            EditorGUILayout.PropertyField(IntensityProperty, new GUIContent("Effect Intensity"));
        }


        EditorGUILayout.PropertyField(stripeElementsProperty, new GUIContent("Stripe Elements"), true);
        serializedObject.ApplyModifiedProperties();
    }
}
