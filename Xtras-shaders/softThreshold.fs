uniform sampler2DRect tex0;
uniform float Soft;// 0.001;
uniform float Threshold;// 0.3;

void main(void)
{
	float f = Soft/2.0;
	float a = Threshold - f;
	float b = Threshold + f;
	
	vec4 tx = texture2DRest(tex0, gl_FragCoord.xy);
	float l = (tx.x + tx.y + tx.z) / 3.0;
	
	float v = smoothstep(a, b, l);
	
	gl_FragColor= vec4(v);

}