//This is a list of commonly used namespaces for a window.
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media3D;

namespace Basic3D_SDK
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : Window
    {
         private void WindowLoaded(object sender, EventArgs e) {
		 
		 //Create a ViewPort for the scene.
			 ViewPort3D myViewport = new ViewPort3D();

		 //Add a camera and set viewing properties.
			 PerspectiveCamera myCamera = new PerspectiveCamera();

			 myCamera.LookAtPoint = new Point3D(0, 0, 0);
			 myCamera.Up = new Vector3D(0, 1, 0);
			 myCamera.NearPlaneDistance = 1;
			 myCamera.FarPlaneDistance = 20;
			 myCamera.Position = new Point3D(0, 0, 5);
			 myCamera.FieldOfView = 45;

		 //Add light sources to the scene.
			 DirectionalLight myDirLight = new DirectionalLight();
			 myDirLight.Color = Colors.White;
			 myDirLight.Direction = new Vector3D(-3, -4, -5);
			 
		//Define cube vertices and faces
			 Mesh3D cubemesh = new Mesh3D();

			 // vertices added in z-order
			 // vertex 0
			 cubemesh.Positions.Add(new Point3D(1.0, 1.0, 0));
			 cubemesh.Normals.Add(new Vector3D(0, 0, 1));
			 cubemesh.TextureCoordinates.Add(new Point(1, 0));

			 // vertex 1
			 cubemesh.Positions.Add(new Point3D(0, 1.0, 0));
			 cubemesh.Normals.Add(new Vector3D(0, 0, 1));
			 cubemesh.TextureCoordinates.Add(new Point(0, 0));

			 // vertex 2
			 cubemesh.Positions.Add(new Point3D(0, 0, 0));
			 cubemesh.Normals.Add(new Vector3D(0, 0, 1));
			 cubemesh.TextureCoordinates.Add(new Point(0, 1));

			 // vertex 3
			 cubemesh.Positions.Add(new Point3D(1.0, 0, 0));
			 cubemesh.Normals.Add(new Vector3D(0, 0, 1));
			 cubemesh.TextureCoordinates.Add(new Point(1, 1));

			 // vertex 4
			 cubemesh.Positions.Add(new Point3D(0, 1.0, -1.0));
			 cubemesh.Normals.Add(new Vector3D(0, 0, 1));
			 cubemesh.TextureCoordinates.Add(new Point(0, 0));

			 // vertex 5
			 cubemesh.Positions.Add(new Point3D(0, 0, -1.0));
			 cubemesh.Normals.Add(new Vector3D(0, 0, 1));
			 cubemesh.TextureCoordinates.Add(new Point(0, 1));

			 // vertex 6
			 cubemesh.Positions.Add(new Point3D(1.0, 1.0, -1.0));
			 cubemesh.Normals.Add(new Vector3D(0, 0, 1));
			 cubemesh.TextureCoordinates.Add(new Point(1, 0));

			 // vertex 7
			 cubemesh.Positions.Add(new Point3D(1.0, 0, -1.0));
			 cubemesh.Normals.Add(new Vector3D(0, 0, 1));
			 cubemesh.TextureCoordinates.Add(new Point(1, 1));

			 // Front Face
			 cubemesh.TriangleIndices.Add((ushort)0); cubemesh.TriangleIndices.Add((ushort)2); cubemesh.TriangleIndices.Add((ushort)1);
			 cubemesh.TriangleIndices.Add((ushort)0); cubemesh.TriangleIndices.Add((ushort)3); cubemesh.TriangleIndices.Add((ushort)2);

			 // Left Side
			 cubemesh.TriangleIndices.Add((ushort)1); cubemesh.TriangleIndices.Add((ushort)5); cubemesh.TriangleIndices.Add((ushort)4);
			 cubemesh.TriangleIndices.Add((ushort)1); cubemesh.TriangleIndices.Add((ushort)2); cubemesh.TriangleIndices.Add((ushort)5);

			 // Right Side
			 cubemesh.TriangleIndices.Add((ushort)0); cubemesh.TriangleIndices.Add((ushort)6); cubemesh.TriangleIndices.Add((ushort)7);
			 cubemesh.TriangleIndices.Add((ushort)0); cubemesh.TriangleIndices.Add((ushort)7); cubemesh.TriangleIndices.Add((ushort)3);

			 // Back Side
			 cubemesh.TriangleIndices.Add((ushort)6); cubemesh.TriangleIndices.Add((ushort)4); cubemesh.TriangleIndices.Add((ushort)5);
			 cubemesh.TriangleIndices.Add((ushort)6); cubemesh.TriangleIndices.Add((ushort)7); cubemesh.TriangleIndices.Add((ushort)5);

			 // Bottom Side
			 cubemesh.TriangleIndices.Add((ushort)3); cubemesh.TriangleIndices.Add((ushort)5); cubemesh.TriangleIndices.Add((ushort)2);
			 cubemesh.TriangleIndices.Add((ushort)3); cubemesh.TriangleIndices.Add((ushort)7); cubemesh.TriangleIndices.Add((ushort)5);

			 // Top Side
			 cubemesh.TriangleIndices.Add((ushort)0); cubemesh.TriangleIndices.Add((ushort)1); cubemesh.TriangleIndices.Add((ushort)4);
			 cubemesh.TriangleIndices.Add((ushort)0); cubemesh.TriangleIndices.Add((ushort)4); cubemesh.TriangleIndices.Add((ushort)6);

			 //Define meshes for backdrop planes
			 Model3DCollection newcollection = new Model3DCollection();
			 Mesh3D firstMesh = new Mesh3D();

			 firstMesh.TriangleIndices.Add(0);
			 firstMesh.TriangleIndices.Add(1);
			 firstMesh.TriangleIndices.Add(2);
			 firstMesh.TriangleIndices.Add(2);
			 firstMesh.TriangleIndices.Add(1);
			 firstMesh.TriangleIndices.Add(3);
			 firstMesh.TextureCoordinates.Add(new Point(0, 0));
			 firstMesh.TextureCoordinates.Add(new Point(0, 0));
			 firstMesh.TextureCoordinates.Add(new Point(0, 0));
			 firstMesh.TextureCoordinates.Add(new Point(0, 0));
			 firstMesh.Normals.Add(new Vector3D(0, 1, 0));
			 firstMesh.Normals.Add(new Vector3D(0, 1, 0));
			 firstMesh.Normals.Add(new Vector3D(0, 1, 0));
			 firstMesh.Normals.Add(new Vector3D(0, 1, 0));
			 firstMesh.Positions.Add(new Point3D(-2, -1, 2));
			 firstMesh.Positions.Add(new Point3D(-2, -1, -2));
			 firstMesh.Positions.Add(new Point3D(2, -1, 2));
			 firstMesh.Positions.Add(new Point3D(2, -1, -2));

			 Mesh3D secondMesh = new Mesh3D();

			 secondMesh.TriangleIndices.Add(0);
			 secondMesh.TriangleIndices.Add(1);
			 secondMesh.TriangleIndices.Add(2);
			 secondMesh.TriangleIndices.Add(2);
			 secondMesh.TriangleIndices.Add(1);
			 secondMesh.TriangleIndices.Add(3);
			 secondMesh.TextureCoordinates.Add(new Point(0, 0));
			 secondMesh.TextureCoordinates.Add(new Point(0, 0));
			 secondMesh.TextureCoordinates.Add(new Point(0, 0));
			 secondMesh.TextureCoordinates.Add(new Point(0, 0));
			 secondMesh.Normals.Add(new Vector3D(1, 0, 0));
			 secondMesh.Normals.Add(new Vector3D(1, 0, 0));
			 secondMesh.Normals.Add(new Vector3D(1, 0, 0));
			 secondMesh.Normals.Add(new Vector3D(1, 0, 0));
			 secondMesh.Positions.Add(new Point3D(-2, -1, 2));
			 secondMesh.Positions.Add(new Point3D(-2, 1, 2));
			 secondMesh.Positions.Add(new Point3D(-2, -1, -2));
			 secondMesh.Positions.Add(new Point3D(-2, 1, -2));

			 Mesh3D thirdMesh = new Mesh3D();

			 thirdMesh.TriangleIndices.Add(0);
			 thirdMesh.TriangleIndices.Add(1);
			 thirdMesh.TriangleIndices.Add(2);
			 thirdMesh.TriangleIndices.Add(2);
			 thirdMesh.TriangleIndices.Add(1);
			 thirdMesh.TriangleIndices.Add(3);
			 thirdMesh.TextureCoordinates.Add(new Point(0, 0));
			 thirdMesh.TextureCoordinates.Add(new Point(0, 0));
			 thirdMesh.TextureCoordinates.Add(new Point(0, 0));
			 thirdMesh.TextureCoordinates.Add(new Point(0, 0));
			 thirdMesh.Normals.Add(new Vector3D(0, 0, 1));
			 thirdMesh.Normals.Add(new Vector3D(0, 0, 1));
			 thirdMesh.Normals.Add(new Vector3D(0, 0, 1));
			 thirdMesh.Normals.Add(new Vector3D(0, 0, 1));
			 thirdMesh.Positions.Add(new Point3D(-2, -1, -2));
			 thirdMesh.Positions.Add(new Point3D(-2, 1, -2));
			 thirdMesh.Positions.Add(new Point3D(2, -1, -2));
			 thirdMesh.Positions.Add(new Point3D(2, 1, -2));

			 //Define some primitives
			 MeshPrimitive3D firstPrimitive = new MeshPrimitive3D();
			 MeshPrimitive3D secondPrimitive = new MeshPrimitive3D();
			 MeshPrimitive3D thirdPrimitive = new MeshPrimitive3D();
			 MeshPrimitive3D cubePrimitive = new MeshPrimitive3D();

			 //Assign meshes to primitives
			 firstPrimitive.Mesh = firstMesh;
			 secondPrimitive.Mesh = secondMesh;
			 thirdPrimitive.Mesh = thirdMesh;
			 cubePrimitive.Mesh = cubemesh;

			 //Define BrushMaterial and apply to primitives
			 BrushMaterial material = new BrushMaterial(new SolidColorBrush(Colors.LightYellow));
			 BrushMaterial spherematerial = new BrushMaterial(new SolidColorBrush(Colors.Cyan));
			 BrushMaterial cubematerial = new BrushMaterial(new SolidColorBrush(Colors.Gold));

			 firstPrimitive.Material = material;
			 secondPrimitive.Material = material;
			 thirdPrimitive.Material = material;
			 cubePrimitive.Material = cubematerial;
//			 cubePrimitive.Material = imagematerial;

		//Create a transformation and apply to the cube. 
		//Note that this transformation is defined by an axis and angle of rotation.
			 RotateTransform3D rotation = new RotateTransform3D(new Quaternion(new Vector3D(1,2,1),65));
			 cubePrimitive.Transform = rotation;

		//Add 3D objects to the collection and add the collection to the ViewPort3D.

			 newcollection.Children.Add(firstPrimitive);
			 newcollection.Children.Add(secondPrimitive);
			 newcollection.Children.Add(thirdPrimitive); 
			 newcollection.Children.Add(cubePrimitive);
			 newcollection.Children.Add(myDirLight);
			 myViewport.Models.Children.Add(newcollection);
			 myViewport.Camera = myCamera;

		//Render by adding the ViewPort3D to the window.

			 mainWindow = new System.Windows.Window();
			 mainWindow.Content = myViewport;
			 mainWindow.Show();
		 }

    }
}