shader_type sky;

uniform sampler2D skyTexture;

void sky() {
	COLOR = texture(skyTexture,SKY_COORDS).rgb;
}
