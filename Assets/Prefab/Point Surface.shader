Shader "Graph/Point Surface"//定义着色器菜单项
{
    //子着色器
    SubShader
    {
        CGPROGRAM
        ENDCG
        #pragma surface ConfigureSurface Standard fullforwardshadows //pragma一词来自希腊语，指的是一项行动或需要完成的事情
    }
    //通过编写FallBack“ Diffuse”向标准的漫反射着色器添加一个后备
    FallBack "Diffuse"
}
