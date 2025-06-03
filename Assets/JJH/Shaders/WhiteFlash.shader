Shader "Custom/SimpleWhiteFlash"
{
    Properties
    {
        _MainTex     ("Sprite Texture", 2D) = "white" {}
        _FlashAmount ("Flash Amount", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float    _FlashAmount;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv  : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv  = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // 원본 스프라이트 색
                fixed4 baseCol = tex2D(_MainTex, i.uv);
                // _FlashAmount만큼 흰색을 섞어서 빛나게 함
                baseCol.rgb = lerp(baseCol.rgb, fixed3(1,1,1), _FlashAmount);
                return baseCol;
            }
            ENDCG
        }
    }
}