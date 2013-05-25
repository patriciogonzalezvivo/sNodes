uniform vec2 resolution;
uniform vec2 mouse;
uniform float time;

void main( void ) {

	float t = time*1.0;
	vec2 position = ( gl_FragCoord.xy / resolution.xy ) + mouse / resolution.xy;

	float color = 0.0;
	color += sin( position.x * cos( t / 10.0 ) * 60.0 ) + cos( position.y * cos( t / 5.0 ) * 10.0 );
	color += sin( position.y * sin( t / 5.0 ) * 20.0 ) + cos( position.x * sin( t / 5.0 ) * 40.0 );
	color += sin( position.x * sin( t / 5.0 ) * 30.0 ) + sin( position.y * sin( t / 35.0 ) * 80.0 );
	color *= sin( t / 10.0 ) * 0.5;

	gl_FragColor = vec4( vec3( color, color * 0.5, sin( color + t / 3.0 ) * 0.75 ), 1.0 );

}