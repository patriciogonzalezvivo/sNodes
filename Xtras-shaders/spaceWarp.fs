uniform sampler2DRect tex0;
uniform vec2 resolution;
uniform float size;
uniform float time;

// 'Warp Speed' by David Hoskins 2013.
// Adapted it from here:-   https://www.shadertoy.com/view/MssGD8
// I tried to find gaps and variation in the star cloud for a feeling of structure.
float ttime = (time+2.4) * 60.0;

void main(void)
{
	float s = 0.0, v = 0.0;
	vec2 uv = (gl_FragCoord.xy / resolution.xy) * 2.0 - 1.0;
	float t = ttime*0.005;
	uv.x = (uv.x * resolution.x / resolution.y) + sin(t)*.5;
	float si = sin(t+2.17); // ...Squiffy rotation matrix!
	float co = cos(t);
	uv *= mat2(co, si, -si, co);
	vec3 col = vec3(0.0);
	for (int r = 0; r < 100; r++) 
	{
		vec3 p= vec3(0.3, 0.2, floor(ttime) * 0.0008) + s * vec3(uv, 0.143);
		p.z = mod(p.z,2.0);
		for (int i=0; i < 10; i++) p = abs(p*2.04) / dot(p, p) - 0.75;
		v += length(p*p)*smoothstep(0.0, 0.5, 0.9 - s) * .002;
		col +=  vec3(v * 0.8, 1.1 - s * 0.5, .7 + v * 0.5) * v * 0.013;
		s += .01;
	}
	gl_FragColor = vec4(col, 1.0);
}