uniform sampler2DRect backbuffer;
uniform sampler2DRect tex0;
uniform vec2 resolution;
uniform vec2 mouse;
uniform float time;

void main(void){
    vec2 uv = gl_FragCoord.xy / resolution.xy;

	float amount = 0.0;
	
	amount = (1.0 + sin(time*6.0)) * 0.5;
	amount *= 1.0 + sin(time*16.0) * 0.5;
	amount *= 1.0 + sin(time*19.0) * 0.5;
	amount *= 1.0 + sin(time*27.0) * 0.5;
	amount = pow(amount, 3.0);

	amount *= 0.05;
	
    vec3 col;
    col.r = texture2DRect( tex0, vec2(uv.x+amount,uv.y) ).r;
    col.g = texture2DRect( tex0, uv ).g;
    col.b = texture2DRect( tex0, vec2(uv.x-amount,uv.y) ).b;

	col *= (1.0 - amount * 0.5);
	
    gl_FragColor = vec4(col,1.0);
}
