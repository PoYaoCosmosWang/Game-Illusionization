using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
[ExecuteInEditMode]
public class HurtEffect : MonoBehaviour
{

	#region Variables
	public Shader curShader;
	public float grayScaleAmount = 1.0f;
	[SerializeField]
	private Material curMaterial;
	#endregion

	#region Properties
	public Material material
	{
		get
		{
			if (curMaterial == null)
			{
				curMaterial = new Material(curShader);
				curMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return curMaterial;
		}
	}
	#endregion

	// Use this for initialization
	void Start()
	{
		/*if (SystemInfo.supportsImageEffects == false)
		{
			enabled = false;
			return;
		}

		if (curShader != null && curShader.isSupported == false)
		{
			enabled = false;
		}*/
	}

	void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (curShader != null)
		{
			//material.SetFloat("_LuminosityAmount", grayScaleAmount);

			Graphics.Blit(sourceTexture, destTexture, material);
		}
		else
		{
			Graphics.Blit(sourceTexture, destTexture);
		}
	}

	// Update is called once per frame
	void Update()
	{
		//grayScaleAmount = Mathf.Clamp(grayScaleAmount, 0.0f, 1.0f);
	}
	/*
	void OnDisable()
	{
		if (curMaterial != null)
		{
			DestroyImmediate(curMaterial);
		}
	}*/
	
}
/*
————————————————
版权声明：本文为CSDN博主「妈妈说女孩子要自立自强」的原创文章，遵循 CC 4.0 BY-SA 版权协议，转载请附上原文出处链接及本声明。
原文链接：https://blog.csdn.net/candycat1992/article/details/39255575
*/