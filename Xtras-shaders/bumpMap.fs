uniform sampler2DRect tex0;
uniform sampler2DRect tex1;
uniform vec2 resolution;
uniform vec2 mouse;

float SPOT_SIZE = 300.0;
float BUMP_STRENGTH = 2.0;
float SPOT_BRIGHTNESS = 1.3;

vec4 HEIGHT_VECTOR = vec4(1.0/3.0, 1.0/3.0, 1.0/3.0, 0.0);

float getHeight(sampler2DRect channel, vec2 offset) {
	return dot(texture2DRect(channel, offset / resolution.xy), HEIGHT_VECTOR);
}

void main(void)
{
	// get "height" of adjucent pixels based on their brightness
	float upHeight = getHeight(tex1, gl_FragCoord.xy + vec2(0,-1));
	float bottomHeight = getHeight(tex1, gl_FragCoord.xy + vec2(0,+1));
	float leftHeight = getHeight(tex1, gl_FragCoord.xy + vec2(-1,0));
	float rightHeight = getHeight(tex1,gl_FragCoord.xy + vec2(1,0));
	
	// calculate normal vector
	float dx = (rightHeight - leftHeight) * BUMP_STRENGTH;
	float dy = (bottomHeight - upHeight) * BUMP_STRENGTH;
	float dz = sqrt (1.0 - dx*dx - dy*dy);	
	vec3 normal = vec3(dx,dy,dz);
	
	// get "light" vector based on mouse position
	vec2 mouse = mouse.xy - gl_FragCoord.xy;
	
	// dot product of normal and light
	float bumpMultiplier = dot(normal, vec3(mouse/resolution.y,1.0));
	
	// spot light multiplier
	float spotMultiplier = min(exp(1.0-length(mouse)/SPOT_SIZE), SPOT_BRIGHTNESS);
	
	// output
	vec2 uv = gl_FragCoord.xy / resolution.xy;
	vec4 color = texture2DRect(tex1, uv);
	gl_FragColor = color * bumpMultiplier * spotMultiplier;

}