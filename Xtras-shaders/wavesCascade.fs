uniform float time;
uniform vec2 mouse;
uniform vec2 resolution;

float rand(vec2 n) { 
    return fract(sin(dot(n, vec2(12.9898, 4.1414))) * 43758.5453);
}

float noise(vec2 n) {
    const vec2 d = vec2(0.0, 1.0);
    vec2 b = floor(n), f = smoothstep(vec2(0.0), vec2(1.0), fract(n));
    return mix(mix(rand(b), rand(b + d.yx), f.x), mix(rand(b + d.xy), rand(b + d.yy), f.x), f.y);
}

void main( void ) {

    vec2 p = ( gl_FragCoord.xy / resolution.xy-0.5)  ;

    float color =0.;
    float Mx = 22.;
    for ( float i   =22. ; i >8.; i-- ){
        color   *=  0.8;
        color   =   pow(color,1.1);
        float a = p.y + sin(p.x*(1.+i*0.5)+time*2.+sin(time*(0.2 + i*0.0001))*5.0+noise(vec2(time*(0.5+i*0.1),p.x*(0.5+i*10.0))))*0.005*i-0.35*sin(time+i*0.2);
        color   +=  max(0.0,pow(1.0-abs(a),500.)+pow(1.0-abs(a),20.)*0.4);
    }
    
    gl_FragColor = vec4( vec3( color ), 1.0 );

}