uniform float time;
uniform vec2 mouse;
uniform vec2 resolution;

vec3 sim(vec3 p,float s);
vec2 rot(vec2 p,float r);
vec2 rotsim(vec2 p,float s);

vec2 makeSymmetry(vec2 p){
  vec2 ret=p;
  ret.y=abs(ret.y);
  ret=rotsim(ret,sin(time*0.3)*2.0+3.0);
  ret.x=abs(ret.x);
  return ret;
}

float makePoint(float x,float y,float fx,float fy,float sx,float sy,float t){
  float xx=x+tan(t*fx)*sx;
  float yy=y-tan(t*fy)*sy;
  return 0.5/sqrt(abs(x*xx+yy*yy));
}

const float PI=3.14159265;

vec3 sim(vec3 p,float s){
  vec3 ret=p;
  ret=p+s/2.0;
  ret=fract(ret/s)*s-s/2.0;
  return ret;
}

vec2 rot(vec2 p,float r){
  vec2 ret;
  ret.x=p.x*cos(r)-p.y*sin(r);
  ret.y=p.x*sin(r)+p.y*cos(r);
  return ret;
}

vec2 rotsim(vec2 p,float s){
  vec2 ret=p;
  ret=rot(p,-PI/(s*2.0));
  ret=rot(p,floor(atan(ret.x,ret.y)/PI*s)*(PI/s));
  return ret;
}

void main( void ) {
  vec2 p=(gl_FragCoord.xy/resolution.x)*2.0-vec2(1.0,resolution.y/resolution.x);
  p=p*2.0;
  p=makeSymmetry(p);
  
  float x=p.x;
  float y=p.y;
  
  float t=time*0.5;
  
  float a=
  makePoint(x,y,3.3,2.9,0.3,0.3,t);
  a=a+makePoint(x,y,1.9,2.0,0.4,0.4,t);
  a=a+makePoint(x,y,0.8,0.7,0.4,0.5,t);
  a=a+makePoint(x,y,2.3,0.1,0.6,0.3,t);
  a=a+makePoint(x,y,0.8,1.7,0.5,0.4,t);
  a=a+makePoint(x,y,0.3,1.0,0.4,0.4,t);
  a=a+makePoint(x,y,1.4,1.7,0.4,0.5,t);
  a=a+makePoint(x,y,1.3,2.1,0.6,0.3,t);
  a=a+makePoint(x,y,1.8,1.7,0.5,0.4,t);
  
  float b=
  makePoint(x,y,1.2,1.9,0.3,0.3,t);
  b=b+makePoint(x,y,0.7,2.7,0.4,0.4,t);
  b=b+makePoint(x,y,1.4,0.6,0.4,0.5,t);
  b=b+makePoint(x,y,2.6,0.4,0.6,0.3,t);
  b=b+makePoint(x,y,0.7,1.4,0.5,0.4,t);
  b=b+makePoint(x,y,0.7,1.7,0.4,0.4,t);
  b=b+makePoint(x,y,0.8,0.5,0.4,0.5,t);
  b=b+makePoint(x,y,1.4,0.9,0.6,0.3,t);
  b=b+makePoint(x,y,0.7,1.3,0.5,0.4,t);
  
  float c=
  makePoint(x,y,3.7,0.3,0.3,0.3,t);
  c=c+makePoint(x,y,1.9,1.3,0.4,0.4,t);
  c=c+makePoint(x,y,0.8,0.9,0.4,0.5,t);
  c=c+makePoint(x,y,1.2,1.7,0.6,0.3,t);
  c=c+makePoint(x,y,0.3,0.6,0.5,0.4,t);
  c=c+makePoint(x,y,0.3,0.3,0.4,0.4,t);
  c=c+makePoint(x,y,1.4,0.8,0.4,0.5,t);
  c=c+makePoint(x,y,0.2,0.6,0.6,0.3,t);
  c=c+makePoint(x,y,1.3,0.5,0.5,0.4,t);
  
  vec3 d=vec3(a,b,c)/32.0;
  
  gl_FragColor = vec4(d.x,d.y,d.z,1.0);
}