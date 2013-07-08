uniform float time;
uniform vec2 mouse;
uniform vec2 resolution;

uniform sampler2D backbuffer;

float orbitDistance = 0.000025;
float waveLength = 300.00;

vec3 sim(vec3 p,float s);
vec2 rot(vec2 p,float r);
vec2 rotsim(vec2 p,float s);

vec2 makeSymmetry(vec2 p){
	vec2 ret=p;
	ret.y=abs(ret.y);
	ret=rotsim(ret,sin(time*0.1)*2.0+9.0);
	ret.x=abs(ret.x);
	return ret;
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
	vec2 uv = (gl_FragCoord.xy/resolution.x)*2.0-vec2(1.0,resolution.y/resolution.x);
	uv=uv*.2;
	
	uv=makeSymmetry(uv);
	
	vec2 p1 = (vec2(sin(time), cos(time))*orbitDistance)+.1;
	vec2 p2 = (vec2(sin(time+3.142), cos(time+3.142))*orbitDistance)+.125;
	
	float d1 = .1-length(uv +p1);
	float d2 = .1-length(uv -p2);
	
	float wave1 = sin(d1*waveLength+(time*5.))*0.5 + 0.5 * (((d1 - 0.5) * .5) - .5);
	float wave2 = sin(d2*waveLength+(time*5.))*0.5 + 0.5 * (((d1 - 0.5) * 1.5) + 1.5);
	float c = d1 > 0.99 || d2 > 0.995 ? 1. : 0.;
	c + wave1*wave2;
	gl_FragColor = vec4(c + wave1*wave2,c,c,1.);
	
	vec2 d = 1./resolution;
	float dx = texture2D(backbuffer, uv + vec2(-1.,0.)*d).x - texture2D(backbuffer, uv + vec2(1.,0.)*d).x ;
	float dy = texture2D(backbuffer, uv + vec2(0.,-1.)*d).x - texture2D(backbuffer, uv + vec2(0.,1.)*d).x ;
	d = vec2(dx,dy)*resolution/resolution.x*2.;
	gl_FragColor.z = pow(clamp(1.-1.5*length(uv  - vec2(1.15*cos(time*.2)+.5,1.*sin(time*.1)+.5) + d),0.,1.),4.0);
	gl_FragColor.y = gl_FragColor.z*1.5 + gl_FragColor.x*0.3;
	
	gl_FragColor *=10.25;
}