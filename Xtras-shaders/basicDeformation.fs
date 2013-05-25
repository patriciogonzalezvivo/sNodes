uniform sampler2DRect tex0;
uniform vec2 resolution;
uniform float time;	

void main(void){	
	float stongth = sin(time) * 0.5 + 0.5;
	vec2 uv = gl_FragCoord.xy;// / resolution.xy;
	float waveu = sin((uv.y + time) * 20.0) * 0.5 * 0.05 * stongth;
	gl_FragColor = texture2DRect(tex0, uv + vec2(waveu, 0)*resolution*0.25);
}