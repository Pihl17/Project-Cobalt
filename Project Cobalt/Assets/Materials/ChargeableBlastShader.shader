Shader "Unlit/ChargeableBlastShader"
{
    Properties
    {
        //_MainTex ("Texture", 2D) = "white" {}
		_ChargeProcent ("Charge Procent", Range(0,1)) = 0
		_FireTime ("Fire time", float) = 0
		_ChargeProcentWhenFired ("Charge procent when fired", Range(0,1)) = 0
		_MinRange ("Minimun range", Range(0,1)) = 0
		//_BlastRange ("Blast range", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

		ZWrite Off
		Blend srcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                //float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                //float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
				float4 localPos : TEXCOORD0;
            };

            //sampler2D _MainTex;
            //float4 _MainTex_ST;
			float _ChargeProcent;
			float _FireTime;
			float _ChargeProcentWhenFired;
			float _MinRange;
			//float _BlastRange;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.localPos = v.vertex;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = float4(0.0, 0.0, 1.0, 0.0);
				if (i.localPos.y <= -0.4)
					col.a = _ChargeProcent;
				if (_ChargeProcent >= 1)
					col.g = 1.0;
				
				float timeSincefire = _Time.y - _FireTime;
				if (timeSincefire < 1.0) {
					float blastRange = (_MinRange + (1.0 - _MinRange) * _ChargeProcentWhenFired) / 2;
					if (i.localPos.y+0.5 <= blastRange*2 && i.localPos.x <= blastRange && i.localPos.x >= -blastRange)
						col.rba = 1.0 - timeSincefire;
					}

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
