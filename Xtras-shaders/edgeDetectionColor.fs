uniform sampler2DRect tex0;

void main(void)
{
	vec3 c[9];
	for (int i=0; i < 3; ++i)
	{
		for (int j=0; j < 3; ++j)
		{
			c[3*i+j] = texture2DRect(tex0, (gl_FragCoord.xy+vec2(i-1,j-1))).rgb;
		}
	}
	
	vec3 Lx = 2.0*(c[7]-c[1]) + c[6] + c[8] - c[2] - c[0];
	vec3 Ly = 2.0*(c[3]-c[5]) + c[6] + c[0] - c[2] - c[8];
	vec3 G = sqrt(Lx*Lx+Ly*Ly);
	
	gl_FragColor = vec4(G, 1.0);
}