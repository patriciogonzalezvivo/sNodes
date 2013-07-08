uniform float time;
uniform vec2 mouse;
uniform vec2 resolution;

void main( void ){
   vec2 position = (gl_FragCoord.xy - resolution * 0.5) / resolution.yy;
   float longest = sqrt(float(resolution.x*resolution.x) + float(resolution.y*resolution.y))*0.5;
   float dx = gl_FragCoord.x-resolution.x/2.0;
   float dy = 0.2+gl_FragCoord.y-resolution.y/2.0;
   float len = sqrt(dx*dx+dy*dy);
   float ds = len/longest;
   float md = time*2.0;
   
   float ang = 2.0*atan(dy,(len+dx));
   ang += pow(len, 0.5)*5.0;
   
   float red = (128.0 - sin(ang + md*3.141592*2.0) * 127.0)*(1.0-ds);
   float green = (128.0 - cos(ang + md*3.141592*2.0) * 127.0)*(1.0-ds);
   float blue = (128.0 + sin(ang  + md*3.141592*2.0) * 127.0)*(1.0-ds);
   
   gl_FragColor = vec4( vec3( red/255.0, green/255.0, blue/255.0), 1.0 );
}