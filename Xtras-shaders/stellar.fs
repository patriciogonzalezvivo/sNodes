uniform float time;
uniform vec2 mouse;
uniform vec2 resolution;

float getValue(vec2 p, float x2, float y2, float cmin, float cmax){
   float x=p.x;
   float y=p.y;
   float theta=atan(y-y2, x+x2);
   vec2 d=vec2(cos(theta * x2), sin(theta * cmin));
   d *= abs(sin(time)) * (cmax-cmin) + cmin;
   d += vec2(x2, y2) + length(p) * dot(y,x);
   d *= vec2(y2, x2) * length(p * (time) * 0.1) * distance(y,x);
   return length(d-p);
}

void main( void ) {
   vec2 position = ( gl_FragCoord.xy / resolution.xy ) * 2.0 - vec2(1.0,resolution.y/resolution.x);
   position = position * 2.0 ;
   
   float a = 0.0;
   a += getValue(position, tan(time)*.77, sin(time)*.2, 5.3, 0.8);
   a *= getValue(position, cos(time)*.1, cos(-1.*time)*.1, 8.3, 0.18);
   a += getValue(position, sin(time)*.8, cos(time)*.2, 4.3, 1.18);
   a *= getValue(position, tan(time)*.3, cos(time)*.8, -1.3, 5.18);
   a = 1./a*1.75;
   
   float b = 0.0;
   b += getValue(position, cos(time)*.81, sin(time)*.2, 5.3, 0.8);
   b *= getValue(position, sin(time)*.1, tan(time)*.2, 1.3, 1.18);
   b += getValue(position, tan(time)*.8, cos(time)*.01, 7.3, 1.18);
   b *= getValue(position, cos(time)*.8, cos(time)*.2, -1.3, 5.18);
   b = 1./b*1.75;
   
   float c = 0.0;
   c += getValue(position, cos(time)*.41, cos(time)*.2, 5.3, 10.8);
   c *= getValue(position, sin(time)*.1, sin(time)*.1, 1.3, 1.18);
   c += getValue(position, tan(time)*.8, tan(time)*.2, 4.3, 0.18);
   c *= getValue(position, cos(time)*1.8, sin(time)*.8, -10.3, 2.18);
   c = 1./c*1.75;
   
   gl_FragColor = vec4( a, b, c, 1.0 );
}