#include "testApp.h"

//--------------------------------------------------------------
void testApp::setup(){
    ofSetVerticalSync(true);
    
    sServer.setName(ofGetTimestampString("%H-%M-%S"));
    ofSetWindowTitle(sServer.getName() + "@sNode-Tex" );
    
    ofSetWindowShape(600, 400);
}

//--------------------------------------------------------------
void testApp::update(){
    if (video != NULL){
        video->update();
    }
}

//--------------------------------------------------------------
void testApp::draw(){
    ofBackground(0);
    
    ofSetColor(255);
    ofDrawBitmapString("Drag something with texture here", ofGetWidth()*0.5-130, ofGetHeight()*0.5-4);
    
    if (video != NULL) {
        video->draw(0, 0, ofGetWidth(), ofGetHeight());
    }
    
    if (tex != NULL){
        tex->draw(0,0, ofGetWidth(), ofGetHeight());
    }
    
    sServer.publishScreen();
}

//--------------------------------------------------------------
void testApp::keyPressed(int key){
    if (video != NULL){
        if (key == ' '){
            if (video->isPlaying()){
                video->stop();
            } else {
                video->play();
            }
        }
    }
}

//--------------------------------------------------------------
void testApp::keyReleased(int key){

}

//--------------------------------------------------------------
void testApp::mouseMoved(int x, int y ){

}

//--------------------------------------------------------------
void testApp::mouseDragged(int x, int y, int button){

}

//--------------------------------------------------------------
void testApp::mousePressed(int x, int y, int button){

}

//--------------------------------------------------------------
void testApp::mouseReleased(int x, int y, int button){

}

//--------------------------------------------------------------
void testApp::windowResized(int w, int h){

}

//--------------------------------------------------------------
void testApp::gotMessage(ofMessage msg){

}

//--------------------------------------------------------------
void testApp::dragEvent(ofDragInfo dragInfo){
    vector<string> file = ofSplitString(dragInfo.files[0], ".");
    
    if ( tex != NULL )
        delete tex;
    
    if ( video != NULL )
        delete video;
    
    if (file[1] == "mov" || file[1] == "MOV" ||
        file[1] == "m4v" || file[1] == "M4V") {
        
        video = new ofVideoPlayer();
        video->loadMovie(dragInfo.files[0]);
        video->play();
        ofSetWindowShape(video->getWidth(), video->getHeight());
        
    } else if (file[1] == "jpg"  || file[1] == "JPG" ||
               file[1] == "jpeg" || file[1] == "JPEG" ||
               file[1] == "png"  || file[1] == "PNG" ) {
        
        ofImage img;
        img.loadImage(dragInfo.files[0]);
        img.update();
        
        tex = new ofTexture;
        tex->allocate(img.getWidth(), img.getHeight(),GL_RGB);
        tex->loadData(img.getPixelsRef());
        ofSetWindowShape(img.getWidth(), img.getHeight());
        
    } else if (file[1] == "lut" || file[1] == "LUT" ||
               file[1] == "cube" || file[1] == "CUBE"){
        
        ofBuffer buffer = ofBufferFromFile( dragInfo.files[0] );
        
        int mapSize = 32;
        int width = mapSize * mapSize;
        int height = mapSize;
        
        float * pixels = new float[mapSize * mapSize * mapSize * 3];
        
        tex = new ofTexture();
        tex->allocate( width, height, GL_RGB32F);
        for(int z=0; z<mapSize ; z++){
            for(int y=0; y<mapSize; y++){
                for(int x=0; x<mapSize; x++){
                    string content = buffer.getNextLine();
                    int pos = x + y*height + z*width;
                    
                    vector <string> splitString = ofSplitString(content, " ", true, true);
                    
                    if (splitString.size() >=3) {
                        pixels[pos*3 + 0] = ofToFloat(splitString[0]);
                        pixels[pos*3 + 1] = ofToFloat(splitString[1]);
                        pixels[pos*3 + 2] = ofToFloat(splitString[2]);
                    }
                }
            }
        }
        tex->loadData( pixels, width, height, GL_RGB);
        
        ofSetWindowShape(width, height);
    }
}