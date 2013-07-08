uniform float time;

void main()
{
	float v=.0,t=.001;
	for (float s=.0; s<1.5; s+=.01) {
		vec3 p=s*gl_FragCoord.xyz*.001+vec3(.1,.2,fract(s+floor(time*25.)*.01));
		
		for (int i=0; i<10; i++) 
			p=abs(p)/dot(p,p)-.5;
		
		v+=dot(p,p)*(2.-s)*t;
	}
	gl_FragColor = vec4(v);
}