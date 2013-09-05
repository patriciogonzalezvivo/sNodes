#include "ofApp.h"
		
//--------------------------------------------------------------
void testApp::setup(){	 
	
	
	
	//left = new float[BUFFER_SIZE];
	//right = new float[BUFFER_SIZE];

	ofSetHexColor(0x666666);
	FFTanalyzer.setup(44100, BUFFER_SIZE/2, 2);
	FFTanalyzer.peakHoldTime = 15; // hold longer
	FFTanalyzer.peakDecayRate = 0.95f; // decay slower
	FFTanalyzer.linearEQIntercept = 0.9f; // reduced gain at lowest frequency
	FFTanalyzer.linearEQSlope = 0.01f; // increasing gain at higher frequencies
	
	ofSetVerticalSync(true);
    ofSetWindowShape(90,50);
    sServer.setName("");
    
    // 0 output channels,
	// 2 input channels
	// 44100 samples per second
	// BUFFER_SIZE samples per buffer
	// 4 num buffers (latency)
	
	ofSoundStreamSetup(0,2,this, 44100, BUFFER_SIZE, 4);
}


//--------------------------------------------------------------
void testApp::update(){
		ofBackground(0);
}

//--------------------------------------------------------------
void testApp::draw(){
    
    
	float avg_power = 0.0f;	
		
	/* do the FFT	*/
	myfft.powerSpectrum(0,(int)BUFFER_SIZE/2, left,BUFFER_SIZE,&magnitude[0],&phase[0],&power[0],&avg_power);
	 
	for (int i = 0; i < (int)(BUFFER_SIZE/2); i++){
		freq[i] = magnitude[i];
	}
	
	FFTanalyzer.calculate(freq);
//	
//	ofSetHexColor(0xffffff);
//	for (int i = 0; i < (int)(BUFFER_SIZE/2 - 1); i++){
//		//ofRect(200+(i*4),600,4,-freq[i]*10.0f);
//	}
	
    int nBars = FFTanalyzer.nAverages;
    float barWidth = (float)ofGetWidth()/(float)nBars;
	for (int i = 0; i < nBars; i++){
        
        float valuePeak = ofMap(FFTanalyzer.peaks[nBars-i-1], 0, 40, 0, 1);
        float valueAver = ofMap(FFTanalyzer.averages[nBars-i-1], 0, 40, 0, 1);
        ofFloatColor color = ofFloatColor(1.0,0.4,0.0);
        color.setHue(0.5-valueAver*0.5);
        ofSetColor(color);
		ofRect((i*barWidth),ofGetHeight(),barWidth,-valuePeak*ofGetHeight());
	}
    
    sServer.publishScreen();
}


//--------------------------------------------------------------
void testApp::keyPressed  (int key){ 
	
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
void testApp::mouseReleased(){

}

//--------------------------------------------------------------
void testApp::audioReceived 	(float * input, int bufferSize, int nChannels){	
	// samples are "interleaved"
	for (int i = 0; i < bufferSize; i++){
		left[i] = input[i*2];
		right[i] = input[i*2+1];
	}
}

