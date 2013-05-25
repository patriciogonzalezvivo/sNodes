uniform sampler2DRect tex0;	// Input
uniform sampler2DRect lut;	// Lookup table	
uniform sampler2DRect oldFilm;

uniform vec2 tex0Res;
uniform vec2 filmRes;

uniform float contrast;

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

    vec3 modiff = texture2DRect(oldFilm, st*1.0 ).rgb;
    float dust = 0.3 *modiff.g + 0.59*modiff.r + 0.11*modiff.b;
    
    color = mix( color, vec3(1.0) ,dust);
    
    color = mix( vec3(1.0),color,contrast);


    gl_FragColor = vec4( color , 1.0);
}