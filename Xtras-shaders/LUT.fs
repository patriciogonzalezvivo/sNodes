uniform sampler2DRect tex0;	// Input
uniform sampler2DRect lut;	// Lookup table	

void main(void){
    vec2 st = gl_FragCoord.st;
    float size = 32.0;

    vec4 srcColor = texture2DRect(tex0, st);
    
    float x = srcColor.r * (size-1.0);
    float y = srcColor.g * (size-1.0);
    float z = srcColor.b * (size-1.0);
    
    vec2 lutPos;
    lutPos.x = floor(x)+(floor(y)+1.0)*size;
    lutPos.y = floor(z)+1.0;
    lutPos *= vec2(1.0,-1.0);

    vec3 color = texture2DRect(lut, lutPos).rgb;
    gl_FragColor = vec4( color , 1.0);
}