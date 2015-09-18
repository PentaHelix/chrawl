using System;
using UnityEngine;
namespace UnityStandardAssets.ImageEffects
{
	[ExecuteInEditMode]
	public class GridScript : PostEffectsBase{
		public Shader GridShader;
		private Material m_Grid;

		public override bool CheckResources (){
            CheckSupport (false);
            m_Grid = CheckShaderAndCreateMaterial(GridShader, m_Grid);

            if (!isSupported)
                ReportAutoDisable ();
            return isSupported;
        }

		private void OnRenderImage(RenderTexture source, RenderTexture destination){
			Graphics.Blit(source, destination, m_Grid);
		}
	}
}
