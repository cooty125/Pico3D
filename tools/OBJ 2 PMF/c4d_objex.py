# ============================================================
# * File Name: objexport.py
# * Location: C:\Users\DevStar\AppData\Roaming\MAXON\Cinema 4D R13_05DFD2A0\library\scripts\
# * Project: objexport
# ------------------------------------------------------------
# * Copyright: © David Kutnar 2016
# * Last Update: 11.02 2016
# ============================================================

import c4d
import os
from c4d import gui, documents, storage, Vector

def main():   
    doc = documents.GetActiveDocument( );
    aobj = doc.GetActiveObject( );
    axis = aobj.GetAbsPos( );
    center = aobj.GetMp( );    

    c4d.CallCommand( 12112 );
    c4d.CallCommand( 14048 );
    c4d.CallCommand( 12479 );

    print ( "OBJECT: " + aobj.GetName( ) );
    print ( "CENTER: " + str( center ) );
    print ( "AXIS: " + str( axis ) );

    fn = storage.SaveDialog( c4d.FILESELECTTYPE_ANYTHING, "OBJ Exporter", "", "./"  );
    fn = ( fn + ".obj" );
    
    documents.SaveDocument( doc, fn, c4d.SAVEDOCUMENTFLAGS_0, c4d.FORMAT_OBJEXPORT );
    
    c4d.EventAdd( );
    c4d.CallCommand( 12147 );
    
    return True
        
if __name__=='__main__' :
    main( );