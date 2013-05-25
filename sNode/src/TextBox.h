//
//  TextBox.h
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

#define TEXTFIELD_IS_ACTIVE "textfieldIsActive"
#define TEXTFIELD_IS_INACTIVE "textfieldIsInactive"

#ifndef TEXTBOX
#define TEXTBOX

#include "ofMain.h"

enum UniformType{
	UNIFORM_SAMPLE2DRECT = 0,
    UNIFORM_VEC2,
    UNIFORM_FLOAT
};

class TextBox : public ofRectangle {
public:
    TextBox(UniformType _type, string _name);
    ~TextBox();
    
    bool    isBeingEdited(){return isEditing;};

    void    clear();
    void    draw();
    
	ofEvent<string> textChanged;
    
    UniformType type;
    string      name;
    string      text;
    
    string      sValue;
    ofVec2f     vec2Value;
    float       fValue;
    
    bool    drawCursor;
	bool    autoClear;
    bool    bUpdated;
	
protected:
    void    keyPressed(ofKeyEventArgs &a);
	void    mousePressed(ofMouseEventArgs& args);
    void    mouseDragged(ofMouseEventArgs& args);
    void    mouseReleased(ofMouseEventArgs& args);
    
    void    beginEditing();
	void    endEditing();
    
    ofPoint lastMousePos;
    float   lastAngle;
    
    int     cursorPosition;
    int     cursorx, cursory;

    bool    mouseDownInRect;
    bool    isEditing;
    bool    isEnabled;
};

#endif
