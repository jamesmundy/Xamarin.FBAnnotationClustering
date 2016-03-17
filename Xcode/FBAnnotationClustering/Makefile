XBUILD=/Applications/Xcode.app/Contents/Developer/usr/bin/xcodebuild
PROJECT_ROOT=.
PROJECT=$(PROJECT_ROOT)/FBAnnotationClustering.xcodeproj
TARGET=FBAnnotationClustering

all: libFBAnnotationClustering.a

libFBAnnotationClustering-i386.a:
	$(XBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphonesimulator -configuration Release clean build 
	-mv $(PROJECT_ROOT)/build/Release-iphonesimulator/lib$(TARGET).a $@

libFBAnnotationClustering-armv7.a:
	$(XBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphoneos -arch armv7 -configuration Release clean build 
	-mv $(PROJECT_ROOT)/build/Release-iphoneos/lib$(TARGET).a $@

libFBAnnotationClustering-arm64.a:
	$(XBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphoneos -arch arm64 -configuration Release clean build 
	-mv $(PROJECT_ROOT)/build/Release-iphoneos/lib$(TARGET).a $@

libFBAnnotationClustering.a: libFBAnnotationClustering-i386.a libFBAnnotationClustering-armv7.a libFBAnnotationClustering-arm64.a
	lipo -create -output $@ $^

clean:
	-rm -f *.a *.dll