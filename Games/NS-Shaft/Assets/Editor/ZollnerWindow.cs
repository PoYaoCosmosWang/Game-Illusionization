using UnityEngine;
using UnityEditor;
using System.Collections;

public class ZollnerWindow : EditorWindow
{

	GameObject target;
	float myStrength;
   	float density;
   	float length;
   	float width;
   	int angle; 
   	float alpha; 
   	bool isfold=true;

/*	[MenuItem ("Window/myWindow")]
    public static void  ShowWindow () 
    {
        EditorWindow.GetWindow(typeof(ZollnerWindow));
    }

    void OnGUI () 
    {
        // 純文字顯示
		GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
		//target=EditorGUILayout.ObjectField("target",target);
		myStrength = EditorGUILayout.Slider ("Strength", myStrength, 0, 10);

		// 切換區塊是否開啟或關閉功能。EditorGUIlayout 提供的功能。
		// 若使用者將欄位勾選，便會使 BeginToggleGroup 到 EndToggleGroup 之間的元件開啟編輯。反之，則關閉不給予修改
		//isfold=EditorGUILayout.BeginFoldoutHeaderGroup(isfold,"Advanced Settings",EditorStyles.boldLabel);

		//myBool = EditorGUILayout.Toggle ("Toggle", myBool);

		density = EditorGUILayout.Slider ("density", density, 0, 1);
		length = EditorGUILayout.Slider ("length", length, 0, 1);
		width = EditorGUILayout.Slider ("density", width, 0, 1);
		angle = EditorGUILayout.IntSlider ("angle", angle, -3, 3);
		alpha = EditorGUILayout.Slider ("alpha", alpha, 0, 10);
		//EditorGUILayout.EndFoldoutHeaderGroup ();
    }
    */
}