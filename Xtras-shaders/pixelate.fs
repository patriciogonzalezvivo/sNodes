uniform sampler2DRect tex0;
uniform float size;	

void main(void){
	vec2 uv = gl_FragCoord.xy;// / windows.xy;
	vec2 divs = vec2(size);//vec2(resolution.x * size / resolution.y, resolution.x * size/ resolution.y);

	uv = floor(uv * divs)/ divs;
	gl_FragColor = texture2DRect(tex0, uv);
}