uniform sampler2DRect tex0;
uniform sampler2DRect tex1;
uniform vec2 resolution;
uniform vec2 fftres;
uniform float time;

uniform float centerBuffer; // = 0.01;
uniform float vignetteStrength;// = 0.6;
uniform float aberrationStrength;// = 5.0;

void main(void){
	// float 	centerBuffer 		= 0.01;
	// float	vignetteStrength 	= 0.6;
	// float	aberrationStrength 	= 5.0;
		
	float glitchSize = 0.05;
	// vec2 fftres = vec2(90.0,50.0);

	float 	chrDist,
			vigDist;
	
	vec2 uv = gl_FragCoord.xy / resolution.xy;
	float wave = texture2DRect( tex1, vec2(0.1,0.1)*fftres.xy ).x / 2.0 ;
	float wave2 = texture2DRect( tex1, vec2(0.5,1.0)*fftres.xy ).x * 2.0 - 1.0;
	uv.x += sin( uv.y / 5.0 * wave2 )/5.0;
	
	vec2 uvG = vec2( 0.5 , sin( wave + wave2 * 2.0 ) );
	glitchSize *= wave + wave2 * 10.0;
	
	if( uv.y > uvG.y && uv.y < uvG.y + glitchSize ){
		uv.x = 0.8-uv.x;
		uv.y = sin(time*10.0) - uv.y;
	}

	if( 1.0-uv.y > 1.0-uvG.y && 1.0-uv.y < 1.0-uvG.y + glitchSize ){
		uv.x = uv.x;
		uv.y = uv.y;
	}
	
	uvG = vec2( 0.5 , sin( wave + wave2 * 2.5 ) );
	glitchSize *= 1.3;
	
	if( uv.y > uvG.y && uv.y < uvG.y + glitchSize ){
		uv.x = 0.3-uv.x;
		uv.y = sin(time*10.0) - uv.y;
	}

	if( 1.0-uv.y > 1.0-uvG.y && 1.0-uv.y < 1.0-uvG.y + glitchSize ){
		uv.x = uv.x;
		uv.y = uv.y;
	}

	vec2 vecDist = uv;// - ( 0.5 , 0.5 );
	chrDist = vigDist = length( vecDist );
	
	chrDist	-= centerBuffer;
	if( chrDist < 0.0 ) chrDist = 0.0;

	vec2 uvR = uv * ( 1.0 + chrDist * 0.02 * aberrationStrength * wave ),
		 uvB = uv * ( 1.0 - chrDist * 0.02 * aberrationStrength * wave );
	
	vec4 c = texture2DRect( tex0, gl_FragCoord.xy );
	c.x = texture2DRect( tex0 , uvR * resolution ).x; 
	c.y = texture2DRect( tex0 , uv * resolution).y; 
	c.z = texture2DRect( tex0 , uvB * resolution).z;
	
	c *= 1.0 - vigDist* vignetteStrength * (wave*2.0);
	
	float scanline = sin( uv.y * 800.0 * wave2 )/30.0; 
	c *= 1.0 + scanline; 
	
	gl_FragColor = c;
}