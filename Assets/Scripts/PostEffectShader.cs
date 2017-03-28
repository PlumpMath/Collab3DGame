using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Run Shader in edit
[ExecuteInEditMode]
public class PostEffectShader : MonoBehaviour {

    public Material mat;
    // Run Shader through script that way we can run in edit mode
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, mat);
    }
}
