uniform sampler2DRect tex0;
uniform sampler2DRect tex1;

uniform vec2 mouse;
uniform vec2 resolution;

uniform float time;

void main( void ){
    vec2 st = gl_FragCoord.xy;

    vec4 color1 = texture2DRect(tex0,st);
    vec4 color2 = texture2DRect(tex1,st);
    gl_FragColor = mix(color1,color2, abs(sin(time)) );
}