uniform sampler2DRect tex0;
uniform float threshold;

void main(){
    vec4 color = texture2DRect(tex0,gl_TexCoord[0].st);
    float c = color.g;
    c = c - threshold;
    c = max(c,0.0);
    c = c/c;
    gl_FragColor = vec4(c,c,c,1.0);
}
