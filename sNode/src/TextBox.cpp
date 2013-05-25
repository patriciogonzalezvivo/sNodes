//
//  TextBox.cpp
//
//  Created by Elliot Woods on 09/12/2011.
//  Copyright 2011 Kimchi and Chips.
//
//  modified by James George 12/2/2011
//  modified by Momo the Monster 7/10/2012
//  swappable fonts added by James George 9/11/2012
//
//	MIT license
//	http://www.opensource.org/licenses/mit-license.php
//

#include "TextBox.h"

TextBox::TextBox(UniformType _type, string _name) {
    type = _type;
    name = _name;
	text = "";
    
	cursorPosition = 0;
	cursorx = 0;
	cursory = 0;
    isEnabled = false;
	isEditing = false;
    drawCursor = false;
	autoClear = false;
	mouseDownInRect = false;
    
    ofAddListener(ofEvents().mousePressed, this, &TextBox::mousePressed);
    ofAddListener(ofEvents().mouseDragged, this, &TextBox::mouseDragged);
    ofAddListener(ofEvents().mouseReleased, this, &TextBox::mouseReleased);
    isEnabled = true;
    
    sValue = "";
    fValue = 1.0;
    
}

TextBox::~TextBox(){
	if(isEditing){
		endEditing();
	}
    
	if(isEnabled){
        ofRemoveListener(ofEvents().mousePressed, this, &TextBox::mousePressed);
        ofRemoveListener(ofEvents().mouseDragged, this, &TextBox::mouseDragged);
		ofRemoveListener(ofEvents().mouseReleased, this, &TextBox::mouseReleased);
		isEnabled = false;
    }
}

void TextBox::beginEditing() {
    if(!isEditing){
        ofAddListener(ofEvents().keyPressed, this, &TextBox::keyPressed);
        ofSendMessage(TEXTFIELD_IS_ACTIVE);
        isEditing = true;
        drawCursor = true;
		if(autoClear){
			clear();
		} else {
			cursory = 0;
			cursorPosition = cursorx = text.size();
		}
    }
}

void TextBox::endEditing() {
    if(isEditing){
        ofRemoveListener(ofEvents().keyPressed, this, &TextBox::keyPressed);
        ofSendMessage(TEXTFIELD_IS_INACTIVE);
        ofNotifyEvent(textChanged, text, this);
        
        isEditing = false;
        drawCursor = false;
    }
}

void TextBox::draw() {
    
    ofPushStyle();
    ofNoFill();
    ofSetColor(255,100);
    ofRectRounded(*this, 3);
    ofFill();
    ofSetColor(100,100);
    ofRectRounded(*this, 3);
    ofPopStyle();
    
	ofPushMatrix();
	ofTranslate(x, y);
    
    int margin = 10;
    
    string sType = "";
    if (type == UNIFORM_SAMPLE2DRECT) {
        sType = "sample2DRect";
    } else if (type == UNIFORM_VEC2) {
        sType = "vec2";
    } else if (type == UNIFORM_FLOAT) {
        sType = "float";
    }
    
    ofDrawBitmapString(sType+" "+name+": ", margin,12);
    int nameHeight = (sType.size()+1)*8+margin+(name.size()+2)*8;
    ofDrawBitmapString(text, nameHeight,12);
    width = nameHeight+(text.size()+2)*8;
	
    int cursorPos;
	//draw cursor line
    if(drawCursor) {
        ofPushStyle();
        float timeFrac = 0.5 * sin(3.0f * ofGetElapsedTimef()) + 0.5;
        ofColor col = ofGetStyle().color;
        cursorPos = nameHeight + 8*cursorx ;
        ofSetColor(col.r * timeFrac, col.g * timeFrac, col.b * timeFrac);
        ofSetLineWidth(3.0f);
		//TODO: multiline with fontRef
        ofLine(cursorPos, 13.7*cursory+2,cursorPos, 13.7*cursory+12);
        ofPopStyle();
    }
	
	ofPopMatrix();
    
    if (type == UNIFORM_FLOAT && mouseDownInRect && ofGetMousePressed()) {
        ofLine(x+nameHeight+16,y+height*0.5,lastMousePos.x,lastMousePos.y);
    }
}

void TextBox::mousePressed(ofMouseEventArgs& args){
    ofPoint mouse = ofPoint(args.x,args.y);
	mouseDownInRect = inside(mouse);
    lastMousePos = mouse;
}

void TextBox::mouseDragged(ofMouseEventArgs& args){
    ofPoint mouse = ofPoint(args.x,args.y);
    
    if (mouseDownInRect && type == UNIFORM_FLOAT)
        if (text != "time" &&
            text != "mouse.x" &&
            text != "mouse.y" &&
            text.find('/') != 0){
            
        ofPoint diff = mouse - lastMousePos;
        float angle = atan2(diff.y,diff.x);
        float diffAngle = (angle - lastAngle);
        if (diffAngle < -PI) diffAngle += TWO_PI;
        if (diffAngle > PI) diffAngle -= TWO_PI;
            
        float value = ofToFloat(text);
        text = ofToString(value+diffAngle*0.01*diff.length());
        
        lastMousePos = mouse;
        lastAngle = angle;
    }
}

void TextBox::mouseReleased(ofMouseEventArgs& args){
    ofPoint mouse = ofPoint(args.x,args.y);
    if(inside(mouse)) {
        if(!isEditing && mouseDownInRect){
	        beginEditing();
        }
    } else if(isEditing){
		endEditing();
	}
}

void TextBox::keyPressed(ofKeyEventArgs& args) {
	//ew: add charachter (non unicode sorry!)
	//jg: made a step closer to this with swappable renderers and ofxFTGL -- but need unicode text input...
	
	int key = args.key;
	if (key == OF_KEY_RETURN) {
        endEditing();
        return;
	}
	
	if (key >=32 && key <=126) {
		text.insert(text.begin()+cursorPosition, key);
		cursorPosition++;
	}
	
	
	if (key==OF_KEY_BACKSPACE) {
		if (cursorPosition>0) {
			text.erase(text.begin()+cursorPosition-1);
			--cursorPosition;
		}
	}
	
	if (key==OF_KEY_DEL) {
		if (text.size() > cursorPosition) {
			text.erase(text.begin()+cursorPosition);
		}
	}
	
	if (key==OF_KEY_LEFT){
		if (cursorPosition>0){
			--cursorPosition;
		}
	}
	
	if (key==OF_KEY_RIGHT){
		if (cursorPosition<text.size()){
			++cursorPosition;
		}
	}
	
	//for multiline:
	cursorx = cursory = 0;
	if(text.size() > 0){
		for (int i=0; i<cursorPosition; ++i) {
			if (*(text.begin()+i) == '\n') {
				++cursory;
				cursorx = 0;
			} else {
				cursorx++;
			}
		}
	}
}

void TextBox::clear() {
	text.clear();
	cursorx = cursory = 0;
	cursorPosition = 0;
}