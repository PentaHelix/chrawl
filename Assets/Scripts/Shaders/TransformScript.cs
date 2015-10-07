using System;
using UnityEngine;
namespace UnityStandardAssets.ImageEffects{
	[ExecuteInEditMode]
	public class TransformScript : PostEffectsBase{
		public Shader TransformShader;
		public Material m_Transform;

		private float rippleTimer = 0;
		private float tintTimer = 0;

		void Update(){
			if(rippleTimer > 0){
				rippleTimer -= Time.deltaTime;
				if(rippleTimer < 0){
					m_Transform.SetFloat("_Ripple", 0);
				}else{
					m_Transform.SetFloat("_Ripple", (rippleTimer % 0.2f - 0.1f) * 20f);
				}
			}

			if(tintTimer > 0){
				tintTimer -= Time.deltaTime;
				if(tintTimer < 0)m_Transform.SetColor("_Tint", Color.white);
			}
		}

		public override bool CheckResources (){
            m_Transform = CheckShaderAndCreateMaterial(TransformShader, m_Transform);
            CheckSupport(false);

            if (!isSupported)
                ReportAutoDisable ();
            return isSupported;
        }

		private void OnRenderImage(RenderTexture source, RenderTexture destination){
			Graphics.Blit(source, destination, m_Transform);
		}

		public void Ripple(float t){
			rippleTimer = t;
		}

		public void Tint(float t, Color c){
			tintTimer = t;
			m_Transform.SetColor("_Tint", c);
		}
	}
}
