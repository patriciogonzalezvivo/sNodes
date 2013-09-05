#pragma once

#include "ofMain.h"

#include "TextBox.h"
#include "PingPong.h"
#include "SyphonTextures.h"

#include "ofxOsc.h"

// listen on port 12345
#define PORT 12345
#define BOX_HEIGHT  15

class testApp : public ofBaseApp{
public:
    void setup();
    void update();
    void draw();

    void keyPressed  (int key);
    void keyReleased(int key);
    void mouseMoved(int x, int y );
    void mouseDragged(int x, int y, int button);
    void mousePressed(int x, int y, int button);
    void mouseReleased(int x, int y, int button);
    void windowResized(int w, int h);
    void dragEvent(ofDragInfo dragInfo);
	
    void checkShaderFile();
    bool reloadShader(string _filePath = "none");
    
    void addUniform(UniformType _type, string _name);
    void reloadUniforms(string &_text);
    
    std::time_t getLastModified( ofFile& _file );
    
	ofShader            shader;
    ofFile              shaderFile;
    string              shaderFilename;
    std::time_t         shaderChangedTimes;
    int                 lastTimeCheckMillis;
    int                 millisBetweenFileCheck;
    
    vector<TextBox*>        uniforms;
    vector<SyphonTexture*>  sTextures;
   
    ofVideoGrabber      *video;
    
    ofxOscReceiver      oscReceiver;
    
    PingPong            pingpong;
    ofxSyphonServer     sServer;
    int                 width, height, nTex;
};
