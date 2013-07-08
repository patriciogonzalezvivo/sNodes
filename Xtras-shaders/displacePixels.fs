uniform sampler2DRect backbuffer;
uniform sampler2DRect tex0;

void main(){
    vec2 st = gl_TexCoord[0].st;
    
    vec4 newFrame = texture2DRect(backbuffer, st);
    vec4 color   = vec4(0,0,0,0);
    vec2 norm	= ( texture2DRect(tex0, st).rg - 0.5 ) * 2.0;
    float inc	= ( abs(norm.x) + abs(norm.y) ) * 0.5;
        
    vec2 offset[36];
    int iTotal = 36;
    float fTotal = 36.0;
    
    float pi = 3.14159265358979323846;
    float step = (pi*2.0)/fTotal;
    float angle = 0.0;
    for (int i = 0; i < iTotal; i++) {
        offset[i].x = cos(angle);
        offset[i].y = sin(angle);
        angle += step;
    }
    
    float sources = 0.0;
    for (int i = 0; i < iTotal; i++){
        vec4 goingTo = ( texture2DRect( tex0, st + offset[i] ) - 0.5 ) * 2.0;
        
        if ( dot(goingTo.rg,offset[i]) < -1.0/fTotal ){
            sources += 1.0;
            color += texture2DRect(backbuffer, st + offset[i]);
        }
    }
    
    color = color / sources;
    
    gl_FragColor = color*(1.0-inc) + newFrame*inc ;
}