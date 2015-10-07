using System;
using UnityEngine;
namespace UnityStandardAssets.ImageEffects{
	[ExecuteInEditMode]
	public class VignetteScript : PostEffectsBase{
		public float amount;
		public Shader VignetteShader;
		public Material m_Vignette;

		public override bool CheckResources (){
            CheckSupport(false);
            m_Vignette = CheckShaderAndCreateMaterial(VignetteShader, m_Vignette);

            if (!isSupported)
                ReportAutoDisable ();
            return isSupported;
        }

		private void OnRenderImage(RenderTexture source, RenderTexture destination){
			m_Vignette.SetFloat("_Amount", amount);
			Graphics.Blit(source, destination, m_Vignette);
		}
	}
}
