uniform sampler2DRect tex0;
uniform vec2 resolution;
uniform float cv;

#define PI 3.141592
#define PI2 6.283184

#define CV 0.0
#define ST 0.05

vec4 colorat(vec2 uv) {
	return texture2DRect(tex0, vec2(uv.x, uv.y)*resolution);
}
vec4 convolve(vec2 uv) {
	vec4 col = vec4(0.0);
	for(float r0 = 0.0; r0 < 1.0; r0 += ST) {
		float r = r0 * cv*0.01;
		for(float a0 = 0.0; a0 < 1.0; a0 += ST) {
			float a = a0 * PI2;
			col += colorat(uv + vec2(cos(a), sin(a)) * r);
		}
	}
	col *= ST * ST;
	return col;
}

void main(void)
{
	vec2 uv = gl_FragCoord.xy / resolution.xy;
	gl_FragColor = convolve(uv);
}