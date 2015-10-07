using System;
using UnityEngine;
namespace UnityStandardAssets.ImageEffects{
	[ExecuteInEditMode]
	public class ASCIIScript:PostEffectsBase{
		
		//Variables required for the ImageEffect
		public Shader ASCIIShader;
		private Material m_ASCII;

		//Values for the Shader
		public Texture2D CharTex;
		public float tilesX = 160;
		public float tilesY = 50;
		public float darkness = .8f;

		public override bool CheckResources (){
			// Necessary shader stuff
            CheckSupport(false);
            m_ASCII = CheckShaderAndCreateMaterial(ASCIIShader, m_ASCII);

            // Setting shader properties
            if (isSupported){
				m_ASCII.SetTexture("_CharTex", CharTex);
				
				m_ASCII.SetFloat("_tilesX", tilesX);
				m_ASCII.SetFloat("_tilesY", tilesY);

				m_ASCII.SetFloat("_tileW", 1/tilesX);
				m_ASCII.SetFloat("_tileH", 1/tilesY);

				m_ASCII.SetFloat("_darkness", darkness);
            }
            return isSupported;
        }

		private void OnRenderImage(RenderTexture source, RenderTexture destination){
			Graphics.Blit(source, destination, m_ASCII);
		}
	}
}
