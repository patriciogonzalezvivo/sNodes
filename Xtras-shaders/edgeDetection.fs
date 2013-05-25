uniform sampler2DRect tex0;

float getIntensity(vec2 u){
	vec3 a = texture2DRect(tex0,u).xyz;
	return (a.x+a.y+a.z)/3.0;
}

void main(void){
	vec2 uv = gl_FragCoord.xy;
	vec2 p = vec2(1.0);
	
	float avg = 0.0;
	avg += getIntensity(uv+vec2(p.x,0.0));
	avg += getIntensity(uv+vec2(-p.x,0.0));
	avg += getIntensity(uv+vec2(0.0,p.y));
	avg += getIntensity(uv+vec2(0.0,-p.y));
	avg += getIntensity(uv+vec2(p.x,p.y));
	avg += getIntensity(uv+vec2(-p.x,-p.y));
	avg += getIntensity(uv+vec2(p.x,-p.y));
	avg += getIntensity(uv+vec2(-p.x,p.y));
	avg /= 8.0;
	
	float result = (1.0-getIntensity(uv)) + avg;
	result = (1.0 - result) * 10.0;
	
	gl_FragColor = vec4(vec3(result),1.0);
}