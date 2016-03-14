using System;
using CoreLocation;
using Foundation;
using MapKit;
using ObjCRuntime;

namespace FBAnnotationClustering
{
	// @interface FBAnnotationCluster : NSObject <MKAnnotation>
	[BaseType (typeof(NSObject))]
	interface FBAnnotationCluster : IMKAnnotation
	{
		// @property (assign, nonatomic) CLLocationCoordinate2D coordinate;
		[Export ("coordinate", ArgumentSemantic.Assign)]
		CLLocationCoordinate2D Coordinate { get; set; }

		// @property (copy, nonatomic) NSString * title;
		[Export ("title")]
		string Title { get; set; }

		// @property (copy, nonatomic) NSString * subtitle;
		[Export ("subtitle")]
		string Subtitle { get; set; }

		// @property (nonatomic, strong) NSArray * annotations;
		[Export ("annotations", ArgumentSemantic.Strong)]
    MKAnnotation[] Annotations { get; set; }
	}

	// @interface FBQuadTreeNode : NSObject
	[BaseType (typeof(NSObject))]
	interface FBQuadTreeNode
	{
		// @property (assign, nonatomic) NSUInteger count;
		[Export ("count")]
		nuint Count { get; set; }

		// @property (assign, nonatomic) FBBoundingBox boundingBox;
		[Export ("boundingBox", ArgumentSemantic.Assign)]
		FBBoundingBox BoundingBox { get; set; }

		// @property (nonatomic, strong) NSMutableArray * annotations;
		[Export ("annotations", ArgumentSemantic.Strong)]
		NSMutableArray Annotations { get; set; }

		// @property (nonatomic, strong) FBQuadTreeNode * northEast;
		[Export ("northEast", ArgumentSemantic.Strong)]
		FBQuadTreeNode NorthEast { get; set; }

		// @property (nonatomic, strong) FBQuadTreeNode * northWest;
		[Export ("northWest", ArgumentSemantic.Strong)]
		FBQuadTreeNode NorthWest { get; set; }

		// @property (nonatomic, strong) FBQuadTreeNode * southEast;
		[Export ("southEast", ArgumentSemantic.Strong)]
		FBQuadTreeNode SouthEast { get; set; }

		// @property (nonatomic, strong) FBQuadTreeNode * southWest;
		[Export ("southWest", ArgumentSemantic.Strong)]
		FBQuadTreeNode SouthWest { get; set; }

		// -(id)initWithBoundingBox:(FBBoundingBox)box;
		[Export ("initWithBoundingBox:")]
		IntPtr Constructor (FBBoundingBox box);

		// -(BOOL)isLeaf;
		[Export ("isLeaf")]
		bool IsLeaf { get; }

		// -(void)subdivide;
		[Export ("subdivide")]
		void Subdivide ();
	}

	// @protocol FBClusteringManagerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface FBClusteringManagerDelegate
	{
		// @optional -(CGFloat)cellSizeFactorForCoordinator:(FBClusteringManager *)coordinator;
		[Export ("cellSizeFactorForCoordinator:")]
		nfloat CellSizeFactorForCoordinator (FBClusteringManager coordinator);
	}

	// @interface FBClusteringManager : NSObject
	[BaseType (typeof(NSObject))]
	interface FBClusteringManager
	{
		[Wrap ("WeakDelegate")]
		FBClusteringManagerDelegate Delegate { get; set; }

		// @property (assign, nonatomic) id<FBClusteringManagerDelegate> delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Assign)]
		NSObject WeakDelegate { get; set; }

		// -(id)initWithAnnotations:(NSArray *)annotations;
		[Export ("initWithAnnotations:")]
    IntPtr Constructor (MKAnnotation[] annotations);

		// -(void)setAnnotations:(NSArray *)annotations;
		[Export ("setAnnotations:")]
    void SetAnnotations (MKAnnotation[] annotations);

		// -(void)addAnnotations:(NSArray *)annotations;
		[Export ("addAnnotations:")]
    void AddAnnotations (MKAnnotation[] annotations);

		// -(void)removeAnnotations:(NSArray *)annotations;
		[Export ("removeAnnotations:")]
    void RemoveAnnotations (MKAnnotation[] annotations);

		// -(NSArray *)clusteredAnnotationsWithinMapRect:(MKMapRect)rect withZoomScale:(double)zoomScale;
		[Export ("clusteredAnnotationsWithinMapRect:withZoomScale:")]
    MKAnnotation[] ClusteredAnnotationsWithinMapRect (MKMapRect rect, double zoomScale);

		// -(NSArray *)clusteredAnnotationsWithinMapRect:(MKMapRect)rect withZoomScale:(double)zoomScale withFilter:(BOOL (^)(id<MKAnnotation>))filter;
		[Export ("clusteredAnnotationsWithinMapRect:withZoomScale:withFilter:")]
    MKAnnotation[] ClusteredAnnotationsWithinMapRect (MKMapRect rect, double zoomScale, Func<MKAnnotation, bool> filter);

		// -(NSArray *)allAnnotations;
		[Export ("allAnnotations")]
    MKAnnotation[] AllAnnotations { get; }

		// -(void)displayAnnotations:(NSArray *)annotations onMapView:(MKMapView *)mapView;
		[Export ("displayAnnotations:onMapView:")]
    void DisplayAnnotations (MKAnnotation[] annotations, MKMapView mapView);
	}

	// @interface FBQuadTree : NSObject
	[BaseType (typeof(NSObject))]
	interface FBQuadTree
	{
		// @property (nonatomic, strong) FBQuadTreeNode * rootNode;
		[Export ("rootNode", ArgumentSemantic.Strong)]
		FBQuadTreeNode RootNode { get; set; }

		// -(BOOL)insertAnnotation:(id<MKAnnotation>)annotation;
		[Export ("insertAnnotation:")]
		bool InsertAnnotation (MKAnnotation annotation);

		// -(BOOL)removeAnnotation:(id<MKAnnotation>)annotation;
		[Export ("removeAnnotation:")]
		bool RemoveAnnotation (MKAnnotation annotation);

		// -(void)enumerateAnnotationsInBox:(FBBoundingBox)box usingBlock:(void (^)(id<MKAnnotation>))block;
		[Export ("enumerateAnnotationsInBox:usingBlock:")]
		void EnumerateAnnotationsInBox (FBBoundingBox box, Action<MKAnnotation> block);

		// -(void)enumerateAnnotationsUsingBlock:(void (^)(id<MKAnnotation>))block;
		[Export ("enumerateAnnotationsUsingBlock:")]
		void EnumerateAnnotationsUsingBlock (Action<MKAnnotation> block);
	}
}
