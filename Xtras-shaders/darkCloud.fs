uniform float time;
uniform vec2 resolution;

vec4 textureRND2D(vec2 uv){
	uv = floor(fract(uv)*1e3);
	float v = uv.x+uv.y*1e3;
	return fract(1e5*sin(vec4(v*1e-2, (v+1.)*1e-2, (v+1e3)*1e-2, (v+1e3+1.)*1e-2)));
}

float noise(vec2 p) {
	vec2 f = fract(p*1e3);
	vec4 r = textureRND2D(p);
	f = f*f*(3.0-2.0*f);
	return (mix(mix(r.x, r.y, f.x), mix(r.z, r.w, f.x), f.y));	
}

float cloud(vec2 p) {
	float v = 0.0;
	v += noise(p*1.)*.50000;
	v += noise(p*2.)*.25000;
	v += noise(p*4.)*.12500;
	v += noise(p*8.)*.06250;
	v += noise(p*16.)*.03125;
	return v*v*v;
}

void main( void ) {
	vec2 p = (gl_FragCoord.xy/resolution)*.05+.5;
	vec3 c = vec3(.1, .1, .3);
	float c1 = cloud(p*.3+time*.0002)*.5;
	c1 = pow(c1,1.5);
	float c2 = cloud(p*.2+time*.0002)*.75;
	c2 = pow(c2, 1.5);
	c.rgb += vec3(.6, .6, .8) * c1*.5;
	p += c1*0.006125;
	p -= c2*0.006125;
	c.gbr += vec3(.8, .8, 1.) * c2*.5;
	//p.y *= c.g;
	c.grb += vec3(1., 1., 1.) * cloud(p*.1+time*.0002)*1.;
	//c += .125;
	gl_FragColor = vec4(c, 1.);
}