uniform sampler2DRect tex0;
uniform vec2 mouse;
uniform vec2 resolution;

vec3 highlights(vec3 pixel, float thres){
    float val = (pixel.x + pixel.y + pixel.z) / 3.0;
    return pixel * smoothstep(thres - 0.1, thres + 0.1, val);
}

vec3 sample(vec2 tc){
    return pow( texture2DRect(tex0, tc).xyz, vec3(2.2, 2.2, 2.2));
}

vec3 hsample(vec2 tc){
    return highlights(sample(tc), 0.6);
}

vec3 blur(vec2 tc, float offs){
    vec4 xoffs = offs * vec4(-2.0, -1.0, 1.0, 2.0);// / iResolution.x;
    vec4 yoffs = offs * vec4(-2.0, -1.0, 1.0, 2.0);// / iResolution.y;
    
    vec3 color = vec3(0.0, 0.0, 0.0);
    color += hsample(tc + vec2(xoffs.x, yoffs.x)) * 0.00366;
    color += hsample(tc + vec2(xoffs.y, yoffs.x)) * 0.01465;
    color += hsample(tc + vec2(    0.0, yoffs.x)) * 0.02564;
    color += hsample(tc + vec2(xoffs.z, yoffs.x)) * 0.01465;
    color += hsample(tc + vec2(xoffs.w, yoffs.x)) * 0.00366;
    
    color += hsample(tc + vec2(xoffs.x, yoffs.y)) * 0.01465;
    color += hsample(tc + vec2(xoffs.y, yoffs.y)) * 0.05861;
    color += hsample(tc + vec2(    0.0, yoffs.y)) * 0.09524;
    color += hsample(tc + vec2(xoffs.z, yoffs.y)) * 0.05861;
    color += hsample(tc + vec2(xoffs.w, yoffs.y)) * 0.01465;
    
    color += hsample(tc + vec2(xoffs.x, 0.0)) * 0.02564;
    color += hsample(tc + vec2(xoffs.y, 0.0)) * 0.09524;
    color += hsample(tc + vec2(    0.0, 0.0)) * 0.15018;
    color += hsample(tc + vec2(xoffs.z, 0.0)) * 0.09524;
    color += hsample(tc + vec2(xoffs.w, 0.0)) * 0.02564;
    
    color += hsample(tc + vec2(xoffs.x, yoffs.z)) * 0.01465;
    color += hsample(tc + vec2(xoffs.y, yoffs.z)) * 0.05861;
    color += hsample(tc + vec2(    0.0, yoffs.z)) * 0.09524;
    color += hsample(tc + vec2(xoffs.z, yoffs.z)) * 0.05861;
    color += hsample(tc + vec2(xoffs.w, yoffs.z)) * 0.01465;
    
    color += hsample(tc + vec2(xoffs.x, yoffs.w)) * 0.00366;
    color += hsample(tc + vec2(xoffs.y, yoffs.w)) * 0.01465;
    color += hsample(tc + vec2(    0.0, yoffs.w)) * 0.02564;
    color += hsample(tc + vec2(xoffs.z, yoffs.w)) * 0.01465;
    color += hsample(tc + vec2(xoffs.w, yoffs.w)) * 0.00366;
    
    return color;
}

void main(void){
    vec2 tc = gl_FragCoord.xy;
    vec3 color = blur(tc, 2.0);
    color += blur(tc, 3.0);
    color += blur(tc, 5.0);
    color += blur(tc, 7.0);
    color /= 4.0;
    
    color += sample(tc);
    
    float divider = smoothstep(-0.01, 0.01, tc.x);
    gl_FragColor.xyz = mix(sample(tc), color, divider) * (divider * divider + (1.0 - divider) * (1.0 - divider));
    gl_FragColor.w = 1.0;
}