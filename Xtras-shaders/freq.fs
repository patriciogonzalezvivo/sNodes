uniform float time;
uniform vec2 resolution;
    
float viewAngle = 1.0;
float speed = 9.0;
float rate = 15.0;
float baseamp = 0.10;
     
    void main(void){
        vec2 p = -1.0 + 3.0 * ( gl_FragCoord.xy / resolution.xy );
        p.y = 1.0 - p.y;
        float x = speed * viewAngle * time + rate * p.x;
        float base = (1.0 + cos(x * 2.5 + time)) * (1.0 + sin(x * 3.5 + time));
        float z = fract(0.05 * x);
        z = max(z, 1.0 - z);
        z = pow(z, 2.0);
        float pulse = exp(-10.0 * z);
        vec4 ecg_color = vec4(0.3, 1.0, 0.4, 1.0);
        vec4 c = pow(clamp(1.0 - abs(p.y - (baseamp * base + pulse - 0.5)), 0.0, 1.0), 16.0) * ecg_color;
        gl_FragColor = c;
    } 